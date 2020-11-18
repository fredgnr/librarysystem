using cSharpfdfjk.domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.Dao
{
    class ManagerDao
    {
        string connStr = string.Format("user=root;pwd=159753456;server=127.0.0.1;database=library;");
        //string connStr = string.Format("user=root;pwd=zz434370;server=127.0.0.1;database=library;");
        public List<Manager> findAll()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            List<Manager> managers = new List<Manager>();
            string str = "select * from Managers";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Manager tmp = new Manager();
                tmp.account = (string)mySqlDataReader["ManagerAccount"];
                tmp.Password = (string)mySqlDataReader["Pwd"];
                tmp.Name = (string)mySqlDataReader["ManagerName"];
                managers.Add(tmp);
            }
            conn.Close();
            return managers;
        }
        public Manager findByName(string name)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            Manager tmp = new Manager();
            string str = "select * from Managers where ManagerName  like '%" +
                name +
                "%';";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                tmp.account = (string)mySqlDataReader["ManagerAccount"];
                tmp.Password = (string)mySqlDataReader["Pwd"];
                tmp.Name = (string)mySqlDataReader["ManagerName"];
            }
            conn.Close();
            return tmp;
        }

        public Manager findByAccount(string account)
        {
             MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            Manager tmp = null;
            string str = "select * from Managers where ManagerAccount=?ManagerAccount";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ManagerAccount", account.Trim());
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                tmp = new Manager();
                tmp.account = (string)mySqlDataReader["ManagerAccount"];
                tmp.Password = (string)mySqlDataReader["Pwd"];
                tmp.Name = (string)mySqlDataReader["ManagerName"];
                break;
            }
            conn.Close();
            return tmp;
        }

        public void updateManager(Manager manager)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "update Managers set " +
                " Pwd=?Pwd,ManagerName=?ManagerName where ManagerAccount = ?ManagerAccount";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("Pwd", manager.Password);
            cmd.Parameters.AddWithValue("ManagerName", manager.Name);
            cmd.Parameters.AddWithValue("ManagerAccount", manager.account);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteManager(string id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "delete  from Managers where ManagerAccount = ?ManagerAccount";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ManagerAccount", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void createManager(Manager manager)
        {
           MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "insert into  Managers(ManagerAccount,Pwd,ManagerName)" +
                " values(?ManagerAccount,?Pwd,?ManagerName)";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ManagerAccount", manager.account);
            cmd.Parameters.AddWithValue("Pwd", manager.Password);
            cmd.Parameters.AddWithValue("ManagerName", manager.Name);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
