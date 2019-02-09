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
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
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
