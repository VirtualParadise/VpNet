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
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Obsolete("Currently unsupported")]
    public abstract class BaseWorldAttributes : IWorldAttributes
    {
        /// <summary>
        /// Gets or sets the object path. db key: objectpath
        /// </summary>
        /// <value>
        /// The object path.
        /// </value>
        /// <Author>8/5/2012 6:27 PM cube3</Author>
        public Uri ObjectPath { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to enable terrain. db key: terrain
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable terrain]; otherwise, <c>false</c>.
        /// </value>
        /// <Author>8/5/2012 6:28 PM cube3</Author>
        public bool EnableTerrain { get; set; }
        /// <summary>
        /// Gets or sets the terrain scale. db key: terrainkey
        /// </summary>
        /// <value>
        /// The terrain scale.
        /// </value>
        /// <Author>8/5/2012 6:28 PM cube3</Author>
        public double TerrainScale { get; set; }
        /// <summary>
        /// Gets or sets the ground model. db key: gound
        /// </summary>
        /// <value>
        /// The ground model.
        /// </value>
        /// <Author>8/5/2012 6:28 PM cube3</Author>
        public string GroundModel { get; set; }
        /// <summary>
        /// Gets or sets the skybox. The skybox is the name of the skybox. The world server will automatically look for jpg images and append the following:
        /// _
        /// </summary>
        /// <value>
        /// The skybox.
        /// </value>
        /// <Author>8/5/2012 6:30 PM cube3</Author>
        public string Skybox { get; set; }
        public bool SkyboxSwapLr { get; set; }
        public IColor WorldLightAmbient { get; set; }

//        objectpath, terrain, terrainscale, ground, skybox, skybox_swaplr, worldlight_ambient, worldlight_diffuse, worldlight_specular, worldlight_position, w
//elcome, fov, nearplane, farplane
    }
}
