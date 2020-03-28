using System;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseCell : ICell
    {
        [XmlAttribute]
        public int X { get; set;}
        [XmlAttribute]
        public int Z { get; set; }

        protected BaseCell(int x, int z)
        {
            X = x;
            Z = z;
        }

        protected BaseCell(){}
    }
}
