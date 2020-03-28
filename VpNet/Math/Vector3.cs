using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IEquatable<Vector3>
    {
        [XmlAttribute]
        public double X;
        [XmlAttribute]
        public double Y;
        [XmlAttribute]
        public double Z;

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(double value)
        {
            X = Y = Z = value;
        }

        public bool Equals(Vector3 other) => (X == other.X) && (Y == other.Y) && (Z == other.Z);

        public override bool Equals(object obj) => obj is Vector3 v && Equals(v);

        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
    }
}
