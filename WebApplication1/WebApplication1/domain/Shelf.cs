using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.domain
{
    public class Shelf
    {
        private int shelfID;
        private int capacity;
        private int layers;
        private string category;
        private int libraryID;

        public int ShelfID { get => shelfID; set => shelfID = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public int Layers { get => layers; set => layers = value; }
        public string Category { get => category; set => category = value; }
        public int LibraryID { get => libraryID; set => libraryID = value; }
    }
}
