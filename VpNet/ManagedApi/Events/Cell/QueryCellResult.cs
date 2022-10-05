using System.Collections.Generic;

namespace VpNet
{
    public  class QueryCellResult
    {

        public int Revision { get; set; }
        public CellStatus Status { get; set; }

        public List<VpObject> Objects { get; set; }
    }
}
