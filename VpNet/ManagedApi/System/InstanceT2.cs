using System;
using System.Xml.Serialization;
using VpNet.Abstract;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Instance", Namespace = Global.XmlNsInstance)]
    public class Instance<TAvatar, TVpObject> : BaseInstanceT<Instance<TAvatar, TVpObject>,
        TAvatar,
        Friend,
        TerrainCell,
        TerrainNode,
        TerrainTile,
        TVpObject,
        World,
        Cell,
        ChatMessage,
        Terrain,
        Universe,
        Teleport<World, TAvatar>,
        UserAttributes
        >
        where TAvatar : class, IAvatar, new()
        where TVpObject : class, IVpObject, new()
    {
        public Instance()
        {
            Implementor = this;
        }

        public Instance(BaseInstanceEvents<World> parentInstance)
            : base(parentInstance)
        {
            Implementor = this;
        }

        public Instance(InstanceConfiguration<World> instanceConfiguration)
            : base(instanceConfiguration)
        {
            Implementor = this;
        }
    }
}
