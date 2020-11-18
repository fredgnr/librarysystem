using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.domain
{
    public class User
    {
        private string UserAcccount;
        private string Pwd;
        private string UserName;
        public string account
        {
            get
            {
                return UserAcccount;
            }
            set
            {
                UserAcccount = value;
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
                UserName = value;
            }
            get
            {
                return UserName;
            }
        }

    }
}
