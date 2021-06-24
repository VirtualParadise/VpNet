using System;
using System.Globalization;

namespace VpNet
{
    /// <summary>
    ///     Represents a 3-dimensional rotation composing of yaw, pitch, and roll.
    /// </summary>
    public struct Rotation : IEquatable<Rotation>
    {
        /// <summary>
        ///     A value which applies no rotation.
        /// </summary>
        /// <value>The rotation (0, 0, 0).</value>
        public static readonly Rotation Zero = new Rotation(0, 0, 0);
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="Rotation" /> structure by initializing <see cref="Yaw" /> and
        ///     <see cref="Pitch" /> to specified values, and <see cref="Roll" /> to 0. 
        /// </summary>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        public Rotation(double pitch, double yaw)
            : this(yaw, pitch, 0)
        {
        }
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="Rotation" /> structure by initializing <see cref="Yaw" />,
        ///     <see cref="Pitch" /> and <see cref="Roll" /> to specified values. 
        /// </summary>
        /// <param name="yaw">The yaw.</param>
        /// <param name="pitch">The pitch.</param>
        /// <param name="roll">The roll.</param>
        public Rotation(double pitch, double yaw, double roll)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }

        /// <summary>
        ///     Gets or sets the pitch of this rotation.
        /// </summary>
        /// <value>The pitch.</value>
        public double Pitch { get; set; }
        
        /// <summary>
        ///     Gets or sets the roll of this rotation.
        /// </summary>
        /// <value>The roll.</value>
        public double Roll { get; set; }
        
        /// <summary>
        ///     Gets or sets the yaw of this rotation.
        /// </summary>
        /// <value>The yaw.</value>
        public double Yaw { get; set; }

        /// <summary>
        ///     Implicitly converts a <see cref="Rotation" /> value to a <see cref="Vector3" />. 
        /// </summary>
        /// <param name="rotation">The rotation to convert.</param>
        /// <returns>
        ///     A <see cref="Vector3" /> whose <see cref="Vector3.X" /> component is equal to <see cref="Rotation.Pitch" />,
        ///     <see cref="Vector3.Y" /> component is equal to <see cref="Rotation.Yaw" />, and whose <see cref="Vector3.Z" />
        ///     component is equal to <see cref="Rotation.Roll" />.
        /// </returns>
        public static implicit operator Vector3(Rotation rotation) => new Vector3(rotation.Pitch, rotation.Yaw, rotation.Roll);
        
        /// <summary>
        ///     Implicitly converts a <see cref="Vector3" /> value to a <see cref="Rotation" />. 
        /// </summary>
        /// <param name="vector">The vector to convert.</param>
        /// <returns>
        ///     A <see cref="Rotation" /> whose <see cref="Rotation.Pitch" /> component is equal to <see cref="Vector3.X" />,
        ///     <see cref="Rotation.Yaw" /> component is equal to <see cref="Vector3.Y" />, and whose <see cref="Rotation.Roll" />
        ///     component is equal to <see cref="Vector3.Z" />.
        /// </returns>
        public static implicit operator Rotation(Vector3 vector) => new Rotation(vector.Y, vector.X, vector.Z);

        /// <summary>
        ///     Returns a value that indicates whether each pair of elements in two specified rotations is equal.
        /// </summary>
        /// <param name="left">The first rotation to compare.</param>
        /// <param name="right">The second rotation to compare.</param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        /// <remarks>
        ///     Two <see cref="Rotation" /> objects are equal if each element in left is equal to the corresponding element in
        ///     right.
        /// </remarks>
        public static bool operator ==(Rotation left, Rotation right) => left.Equals(right);

        /// <summary>
        ///     Returns a value that indicates whether two specified rotations are not equal.
        /// </summary>
        /// <param name="left">The first rotation to compare.</param>
        /// <param name="right">The second rotation to compare.</param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public static bool operator !=(Rotation left, Rotation right) => !left.Equals(right);

        /// <inheritdoc />
        public bool Equals(Rotation other)
        {
            return Pitch.Equals(other.Pitch) && Roll.Equals(other.Roll) && Yaw.Equals(other.Yaw);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Rotation other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Pitch.GetHashCode();
                hashCode = (hashCode * 397) ^ Roll.GetHashCode();
                hashCode = (hashCode * 397) ^ Yaw.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Returns the string representation of the current instance using default formatting.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        /// <remarks>
        ///     This method returns a string in which each element of the vector is formatted using the "G" (general) format
        ///     string and the formatting conventions of the current thread culture.
        ///     The "&lt;" and "&gt;" characters are used to begin and end the string, and the current culture's
        ///     <see cref="System.Globalization.NumberFormatInfo.NumberGroupSeparator" /> property followed by a space is used to
        ///     separate each element.
        /// </remarks>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Returns the string representation of the current instance using the specified format string to format individual
        ///     elements.
        /// </summary>
        /// <param name="format">
        ///     A standard or custom numeric format string that defines the format of individual elements.
        /// </param>
        /// <returns>The string representation of the current instance.</returns>
        /// <remarks>
        ///     This method returns a string in which each element of the vector is formatted using <paramref name="format" /> and
        ///     the current culture's formatting conventions. The "&lt;" and "&gt;" characters are used to begin and end the
        ///     string, and the current culture's <see cref="System.Globalization.NumberFormatInfo.NumberGroupSeparator" />
        ///     property followed by a space is used to separate each element.
        /// </remarks>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Returns the string representation of the current instance using the specified format string to format individual
        ///     elements and the specified format provider to define culture-specific formatting.
        /// </summary>
        /// <param name="format">
        ///     A standard or custom numeric format string that defines the format of individual elements.
        /// </param>
        /// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the current instance.</returns>
        /// <remarks>
        ///     This method returns a string in which each element of the vector is formatted using <paramref name="format" /> and
        ///     <paramref name="formatProvider" />. The "&lt;" and "&gt;" characters are used to begin and end the string, and the
        ///     format provider's <see cref="System.Globalization.NumberFormatInfo.NumberGroupSeparator" /> property followed by a
        ///     space is used to separate each element.
        /// </remarks>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            string pitch = Pitch.ToString(format, formatProvider);
            string yaw = Yaw.ToString(format, formatProvider);
            string roll = Roll.ToString(format, formatProvider);

            return $"<Yaw: {pitch + separator} Pitch: {yaw + separator} Roll: {roll + separator}>";
        }
    }
}
