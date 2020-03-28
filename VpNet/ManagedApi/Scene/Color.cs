using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Color", Namespace = Global.XmlNsScene)]
    public class Color : Abstract.BaseColor
    {
        public Color() { }

        public Color(byte r, byte g, byte b) : base (r,g,b)
        {
        }
    }
}