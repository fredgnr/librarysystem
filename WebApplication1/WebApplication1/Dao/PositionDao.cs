using cSharpfdfjk.domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.Dao
{
    class PositionDao
    {
         public enum statecode{
            OCCUPIED,
            NOT_OCCUPIED
        }
        //string connStr = string.Format("user=root;pwd=zz434370;server=127.0.0.1;database=library;");
        string connStr = string.Format("user=root;pwd=159753456;server=127.0.0.1;database=library;");
        public List<Position> findAll()
        {
            
            List<Position> positions = new List<Position>();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "select * from Positions";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader reader = cmd.ExecuteReader(); 
            while (reader.Read())
            {
                
                Position position = new Position();
                position.PositionID = (int)reader["PositionID"];
                position.ShelfID = (int)reader["ShelfID"];
                position.Layer = (int)reader["Layer"];
                position.Pindex = (int)reader["Pindex"];
                position.Tag = (int)reader["Tag"];
                positions.Add(position);
             }
            return positions;
        }
        public Position findByID(int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Positions where PositionID= ?PositionID ";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("PositionID", id));
            Position position = null;
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                position = new Position();
                position.PositionID = (int)reader["PositionID"];
                position.ShelfID = (int)reader["ShelfID"];
                position.Layer = (int)reader["Layer"];
                position.Pindex = (int)reader["Pindex"];
                position.Tag = (int)reader["Tag"];
            }
            conn.Close();
            return position;
        }

        public List<Position> findByShelfID(int id)
        {
           MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Positions where ShelfID= ?ShelfID ";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("ShelfID", id));
            List<Position> positions = new List<Position>();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Position position = new Position();
                position.PositionID = (int)reader["PositionID"];
                position.ShelfID = (int)reader["ShelfID"];
                position.Layer = (int)reader["Layer"];
                position.Pindex = (int)reader["Pindex"];
                position.Tag = (int)reader["Tag"];
                positions.Add(position);
            }
            conn.Close();
            return positions;
        }
        public int addPosition(Position position)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "insert into Positions(ShelfID,Layer,Pindex,Tag) " +
                "values(?ShelfID,?Layer,?Pindex,?Tag)";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ShelfID", position.ShelfID);
            cmd.Parameters.AddWithValue("Layer", position.Layer);
            cmd.Parameters.AddWithValue("Pindex", position.Pindex);
            cmd.Parameters.AddWithValue("Tag", position.Tag);
            cmd.ExecuteNonQuery();
            int i = (int)cmd.LastInsertedId;
            conn.Close();
            return i;
        }

        public void updatePosition(Position position)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "update Positions set  " +
                "ShelfID=?ShelfID,Layer=?Layer,Pindex=?Pindex,Tag=?Tag" +
                " where PositionID=?PositionID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ShelfID", position.ShelfID);
            cmd.Parameters.AddWithValue("Layer",position.Layer);
            cmd.Parameters.AddWithValue("Pindex", position.Pindex);
            cmd.Parameters.AddWithValue("Tag", position.Tag);
            cmd.Parameters.AddWithValue("PositionID", position.PositionID);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteByID(int id)
        {
             MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "delete from Positions where PositionID=?PositionID;";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("PositionID", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
