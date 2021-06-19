using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnTerrainNode" />.
    /// </summary>
    [XmlRoot("OnTerrainNode", Namespace = Global.XmlNsEvent)]
    public class TerrainNodeEventArgs : BaseTerrainNodeEventArgs
    {
    }
}
