using System;
using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Instance", Namespace = Global.XmlNsInstance)]
    public class Instance : BaseInstanceT<Instance,
        Avatar, 
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
        Teleport<World,Avatar>,
        UserAttributes
        >
      
           
    {
        public Instance()
        {
            Implementor = this;
            Avatars();
        }

        public Instance(BaseInstanceEvents<World> parentInstance)
            : base(parentInstance)
        {
            Implementor = this;
        }

        public Instance(InstanceConfiguration<World> instanceConfiguration): base(instanceConfiguration)
        {
            Implementor = this;
        }
    }
}
