using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    /// <summary>
    /// Default Event Arguments implementation mapping. You can define your own mappings when implementing VpNet.Abstract.BaseInstanceT
    /// </summary>
    [XmlRoot("OnTerrainNode", Namespace = Global.XmlNsEvent)]
    public class TerrainNodeEventArgs : BaseTerrainNodeEventArgs<Terrain>
    {
    }
}
