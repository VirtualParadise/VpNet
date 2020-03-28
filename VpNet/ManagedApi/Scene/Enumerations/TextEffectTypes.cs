using System;

namespace VpNet
{
    /// <summary>
    /// Text effect type flags for in world chat font.
    /// </summary>
    [Flags]
    [Serializable]
    public enum TextEffectTypes
    {
        /// <summary>
        /// Set font to bold.
        /// </summary>
        Bold = 1,
        /// <summary>
        /// Set font to italics.
        /// </summary>
        Italic = 2,
        /// <summary>
        /// Combine bold and italics.
        /// </summary>
        BoldItalic = Bold | Italic
    };
}
