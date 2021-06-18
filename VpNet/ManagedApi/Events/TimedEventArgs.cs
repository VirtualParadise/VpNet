using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace VpNet
{
    public abstract class TimedEventArgs : EventArgs
    {
        private DateTime _creationDate = DateTime.UtcNow;

        [XmlAttribute]
        public DateTime CreationDateUtc
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void Initialize() { }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public virtual object Implementor { get; set; }

    }
}