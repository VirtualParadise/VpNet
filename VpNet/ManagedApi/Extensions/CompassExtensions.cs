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

using VpNet.Interfaces;

namespace VpNet.Extensions
{
    public static class CompassExtensions
    {
        public static string ToCompassLongString<TAvatar>(this TAvatar avatar)
            where TAvatar : IAvatar
        {
            var direction = (avatar.Rotation.Y % 360 + 360) % 360;
            if (direction <= 22.5f) return "South";
            if (direction <= 67.5f) return "South-West";
            if (direction <= 112.5f) return "West";
            if (direction <= 157.5f) return "North-West";
            if (direction <= 202.5f) return "North";
            if (direction <= 247.5f) return "North-East";
            if (direction <= 292.5f) return "East";
            return direction <= 337.5 ? "South-East" : "South";
        }

        public static string ToCompassString<TAvatar>(this TAvatar avatar)
            where TAvatar : IAvatar
        {
            var direction = (avatar.Rotation.Y % 360 + 360) % 360;
            if (direction <= 22.5f) return "S";
            if (direction <= 67.5f) return "SE";
            if (direction <= 112.5f) return "W";
            if (direction <= 157.5f) return "NW";
            if (direction <= 202.5f) return "N";
            if (direction <= 247.5f) return "NE";
            if (direction <= 292.5f) return "E";
            return direction <= 337.5 ? "SE" : "S";
        }

        public static CompassDirectionType ToCompassType<TAvatar>(this TAvatar avatar)
             where TAvatar : IAvatar
        {
            var direction = (avatar.Rotation.Y % 360 + 360) % 360;
            if (direction <= 22.5f) return CompassDirectionType.S;
            if (direction <= 67.5f) return CompassDirectionType.SW;
            if (direction <= 112.5f) return CompassDirectionType.W;
            if (direction <= 157.5f) return CompassDirectionType.NW;
            if (direction <= 202.5f) return CompassDirectionType.N;
            if (direction <= 247.5f) return CompassDirectionType.NE;
            if (direction <= 292.5f) return CompassDirectionType.E;
            return direction <= 337.5 ? CompassDirectionType.SE : CompassDirectionType.S;
        }
    }
}
