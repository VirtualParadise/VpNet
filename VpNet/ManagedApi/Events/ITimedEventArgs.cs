using System;
using System.Xml.Serialization;

namespace VpNet
{
    public interface ITimedEventArgs
    {
        [XmlAttribute]
        DateTime CreationDateUtc { get; set; }
        [XmlIgnore]
        object Implementor { get;  set; }

        void Initialize();
    }
}