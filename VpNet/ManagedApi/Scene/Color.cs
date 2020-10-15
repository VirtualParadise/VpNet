using System;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace VpNet
{

    /// <summary>
    ///     Represents an RGB (red, green, blue) color which uses <see cref="byte" /> components.
    /// </summary>
    [Serializable]
    [XmlRoot("Color", Namespace = Global.XmlNsScene)]
    public struct Color : IEquatable<Color>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Color" /> struct.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Color(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        /// <summary>
        ///     Gets or sets the red component value of this color.
        /// </summary>
        [XmlAttribute]
        public byte R { get; set; }

        /// <summary>
        ///     Gets or sets the green component value of this color.
        /// </summary>
        [XmlAttribute]
        public byte G { get; set; }

        /// <summary>
        ///     Gets or sets the blue component value of this color.
        /// </summary>
        [XmlAttribute]
        public byte B { get; set; }

        /// <summary>
        ///     Returns a boolean indicating whether the two given colors are equal.
        /// </summary>
        /// <param name="left">The first color to compare.</param>
        /// <param name="right">The second color to compare.</param>
        /// <returns><see langword="true" /> if the color are equal; <see langword="false" /> otherwise.</returns>
        public static bool operator ==(Color left, Color right)
        {
            return left.R == right.R &&
                   left.G == right.G &&
                   left.B == right.B;
        }

        /// <summary>
        ///     Returns a boolean indicating whether the two given colors are not equal.
        /// </summary>
        /// <param name="left">The first color to compare.</param>
        /// <param name="right">The second color to compare.</param>
        /// <returns><see langword="true" /> if the color are not equal; <see langword="false" /> if they are equal.</returns>
        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        /// <inheritdoc />
        public readonly bool Equals(Color other)
        {
            return this == other;
        }

        /// <inheritdoc />
        public override readonly bool Equals(object obj)
        {
            return obj is Color other && Equals(other);
        }

        /// <inheritdoc />
        public override readonly int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Returns a <see cref="string" /> representing this <see cref="Color" /> instance.
        /// </summary>
        /// <returns>The <see cref="string" /> representation.</returns>
        public override readonly string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Returns a <see cref="string" /> representing this <see cref="Color" /> instance, , using the specified format to
        ///     format individual elements.
        /// </summary>
        /// <param name="format">The format of individual elements.</param>
        /// <returns>The string representation.</returns>
        public readonly string ToString(string? format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Returns a <see cref="string" /> representing this <see cref="Color" /> instance, , using the specified format to
        ///     format individual elements.
        /// </summary>
        /// <param name="format">The format of individual elements.</param>
        /// <param name="formatProvider">The format provider to use when formatting elements.</param>
        /// <returns>The string representation.</returns>
        public readonly string ToString(string? format, IFormatProvider? formatProvider)
        {
            var builder = new StringBuilder();
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            builder.Append('<')
                   .Append(R.ToString(format, formatProvider))
                   .Append(separator)
                   .Append(' ')
                   .Append(G.ToString(format, formatProvider))
                   .Append(separator)
                   .Append(' ')
                   .Append(B.ToString(format, formatProvider))
                   .Append('>');

            return builder.ToString();
        }
    }
}
