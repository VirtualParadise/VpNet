using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet
{
    [XmlRoot("CellRangeQuery",Namespace=Global.XmlNsEvent)]
    public class CellRangeQueryCompletedArgs<TVpObject> : EventArgs 
        where TVpObject: class, IVpObject, new()
    {
        [XmlArray("VpObjects")]
        [XmlArrayItem("VpObject")]
        public List<TVpObject> VpObjects { get; set; }
        public CellRangeQueryCompletedArgs(){}

        public CellRangeQueryCompletedArgs(List<TVpObject> vpObjects)
        {
            VpObjects = vpObjects;
        }
    }
}