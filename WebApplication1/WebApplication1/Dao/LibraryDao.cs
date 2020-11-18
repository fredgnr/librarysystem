using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using cSharpfdfjk.domain;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace cSharpfdfjk.Dao
{
    class LibraryDao
    {
        string connStr = string.Format("user=root;pwd=159753456;server=127.0.0.1;database=library;");
        // string connStr = string.Format("user=root;pwd=zz434370;server=127.0.0.1;database=library;");
        public List<Library> findALL()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            List<Library> libraries = new List<Library>();
            string str = "select * from librarys";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Library tmp = new Library();
                tmp.ID = (Int32)mySqlDataReader["LibraryID"];
                tmp.Name = (string)mySqlDataReader["LibraryName"];
                libraries.Add(tmp);
            }
            conn.Close();
            return libraries;
            
        }

        public Library findByID(int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            Library library = null;
            string str = "select *from librarys where LibraryID=?LibraryID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("LibraryID", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                library = new Library();
                library.ID = (Int32)reader["LibraryID"];
                library.Name = (string)reader["LibraryName"];
            }
            return library;
        }
        public Library findByName(string lname)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            Library library = null;
            string str = "select * from librarys where LibraryName like '%" +
                lname +
                "%';";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                library = new Library();
                library.ID = (Int32)mySqlDataReader["LibraryID"];
                library.Name = (string)mySqlDataReader["LibraryName"];
                break;
            }
            conn.Close();
            return library;
        }

        public Library findByfullName(string lname)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            Library library =  null;
            string str = "select * from librarys where LibraryName=?LibraryName";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("LibraryName", lname);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                library = new Library();
                library.ID = (Int32)mySqlDataReader["LibraryID"];
                library.Name = (string)mySqlDataReader["LibraryName"];
                break;
            }
            conn.Close();
            return library;
        }

        public void deleteLibrary(int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "delete  from librarys where LibraryID = " + Convert.ToString(id);
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void updateLibrary(Library library)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "update librarys set LibraryName=?LibraryName where LibraryID =?LibraryID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("LibraryName", library.Name);
            cmd.Parameters.AddWithValue("LibraryID", library.ID);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int createLibrary(Library library)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "insert into librarys( LibraryName) values(?LibraryName)";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("LibraryName", library.Name);
            cmd.ExecuteNonQuery();
            conn.Close();
            return (int)cmd.LastInsertedId;
        }

    }
}
