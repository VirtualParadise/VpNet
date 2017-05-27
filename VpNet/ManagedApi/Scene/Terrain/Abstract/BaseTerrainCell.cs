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
    Portions herein taken from: Roy Curtis (Cit:182)
    https://github.com/RoyCurtis/VPNet

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
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseTerrainCell : ITerrainCell
    {
        [XmlAttribute]
        public double Height { get; set; }
        [XmlIgnore]
        public ushort Attributes { get; set; }

        [XmlAttribute]
        public bool IsHole
        {
            get { return (Attributes & 0x8000) >> 15 == 1; }
            set { Attributes = (ushort) (Attributes | ((value ? 1 : 0) << 15)); }
        }

        [XmlAttribute]
        public TerrainRotation Rotation
        {
            get { return (TerrainRotation) ((Attributes & 0x6000) >> 13); }
            set { Attributes = (ushort) (Attributes | ((int)value << 13)); }
        }
        [XmlAttribute]
        public ushort Texture
        {
            get { return (ushort) (Attributes & 0x0FFF);}
            set { Attributes = (ushort) (Attributes | (value & 0x1FFF)); }
        }
    }
}
