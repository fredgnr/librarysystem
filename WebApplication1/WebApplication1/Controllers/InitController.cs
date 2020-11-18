using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using cSharpfdfjk.Dao;
using cSharpfdfjk.domain;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitController : ControllerBase
    {
        // string connStr = string.Format("user=root;pwd=zz434370;server=127.0.0.1;database=library;");
        string connStr = string.Format("user=root;pwd=159753456;server=127.0.0.1;database=library;");
        RequestController requestController = new RequestController();
        BookController bookController = new BookController();
        LibraryController libraryController = new LibraryController();
        UserController userController = new UserController();
        ShelfController shelfController = new ShelfController();
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            ResourceManager resManagerA = new ResourceManager("WebApplication1.Resource", typeof(Program).Assembly);
            string astring = resManagerA.GetString("datebasesql");
            MySqlCommand cmd = new MySqlCommand(astring, conn);
            cmd.ExecuteNonQuery();
            User user = new User();
            user.account = "513317651";
            user.Password = "963852741";
            user.Name = "Fred";
            userController.Post(user);

            Library library = new Library();
            library.Name = "信息学部";
           // library = api.postlib(library).Data;
            libraryController.Post(library);

            Shelf shelf = new Shelf();
            shelf.LibraryID = library.ID;
            shelf.Category = "政治";
            shelf.Layers = 3;
            shelf.Capacity = 15;
            shelfController.Post(shelf);
            //shelf = api.postshelf(shelf).Data;


            Book book = new Book();
            book.BookName = "大国政治的悲剧";
            book.ISBN = "abcdefg";
            book.LibraryID = library.ID;
            book.Category = "政治";
            book.Author = "米尔斯海默";
            book.Pstate = 1;
            bookController.Post(book);
           // book = api.postbook(book).Data;

            Book book1 = new Book();
            book1.BookName = "大国政治的悲剧";
            book1.ISBN = "abcdefg";
            book1.LibraryID = library.ID;
            book1.Category = "政治";
            book1.Author = "米尔斯海默";
            book1.Pstate = 1;
            //book1 = api.postbook(book1).Data;
            bookController.Post(book1);

            Book book2 = new Book();
            book2.BookName = "大国政治的悲剧";
            book2.ISBN = "abcdefg";
            book2.LibraryID = library.ID;
            book2.Category = "政治";
            book2.Author = "米尔斯海默";
            book2.Pstate = 1;
            //book2 = api.postbook(book2).Data;
            bookController.Post(book2);


            Request request = new Request();
            request.BookID = book.BookID;
            request.UserAccount = user.account;
            request.StartTime = DateTime.Now;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(3);
            request.EndTime = dateTime;
            //var a = api.postrequest(request);
            requestController.Post(request);

            Request request1 = new Request();
            request1.BookID = book2.BookID;
            request1.UserAccount = user.account;
            request1.StartTime = DateTime.Now;
            request1.StartTime = request1.StartTime.AddDays(-1);
            DateTime dateTime1 = DateTime.Now;
            dateTime1 = dateTime1.AddDays(2);
            request1.EndTime = dateTime1;
            //var a1 = api.postrequest(request1);
            requestController.Post(request1);
            return new List<string>(){"ok" }.ToArray();
        }

        // GET api/<InitController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InitController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InitController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InitController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
