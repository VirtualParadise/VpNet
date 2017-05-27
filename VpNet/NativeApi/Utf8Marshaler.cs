#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2016 CUBE3 (Cit:36)

    VPNET is free software: you can redistribute it and/or modify it under the terms of the 
    GNU Lesser General Public License (LGPL) as published by the Free Software Foundation, either
    version 2.1 of the License, or (at your option) any later version.

    VPNET is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even
    the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the LGPL License
    for more details.

    You should have received a copy of the GNU Lesser General Public License (LGPL) along with VPNET.
    If not, see <http://www.gnu.org/licenses/>. 
*/
#endregion

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VpNet.NativeApi
{
    internal class Utf8StringToNative : ICustomMarshaler
    {
        private static Utf8StringToNative _instance;

        public static ICustomMarshaler GetInstance(string cookie)
        {
            if (_instance == null)
            {
                _instance = new Utf8StringToNative();
            }
            return _instance;
        }

        public void CleanUpManagedData(object ManagedObj)
        {
        }

        public virtual void CleanUpNativeData(IntPtr pNativeData)
        {
            Marshal.FreeHGlobal(pNativeData);
        }

        public int GetNativeDataSize()
        {
            return -1;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            var UTF32Data = Encoding.UTF8.GetBytes((string)ManagedObj);
            var buffer = Marshal.AllocHGlobal(UTF32Data.Length + 1);
            Marshal.Copy(UTF32Data, 0, buffer, UTF32Data.Length);
            Marshal.WriteByte(buffer, UTF32Data.Length, 0);
            return buffer;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            throw new NotImplementedException();
        }
    }

    internal class Utf8StringToManaged : ICustomMarshaler
    {
        private static Utf8StringToManaged _instance;

        public static ICustomMarshaler GetInstance(string cookie)
        {
            if (_instance == null)
            {
                _instance = new Utf8StringToManaged();
            }
            return _instance;
        }

        /// <summary>
        /// Do nothing, for some reason CleanUpNativeData is called for pointers
        /// that are not even created by the marshaler. This can 
        /// cause heap corruption because of double free or because a different
        /// allocater may have been used to allocate the memory block.
        /// </summary>
        /// <param name="pNativeData"></param>
        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }
    
        public void  CleanUpManagedData(object ManagedObj)
        {
        }

        public int  GetNativeDataSize()
        {
            return -1;
        }

        public IntPtr  MarshalManagedToNative(object ManagedObj)
        {
 	        throw new NotImplementedException();
        }

        private int GetStringLength(IntPtr ptr)
        {
            int offset = 0;
            for (offset = 0; Marshal.ReadByte(ptr, offset) != 0; offset++) { }
            return offset;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
            {
                return null;
            }

            var buffer = new byte[GetStringLength(pNativeData)];
            Marshal.Copy(pNativeData, buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }
    }
}
