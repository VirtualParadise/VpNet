using System;
using System.Xml.Serialization;
using VpNet.Abstract;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Instance", Namespace = Global.XmlNsInstance)]
    public class Instance<TAvatar> : BaseInstanceT<Instance<TAvatar>,
        TAvatar,
        Friend,
        TerrainCell,
        TerrainNode,
        TerrainTile,
        VpObject,
        World,
        Cell,
        ChatMessage,
        Terrain,
        Universe,
        Teleport<World, TAvatar>,
        UserAttributes
        >
        where TAvatar : class, IAvatar, new()
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
