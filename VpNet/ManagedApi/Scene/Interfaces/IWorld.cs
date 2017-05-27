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

using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// World interface specifications
    /// </summary>
    public interface IWorld
    {
        /// <summary>
        /// Gets or sets the name of the world.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the numbe rof users that are currently in the world.
        /// </summary>
        /// <value>
        /// The user count.
        /// </value>
        [XmlAttribute]
        int UserCount { get; set; }
        /// <summary>
        /// Gets or sets the state of the world.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [XmlAttribute]
        WorldState State { get; set; }
        /// <summary>
        /// Gets or sets the attributes of the world settings.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        Dictionary<string,string> RawAttributes { get; set; }
        /// <summary>
        /// Gets or sets the local cache path, if this is specified, the SDK will allow the option to
        /// cache models, unzip them and read their contents.
        /// </summary>
        /// <value>
        /// The local cache path.
        /// </value>
        [XmlAttribute]
        string LocalCachePath { get; set; }
    }
}