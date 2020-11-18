using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Markup;
using cSharpfdfjk.Dao;
using cSharpfdfjk.domain;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private BookDao bookDao = new BookDao();
        private LibraryDao libraryDao = new LibraryDao();
        private ShelfDao shelfDao = new ShelfDao();
        private PositionDao positionDao = new PositionDao();
        private RequestDao requestDao = new RequestDao();
        // GET: api/<BookController>
        [HttpGet]
        public httpResponse<List<Book>> Get()
        {
            List<Book> books = bookDao.findAll();
            return new httpResponse<List<Book>>("",(int) ResultCode.Success_ALL, books);
        }

        /// <summary>
        /// 根据id查询图书基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public httpResponse<Book> Get(int id)
        {
            Book book = bookDao.findByID(id);
            if (book != null)
            {
                return new httpResponse<Book>("", (int)ResultCode.Success_ALL, book);
            }
            else
            {
                return new httpResponse<Book>("id为"+id+"的书不存在", (int)ResultCode.Book_Not_Exist, book);
            }
        }

        /// <summary>
        /// 添加图书
        /// </summary>
        /// <param name="value">需要插入的图书</param>
        /// <returns></returns>
        [HttpPost]
        public httpResponse<Book> Post([FromBody] Book value)
        {
            if (libraryDao.findByID(value.LibraryID) == null)
                return new httpResponse<Book>("图书馆不存在", (int)ResultCode.Library_Not_Exist, null);
            List<Shelf> shelves = shelfDao.findByLibraryID(value.LibraryID); 
            //为图书自动分配位置，如果没有位置就放入仓库
            bool tag = false;
            foreach(Shelf shelf in shelves)
            {
                if (shelf.Category != value.Category)
                    continue;
                List<Position> positions = positionDao.findByShelfID(shelf.ShelfID);
                foreach(Position position in positions){
                    if (position.Tag ==(int) PositionDao.statecode.NOT_OCCUPIED)
                    {
                        tag = true;
                        value.PositionID = position.PositionID;
                        position.Tag = (int)PositionDao.statecode.OCCUPIED;
                        positionDao.updatePosition(position);
                        value.Btype =(int) BookDao.statecode.IN_LIBRARY;
                        break;
                    }
                }
                if (tag)
                    break;
            }
            if (!tag)
                value.Btype = (int)BookDao.statecode.IN_BACKUP;
            value.BookID=bookDao.addbook(value);
            return new httpResponse<Book>("", (int)ResultCode.Success_ALL, value);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public httpResponse<Book> Put(int id, [FromBody] Book value)
        {
            if (bookDao.findByID(id) != null)
            {
                value.BookID = id;
                bookDao.updateByID(value);
                return new httpResponse<Book>("", (int)ResultCode.Success_ALL, value);
            }
            else
            {
                return new httpResponse<Book>("bookid为" + value.BookID + "的书不存在",
                    (int)ResultCode.Book_Not_Exist, value);
            }
        }

        [HttpPut("changelib")]
        public httpResponse<Book> changelib(int id,int newlib)
        {
            if (bookDao.findByID(id) != null)
            {
                Book book = bookDao.findByID(id);
                if (libraryDao.findByID(newlib) == null)
                    return new httpResponse<Book>("图书馆不存在", (int)ResultCode.Library_Not_Exist, null);
                List<Shelf> shelves = shelfDao.findByLibraryID(newlib);
                //为图书自动分配位置，如果没有位置就放入仓库
                bool tag = false;
                foreach (Shelf shelf in shelves)
                {
                    if (shelf.Category != book.Category)
                        continue;
                    List<Position> positions = positionDao.findByShelfID(shelf.ShelfID);
                    foreach (Position position in positions)
                    {
                        if (position.Tag == (int)PositionDao.statecode.NOT_OCCUPIED)
                        {
                            tag = true;
                            book.PositionID = position.PositionID;
                            position.Tag = (int)PositionDao.statecode.OCCUPIED;
                            positionDao.updatePosition(position);
                            book.Btype = (int)BookDao.statecode.IN_LIBRARY;
                            break;
                        }
                    }
                    if (tag)
                        break;
                }
                if (!tag)
                    book.Btype = (int)BookDao.statecode.IN_BACKUP;
                bookDao.updateByID(book);
                return new httpResponse<Book>("", (int)ResultCode.Success_ALL, book);
            }
            else
            {
                return new httpResponse<Book>("bookid为" + id + "的书不存在",
                    (int)ResultCode.Book_Not_Exist, null);
            }
        }

        /// <summary>
        /// 根据id删除图书（暂时不使用）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public httpResponse<Book> Delete(int id)
        {
            if (bookDao.findByID(id) != null)
            {
                Book book = bookDao.findByID(id);
                List<Request> requests = requestDao.findByBookID(book.BookID);
                if (requests.Count > 0)
                {
                    return new httpResponse<Book>("此书尚未归还", (int)ResultCode.Book_Not_Back, book);
                }
                if (book.Btype != (int)BookDao.statecode.IN_BACKUP)
                {
                    Position position = positionDao.findByID(book.PositionID);
                    position.Tag = (int)PositionDao.statecode.NOT_OCCUPIED;
                    positionDao.updatePosition(position);
                }
                bookDao.deleteByID(id);
                return new httpResponse<Book>("成功删除", (int)ResultCode.Success_ALL, null);
            }
            else
            {
                return new httpResponse<Book>("bookid为" + id + "的书不存在",
                    (int)ResultCode.Book_Not_Exist,null);
            }
        }


        [HttpGet("getpos")]
        public httpResponse<BookShelfPosition> getpos(int bookid) 
        {
            BookShelfPosition bookShelfPosition = new BookShelfPosition();
            if (bookDao.findByID(bookid) != null)
            {
                Book book = bookDao.findByID(bookid);
                if (book.Btype == (int)BookDao.statecode.OUT_LIBRARY)
                {
                    bookShelfPosition.position = positionDao.findByID(book.PositionID);
                    bookShelfPosition.shelf = shelfDao.findByID(bookShelfPosition.position.ShelfID);
                    return new httpResponse<BookShelfPosition>(
                        "书已外借", (int)ResultCode.Book_Not_Back, bookShelfPosition);
                }
                if (book.Btype == (int)BookDao.statecode.IN_BACKUP)
                {
                    bookShelfPosition.position = null;
                    bookShelfPosition.shelf = null;
                    return new httpResponse<BookShelfPosition>(
                        "书在仓库", (int)ResultCode.Book_In_Back, bookShelfPosition);
                }
                bookShelfPosition.position = positionDao.findByID(book.PositionID);
                bookShelfPosition.shelf = shelfDao.findByID(bookShelfPosition.position.ShelfID);
                return new httpResponse<BookShelfPosition>(
                    "查询成功", (int)ResultCode.Success_ALL, bookShelfPosition);
            }
            else
            {
                return new httpResponse<BookShelfPosition>("bookid为" + bookid + "的书不存在",
                    (int)ResultCode.Book_Not_Exist, null);
            }
        }

        [HttpGet("byname")]
        public httpResponse<List<Book>> getbyname(string name)
        {
            return new httpResponse<List<Book>>("", (int)ResultCode.Success_ALL,
                bookDao.findByName(name));
        }

        [HttpGet("byauthor")]
        public httpResponse<List<Book>> getbyauthor(string name)
        {
            return new httpResponse<List<Book>>("", (int)ResultCode.Success_ALL,
                bookDao.findByAuthor(name));
        }
    }
}
