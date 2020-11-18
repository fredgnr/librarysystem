using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cSharpfdfjk.Dao;
using cSharpfdfjk.domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        
        RequestDao requestDao = new RequestDao();
        BookDao bookDao = new BookDao();


        [HttpGet]
        public httpResponse<List<Request>> Get()
        {
            List<Request> requests = requestDao.findAll();
            return new httpResponse<List<Request>>("", (int)ResultCode.Success_ALL, requests);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public httpResponse<Request> Get(int id)
        {
            Request request = requestDao.findByID(id);
            if (request != null)
            {
                return new httpResponse<Request>("查找成功", (int)ResultCode.Success_ALL, request);
            }
            else
            {
                return new httpResponse<Request>(
                    "不存在id为" + id + "的请求",
                    (int)ResultCode.Request_Not_Found, null);
            }
        }

        /// <summary>
        /// 根据bookid查找书的请求，可用于判断书能否外借
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("getbybookid")]
        public httpResponse<List< Request>> Getbybook(int id)
        {
            List<Request> requests = requestDao.findByBookID(id);
            return new httpResponse<List<Request>>("查找成功", (int)ResultCode.Success_ALL, requests);

        }
        /// <summary>
        /// 创建request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public httpResponse<Request> Post([FromBody] Request request)
        {
            List<Request> requests1 = requestDao.searchByTimeWithid(request.StartTime, request.EndTime,request.BookID);
            List<Request> requests=new List<Request>();
            foreach(Request item in requests1)
            {
                if (item.Tag == (int)RequestDao.statecode.UNFINISHED)
                    requests.Add(item);
            }
            if (requests.Count > 0)
                return new httpResponse<Request>("图书已被占用",(int)ResultCode.Book_Borrowed, request);
            Book book = bookDao.findByID(request.BookID);
            if (book == null)
                return new httpResponse<Request>("请求图书不存在", (int)ResultCode.Book_Not_Exist, null);
            else if(book.Btype==(int)BookDao.statecode.ONSHELF_ONLY)
                return new httpResponse<Request>("请求图书不允许借阅", (int)ResultCode.Book_Cannot_Borrowed, null);
            book.Btype = (int)BookDao.statecode.OUT_LIBRARY;
            bookDao.updateByID(book);
            request.Tag = (int)RequestDao.statecode.UNFINISHED;
            request.RequestID = requestDao.createrequest(request);
            return new httpResponse<Request>("成功创建请求", (int)ResultCode.Success_ALL, request);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public httpResponse<Request> Put(int id, [FromBody] Request request)
        {
            if (requestDao.findByID(id) != null)
            {
                request.RequestID = id;
                requestDao.updaterequest(request);
                return new httpResponse<Request>("成功修改请求信息", (int)ResultCode.Success_ALL, request);
            }
            else
            {
                return new httpResponse<Request>("请求不存在", (int)ResultCode.Request_Not_Found, null);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public httpResponse<Request> Delete(int id)
        {
            if (requestDao.findByID(id) != null)
            {
                requestDao.deleterequest(id);
                return new httpResponse<Request>("删除请求成功", (int)ResultCode.Success_ALL, null);
            }
            else
            {
                return new httpResponse<Request>("请求" + id + "不存在,无法删除", (int)ResultCode.Request_Not_Found, null);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("finishrequest/{id}")]
        public httpResponse<Request> finishPut(int id)
        {
            Request request = new Request();
            request.RequestID = id;
            if ((request=requestDao.findByID(id)) != null)
            {
                if (request.Tag == (int)RequestDao.statecode.FINISHED)
                {
                    return new httpResponse<Request>("该图书已归还", 
                        (int)ResultCode.Book_Back, request);
                }
                request.RequestID = id;
                request.Tag = (int)RequestDao.statecode.FINISHED;
                Book book = bookDao.findByID(request.BookID);
                book.Btype = (int)BookDao.statecode.IN_LIBRARY;
                requestDao.updaterequest(request);
                bookDao.updateByID(book);
                return new httpResponse<Request>("成功修改请求信息", (int)ResultCode.Success_ALL, request);
            }
            else
            {
                return new httpResponse<Request>("请求不存在", (int)ResultCode.Request_Not_Found, null);
            }
        }

        [HttpGet("gtest")]
        public httpResponse<List<Request>> fdd(DateTime start,DateTime end,int bookid)
        {
            return new httpResponse<List<Request>>("", (int)ResultCode.Success_ALL,
                requestDao.searchByTimeWithid(start, end, bookid));
        }


        [HttpGet("gtdest")]
        public httpResponse<List<Request>> fddd(DateTime start, DateTime end)
        {
            return new httpResponse<List<Request>>("", (int)ResultCode.Success_ALL,
                requestDao.searchByTime(start, end));
        }

        [HttpGet("byuser")]
        public httpResponse<List<Request>> byuser(string account)
        {
            return new httpResponse<List<Request>>("", (int)ResultCode.Success_ALL,
                requestDao.byuserundo(account));
        }
    }
}
