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

namespace VpNet.NativeApi
{
    internal class Functions
    {
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_init(int build);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr vp_create(ref NetConfig netConfig);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_destroy(IntPtr instance);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_connect_universe(IntPtr instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string host, 
            int port);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_login(IntPtr instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string username,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string password,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string botname);
        
        [DllImport("vpsdk", CallingConvention=CallingConvention.Cdecl)]
        public static extern int vp_wait(IntPtr instance, int time);
        
        [DllImport("vpsdk", CallingConvention=CallingConvention.Cdecl)]
        public static extern int vp_enter(IntPtr instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string worldname);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_leave(IntPtr instance);
        
        [DllImport("vpsdk", CallingConvention=CallingConvention.Cdecl)]
        public static extern int vp_say(IntPtr instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string message);
        
        [DllImport("vpsdk", CallingConvention=CallingConvention.Cdecl)]
        internal static extern int vp_event_set(IntPtr instance, int eventName, [MarshalAs(UnmanagedType.FunctionPtr)]EventDelegate eventFunction);

        [DllImport("vpsdk", CallingConvention=CallingConvention.Cdecl)]
        internal static extern int vp_callback_set(IntPtr instance, int callbackname, [MarshalAs(UnmanagedType.FunctionPtr)]CallbackDelegate callbackFunction);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr vp_user_data(IntPtr instance);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern void vp_user_data_set(IntPtr instance, IntPtr data);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_state_change(IntPtr instance);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_int(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern float vp_float(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern double vp_double(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToManaged))]
        public static extern string vp_string(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr vp_data(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name, out int length);
             
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_int_set(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name, int value);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_float_set(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name, float value);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_double_set(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name, double value);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern void vp_string_set(IntPtr instance, 
            [MarshalAs(UnmanagedType.I4)] Attributes name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string value);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_data_set(IntPtr instance, [MarshalAs(UnmanagedType.I4)]Attributes name, int length, byte[] data);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_query_cell(IntPtr instance, int x, int z);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_query_cell_revision(IntPtr instance, int x, int z, int revision);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_object_add(IntPtr instance);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_object_load(IntPtr instance);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_object_change(IntPtr instance);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_object_delete(IntPtr instance, int object_id);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_object_click(IntPtr instance, int object_id, 
                              int session_to, float hit_x, 
                              float hit_y, float hit_z);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_world_list(IntPtr instance, int time);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_user_attributes_by_id(IntPtr instance, int userId);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_user_attributes_by_name(IntPtr instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string name);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_friends_get(IntPtr instance);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_friend_add_by_name(IntPtr instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string name);
        
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_friend_delete(IntPtr instance, int friendUserId);

        public static byte[] GetData(IntPtr instance, Attributes attribute)
        {
            int length;
            var ptr = vp_data(instance, attribute, out length);
            var result = new byte[length];
            Marshal.Copy(ptr, result, 0, length);
            return result;
        }

        public static void SetData(IntPtr instance, Attributes attribute, byte[] data)
        {
            vp_data_set(instance, attribute, data != null ? data.Length : 0, data);
        }

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_avatar_click(IntPtr instance, int session);


        /// <summary>
        /// Vp_teleport_avatars the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="targetSession">The target_session.</param>
        /// <param name="world">The world.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns></returns>
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_teleport_avatar(IntPtr instance, int targetSession, string world, float x, float y, float z, float yaw, float pitch);

        /// <summary>
        /// Vp_console_messages the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="session">The session.</param>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        /// <param name="effects">The effects.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <returns></returns>
        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_console_message(IntPtr instance, int targetSession, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string name, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string message, int effects, byte red, byte green, byte blue);


        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_terrain_query(IntPtr instance, int tile_x, int tile_z, int[,] nodes);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_terrain_node_set(IntPtr instance, int tile_x, int tile_z, int node_x, int node_z, TerrainCell[,] node);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_object_get(IntPtr instance, int object_id);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_url_send(IntPtr instance, int session_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string url, int url_target);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_join(IntPtr instance, int user_id);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_join_accept(IntPtr instance, int requestId, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string world, double x, double y, double z, float yaw, float pitch);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_join_decline(IntPtr instance, int requestId);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_world_permission_user_set(IntPtr instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string permission, int user_id, int enable);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_world_permission_session_set(IntPtr instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string permission, int session_id, int enable);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_world_setting_set(IntPtr instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string setting, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringToNative))] string value, int session_to);

        [DllImport("vpsdk", CallingConvention = CallingConvention.Cdecl)]
        public static extern int vp_net_notify(IntPtr vpConnection, int type, int status);

    }
}
