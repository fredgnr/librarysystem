using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using cSharpfdfjk.Dao;
using cSharpfdfjk.domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 控制书架的API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        ShelfDao shelfDao = new ShelfDao();
        PositionDao positionDao = new PositionDao();
        LibraryDao libraryDao = new LibraryDao();
        BookDao bookDao = new BookDao();

        /// <summary>
        /// 工具api，实际中不使用
        /// </summary>
        /// <param name="value"></param>
        [HttpPut("{test}")]
        public void insertbook(Book value,int shelfID)
        {
            List<Shelf> shelves = shelfDao.findByLibraryID(value.LibraryID);
            //为图书自动分配位置，如果没有位置就放入仓库
            bool tag = false;
            foreach (Shelf shelf in shelves)
            {
                if (shelf.Category != value.Category||shelf.ShelfID==shelfID)
                    continue;
                List<Position> positions = positionDao.findByShelfID(shelf.ShelfID);
                foreach (Position position in positions)
                {
                    if (position.Tag == (int)PositionDao.statecode.NOT_OCCUPIED)
                    {
                        tag = true;
                        value.PositionID = position.PositionID;
                        position.Tag = (int)PositionDao.statecode.OCCUPIED;
                        positionDao.updatePosition(position);
                        value.Btype = (int)BookDao.statecode.IN_LIBRARY;
                        break;
                    }
                }
                if (tag)
                    break;
            }
            if (!tag)
            {
                value.PositionID =0;
                value.Btype = (int)BookDao.statecode.IN_BACKUP;
            }
             bookDao.updateByID(value);
        }

        [HttpGet]
        public httpResponse<List<Shelf>> Get()
        {
            List<Shelf> shelves = shelfDao.findAll();
            return new httpResponse<List<Shelf>>("", (int)ResultCode.Success_ALL, shelves);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public httpResponse<Shelf> Get(int id)
        {
           Shelf shelf = shelfDao.findByID(id);
            if (shelf != null)
            {
                return new httpResponse<Shelf>("查找成功", (int)ResultCode.Success_ALL, shelf);
            }
            else
            {
                return new httpResponse<Shelf>(
                    "不存在id为" + id + "的书架",
                    (int)ResultCode.Shelf_Not_Found, null);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public httpResponse<Shelf> Post([FromBody] Shelf shelf)
        {

            if (libraryDao.findByID(shelf.LibraryID) == null)
                return new httpResponse<Shelf>("图书馆不存在",(int) ResultCode.Library_Not_Exist, null);
            shelf.ShelfID=shelfDao.AddShelf(shelf);
            for(int row = 0; row < shelf.Layers; row++) 
                for(int col = 0; col < shelf.Capacity / shelf.Layers; col++)
                {
                    Position position = new Position();
                    position.ShelfID = shelf.ShelfID;
                    position.Layer = row;
                    position.Pindex = col;
                    position.Tag = (int)PositionDao.statecode.NOT_OCCUPIED;
                    positionDao.addPosition(position);
                }
            return new httpResponse<Shelf>("成功创建书架", (int)ResultCode.Success_ALL, shelf);
     
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public httpResponse<Shelf> Put(int id, [FromBody] Shelf shelf)
        {
            if (shelfDao.findByID(id) != null)
            {
                shelf.ShelfID = id;
                shelfDao.updateByID(shelf);
                return new httpResponse<Shelf>("成功修改书架信息", (int)ResultCode.Success_ALL,shelf);
            }
            else
            {
                return new httpResponse<Shelf>("书架不存在", (int)ResultCode.Shelf_Not_Found, shelf);
            }
        }

        [HttpGet("bylib")]
        public httpResponse<List<Shelf>> getbylib(int lib)
        {
            return new httpResponse<List<Shelf>>("", (int)ResultCode.Success_ALL,
                shelfDao.findByLibraryID(lib));
        }

       /// <summary>
       /// 删除书架，并且检测是否满足数据库约束
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpDelete("{id}")]
        public httpResponse<Shelf> Delete(int id)
        {
            if (shelfDao.findByID(id) != null)
            {
                var positions = positionDao.findByShelfID(id);
                List<Book> books = new List<Book>();
                foreach(var position in positions)
                {
                    Book book = bookDao.findByPos(position.PositionID);
                    if (book == null)
                        continue;
                    if(book.Btype==(int)BookDao.statecode.OUT_LIBRARY)
                        return new httpResponse<Shelf>("有书还未归还", (int)ResultCode.Book_Not_Back, null);
                    book.Pstate = (int)BookDao.statecode.IN_BACKUP;
                    books.Add(book);
                }
                foreach(var book in books)
                {
                    insertbook(book,id);
                }
                foreach(var position in positions)
                {
                    positionDao.deleteByID(position.PositionID);
                }
                shelfDao.deleteByID(id);
                return new httpResponse<Shelf>("删除书架成功", (int)ResultCode.Success_ALL, null);
            }
            else
            {
                return new httpResponse<Shelf>("书架" + id + "不存在,无法删除", (int)ResultCode.Shelf_Not_Found, null);
            }
        }
    }
}
