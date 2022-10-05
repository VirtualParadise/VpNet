using System.Collections.Generic;

namespace VpNet
{
    public class QueryCellResult
    {

        public int Revision { get; set; }
        public CellStatus Status { get; set; }

        public IReadOnlyList<VpObject> Objects { get; set; }
    }
}
