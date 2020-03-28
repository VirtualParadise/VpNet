namespace VpNet.NativeApi
{
    /// <summary>
    /// Event Types
    /// </summary>
    internal enum Events
    {
        /// <summary>
        /// Chat event
        /// </summary>
        Chat = 0,
        /// <summary>
        /// Avatar add event
        /// </summary>
        AvatarAdd = 1,
        /// <summary>
        /// Avatar change event
        /// </summary>
        AvatarChange = 2,
        /// <summary>
        /// Avatar delete event
        /// </summary>
        AvatarDelete = 3,
        /// <summary>
        /// Object event
        /// </summary>
        Object = 4,
        /// <summary>
        /// Object change event
        /// </summary>
        ObjectChange = 5,
        /// <summary>
        /// Oject delete event
        /// </summary>
        ObjectDelete = 6,
        /// <summary>
        /// Object click event
        /// </summary>
        ObjectClick = 7,
        /// <summary>
        /// World list event
        /// </summary>
        WorldList = 8,
        /// <summary>
        /// World setting event
        /// </summary>
        WorldSetting = 9,
        /// <summary>
        /// World settings changed event
        /// </summary>
        WorldSettingsChanged = 10,
        /// <summary>
        /// Friend event
        /// </summary>
        Friend = 11,
        /// <summary>
        /// World disconnect event
        /// </summary>
        WorldDisconnect = 12,
        /// <summary>
        /// Universe disconnect event
        /// </summary>
        UniverseDisconnect = 13,
        /// <summary>
        /// User attributes event
        /// </summary>
        UserAttributes = 14,
        /// <summary>
        /// Cell query end event
        /// </summary>
        QueryCellEnd = 15,
        /// <summary>
        /// Terrain node event
        /// </summary>
        TerrainNode = 16,
        /// <summary>
        /// Avatar click event
        /// </summary>
        AvatarClick = 17,
        /// <summary>
        /// Teleport event
        /// </summary>
        Teleport = 18,
        /// <summary>
        /// Url Event
        /// </summary>
        Url = 19,
        /// <summary>
        /// The object bump begin
        /// </summary>
        ObjectBumpBegin,
        /// <summary>
        /// The object bump end
        /// </summary>
        ObjectBumpEnd,
        /// <summary>
        /// Terrain node changed
        /// </summary>
        TerrainNodeChanged,
        /// <summary>
        /// Join Event
        /// </summary>
        Join,
        /// <summary>
        /// Invite event
        /// </summary>
        Invite
    }
}
