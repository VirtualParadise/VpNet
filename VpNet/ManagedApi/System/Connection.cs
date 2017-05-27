using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VpNet.NativeApi;

namespace VpNet
{
    class Connection
    {
        private IntPtr vpConnection;
        private readonly Socket socket;
        private readonly object lockObject;


        private byte[] pendingBuffer;
        private List<byte[]> readyBuffers = new List<byte[]>();
        private Timer timer;

        public Connection(IntPtr vpConnection, object lockObject)
        {
            this.vpConnection = vpConnection;
            this.lockObject = lockObject;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public int Connect(string host, ushort port)
        {
            socket.BeginConnect(host, port, ConnectCallback, this);
            return (int)NetworkReturnCode.Success;
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            var connection = ar.AsyncState as Connection;
            try
            {
                connection.socket.EndConnect(ar);

                connection.pendingBuffer = new byte[1024];
                connection.socket.BeginReceive(connection.pendingBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, connection);

                lock (connection.lockObject)
                {
                    Functions.vp_net_notify(connection.vpConnection,
                        (int)NetworkNotification.Connect,
                        (int)NetworkReturnCode.Success);
                }
            }
            catch (SocketException e)
            {
                lock (connection.lockObject)
                {
                    Functions.vp_net_notify(connection.vpConnection,
                    (int)NetworkNotification.Connect,
                    (int)NetworkReturnCode.ConnectionError);
                }
            }
        }


        public int Send(IntPtr data, uint length)
        {
            var buffer = new byte[length];
            Marshal.Copy(data, buffer, 0, (int)length);
            return socket.Send(buffer);
        }

        public int Receive(IntPtr data, uint length)
        {
            if (readyBuffers.Count == 0)
            {
                return (int)NetworkReturnCode.WouldBlock;
            }

            var spaceLeft = (int)length;
            var destination = data;

            int i;
            for (i=0; i<readyBuffers.Count && spaceLeft > 0; ++i)
            {
                var source = readyBuffers[i];
                if (source.Length > spaceLeft)
                {
                    Marshal.Copy(source, 0, data, spaceLeft);

                    readyBuffers[i] = new byte[source.Length - spaceLeft];
                    Array.Copy(source, spaceLeft, readyBuffers[i], 0, source.Length - spaceLeft);

                    spaceLeft = 0;
                    break;
                } else
                {
                    Marshal.Copy(source, 0, destination, source.Length);
                    destination += source.Length;
                    spaceLeft -= source.Length;
                }
            }

            readyBuffers.RemoveRange(0, i);

            return (int)(length - spaceLeft);
        }


        private static void ReceiveCallback(IAsyncResult ar)
        {
            var connection = ar.AsyncState as Connection;
            var bytesRead = connection.socket.EndReceive(ar);
            if (bytesRead < connection.pendingBuffer.Length)
            {
                var buffer = new byte[bytesRead];
                Array.Copy(connection.pendingBuffer, buffer, bytesRead);
                connection.readyBuffers.Add(buffer);
            }
            else
            {
                connection.readyBuffers.Add(connection.pendingBuffer);
                connection.pendingBuffer = new byte[1024];
            }

            if (connection.vpConnection != IntPtr.Zero)
            {
                if (bytesRead > 0)
                {
                    lock (connection.lockObject)
                    {
                        Functions.vp_net_notify(connection.vpConnection, (int)NetworkNotification.ReadReady, 0);
                    }
                    connection.socket.BeginReceive(connection.pendingBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, connection);
                } else
                {
                    lock (connection.lockObject)
                    {
                        Functions.vp_net_notify(connection.vpConnection, (int)NetworkNotification.Disconnect, 0);
                    }
                }
            }
        }

        private void HandleTimeout()
        {
            if (timer != null)
            {
                lock (lockObject)
                {
                    Functions.vp_net_notify(vpConnection, (int)NetworkNotification.Timeout, 0);
                }
            }
        }

        private static void HandleTimeout(object state)
        {
            (state as Connection).HandleTimeout();
        }

        public int Timeout(int seconds)
        {
            if (seconds < 0)
            {
                timer = null;
            }
            else
            {
                timer = new Timer(HandleTimeout, this, seconds * 1000, System.Threading.Timeout.Infinite);
            }
            return 0;
        }

        public void BeforeDestroy()
        {
            vpConnection = IntPtr.Zero;
        }


        public static IntPtr CreateNative(IntPtr vpConnection, IntPtr context)
        {
            var contextHandle = GCHandle.FromIntPtr(context);
            var connection = new Connection(vpConnection, contextHandle.Target);
            var handle = GCHandle.Alloc(connection, GCHandleType.Normal);
            var ptr = GCHandle.ToIntPtr(handle);
            return ptr;
        }

        public static int ConnectNative(IntPtr ptr, IntPtr hostPtr, ushort port)
        {
            var handle = GCHandle.FromIntPtr(ptr);
            var connection = handle.Target as Connection;
            var host = Marshal.PtrToStringAnsi(hostPtr);
            return connection.Connect(host, port);
        }

        public static void DestroyNative(IntPtr ptr)
        {
            var handle = GCHandle.FromIntPtr(ptr);
            var connection = handle.Target as Connection;
            connection.BeforeDestroy();
            handle.Free();
        }

        public static int SendNative(IntPtr ptr, IntPtr data, uint length)
        {
            var connection = GCHandle.FromIntPtr(ptr).Target as Connection;
            return connection.Send(data, length);
        }

        public static int ReceiveNative(IntPtr ptr, IntPtr data, uint length)
        {
            var connection = GCHandle.FromIntPtr(ptr).Target as Connection;
            return connection.Receive(data, length);
        }

        public static int TimeoutNative(IntPtr ptr, int seconds)
        {
            var connection = GCHandle.FromIntPtr(ptr).Target as Connection;
            return connection.Timeout(seconds);
        }
    }
}
