using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.domain
{
    public class Book
    {
        private int bookID;
        private string iSBN;
        private string bookName;
        private string author;
        private string category;
        private int pstate;
        private int btype;
        private int libraryID;
        private Int32 positionID ;

        public int BookID { get => bookID; set => bookID = value; }
        public string ISBN { get => iSBN; set => iSBN = value; }
        public string BookName { get => bookName; set => bookName = value; }
        public string Author { get => author; set => author = value; }
        public string Category { get => category; set => category = value; }
        public int Pstate { get => pstate; set => pstate = value; }
        public int Btype { get => btype; set => btype = value; }
        public int LibraryID { get => libraryID; set => libraryID = value; }
        public int PositionID { get => positionID; set => positionID = value; }
    }
}
