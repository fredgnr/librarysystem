using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.domain
{
    public class Position
    {
        private int positionID;
        private int layer;
        private int pindex;
        private int shelfID;
        private int tag;

        public int PositionID { get => positionID; set => positionID = value; }
        public int Layer { get => layer; set => layer = value; }
        public int Pindex { get => pindex; set => pindex = value; }
        public int ShelfID { get => shelfID; set => shelfID = value; }
        public int Tag { get => tag; set => tag = value; }
    }
}
