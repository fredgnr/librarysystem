using cSharpfdfjk.domain;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpfdfjk.Dao
{
    class BookDao
    {
        string connStr = string.Format("user=root;pwd=159753456;server=127.0.0.1;database=library;");
        //string connStr = string.Format("user=root;pwd=zz434370;server=127.0.0.1;database=library;");
        public enum statecode
        {
            ONSHELF_ONLY,NOT_ONSHELF_ONLY,
            IN_BACKUP,IN_LIBRARY,OUT_LIBRARY
        }
        public List<Book> findAll()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            List<Book> books = new List<Book>();
            string str = "select * from Books";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Book tmp = new Book();
                tmp.BookID = (int)mySqlDataReader["BookID"];
                tmp.ISBN = (string)mySqlDataReader["ISBN"];
                tmp.BookName = (string)mySqlDataReader["BookName"];
                tmp.Author = (string)mySqlDataReader["Author"];
                tmp.Category = (string)mySqlDataReader["Category"];
                tmp.Pstate = (int)mySqlDataReader["Pstate"];
                tmp.Btype = (int)mySqlDataReader["Btype"];
                tmp.LibraryID = (int)mySqlDataReader["LibraryID"]; 
                tmp.PositionID = (int)(mySqlDataReader["PositionID"]!=DBNull.Value? mySqlDataReader["PositionID"]:0);
                books.Add(tmp);
            }
            conn.Close();
            return books;
        }

        public Book findByID(int BookID)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Books where BookID= ?BookID ";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("BookID", BookID));

            Book tmp = null;
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                tmp = new Book();
                tmp.BookID = (int)mySqlDataReader["BookID"];
                tmp.ISBN = (string)mySqlDataReader["ISBN"];
                tmp.BookName = (string)mySqlDataReader["BookName"];
                tmp.Author = (string)mySqlDataReader["Author"];
                tmp.Category = (string)mySqlDataReader["Category"];
                tmp.Pstate = (int)mySqlDataReader["Pstate"];
                tmp.Btype = (int)mySqlDataReader["Btype"];
                tmp.LibraryID = (int)mySqlDataReader["LibraryID"];
                tmp.PositionID = (int)(mySqlDataReader["PositionID"] != DBNull.Value ? mySqlDataReader["PositionID"] : 0);

                break;
            }
            conn.Close();
            return tmp;
        }

        public List<Book> findByName(string name)
        {
             MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Books where BookName like CONCAT('%',?BookName,'%')";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("BookName", name));
            List<Book> books = new List<Book>();
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Book tmp = new Book();
                tmp.BookID = (int)mySqlDataReader["BookID"];
                tmp.ISBN = (string)mySqlDataReader["ISBN"];
                tmp.BookName = (string)mySqlDataReader["BookName"];
                tmp.Author = (string)mySqlDataReader["Author"];
                tmp.Category = (string)mySqlDataReader["Category"];
                tmp.Pstate = (int)mySqlDataReader["Pstate"];
                tmp.Btype = (int)mySqlDataReader["Btype"];
                tmp.LibraryID = (int)mySqlDataReader["LibraryID"];
                tmp.PositionID = (int)(mySqlDataReader["PositionID"] != DBNull.Value ? mySqlDataReader["PositionID"] : 0);

                books.Add(tmp);
            }
            conn.Close();
            return books;
        }

        public List<Book> findByISBN(string ISBN)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Books where ISBN =?ISBN ";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("ISBN", ISBN));
            List<Book> books = new List<Book>();
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Book tmp = new Book();
                tmp.BookID = (int)mySqlDataReader["BookID"];
                tmp.ISBN = (string)mySqlDataReader["ISBN"];
                tmp.BookName = (string)mySqlDataReader["BookName"];
                tmp.Author = (string)mySqlDataReader["Author"];
                tmp.Category = (string)mySqlDataReader["Category"];
                tmp.Pstate = (int)mySqlDataReader["Pstate"];
                tmp.Btype = (int)mySqlDataReader["Btype"];
                tmp.LibraryID = (int)mySqlDataReader["LibraryID"];
                tmp.PositionID = (int)(mySqlDataReader["PositionID"] != DBNull.Value ? mySqlDataReader["PositionID"] : 0);

                books.Add(tmp);
            }
            conn.Close();
            return books;
        }
        public List<Book> findByBookName(string bookname)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Books where BookName =?BookName ";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("BookName", bookname));
            List<Book> books = new List<Book>();
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Book tmp = new Book();
                tmp.BookID = (int)mySqlDataReader["BookID"];
                tmp.ISBN = (string)mySqlDataReader["ISBN"];
                tmp.BookName = (string)mySqlDataReader["BookName"];
                tmp.Author = (string)mySqlDataReader["Author"];
                tmp.Category = (string)mySqlDataReader["Category"];
                tmp.Pstate = (int)mySqlDataReader["Pstate"];
                tmp.Btype = (int)mySqlDataReader["Btype"];
                tmp.LibraryID = (int)mySqlDataReader["LibraryID"];
                tmp.PositionID = (int)(mySqlDataReader["PositionID"] != DBNull.Value ? mySqlDataReader["PositionID"] : 0);

                books.Add(tmp);
            }
            conn.Close();
            return books;
        }
        public List<Book> findByAuthor(string Author)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Books where Author  like '%?Author%' ";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("Author ", Author));
            List<Book> books = new List<Book>();
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Book tmp = new Book();
                tmp.BookID = (int)mySqlDataReader["BookID"];
                tmp.ISBN = (string)mySqlDataReader["ISBN"];
                tmp.BookName = (string)mySqlDataReader["BookName"];
                tmp.Author = (string)mySqlDataReader["Author"];
                tmp.Category = (string)mySqlDataReader["Category"];
                tmp.Pstate = (int)mySqlDataReader["Pstate"];
                tmp.Btype = (int)mySqlDataReader["Btype"];
                tmp.LibraryID = (int)mySqlDataReader["LibraryID"];
                tmp.PositionID = (int)(mySqlDataReader["PositionID"] != DBNull.Value ? mySqlDataReader["PositionID"] : 0);

                books.Add(tmp);
            }
            conn.Close();
            return books;
        }

        public List<Book> findByCategory(string Category)
        {
           MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Books where Category =?Category ";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("Category", Category));
            List<Book> books = new List<Book>();
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Book tmp = new Book();
                tmp.BookID = (int)mySqlDataReader["BookID"];
                tmp.ISBN = (string)mySqlDataReader["ISBN"];
                tmp.BookName = (string)mySqlDataReader["BookName"];
                tmp.Author = (string)mySqlDataReader["Author"];
                tmp.Category = (string)mySqlDataReader["Category"];
                tmp.Pstate = (int)mySqlDataReader["Pstate"];
                tmp.Btype = (int)mySqlDataReader["Btype"];
                tmp.LibraryID = (int)mySqlDataReader["LibraryID"];
                tmp.PositionID = (int)(mySqlDataReader["PositionID"] != DBNull.Value ? mySqlDataReader["PositionID"] : 0);

                books.Add(tmp);
            }
            conn.Close();
            return books;
        }

        public void updateByID(Book book)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "update Books set  " +
                "ISBN=?ISBN,BookName=?BookName,Author=?Author," +
                "Category=?Category,Pstate=?Pstate,Btype=?Btype," +
                "LibraryID=?LibraryID,PositionID=?PositionID " +
                "where BookID=?BookID";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ISBN",book.ISBN);
            cmd.Parameters.AddWithValue("BookName", book.BookName);
            cmd.Parameters.AddWithValue("Author", book.Author);
            cmd.Parameters.AddWithValue("Category", book.Category);
            cmd.Parameters.AddWithValue("Pstate", book.Pstate);
            cmd.Parameters.AddWithValue("Btype", book.Btype);
            cmd.Parameters.AddWithValue("LibraryID", book.LibraryID);
            cmd.Parameters.AddWithValue("PositionID", book.PositionID!=0?(object)book.PositionID:DBNull.Value);
            cmd.Parameters.AddWithValue("BookID", book.BookID);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int addbook(Book book)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "insert into " +
                "Books(ISBN,BookName,Author,Category,Pstate,Btype,LibraryID,PositionID)" +
                "values(?ISBN,?BookName,?Author,?Category,?Pstate,?Btype,?LibraryID,?PositionID)";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("BookName", book.BookName);
            cmd.Parameters.AddWithValue("Author", book.Author);
            cmd.Parameters.AddWithValue("Category", book.Category);
            cmd.Parameters.AddWithValue("Pstate", book.Pstate);
            cmd.Parameters.AddWithValue("Btype", book.Btype);
            cmd.Parameters.AddWithValue("LibraryID", book.LibraryID);
            cmd.Parameters.AddWithValue("PositionID", book.PositionID != 0 ? (object)book.PositionID : DBNull.Value);

            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;
            conn.Close();
            return id;
        }

        public void deleteByID(int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "delete from Books where BookID=?BookID;";
            MySqlCommand cmd = new MySqlCommand(str, conn);
            cmd.Parameters.AddWithValue("BookID", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public Book findByPos(int Pos)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string str = "Select * from Books where PositionID= ?PositionID ";
            MySqlCommand mySqlCommand = new MySqlCommand(str, conn);
            mySqlCommand.Parameters.Add(new MySqlParameter("PositionID", Pos));

            Book tmp = null;
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                tmp = new Book();
                tmp.BookID = (int)mySqlDataReader["BookID"];
                tmp.ISBN = (string)mySqlDataReader["ISBN"];
                tmp.BookName = (string)mySqlDataReader["BookName"];
                tmp.Author = (string)mySqlDataReader["Author"];
                tmp.Category = (string)mySqlDataReader["Category"];
                tmp.Pstate = (int)mySqlDataReader["Pstate"];
                tmp.Btype = (int)mySqlDataReader["Btype"];
                tmp.LibraryID = (int)mySqlDataReader["LibraryID"];
                tmp.PositionID = (int)mySqlDataReader["PositionID"];
                break;
            }
            conn.Close();
            return tmp;
        }
    }
}
