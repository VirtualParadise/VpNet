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
    /// Chat Message templated interface specifications.
    /// </summary>
    /// <typeparam name="TColor">The type of the color.</typeparam>
    public interface IChatMessage<TColor>
        where TColor : IColor
    {
        [XmlAttribute]
        ChatMessageTypes Type { get; set; }

        TColor Color { get; set; }

        [XmlAttribute]
        TextEffectTypes TextEffectTypes { get; set; }

        [XmlAttribute]
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the name for a console message. Note that this name is not always the same as the bot sending the console message.
        /// Bots can advocate sending messages under different names.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        string Name { get; set; }
    }
}