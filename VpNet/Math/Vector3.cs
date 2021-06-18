using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Represents a vector with three single-precision floating-point values.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IEquatable<Vector3>, IFormattable
    {
        /// <summary>
        ///     A vector whose 3 elements are equal to one.
        /// </summary>
        /// <value>The vector (1, 1, 1).</value>
        [XmlIgnore]
        public static readonly Vector3 One = new Vector3(1);

        /// <summary>
        ///     A vector whose 3 elements are equal to zero.
        /// </summary>
        /// <value>The vector (0, 0, 0).</value>
        [XmlIgnore]
        public static readonly Vector3 Zero = new Vector3(0);

        /// <summary>
        ///     The vector (1, 0, 0).
        /// </summary>
        /// <value>The vector (1, 0, 0).</value>
        [XmlIgnore]
        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);

        /// <summary>
        ///     The vector (0, 1, 0).
        /// </summary>
        /// <value>The vector (0, 1, 0).</value>
        [XmlIgnore]
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);

        /// <summary>
        ///     The vector (0, 0, 1).
        /// </summary>
        /// <value>The vector (0, 0, 1).</value>
        [XmlIgnore]
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);

        /// <summary>
        ///     The X component of the vector.
        /// </summary>
        [XmlAttribute]
        public double X;

        /// <summary>
        ///     The Y component of the vector.
        /// </summary>
        [XmlAttribute]
        public double Y;

        /// <summary>
        ///     The Z component of the vector.
        /// </summary>
        [XmlAttribute]
        public double Z;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> structure.
        /// </summary>
        /// <param name="value">The value to assign to all three elements.</param>
        public Vector3(double value) : this(value, value, value)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> structure.
        /// </summary>
        /// <param name="x">The value to assign to the <see cref="X" /> field.</param>
        /// <param name="y">The value to assign to the <see cref="Y" /> field.</param>
        /// <param name="z">The value to assign to the <see cref="Z" /> field.</param>
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Gets the length of this vector.
        /// </summary>
        /// <value>The length of this vector.</value>
        /// <seealso cref="LengthSquared" />
        /// <remarks>
        ///     This property performs a square root operation. For situations where performance is critical, use
        ///     <see cref="LengthSquared" />.
        /// </remarks>
        [XmlIgnore]
        public double Length => Distance(this, Zero);

        /// <summary>
        ///     Gets the length of this vector.
        /// </summary>
        /// <value>The length of this vector.</value>
        /// <seealso cref="Length" />
        [XmlIgnore]
        public double LengthSquared => DistanceSquared(this, Zero);

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of the specified vector's elements.
        /// </summary>
        /// <param name="value">A vector.</param>
        /// <returns>The absolute value vector.</returns>
        public static Vector3 Abs(Vector3 value)
        {
            double x = Math.Abs(value.X);
            double y = Math.Abs(value.Y);
            double z = Math.Abs(value.Z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Adds two vectors together.
        /// </summary>
        /// <param name="left">The first vector to add.</param>
        /// <param name="right">The second vector to add.</param>
        /// <returns>The summed vector.</returns>
        public static Vector3 Add(Vector3 left, Vector3 right)
        {
            double x = left.X + right.X;
            double y = left.Y + right.Y;
            double z = left.Z + right.Z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Restricts a vector between a minimum and a maximum value.
        /// </summary>
        /// <param name="value">The vector to restrict.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            double x = MathHelper.Clamp(value.X, min.X, max.X);
            double y = MathHelper.Clamp(value.Y, min.Y, max.Y);
            double z = MathHelper.Clamp(value.Z, min.Z, max.Z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Computes the cross product of two vectors.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The cross product.</returns>
        public static Vector3 Cross(Vector3 left, Vector3 right)
        {
            double x = (left.Y * right.Z) - (left.Z * right.Y);
            double y = (left.Z * right.X) - (left.X * right.Z);
            double z = (left.X * right.Y) - (left.Y * right.X);
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Computes the Euclidean distance between two given points.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>The distance.</returns>
        public static double Distance(Vector3 left, Vector3 right)
        {
            double distanceSquared = DistanceSquared(left, right);
            return Math.Sqrt(distanceSquared);
        }

        /// <summary>
        ///     Computes the Euclidean distance squared between two given points.
        /// </summary>
        /// <param name="left">The first point.</param>
        /// <param name="right">The second point.</param>
        /// <returns>The distance squared.</returns>
        public static double DistanceSquared(Vector3 left, Vector3 right)
        {
            Vector3 difference = left - right;
            return Dot(difference, difference);
        }

        /// <summary>
        ///     Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector3 Divide(Vector3 left, Vector3 right)
        {
            double x = left.X / right.X;
            double y = left.Y / right.Y;
            double z = left.Z / right.Z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Divides the specified vector by a specified scalar value.
        /// </summary>
        /// <param name="left">The vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector3 Divide(Vector3 left, double right)
        {
            double x = left.X / right;
            double y = left.Y / right;
            double z = left.Z / right;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Divides the specified scalar value by a specified vector.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector3 Divide(double left, Vector3 right)
        {
            double x = left / right.X;
            double y = left / right.Y;
            double z = left / right.Z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Computes the dot product of two vectors.
        /// </summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The dot product.</returns>
        public static double Dot(Vector3 left, Vector3 right)
        {
            double x = left.X * right.X;
            double y = left.Y * right.Y;
            double z = left.Z * right.Z;
            return x + y + z;
        }

        /// <summary>
        ///     Performs a linear interpolation between two vectors based on the given weighting.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="t">A value between 0 and 1 that indicates the weight of <paramref name="b" />.</param>
        /// <returns>A value between 0 and 1 that indicates the weight of <paramref name="b" />.</returns>
        public static Vector3 Lerp(Vector3 a, Vector3 b, double t)
        {
            double x = MathHelper.Lerp(a.X, b.X, t);
            double y = MathHelper.Lerp(a.Y, b.Y, t);
            double z = MathHelper.Lerp(a.Z, b.Z, t);
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.
        /// </summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The maximized vector.</returns>
        public static Vector3 Max(Vector3 left, Vector3 right)
        {
            double x = Math.Max(left.X, right.X);
            double y = Math.Max(left.Y, right.Y);
            double z = Math.Max(left.Z, right.Z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.
        /// </summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The minimized vector.</returns>
        public static Vector3 Min(Vector3 left, Vector3 right)
        {
            double x = Math.Min(left.X, right.X);
            double y = Math.Min(left.Y, right.Y);
            double z = Math.Min(left.Z, right.Z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Returns a new vector whose values are the product of each pair of elements in two specified vectors.
        /// </summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The element-wise product vector.</returns>
        public static Vector3 Multiply(Vector3 left, Vector3 right)
        {
            double x = left.X * right.X;
            double y = left.Y * right.Y;
            double z = left.Z * right.Z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Multiplies a vector by a specified scalar value.
        /// </summary>
        /// <param name="left">The vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 Multiply(Vector3 left, double right)
        {
            double x = left.X * right;
            double y = left.Y * right;
            double z = left.Z * right;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Multiplies a scalar value by a specified vector.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The vector.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 Multiply(double left, Vector3 right)
        {
            double x = left * right.X;
            double y = left * right.Y;
            double z = left * right.Z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Negates a specified vector.
        /// </summary>
        /// <param name="value">The vector to negate.</param>
        /// <returns>The negated vector.</returns>
        public static Vector3 Negate(Vector3 value)
        {
            double x = -value.X;
            double y = -value.Y;
            double z = -value.Z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Returns a vector with the same direction as the specified vector, but with a length of one.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        public static Vector3 Normalize(Vector3 value)
        {
            return value / value.Length;
        }

        /// <summary>
        ///     Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        public static Vector3 Reflect(Vector3 value, Vector3 normal)
        {
            double dot = Dot(value, normal);
            return value - (2 * dot * normal);
        }

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of a specified vector's elements.
        /// </summary>
        /// <param name="value">A vector.</param>
        /// <returns>The square root vector.</returns>
        public static Vector3 SquareRoot(Vector3 value)
        {
            double x = Math.Sqrt(value.X);
            double y = Math.Sqrt(value.Y);
            double z = Math.Sqrt(value.Z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        ///     Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector3 Subtract(Vector3 left, Vector3 right)
        {
            double x = left.X - right.X;
            double y = left.Y - right.Y;
            double z = left.Z - right.Z;
            return new Vector3(x, y, z);
        }

        /// <inheritdoc cref="Add" />
        public static Vector3 operator +(Vector3 left, Vector3 right) => Add(left, right);

        /// <inheritdoc cref="Subtract" />
        public static Vector3 operator -(Vector3 left, Vector3 right) => Subtract(left, right);

        /// <inheritdoc cref="Multiply(Vector3, Vector3)" />
        public static Vector3 operator *(Vector3 left, Vector3 right) => Multiply(left, right);

        /// <inheritdoc cref="Multiply(double, Vector3)" />
        public static Vector3 operator *(double left, Vector3 right) => Multiply(left, right);

        /// <inheritdoc cref="Multiply(Vector3, double)" />
        public static Vector3 operator *(Vector3 left, double right) => Multiply(left, right);

        /// <inheritdoc cref="Divide(Vector3, Vector3)" />
        public static Vector3 operator /(Vector3 left, Vector3 right) => Divide(left, right);

        /// <inheritdoc cref="Divide(double, Vector3)" />
        public static Vector3 operator /(double left, Vector3 right) => Divide(left, right);

        /// <inheritdoc cref="Divide(Vector3, double)" />
        public static Vector3 operator /(Vector3 left, double right) => Divide(left, right);

        /// <summary>
        ///     Returns a value that indicates whether each pair of elements in two specified vectors is equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        /// <remarks>
        ///     Two <see cref="Vector3" /> objects are equal if each element in left is equal to the corresponding element in
        ///     right.
        /// </remarks>
        public static bool operator ==(Vector3 left, Vector3 right) => left.Equals(right);

        /// <summary>
        ///     Returns a value that indicates whether two specified vectors are not equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public static bool operator !=(Vector3 left, Vector3 right) => !left.Equals(right);

        /// <inheritdoc />
        public bool Equals(Vector3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Vector3 other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
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
            var sb = new StringBuilder();
            string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
            sb.Append('<');
            sb.Append(X.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Y.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Z.ToString(format, formatProvider));
            sb.Append('>');
            return sb.ToString();
        }
    }
}
