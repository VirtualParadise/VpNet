using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    [XmlRoot("VpObject", Namespace = Global.XmlNsScene)]
    public class VpObject : Abstract.BaseVpObject
    {
        internal VpObject(int id, int objectType, DateTime time, int owner, Vector3 position, Vector3 rotation, double angle, string action, string description, string model, byte[] data)
            : base(id, objectType, time, owner, position, rotation, angle, action, description, model, data)
        {
        }

        public VpObject()
        {
            
        }
    }
}
