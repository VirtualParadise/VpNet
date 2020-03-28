using System;

namespace VpNet
{
    /// <summary>
    /// In World Chat Message Types
    /// </summary>
    [Serializable]
    public enum ChatMessageTypes
    {
        /// <summary>
        /// The normal chat message.
        /// </summary>
        Normal,
        /// <summary>
        /// The console chat message.
        /// </summary>
        Console,
        /// <summary>
        /// The whisper chat message.
        /// </summary>
        Whisper
    }
}