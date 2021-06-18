using System;

namespace VpNet
{
    /// <summary>
    ///     Provides useful mathematical functions.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        ///     Returns <paramref name="value" /> clamped to the inclusive range of <paramref name="min" /> and
        ///     <paramref name="max" />.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        ///     <para><paramref name="value" /> if min ≤ <paramref name="value" /> ≤ <paramref name="max" />.</para>
        ///     -or-
        ///     <para><paramref name="min" /> if <paramref name="value" /> &lt; <paramref name="min" />.</para>
        ///     -or-
        ///     <para><paramref name="max" /> if <paramref name="max" /> &lt; <paramref name="value" />.</para>
        ///     -or-
        ///     <para></para>
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="min" /> is greater than <paramref name="max" />.</exception>
        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentException("Minimum cannot be greater than maximum.", nameof(min));
            }

            return value > max ? max : value < min ? min : value;
        }
        
        /// <summary>
        ///     Returns <paramref name="value" /> clamped to the inclusive range of <paramref name="min" /> and
        ///     <paramref name="max" />.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        ///     <para><paramref name="value" /> if min ≤ <paramref name="value" /> ≤ <paramref name="max" />.</para>
        ///     -or-
        ///     <para><paramref name="min" /> if <paramref name="value" /> &lt; <paramref name="min" />.</para>
        ///     -or-
        ///     <para><paramref name="max" /> if <paramref name="max" /> &lt; <paramref name="value" />.</para>
        ///     -or-
        ///     <para></para>
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="min" /> is greater than <paramref name="max" />.</exception>
        public static double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException("Minimum cannot be greater than maximum.", nameof(min));
            }

            return value > max ? max : value < min ? min : value;
        }

        /// <summary>
        ///     Performs a linear interpolation between two double-precision floating-point values based on the given weighting.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <param name="t">A value between 0 and 1 that indicates the weight of <paramref name="b" />.</param>
        /// <returns>The interpolated value.</returns>
        public static double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }
    }
}
