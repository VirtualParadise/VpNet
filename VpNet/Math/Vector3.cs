namespace VpNet
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    ///     Represents a vector with three double-precision floating-point values.
    /// </summary>
    [Serializable]
    public struct Vector3 : IEquatable<Vector3>, IFormattable
    {
        /// <summary>
        ///     Represents a <see cref="Vector3" /> whose component values are &lt;0, 0, 0&gt;.
        /// </summary>
        public static readonly Vector3 Zero = new Vector3(0f);

        /// <summary>
        ///     Represents a <see cref="Vector3" /> whose component values are &lt;1, 1, 1&gt;.
        /// </summary>
        public static readonly Vector3 One = new Vector3(1f);

        /// <summary>
        ///     Represents a <see cref="Vector3" /> whose component values are &lt;1, 0, 0&gt;.
        /// </summary>
        public static readonly Vector3 UnitX = new Vector3(1f, 0f, 0f);

        /// <summary>
        ///     Represents a <see cref="Vector3" /> whose component values are &lt;0, 1, 0&gt;.
        /// </summary>
        public static readonly Vector3 UnitY = new Vector3(0f, 1f, 0f);

        /// <summary>
        ///     Represents a <see cref="Vector3" /> whose component values are &lt;1, 0, 0&gt;.
        /// </summary>
        public static readonly Vector3 UnitZ = new Vector3(0f, 0f, 1f);

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct by initializing all components to the same value.
        /// </summary>
        /// <param name="v">The value for each component.</param>
        public Vector3(double v) : this(v, v, v)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct by copying the component values from another
        ///     instance of the <see cref="Vector3" /> struct. This is the copy constructor.
        /// </summary>
        /// <param name="v">The <see cref="Vector3" /> to copy.</param>
        public Vector3(Vector3 v) : this(v.X, v.Y, v.Z)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct by initializing each component to specific values.
        /// </summary>
        /// <param name="x">The X component value.</param>
        /// <param name="y">The Y component value.</param>
        /// <param name="z">The Z component value.</param>
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Gets the magnitude of this vector.
        /// </summary>
        public readonly double Magnitude
        {
            get => Distance(this, Zero);
        }

        /// <summary>
        ///     Gets the square magnitude of this vector.
        /// </summary>
        public readonly double MagnitudeSquared
        {
            get => DistanceSquared(this, Zero);
        }

        /// <summary>
        ///     Gets or sets the X component value.
        /// </summary>
        [XmlAttribute]
        public double X { get; set; }

        /// <summary>
        ///     Gets or sets the Y component value.
        /// </summary>
        [XmlAttribute]
        public double Y { get; set; }

        /// <summary>
        ///     Gets or sets the Z component value.
        /// </summary>
        [XmlAttribute]
        public double Z { get; set; }

        /// <summary>
        ///     Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.X + right.X,
                left.Y + right.Y,
                left.Z + right.Z
            );
        }

        /// <summary>
        ///     Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.X / right.X,
                left.Y / right.Y,
                left.Z / right.Z
            );
        }

        /// <summary>
        ///     Divides the vector by the given divisor.
        /// </summary>
        /// <param name="dividend">The source vector.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(Vector3 dividend, double divisor)
        {
            return dividend / new Vector3(divisor);
        }

        /// <summary>
        ///     Inversely divides the vector by the given divisor such that the result is <c>divisor * (1 / dividend)</c>.
        /// </summary>
        /// <param name="dividend">The scalar value.</param>
        /// <param name="divisor">The source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(double dividend, Vector3 divisor)
        {
            return new Vector3(dividend) / divisor;
        }

        /// <summary>
        ///     Returns a boolean indicating whether the two given vectors are equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns><see langword="true" /> if the vectors are equal; <see langword="false" /> otherwise.</returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.X.Equals(right.X) &&
                   left.Y.Equals(right.Y) &&
                   left.Z.Equals(right.Z);
        }

        /// <summary>
        ///     Returns a boolean indicating whether the two given vectors are not equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns><see langword="true" /> if the vectors are not equal; <see langword="false" /> if they are equal.</returns>
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.X * right.X,
                left.Y * right.Y,
                left.Z * right.Z
            );
        }

        /// <summary>
        ///     Multiplies the vector by the given scalar.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="scalar">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(Vector3 vector, double scalar)
        {
            return vector * new Vector3(scalar);
        }

        /// <summary>
        ///     Multiplies the vector by the given scalar.
        /// </summary>
        /// <param name="scalar">The scalar value.</param>
        /// <param name="vector">The source vector.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(double scalar, Vector3 vector)
        {
            return new Vector3(scalar) * vector;
        }

        /// <summary>
        ///     Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.X - right.X,
                left.Y - right.Y,
                left.Z - right.Z
            );
        }

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 value)
        {
            return Zero - value;
        }

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of the specified vector's elements.    
        /// </summary>
        /// <param name="value">A vector.</param>
        /// <returns>The absolute value vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(Vector3 value)
        {
            var (x, y, z) = value;
            return new Vector3(
                Math.Abs(x),
                Math.Abs(y),
                Math.Abs(z)
            );
        }

        /// <summary>
        ///     Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(Vector3 left, Vector3 right)
        {
            return left + right;
        }

        /// <summary>
        ///     Restricts a vector between a min and max value.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            return Min(Max(value, min), max);
        }

        /// <summary>Computes the cross product of two vectors.</summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The cross product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                (value1.Y * value2.Z) - (value1.Z * value2.Y),
                (value1.Z * value2.X) - (value1.X * value2.Z),
                (value1.X * value2.Y) - (value1.Y * value2.X)
            );
        }

        /// <summary>
        ///     Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Vector3 value1, Vector3 value2)
        {
            var distanceSquared = DistanceSquared(value1, value2);
            return Math.Sqrt(distanceSquared);
        }

        /// <summary>
        ///     Calculates the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The square distance between <paramref name="value1" /> and <paramref name="value2" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceSquared(Vector3 value1, Vector3 value2)
        {
            var difference = value1 - value2;
            return Dot(difference, difference);
        }

        /// <summary>
        ///     Divides the first vector by the second.
        /// </summary>
        /// <param name="dividend">The first source vector.</param>
        /// <param name="divisor">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(Vector3 dividend, Vector3 divisor)
        {
            return dividend / divisor;
        }

        /// <summary>
        ///     Divides the vector by the given divisor.
        /// </summary>
        /// <param name="dividend">The source vector.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(Vector3 dividend, double divisor)
        {
            return dividend / divisor;
        }

        /// <summary>
        ///     Inversely divides the vector by the given divisor such that the result is <c>divisor * (1 / dividend)</c>.
        /// </summary>
        /// <param name="dividend">The scalar value.</param>
        /// <param name="divisor">The source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(double dividend, Vector3 divisor)
        {
            return dividend / divisor;
        }

        /// <summary>
        ///     Returns the dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dot(Vector3 value1, Vector3 value2)
        {
            return (value1.X * value2.X)
                 + (value1.Y * value2.Y)
                 + (value1.Z * value2.Z);
        }

        /// <summary>
        ///     Performs a linear interpolation between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">A value between 0 and 1 that indicates the weight of <paramref name="value2" />.</param>
        /// <returns>The interpolated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(Vector3 value1, Vector3 value2, double amount)
        {
            return (value1 * (1.0 - amount)) + (value2 * amount);
        }

        /// <summary>
        ///     Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The maximized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Max(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                Math.Max(value1.X, value2.X),
                Math.Max(value1.Y, value2.Y),
                Math.Max(value1.Z, value2.Z)
            );
        }

        /// <summary>
        ///     Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The minimized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Min(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                Math.Min(value1.X, value2.X),
                Math.Min(value1.Y, value2.Y),
                Math.Min(value1.Z, value2.Z)
            );
        }

        /// <summary>
        ///     Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(Vector3 left, Vector3 right)
        {
            return left * right;
        }

        /// <summary>
        ///     Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="scalar">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(Vector3 vector, float scalar)
        {
            return vector * scalar;
        }

        /// <summary>
        ///     Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="scalar">The scalar value.</param>
        /// <param name="vector">The source vector.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(float scalar, Vector3 vector)
        {
            return scalar * vector;
        }

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Negate(Vector3 value)
        {
            return -value;
        }

        /// <summary>
        ///     Returns a vector with the same direction as the given vector, but with magnitude 1.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(Vector3 value)
        {
            return value / value.Magnitude;
        }

        /// <summary>
        ///     Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            var dot = Dot(vector, normal);
            return vector - (2.0 * dot * normal);
        }

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Sqrt(Vector3 value)
        {
            return new Vector3(
                Math.Sqrt(value.X),
                Math.Sqrt(value.Y),
                Math.Sqrt(value.Z)
            );
        }

        /// <summary>
        ///     Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Subtract(Vector3 left, Vector3 right)
        {
            return left - right;
        }

        /// <summary>
        ///     Copies the contents of the vector into the given array, starting from index.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The index at which to copy the first element of the vector.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <see langword="null" />.</exception>
        /// <exception cref="RankException"><paramref name="array" /> is multidimensional.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="index"/> is greater than end of the array or index is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The number of elements in source vector is greater than those available in destination array.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(double[] array, int index = 0)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if ((index < 0) || (index >= array.Length))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if ((array.Length - index) < 3)
            {
                throw new ArgumentException(
                    "The number of elements in source vector is greater than those available in destination array.",
                    nameof(array));
            }

            array[index] = X;
            array[index + 1] = Y;
            array[index + 2] = Z;
        }

        /// <summary>
        ///     Deconstructs the current <see cref="Vector3" />.
        /// </summary>
        /// <param name="x">The X component value of the current <see cref="Vector3" />.</param>
        /// <param name="y">The Y component value of the current <see cref="Vector3" />.</param>
        /// <param name="z">The Z component value of the current <see cref="Vector3" />.</param>
        public readonly void Deconstruct(out double x, out double y, out double z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        /// <inheritdoc />
        public readonly bool Equals(Vector3 other)
        {
            return this == other;
        }

        /// <inheritdoc />
        public override readonly bool Equals(object obj)
        {
            return obj is Vector3 other && Equals(other);
        }

        /// <inheritdoc />
        public override readonly int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Returns a <see cref="string" /> representing this <see cref="Vector3" /> instance.
        /// </summary>
        /// <returns>The <see cref="string" /> representation.</returns>
        public override readonly string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Returns a <see cref="string" /> representing this <see cref="Vector3" /> instance, , using the specified format to
        ///     format individual elements.
        /// </summary>
        /// <param name="format">The format of individual elements.</param>
        /// <returns>The string representation.</returns>
        public readonly string ToString(string? format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Returns a <see cref="string" /> representing this <see cref="Vector3" /> instance, , using the specified format to
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
                   .Append(X.ToString(format, formatProvider))
                   .Append(separator)
                   .Append(' ')
                   .Append(Y.ToString(format, formatProvider))
                   .Append(separator)
                   .Append(' ')
                   .Append(Z.ToString(format, formatProvider))
                   .Append('>');

            return builder.ToString();
        }
    }
}
