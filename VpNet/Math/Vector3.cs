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
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using VpNet.Design;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    //[TypeConverter(typeof(Vector3Converter))]
    public struct Vector3 : IEquatable<Vector3>, IVector3
    {
        [XmlAttribute]
        double IVector3.X
        {
            get { return X; }
            set { X = value; }
        }

        [XmlAttribute]
        double IVector3.Y
        {
            get { return Y; }
            set { Y = value; }
        }

        [XmlAttribute]
        double IVector3.Z
        {
            get { return Z; }
            set { Z = value; }
        }

        [XmlAttribute]
        public double X;
        [XmlAttribute]
        public Double Y;
        [XmlAttribute]
        public double Z;

        private static readonly Vector3 _zero;
        private static readonly Vector3 _unitX = new Vector3(1f, 0f, 0f);
        private static readonly Vector3 _unitY = new Vector3(0f, 1f, 0f);
        private static readonly Vector3 _unitZ = new Vector3(0f, 0f, 1f);
        private static readonly Vector3 _one = new Vector3(1f, 1f, 1f);
        private static readonly Vector3 _min = new Vector3(double.MinValue);
        private static readonly Vector3 _max = new Vector3(double.MaxValue);
        private static readonly Vector3 _up = new Vector3(0f, 1f, 0f);
        private static readonly Vector3 _down= new Vector3(0f, -1f, 0f);
        private static readonly Vector3 _left = new Vector3(-1f, 0f, 0f);
        private static readonly Vector3 _right = new Vector3(1f, 0f, 0f);
        private static readonly Vector3 _forward= new Vector3(0f, 0f, -1f);
        private static readonly Vector3 _backward= new Vector3(0f, 0f, 1f);

        public static Vector3 Zero
        {
            get { return _zero; }
        }

        public static Vector3 One
        {
            get { return _one; }
        }

        public static Vector3 UnitX
        {
            get { return _unitX; }
        }

        public static Vector3 UnitY
        {
            get { return _unitY; }
        }

        public static Vector3 UnitZ
        {
            get { return _unitZ; }
        }

        public static Vector3 Up
        {
            get { return _up; }
        }

        public static Vector3 Down
        {
            get { return _down; }
        }

        public static Vector3 Right
        {
            get { return _right; }
        }

        public static Vector3 Left
        {
            get { return _left; }
        }

        public static Vector3 Forward
        {
            get { return _forward; }
        }

        public static Vector3 Backward
        {
            get { return _backward; }
        }



        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(double value)
        {
            this.X = this.Y = this.Z = value;
        }

        public Vector3(Vector2 value, double z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        public Vector3(string x, string y, string z)
        {
            X = double.Parse(x, CultureInfo.InvariantCulture);
            Y = double.Parse(y, CultureInfo.InvariantCulture);
            Z = double.Parse(z, CultureInfo.InvariantCulture);
        }

        public Vector3(string[] data)
        {
            if (data.Length != 3)
            {
                throw new Exception("should contain 3 double strings");
            }
            X = double.Parse(data[0], CultureInfo.InvariantCulture);
            Y = double.Parse(data[1], CultureInfo.InvariantCulture);
            Z = double.Parse(data[2], CultureInfo.InvariantCulture);

        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2}}}",
                                 new object[]
                                     {
                                         this.X.ToString(currentCulture), this.Y.ToString(currentCulture),
                                         this.Z.ToString(currentCulture)
                                     });
        }

        public bool Equals(Vector3 other)
        {
            return (((this.X == other.X) && (this.Y == other.Y)) && (this.Z == other.Z));
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Vector3)
            {
                flag = this.Equals((Vector3)obj);
            }
            return flag;
        }

        public override int GetHashCode()
        {
            return ((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode());
        }

        public double Length()
        {
            double n = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
            return (double)Math.Sqrt((double)n);
        }

        public double LengthSquared()
        {
            return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
        }

        public static double PointLineDistance(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            Vector3 v1V0 = v1 - v0;
            Vector3 v2V1 = v2 - v1;

            double a = v1V0.LengthSquared()*v2V1.LengthSquared();
            double dot = Vector3.Dot(v1V0, v2V1);
            return (a - dot*dot)/v2V1.LengthSquared();
        }

        /// <summary>
        /// Finds the point along line.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="d">The distance</param>
        /// <returns></returns>
        public static Vector3 PointAlongLine(Vector3 v1, Vector3 v2, double d)
        {
            return new Vector3(v1.X + (v2.X - v1.X) * d, v1.Y + (v2.Y - v1.Y) * d, v1.Z + (v2.Z - v1.Z) * d);
        }

        public Vector2 Xz()
        {
            return new Vector2(X,Z);
        }

        public Vector2 Xy()
        {
            return new Vector2(X,Y);
        }

        public static double Distance(Vector3 v1, Vector3 v2)
        {
            double n = v1.X - v2.X;
            double n2 = v1.Y - v2.Y;
            double n3 = v1.Z - v2.Z;
            double n4 = ((n * n) + (n2 * n2)) + (n3 * n3);
            return (double)Math.Sqrt((double)n4);
        }

        public static void Distance(ref Vector3 v1, ref Vector3 v2, out double ret)
        {
            double n = v1.X - v2.X;
            double n2 = v1.Y - v2.Y;
            double n3 = v1.Z - v2.Z;
            double n4 = ((n * n) + (n2 * n2)) + (n3 * n3);
            ret = (double)Math.Sqrt((double)n4);
        }

        public static double RotateTo(Vector3 source, double currentAngle, Vector3 target)
        {
            var deltaX = source.X - target.X;
            var deltaY = source.Z - target.Z;
            var newAngle = (Math.Atan2(deltaY, deltaX) * 180 / Math.PI) - 90;
            var offset = Math.Floor(currentAngle / 360.0) * 360;

            newAngle += offset;

            var difference = currentAngle - newAngle;

            return Math.Abs(difference) > 180.0
                ? newAngle + 360 * Math.Sign(difference)
                : newAngle;
        }

        public static Vector3 MoveTo(Vector3 source, Vector3 target, double distance)
        {
            var deltaX = source.X - target.X;
            var deltaY = source.Z - target.Z;
            var newAngle = (Math.Atan2(deltaY, deltaX) * 180 / Math.PI) - 90;

            return new Vector3
            {
                X = source.X + (double)(distance * (double)Math.Sin(MathHelper.ToRadians((double)newAngle))),
                Y = target.Y,
                Z = source.Z + -(double)(distance * (double)Math.Cos(MathHelper.ToRadians((double)newAngle)))
            };
        }

        public static double DistanceSquared(Vector3 v1, Vector3 v2)
        {
            double n = v1.X - v2.X;
            double n2 = v1.Y - v2.Y;
            double n3 = v1.Z - v2.Z;
            return (((n * n) + (n2 * n2)) + (n3 * n3));
        }

        public static void DistanceSquared(ref Vector3 v1, ref Vector3 v2, out double ret)
        {
            double n = v1.X - v2.X;
            double n2 = v1.Y - v2.Y;
            double n3 = v1.Z - v2.Z;
            ret = ((n * n) + (n2 * n2)) + (n3 * n3);
        }

        public static double Dot(Vector3 v1, Vector3 v2)
        {
            return (((v1.X * v2.X) + (v1.Y * v2.Y)) + (v1.Z * v2.Z));
        }

        public static void Dot(ref Vector3 v1, ref Vector3 v2, out double ret)
        {
            ret = ((v1.X * v2.X) + (v1.Y * v2.Y)) + (v1.Z * v2.Z);
        }

        public void Normalize()
        {
            double n = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
            double n2 = 1f / ((double)Math.Sqrt((double)n));
            this.X *= n2;
            this.Y *= n2;
            this.Z *= n2;
        }

        public static Vector3 Normalize(Vector3 value)
        {
            Vector3 v;
            double n = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
            double n2 = 1f / ((double)Math.Sqrt((double)n));
            v.X = value.X * n2;
            v.Y = value.Y * n2;
            v.Z = value.Z * n2;
            return v;
        }

        public static void Normalize(ref Vector3 value, out Vector3 ret)
        {
            double n = ((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z);
            double n2 = 1f / ((double)Math.Sqrt((double)n));
            ret.X = value.X * n2;
            ret.Y = value.Y * n2;
            ret.Z = value.Z * n2;
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = (v1.Y * v2.Z) - (v1.Z * v2.Y);
            v.Y = (v1.Z * v2.X) - (v1.X * v2.Z);
            v.Z = (v1.X * v2.Y) - (v1.Y * v2.X);
            return v;
        }

        public static void Cross(ref Vector3 v1, ref Vector3 v2, out Vector3 ret)
        {
            double n = (v1.Y * v2.Z) - (v1.Z * v2.Y);
            double n2 = (v1.Z * v2.X) - (v1.X * v2.Z);
            double n3 = (v1.X * v2.Y) - (v1.Y * v2.X);
            ret.X = n;
            ret.Y = n2;
            ret.Z = n3;
        }

        public static Vector3 Reflect(Vector3 v, Vector3 normal)
        {
            Vector3 v2;
            double n = ((v.X * normal.X) + (v.Y * normal.Y)) + (v.Z * normal.Z);
            v2.X = v.X - ((2f * n) * normal.X);
            v2.Y = v.Y - ((2f * n) * normal.Y);
            v2.Z = v.Z - ((2f * n) * normal.Z);
            return v2;
        }

        public static void Reflect(ref Vector3 v, ref Vector3 normal, out Vector3 ret)
        {
            double n = ((v.X * normal.X) + (v.Y * normal.Y)) + (v.Z * normal.Z);
            ret.X = v.X - ((2f * n) * normal.X);
            ret.Y = v.Y - ((2f * n) * normal.Y);
            ret.Z = v.Z - ((2f * n) * normal.Z);
        }

        public static Vector3 Min(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = (v1.X < v2.X) ? v1.X : v2.X;
            v.Y = (v1.Y < v2.Y) ? v1.Y : v2.Y;
            v.Z = (v1.Z < v2.Z) ? v1.Z : v2.Z;
            return v;
        }

        public static void Min(ref Vector3 v1, ref Vector3 v2, out Vector3 ret)
        {
            ret.X = (v1.X < v2.X) ? v1.X : v2.X;
            ret.Y = (v1.Y < v2.Y) ? v1.Y : v2.Y;
            ret.Z = (v1.Z < v2.Z) ? v1.Z : v2.Z;
        }

        public static Vector3 Max(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = (v1.X > v2.X) ? v1.X : v2.X;
            v.Y = (v1.Y > v2.Y) ? v1.Y : v2.Y;
            v.Z = (v1.Z > v2.Z) ? v1.Z : v2.Z;
            return v;
        }

        public static void Max(ref Vector3 v1, ref Vector3 v2, out Vector3 ret)
        {
            ret.X = (v1.X > v2.X) ? v1.X : v2.X;
            ret.Y = (v1.Y > v2.Y) ? v1.Y : v2.Y;
            ret.Z = (v1.Z > v2.Z) ? v1.Z : v2.Z;
        }

        public static Vector3 Clamp(Vector3 v1, Vector3 min, Vector3 max)
        {
            Vector3 v;
            double x = v1.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            double y = v1.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            double z = v1.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            v.X = x;
            v.Y = y;
            v.Z = z;
            return v;
        }

        public static void Clamp(ref Vector3 v1, ref Vector3 min, ref Vector3 max, out Vector3 ret)
        {
            double x = v1.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            double y = v1.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            double z = v1.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            ret.X = x;
            ret.Y = y;
            ret.Z = z;
        }

        public static Vector3 Lerp(Vector3 v1, Vector3 v2, double amount)
        {
            Vector3 v;
            v.X = v1.X + ((v2.X - v1.X) * amount);
            v.Y = v1.Y + ((v2.Y - v1.Y) * amount);
            v.Z = v1.Z + ((v2.Z - v1.Z) * amount);
            return v;
        }

        public static void Lerp(ref Vector3 v1, ref Vector3 v2, double amount, out Vector3 ret)
        {
            ret.X = v1.X + ((v2.X - v1.X) * amount);
            ret.Y = v1.Y + ((v2.Y - v1.Y) * amount);
            ret.Z = v1.Z + ((v2.Z - v1.Z) * amount);
        }

        public static Vector3 Barycentric(Vector3 v1, Vector3 v2, Vector3 value3, double amount1, double amount2)
        {
            Vector3 v;
            v.X = (v1.X + (amount1 * (v2.X - v1.X))) + (amount2 * (value3.X - v1.X));
            v.Y = (v1.Y + (amount1 * (v2.Y - v1.Y))) + (amount2 * (value3.Y - v1.Y));
            v.Z = (v1.Z + (amount1 * (v2.Z - v1.Z))) + (amount2 * (value3.Z - v1.Z));
            return v;
        }

        public static void Barycentric(ref Vector3 v1, ref Vector3 v2, ref Vector3 value3, double amount1,
                                       double amount2, out Vector3 ret)
        {
            ret.X = (v1.X + (amount1 * (v2.X - v1.X))) + (amount2 * (value3.X - v1.X));
            ret.Y = (v1.Y + (amount1 * (v2.Y - v1.Y))) + (amount2 * (value3.Y - v1.Y));
            ret.Z = (v1.Z + (amount1 * (v2.Z - v1.Z))) + (amount2 * (value3.Z - v1.Z));
        }

        public static Vector3 SmoothStep(Vector3 v1, Vector3 v2, double amount)
        {
            Vector3 v;
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            v.X = v1.X + ((v2.X - v1.X) * amount);
            v.Y = v1.Y + ((v2.Y - v1.Y) * amount);
            v.Z = v1.Z + ((v2.Z - v1.Z) * amount);
            return v;
        }

        public static void SmoothStep(ref Vector3 v1, ref Vector3 v2, double amount, out Vector3 ret)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            ret.X = v1.X + ((v2.X - v1.X) * amount);
            ret.Y = v1.Y + ((v2.Y - v1.Y) * amount);
            ret.Z = v1.Z + ((v2.Z - v1.Z) * amount);
        }

        public static Vector3 CatmullRom(Vector3 v1, Vector3 v2, Vector3 value3, Vector3 value4, double amount)
        {
            Vector3 v;
            double n = amount * amount;
            double n2 = amount * n;
            v.X = 0.5f *
                       ((((2f * v2.X) + ((-v1.X + value3.X) * amount)) +
                         (((((2f * v1.X) - (5f * v2.X)) + (4f * value3.X)) - value4.X) * n)) +
                        ((((-v1.X + (3f * v2.X)) - (3f * value3.X)) + value4.X) * n2));
            v.Y = 0.5f *
                       ((((2f * v2.Y) + ((-v1.Y + value3.Y) * amount)) +
                         (((((2f * v1.Y) - (5f * v2.Y)) + (4f * value3.Y)) - value4.Y) * n)) +
                        ((((-v1.Y + (3f * v2.Y)) - (3f * value3.Y)) + value4.Y) * n2));
            v.Z = 0.5f *
                       ((((2f * v2.Z) + ((-v1.Z + value3.Z) * amount)) +
                         (((((2f * v1.Z) - (5f * v2.Z)) + (4f * value3.Z)) - value4.Z) * n)) +
                        ((((-v1.Z + (3f * v2.Z)) - (3f * value3.Z)) + value4.Z) * n2));
            return v;
        }

        public static void CatmullRom(ref Vector3 v1, ref Vector3 v2, ref Vector3 value3, ref Vector3 value4,
                                      double amount, out Vector3 ret)
        {
            double n = amount * amount;
            double n2 = amount * n;
            ret.X = 0.5f *
                       ((((2f * v2.X) + ((-v1.X + value3.X) * amount)) +
                         (((((2f * v1.X) - (5f * v2.X)) + (4f * value3.X)) - value4.X) * n)) +
                        ((((-v1.X + (3f * v2.X)) - (3f * value3.X)) + value4.X) * n2));
            ret.Y = 0.5f *
                       ((((2f * v2.Y) + ((-v1.Y + value3.Y) * amount)) +
                         (((((2f * v1.Y) - (5f * v2.Y)) + (4f * value3.Y)) - value4.Y) * n)) +
                        ((((-v1.Y + (3f * v2.Y)) - (3f * value3.Y)) + value4.Y) * n2));
            ret.Z = 0.5f *
                       ((((2f * v2.Z) + ((-v1.Z + value3.Z) * amount)) +
                         (((((2f * v1.Z) - (5f * v2.Z)) + (4f * value3.Z)) - value4.Z) * n)) +
                        ((((-v1.Z + (3f * v2.Z)) - (3f * value3.Z)) + value4.Z) * n2));
        }

        public static Vector3 Hermite(Vector3 v1, Vector3 tangent1, Vector3 v2, Vector3 tangent2, double amount)
        {
            Vector3 v;
            double n = amount * amount;
            double n2 = amount * n;
            double n3 = ((2f * n2) - (3f * n)) + 1f;
            double n4 = (-2f * n2) + (3f * n);
            double n5 = (n2 - (2f * n)) + amount;
            double n6 = n2 - n;
            v.X = (((v1.X * n3) + (v2.X * n4)) + (tangent1.X * n5)) + (tangent2.X * n6);
            v.Y = (((v1.Y * n3) + (v2.Y * n4)) + (tangent1.Y * n5)) + (tangent2.Y * n6);
            v.Z = (((v1.Z * n3) + (v2.Z * n4)) + (tangent1.Z * n5)) + (tangent2.Z * n6);
            return v;
        }

        public static void Hermite(ref Vector3 v1, ref Vector3 tangent1, ref Vector3 v2, ref Vector3 tangent2,
                                   double amount, out Vector3 ret)
        {
            double n = amount * amount;
            double n2 = amount * n;
            double n3 = ((2f * n2) - (3f * n)) + 1f;
            double n4 = (-2f * n2) + (3f * n);
            double n5 = (n2 - (2f * n)) + amount;
            double n6 = n2 - n;
            ret.X = (((v1.X * n3) + (v2.X * n4)) + (tangent1.X * n5)) + (tangent2.X * n6);
            ret.Y = (((v1.Y * n3) + (v2.Y * n4)) + (tangent1.Y * n5)) + (tangent2.Y * n6);
            ret.Z = (((v1.Z * n3) + (v2.Z * n4)) + (tangent1.Z * n5)) + (tangent2.Z * n6);
        }

        public static Vector3 Transform(Vector3 position, Matrix matrix)
        {
            Vector3 v;
            double n = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
            double n2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
            double n3 = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
            v.X = n;
            v.Y = n2;
            v.Z = n3;
            return v;
        }

        public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector3 ret)
        {
            double n = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
            double n2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
            double n3 = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
            ret.X = n;
            ret.Y = n2;
            ret.Z = n3;
        }

        public static Vector3 TransformNormal(Vector3 normal, Matrix matrix)
        {
            Vector3 v;
            double n = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
            double n2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
            double n3 = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
            v.X = n;
            v.Y = n2;
            v.Z = n3;
            return v;
        }

        public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 ret)
        {
            double n = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
            double n2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
            double n3 = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);
            ret.X = n;
            ret.Y = n2;
            ret.Z = n3;
        }

        public static Vector3 Transform(Vector3 value, Quaternion rotation)
        {
            Vector3 v;
            double n = rotation.X + rotation.X;
            double n2 = rotation.Y + rotation.Y;
            double n3 = rotation.Z + rotation.Z;
            double n4 = rotation.W * n;
            double n5 = rotation.W * n2;
            double n6 = rotation.W * n3;
            double n7 = rotation.X * n;
            double n8 = rotation.X * n2;
            double n9 = rotation.X * n3;
            double n10 = rotation.Y * n2;
            double n11 = rotation.Y * n3;
            double n12 = rotation.Z * n3;
            double n13 = ((value.X * ((1f - n10) - n12)) + (value.Y * (n8 - n6))) + (value.Z * (n9 + n5));
            double n14 = ((value.X * (n8 + n6)) + (value.Y * ((1f - n7) - n12))) + (value.Z * (n11 - n4));
            double n15 = ((value.X * (n9 - n5)) + (value.Y * (n11 + n4))) + (value.Z * ((1f - n7) - n10));
            v.X = n13;
            v.Y = n14;
            v.Z = n15;
            return v;
        }

        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector3 ret)
        {
            double n = rotation.X + rotation.X;
            double n2 = rotation.Y + rotation.Y;
            double n3 = rotation.Z + rotation.Z;
            double n4 = rotation.W * n;
            double n5 = rotation.W * n2;
            double n6 = rotation.W * n3;
            double n7 = rotation.X * n;
            double n8 = rotation.X * n2;
            double n9 = rotation.X * n3;
            double n10 = rotation.Y * n2;
            double n11 = rotation.Y * n3;
            double n12 = rotation.Z * n3;
            double n13 = ((value.X * ((1f - n10) - n12)) + (value.Y * (n8 - n6))) + (value.Z * (n9 + n5));
            double n14 = ((value.X * (n8 + n6)) + (value.Y * ((1f - n7) - n12))) + (value.Z * (n11 - n4));
            double n15 = ((value.X * (n9 - n5)) + (value.Y * (n11 + n4))) + (value.Z * ((1f - n7) - n10));
            ret.X = n13;
            ret.Y = n14;
            ret.Z = n15;
        }

        public static void Transform(Vector3[] source, ref Matrix matrix, Vector3[] destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            if (destination == null)
            {
                throw new ArgumentNullException();
            }
            if (destination.Length < source.Length)
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < source.Length; i++)
            {
                double x = source[i].X;
                double y = source[i].Y;
                double z = source[i].Z;
                destination[i].X = (((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31)) + matrix.M41;
                destination[i].Y = (((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32)) + matrix.M42;
                destination[i].Z = (((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33)) + matrix.M43;
            }
        }

        public static void Transform(Vector3[] source, int sourceIndex, ref Matrix matrix,
                                     Vector3[] destination, int destinationIndex, int length)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            if (destination == null)
            {
                throw new ArgumentNullException();
            }
            if (source.Length < (sourceIndex + length))
            {
                throw new ArgumentException();
            }
            if (destination.Length < (destinationIndex + length))
            {
                throw new ArgumentException();
            }
            while (length > 0)
            {
                double x = source[sourceIndex].X;
                double y = source[sourceIndex].Y;
                double z = source[sourceIndex].Z;
                destination[destinationIndex].X = (((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31)) + matrix.M41;
                destination[destinationIndex].Y = (((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32)) + matrix.M42;
                destination[destinationIndex].Z = (((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33)) + matrix.M43;
                sourceIndex++;
                destinationIndex++;
                length--;
            }
        }

        public static void TransformNormal(Vector3[] source, ref Matrix matrix, Vector3[] destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (destination.Length < source.Length)
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < source.Length; i++)
            {
                double x = source[i].X;
                double y = source[i].Y;
                double z = source[i].Z;
                destination[i].X = ((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31);
                destination[i].Y = ((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32);
                destination[i].Z = ((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33);
            }
        }

        public static void TransformNormal(Vector3[] source, int sourceIndex, ref Matrix matrix,
                                           Vector3[] destination, int destinationIndex, int length)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (source.Length < (sourceIndex + length))
            {
                throw new ArgumentException();
            }
            if (destination.Length < (destinationIndex + length))
            {
                throw new ArgumentException();
            }
            while (length > 0)
            {
                double x = source[sourceIndex].X;
                double y = source[sourceIndex].Y;
                double z = source[sourceIndex].Z;
                destination[destinationIndex].X = ((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31);
                destination[destinationIndex].Y = ((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32);
                destination[destinationIndex].Z = ((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33);
                sourceIndex++;
                destinationIndex++;
                length--;
            }
        }

        public static void Transform(Vector3[] source, ref Quaternion rotation, Vector3[] destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (destination.Length < source.Length)
            {
                throw new ArgumentException();
            }
            double n = rotation.X + rotation.X;
            double n2 = rotation.Y + rotation.Y;
            double n3 = rotation.Z + rotation.Z;
            double n4 = rotation.W * n;
            double n5 = rotation.W * n2;
            double n6 = rotation.W * n3;
            double n7 = rotation.X * n;
            double n8 = rotation.X * n2;
            double n9 = rotation.X * n3;
            double n10 = rotation.Y * n2;
            double n11 = rotation.Y * n3;
            double n12 = rotation.Z * n3;
            double n13 = (1f - n10) - n12;
            double n14 = n8 - n6;
            double n15 = n9 + n5;
            double n16 = n8 + n6;
            double n17 = (1f - n7) - n12;
            double n18 = n11 - n4;
            double n19 = n9 - n5;
            double n20 = n11 + n4;
            double n21 = (1f - n7) - n10;
            for (int i = 0; i < source.Length; i++)
            {
                double x = source[i].X;
                double y = source[i].Y;
                double z = source[i].Z;
                destination[i].X = ((x * n13) + (y * n14)) + (z * n15);
                destination[i].Y = ((x * n16) + (y * n17)) + (z * n18);
                destination[i].Z = ((x * n19) + (y * n20)) + (z * n21);
            }
        }

        public static void Transform(Vector3[] source, int sourceIndex, ref Quaternion rotation,
                                     Vector3[] destination, int destinationIndex, int length)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (source.Length < (sourceIndex + length))
            {
                throw new ArgumentException();
            }
            if (destination.Length < (destinationIndex + length))
            {
                throw new ArgumentException();
            }
            double n = rotation.X + rotation.X;
            double n2 = rotation.Y + rotation.Y;
            double n3 = rotation.Z + rotation.Z;
            double n4 = rotation.W * n;
            double n5 = rotation.W * n2;
            double n6 = rotation.W * n3;
            double n7 = rotation.X * n;
            double n8 = rotation.X * n2;
            double n9 = rotation.X * n3;
            double n10 = rotation.Y * n2;
            double n11 = rotation.Y * n3;
            double n12 = rotation.Z * n3;
            double n13 = (1f - n10) - n12;
            double n14 = n8 - n6;
            double n15 = n9 + n5;
            double n16 = n8 + n6;
            double n17 = (1f - n7) - n12;
            double n18 = n11 - n4;
            double n19 = n9 - n5;
            double n20 = n11 + n4;
            double n21 = (1f - n7) - n10;
            while (length > 0)
            {
                double x = source[sourceIndex].X;
                double y = source[sourceIndex].Y;
                double z = source[sourceIndex].Z;
                destination[destinationIndex].X = ((x * n13) + (y * n14)) + (z * n15);
                destination[destinationIndex].Y = ((x * n16) + (y * n17)) + (z * n18);
                destination[destinationIndex].Z = ((x * n19) + (y * n20)) + (z * n21);
                sourceIndex++;
                destinationIndex++;
                length--;
            }
        }

        public static Vector3 Negate(Vector3 value)
        {
            Vector3 v;
            v.X = -value.X;
            v.Y = -value.Y;
            v.Z = -value.Z;
            return v;
        }

        public static void Negate(ref Vector3 value, out Vector3 ret)
        {
            ret.X = -value.X;
            ret.Y = -value.Y;
            ret.Z = -value.Z;
        }

        public static Vector3 Add(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = v1.X + v2.X;
            v.Y = v1.Y + v2.Y;
            v.Z = v1.Z + v2.Z;
            return v;
        }

        public static void Add(ref Vector3 v1, ref Vector3 v2, out Vector3 ret)
        {
            ret.X = v1.X + v2.X;
            ret.Y = v1.Y + v2.Y;
            ret.Z = v1.Z + v2.Z;
        }

        public static Vector3 Subtract(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = v1.X - v2.X;
            v.Y = v1.Y - v2.Y;
            v.Z = v1.Z - v2.Z;
            return v;
        }

        public static void Subtract(ref Vector3 v1, ref Vector3 v2, out Vector3 ret)
        {
            ret.X = v1.X - v2.X;
            ret.Y = v1.Y - v2.Y;
            ret.Z = v1.Z - v2.Z;
        }

        public static Vector3 Multiply(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = v1.X * v2.X;
            v.Y = v1.Y * v2.Y;
            v.Z = v1.Z * v2.Z;
            return v;
        }

        public static void Multiply(ref Vector3 v1, ref Vector3 v2, out Vector3 ret)
        {
            ret.X = v1.X * v2.X;
            ret.Y = v1.Y * v2.Y;
            ret.Z = v1.Z * v2.Z;
        }

        public static Vector3 Multiply(Vector3 v1, double scaleFactor)
        {
            Vector3 v;
            v.X = v1.X * scaleFactor;
            v.Y = v1.Y * scaleFactor;
            v.Z = v1.Z * scaleFactor;
            return v;
        }

        public static void Multiply(ref Vector3 v1, double scaleFactor, out Vector3 ret)
        {
            ret.X = v1.X * scaleFactor;
            ret.Y = v1.Y * scaleFactor;
            ret.Z = v1.Z * scaleFactor;
        }

        public static Vector3 Divide(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = v1.X / v2.X;
            v.Y = v1.Y / v2.Y;
            v.Z = v1.Z / v2.Z;
            return v;
        }

        public static void Divide(ref Vector3 v1, ref Vector3 v2, out Vector3 ret)
        {
            ret.X = v1.X / v2.X;
            ret.Y = v1.Y / v2.Y;
            ret.Z = v1.Z / v2.Z;
        }

        public static Vector3 Divide(Vector3 v1, double v2)
        {
            Vector3 v;
            double n = 1f / v2;
            v.X = v1.X * n;
            v.Y = v1.Y * n;
            v.Z = v1.Z * n;
            return v;
        }

        public static void Divide(ref Vector3 v1, double v2, out Vector3 ret)
        {
            double n = 1f / v2;
            ret.X = v1.X * n;
            ret.Y = v1.Y * n;
            ret.Z = v1.Z * n;
        }

        public static Vector3 operator -(Vector3 value)
        {
            Vector3 v;
            v.X = -value.X;
            v.Y = -value.Y;
            v.Z = -value.Z;
            return v;
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return (((v1.X == v2.X) && (v1.Y == v2.Y)) && (v1.Z == v2.Z));
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return (((v1.X != v2.X) || (v1.Y != v2.Y)) || !(v1.Z == v2.Z));
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = v1.X + v2.X;
            v.Y = v1.Y + v2.Y;
            v.Z = v1.Z + v2.Z;
            return v;
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = v1.X - v2.X;
            v.Y = v1.Y - v2.Y;
            v.Z = v1.Z - v2.Z;
            return v;
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = v1.X * v2.X;
            v.Y = v1.Y * v2.Y;
            v.Z = v1.Z * v2.Z;
            return v;
        }

        public static Vector3 operator *(Vector3 value, double scaleFactor)
        {
            Vector3 v;
            v.X = value.X * scaleFactor;
            v.Y = value.Y * scaleFactor;
            v.Z = value.Z * scaleFactor;
            return v;
        }

        public static Vector3 operator *(double scaleFactor, Vector3 value)
        {
            Vector3 v;
            v.X = value.X * scaleFactor;
            v.Y = value.Y * scaleFactor;
            v.Z = value.Z * scaleFactor;
            return v;
        }

        public static Vector3 operator /(Vector3 v1, Vector3 v2)
        {
            Vector3 v;
            v.X = v1.X / v2.X;
            v.Y = v1.Y / v2.Y;
            v.Z = v1.Z / v2.Z;
            return v;
        }

        public static Vector3 operator /(Vector3 value, double divider)
        {
            Vector3 v;
            double n = 1f / divider;
            v.X = value.X * n;
            v.Y = value.Y * n;
            v.Z = value.Z * n;
            return v;
        }

    }
}
