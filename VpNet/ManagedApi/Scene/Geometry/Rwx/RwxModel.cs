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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VpNet.Geometry.Rwx
{
    [Serializable]
    [XmlRoot("rwxMmodel", Namespace = Global.XmlNsGeometryRwx)]
    public class RwxModel
    {
        public List<RwxClump> Models;
        public Dictionary<string, RwxClump> Prototypes;
        [XmlAttribute]
        public bool IsDouble = false; // global double material

        // default colors for debugging (each material gets one distinct color):
        // white, red, green, blue, yellow, cyan, magenta
        public int[] DbgColors = new int[] { 0xeeeeee, 0xee0000, 0x00ee00, 0x0000ee, 0xeeee00, 0x00eeee, 0xee00ee };
        private int _i = -1;
        [XmlAttribute]
        public string SourceFile;
        public int GetNextDbgColor()
        {
            _i++;
            if (_i > DbgColors.Length - 1)
                _i = 0;
            return DbgColors[_i];
        }

        public RwxModel()
        {
            Models = new List<RwxClump>();
            Prototypes = new Dictionary<string, RwxClump>();
        }
    }
}