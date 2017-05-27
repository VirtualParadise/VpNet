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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VpNet
{
    public struct TimeSpan : IXmlSerializable, IComparable
    {
        private global::System.TimeSpan _value;

        public static implicit operator TimeSpan(global::System.TimeSpan value)
        {
            return new TimeSpan { _value = value };
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public TimeSpan(int days, int hours, int minutes, int seconds)
        {
            _value = new System.TimeSpan(days, hours, minutes, seconds);
        }

        public static implicit operator global::System.TimeSpan(TimeSpan value)
        {
            return value._value;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _value = global::System.TimeSpan.Parse(reader.ReadContentAsString());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(_value.ToString());
        }

        public int CompareTo(object other)
        {
            return _value.CompareTo(((TimeSpan)other)._value);
        }
    }
}