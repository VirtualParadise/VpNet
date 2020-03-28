using System;
using System.Xml.Serialization;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseColor
    {
        [XmlAttribute]
        public byte R { get; set; }

        [XmlAttribute]
        public byte G { get; set; }

        [XmlAttribute]
        public byte B { get; set; }

        protected BaseColor() { }

        protected BaseColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}