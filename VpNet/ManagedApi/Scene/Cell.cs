using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Cell", Namespace = Global.XmlNsScene)]
    public class Cell : Abstract.BaseCell
    {
        public Cell(int x, int z) : base(x,z){}

        public Cell(){}
    }
}
