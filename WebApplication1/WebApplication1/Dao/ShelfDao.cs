using cSharpfdfjk.domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.Dao
{
    class ShelfDao
    { //string connStr = string.Format("user=root;pwd=zz434370;server=127.0.0.1;database=library;");
        string connStr = string.Format("user=root;pwd=159753456;server=127.0.0.1;database=library;");
        public List<Shelf> findAll()
        {
            List<Shelf> shelves=new List<Shelf>();
            string str = "select * from Shelfs";
           
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Shelf tmp = new Shelf();
                tmp.ShelfID = (int)reader["ShelfID"];
                tmp.Capacity = (int)reader["Capacity"];
                tmp.Layers = (int)reader["Layers"];
                tmp.Category = (string)reader["Category"];
                tmp.LibraryID = (int)reader["LibraryID"];
                shelves.Add(tmp);
            }
            return shelves;
        }

        public Shelf findByID(int ID)
        {
            Shelf shelf = null;
           MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "select * from Shelfs where ShelfID=?ShelfID;";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ShelfID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                shelf = new Shelf();
                shelf.ShelfID = (int)reader["ShelfID"];
                shelf.Capacity = (int)reader["Capacity"];
                shelf.Layers = (int)reader["Layers"];
                shelf.Category = (string)reader["Category"];
                shelf.LibraryID = (int)reader["LibraryID"];
                break;
            }
            return shelf;
        }

        public List<Shelf> findByLibraryID(int ID)
        {
            List<Shelf> shelves = new List<Shelf>();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "select * from Shelfs where LibraryID=?LibraryID;";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("LibraryID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Shelf shelf = new Shelf();
                shelf.ShelfID = (int)reader["ShelfID"];
                shelf.Capacity = (int)reader["Capacity"];
                shelf.Layers = (int)reader["Layers"];
                shelf.Category = (string)reader["Category"];
                shelf.LibraryID = (int)reader["LibraryID"];
                shelves.Add(shelf);
            }
            return shelves;
        }

        public int AddShelf(Shelf shelf)
        {
           MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "insert into Shelfs(Capacity,Layers ,Category,LibraryID) values(?Capacity,?Layers ,?Category,?LibraryID)";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("Capacity", shelf.Capacity);
            cmd.Parameters.AddWithValue("Layers", shelf.Layers);
            cmd.Parameters.AddWithValue("Category", shelf.Category);
            cmd.Parameters.AddWithValue("LibraryID", shelf.LibraryID);
            cmd.ExecuteNonQuery();
            int i =(int) cmd.LastInsertedId;
            conn.Close();
            return i;
        }

        public void updateByID(Shelf shelf)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "update Shelfs set " +
                "Capacity=?Capacity,Layers=?Layers,Category=?Category,LibraryID=?LibraryID " +
                "where ShelfID=?ShelfID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("Capacity", shelf.Capacity);
            cmd.Parameters.AddWithValue("Layers", shelf.Layers);
            cmd.Parameters.AddWithValue("Category", shelf.Category);
            cmd.Parameters.AddWithValue("LibraryID", shelf.LibraryID);
            cmd.Parameters.AddWithValue("ShelfID", shelf.ShelfID);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void deleteByID(int id)
        {
             MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "delete from  Shelfs where ShelfID=?ShelfID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ShelfID", id);;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
