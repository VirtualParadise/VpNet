using System;
using System.Collections.Generic;

namespace VpNet
{
    public class CellRangeQueryCompletedArgs : EventArgs 
    {
        public List<VpObject> VpObjects { get; set; }
        public CellRangeQueryCompletedArgs(){}

        public CellRangeQueryCompletedArgs(List<VpObject> vpObjects)
        {
            VpObjects = vpObjects;
        }
    }
}