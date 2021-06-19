using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    ///     Provides event arguments for <see cref="Instance.OnObjectCellRangeChange" />.
    /// </summary>
    [XmlRoot("CellRangeQuery",Namespace=Global.XmlNsEvent)]
    public sealed class CellRangeQueryCompletedArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CellRangeQueryCompletedArgs" /> class.
        /// </summary>
        /// <param name="objects">The objects.</param>
        public CellRangeQueryCompletedArgs(IEnumerable<VpObject> objects)
        {
            VpObjects = objects.ToList().AsReadOnly();
        }

        /// <summary>
        ///     Gets a read-only view of the objects.
        /// </summary>
        /// <value>A read-only view of the objects.</value>
        public IReadOnlyList<VpObject> VpObjects { get; }
    }
}