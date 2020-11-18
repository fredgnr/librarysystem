using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.domain
{
    public class Manager
    {
        private string ManagerAccount;
        private string Pwd;
        private string ManagerName;
        public string account
        {
            get
            {
                return ManagerAccount;
            }
            set
            {
                ManagerAccount = value;
            }
        }

        public string Password
        {
            set
            {
                Pwd = value;
            }
            get
            {
                return Pwd;
            }
        }

        public string Name
        {
            set
            {
                ManagerName = value;
            }
            get
            {
                return ManagerName;
            }
        }
    }
}
