using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.domain
{
    public class Request
    {
        private int requestID;
        private int bookID;
        private DateTime startTime;
        private DateTime endTime;
        private string userAccount;
        private int tag;

        public int RequestID { get => requestID; set => requestID = value; }
        public int BookID { get => bookID; set => bookID = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime EndTime { get => endTime; set => endTime = value; }
        public int Tag { get => tag; set => tag = value; }
        public string UserAccount { get => userAccount; set => userAccount = value; }
    }
}
