using cSharpfdfjk.domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.Dao
{
    public class UserDao
    {  //string connStr = string.Format("user=root;pwd=zz434370;server=127.0.0.1;database=library;");
        string connStr = string.Format("user=root;pwd=159753456;server=127.0.0.1;database=library;");
        public List<User> findAll()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            List<User> users = new List<User>();
            string str = "select * from Users";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                User tmp = new User();
                tmp.account = (string)mySqlDataReader["UserAccount"];
                tmp.Password = (string)mySqlDataReader["Pwd"];
                tmp.Name = (string)mySqlDataReader["UserName"];
                 users.Add(tmp);
            }
            conn.Close();
            return users;
        }

        public User findByAccount(string account)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            User tmp = new User();
            string str = "select * from Users where UserAccount=?UserAccount;";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("UserAccount", account.Trim());
            User user = null;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                user = new User();
                user.account = (string)reader["UserAccount"];
                user.Password = (string)reader["Pwd"];
                user.Name = (string)reader["UserName"];
            }
            return user;
        }
        public User findByName(string name)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            User tmp = new User();
            string str = "select * from Users where UserName  like '%" +
                name +
                "%';";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                tmp.account = (string)mySqlDataReader["UserAccount"];
                tmp.Password = (string)mySqlDataReader["Pwd"];
                tmp.Name = (string)mySqlDataReader["UserName"];
            }
            conn.Close();
            return tmp;
        }

        public void updateUser(User user)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "update Users set " +
                " Pwd=?Pwd, " +
                " UserName=?UserName " +
                " where UserAccount =?UserAccount ";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("Pwd", user.Password);
            cmd.Parameters.AddWithValue("UserName", user.Name);
            cmd.Parameters.AddWithValue("UserAccount", user.account);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteUser(string account)
        {
             MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "delete  from Users where UserAccount = " + account;
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void createUser(User user)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "insert into Users( UserAccount,Pwd,UserName)" +
                " values(?UserAccount,?Pwd,?UserName);";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("UserAccount", user.account);
            cmd.Parameters.AddWithValue("Pwd", user.Password);
            cmd.Parameters.AddWithValue("UserName", user.Name);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
