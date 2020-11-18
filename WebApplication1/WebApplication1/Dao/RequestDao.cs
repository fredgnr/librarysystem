using cSharpfdfjk.domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1;

namespace cSharpfdfjk.Dao
{
    class RequestDao
    {
        public enum statecode
        {
            FINISHED=1,UNFINISHED
        }
        // string connStr = string.Format("user=root;pwd=zz434370;server=127.0.0.1;database=library;");
        string connStr = string.Format("user=root;pwd=159753456;server=127.0.0.1;database=library;");
        public List<Request> findAll()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();  List<Request> requests = new List<Request>();
            string str = "select * from Requests;";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Request request = new Request();
                request.RequestID = (int)reader["RequestID"];
                request.BookID = (int)reader["BookID"];
                request.StartTime = (DateTime)reader["StartTime"];
                request.EndTime = (DateTime)reader["EndTime"];
                request.Tag = (int)reader["Tag"];
                request.UserAccount = (string)reader["UserAccount"];
                requests.Add(request);
            }
            conn.Close();
            return requests;
        }

        public Request findByID(int ID)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            Request request = new Request();
            string str = "select * FROM Requests where RequestID=?RequestID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("RequestID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                request.RequestID = (int)reader["RequestID"];
                request.BookID = (int)reader["BookID"];
                request.StartTime = (DateTime)reader["StartTime"];
                request.EndTime = (DateTime)reader["EndTime"];
                request.Tag = (int)reader["Tag"];
                request.UserAccount = (string)reader["UserAccount"];

            }
            conn.Close();
            return request;
        }

        public  List<Request>  findByBookID(int ID)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            List<Request> lists = new List<Request>();
            string str = "select * FROM Requests where BookID=?BookID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("BookID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            { 
                Request request = new Request();
                request.RequestID = (int)reader["RequestID"];
                request.BookID = (int)reader["BookID"];
                request.StartTime = (DateTime)reader["StartTime"];
                request.EndTime = (DateTime)reader["EndTime"];
                request.Tag = (int)reader["Tag"];
                request.UserAccount = (string)reader["UserAccount"];
                lists.Add(request);

            }
            conn.Close();
            return lists;
        }

        public List<Request> findByTag(int tag)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open(); List<Request> requests = new List<Request>();
            string str = "select * from Reuqests where Tag=?Tag";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("Tag", tag);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Request request = new Request();
                request.RequestID = (int)reader["RequestID"];
                request.BookID = (int)reader["BookID"];
                request.StartTime = (DateTime)reader["StartTime"];
                request.EndTime = (DateTime)reader["EndTime"];
                request.Tag = (int)reader["Tag"];
                request.UserAccount = (string)reader["UserAccount"];
                requests.Add(request);
            }
            conn.Close();
            return requests;
        }

        public void updaterequest(Request request)
        {
           MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "update Requests set " +
                "BookID=?BookID,StartTime=?StartTime,EndTime=?EndTime,Tag=?Tag,UserAccount=?UserAccount " +
                "where RequestID=?RequestID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("BookID", request.BookID);
            cmd.Parameters.AddWithValue("StartTime", request.StartTime);
            cmd.Parameters.AddWithValue("EndTime", request.EndTime);
            cmd.Parameters.AddWithValue("Tag", request.Tag);
            cmd.Parameters.AddWithValue("RequestID", request.RequestID);
            cmd.Parameters.AddWithValue("UserAccount", request.UserAccount);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void deleterequest(int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "delete from Requests where RequestID=?RequestID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("RequestID",id);
            cmd.ExecuteNonQuery();
        }

        public int  createrequest(Request request)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "insert into Requests(BookID,StartTime,EndTime,Tag,UserAccount) " +
                "values(?BookID,?StartTime,?EndTime,?Tag,?UserAccount)";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("BookID", request.BookID);
            cmd.Parameters.AddWithValue("StartTime", request.StartTime);
            cmd.Parameters.AddWithValue("EndTime", request.EndTime);
            cmd.Parameters.AddWithValue("Tag", request.Tag);
            cmd.Parameters.AddWithValue("UserAccount", request.UserAccount);
            cmd.ExecuteNonQuery();
            int i = (int)cmd.LastInsertedId;
            conn.Close();
            return i;
        }

        public List<Request> searchByTime(DateTime start,DateTime end)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "select * from  Requests where " +
                "(STR_TO_DATE(?start,'%Y-%m-%d')<=StartTime and STR_TO_DATE(?end,'%Y-%m-%d')>=StartTime) " +
                "or " +
                "(STR_TO_DATE(?start,'%Y-%m-%d')<=EndTime and STR_TO_DATE(?end,'%Y-%m-%d')>=EndTime)" +
                "or" +
                "(STR_TO_DATE(?start,'%Y-%m-%d')>=StartTime and STR_TO_DATE(?end,'%Y-%m-%d')<=EndTime)";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("start", start);
            cmd.Parameters.AddWithValue("end", end);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Request> requests = new List<Request>();
            while (reader.Read())
            {
                Request request = new Request();
                request.RequestID = (int)reader["RequestID"];
                request.BookID = (int)reader["BookID"];
                request.StartTime = (DateTime)reader["StartTime"];
                request.EndTime = (DateTime)reader["EndTime"];
                request.Tag = (int)reader["Tag"];
                request.UserAccount = (string)reader["UserAccount"];

                requests.Add(request);
            }
            conn.Close();
            return requests;

        }
        public List<Request> searchByTimeWithid(DateTime start, DateTime end,int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "select * from  Requests where " +
                "(" +
                "(STR_TO_DATE(?start,'%Y-%m-%d')<=StartTime and STR_TO_DATE(?end,'%Y-%m-%d')>=StartTime) " +
                "or " +
                "(STR_TO_DATE(?start,'%Y-%m-%d')<=EndTime and STR_TO_DATE(?end,'%Y-%m-%d')>=EndTime)" +
                "or" +
                "(STR_TO_DATE(?start,'%Y-%m-%d')>=StartTime and STR_TO_DATE(?end,'%Y-%m-%d')<=EndTime)"+
                ")" +
                " and BookID=?BookID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("start", start.ToShortDateString().Replace('/', '-'));
            cmd.Parameters.AddWithValue("end", end.ToShortDateString().Replace('/', '-'));
            cmd.Parameters.AddWithValue("BookID", id);
            string text = cmd.CommandText;
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Request> requests = new List<Request>();
            while (reader.Read())
            {
                Request request = new Request();
                request.RequestID = (int)reader["RequestID"];
                request.BookID = (int)reader["BookID"];
                request.StartTime = (DateTime)reader["StartTime"];
                request.EndTime = (DateTime)reader["EndTime"];
                request.Tag = (int)reader["Tag"];
                request.UserAccount = (string)reader["UserAccount"];
                requests.Add(request);
            }
            conn.Close();
            return requests;

        }

        public List<Request> byuser(string account)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open(); List<Request> requests = new List<Request>();
            string str = "select * from Requests where UserAccount=?UserAccount;";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("UserAccount", account);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Request request = new Request();
                request.RequestID = (int)reader["RequestID"];
                request.BookID = (int)reader["BookID"];
                request.StartTime = (DateTime)reader["StartTime"];
                request.EndTime = (DateTime)reader["EndTime"];
                request.Tag = (int)reader["Tag"];
                request.UserAccount = (string)reader["UserAccount"];
                requests.Add(request);
            }
            conn.Close();
            return requests;
        }

        public List<Request> byuserundo(string account)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open(); List<Request> requests = new List<Request>();
            string str = "select * from Requests where UserAccount=?UserAccount and Tag=2;";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("UserAccount", account);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Request request = new Request();
                request.RequestID = (int)reader["RequestID"];
                request.BookID = (int)reader["BookID"];
                request.StartTime = (DateTime)reader["StartTime"];
                request.EndTime = (DateTime)reader["EndTime"];
                request.Tag = (int)reader["Tag"];
                request.UserAccount = (string)reader["UserAccount"];
                requests.Add(request);
            }
            conn.Close();
            return requests;
        }

    }
}
