using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.domain
{
    public class Library
    {
        private int LibraryID;
        private string LibraryName;
        public int ID{
            set
            {
                LibraryID = value;
            }
            get
            {
                return LibraryID;
            }
        }
        public string Name
        {
            set
            {
                LibraryName = value;
            }
            get
            {
                return LibraryName;
            }
        }
    }
}
