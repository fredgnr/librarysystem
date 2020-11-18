using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Markup;
using cSharpfdfjk.Dao;
using cSharpfdfjk.domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class LibraryController : ControllerBase
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        LibraryDao libraryDao = new LibraryDao();
        // GET: api/<LibraryController>
        [HttpGet]
        public httpResponse<List<Library>> Get()
        {
            List<Library> libraries = libraryDao.findALL();
            return new httpResponse<List<Library>>("", (int)ResultCode.Success_ALL, libraries);
        }

        // GET api/<LibraryController>/5
        [HttpGet("{id}")]
        public httpResponse<Library> Get(int id)
        {
            Library library = libraryDao.findByID(id);
            if (library != null)
                return new httpResponse<Library>("", (int)ResultCode.Success_ALL, library);
            else
            {
                return new httpResponse<Library>("id为 " + id + " 的图书馆不存在", (int)ResultCode.Library_Not_Exist, null);
            }
        }

        // POST api/<LibraryController>
        [HttpPost]
        public httpResponse<Library> Post([FromBody] Library value)
        {
            if (libraryDao.findByfullName(value.Name) != null)
            {

                return new httpResponse<Library>("图书馆名字重复", (int)ResultCode.LibraryName_Exit,null);
            }
            else
            {
                value.ID=libraryDao.createLibrary(value);
                return new httpResponse<Library>("成功创建图书馆", (int)ResultCode.Success_ALL,value);
            }
        }

        // PUT api/<LibraryController>/5
        [HttpPut("{id}")]
        public httpResponse<Library> Put(int id, [FromBody] Library value)
        {
            if (libraryDao.findByID(id) != null)
            {
                libraryDao.updateLibrary(value);
                return new httpResponse<Library>("成功修改图书馆信息",(int)ResultCode.Success_ALL, value);
            }
            else
            {
                return new httpResponse<Library>("该图书馆不存在", (int)ResultCode.Library_Not_Exist, null);
            }
        }

        // DELETE api/<LibraryController>/5
        [HttpDelete("{id}")]
        public httpResponse<Library> Delete(int id)
        {
            if (libraryDao.findByID(id) != null)
            {
                libraryDao.deleteLibrary(id);
                return new httpResponse<Library>("成功删除", (int)ResultCode.Success_ALL, null);
            }
            else
            {
                return new httpResponse<Library>("该图书馆不存在", (int)ResultCode.Library_Not_Exist, null);
            }
        }
    }
}
