using System;
using System.Runtime.InteropServices;

namespace VpNet.NativeApi
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CallbackDelegate(IntPtr sender, int rc, int reference);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EventDelegate(IntPtr sender);



    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr SocketCreateFunction(IntPtr connection, IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SocketDestroyFunction(IntPtr socket);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SocketConnectFunction(IntPtr socket, 
        IntPtr host,//[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string host, 
        ushort port);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SocketSendFunction(IntPtr socket, IntPtr data, uint length);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SocketReceiveFunction(IntPtr socket, IntPtr data, uint length);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SocketTimeoutFunction(IntPtr socket, int seconds);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SocketWaitFunction(IntPtr context, int duration);

}
