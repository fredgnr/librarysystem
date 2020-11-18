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
    public class ManagerController : ControllerBase
    {
        // GET: api/<ManagerController>
        ManagerDao managerDao = new ManagerDao();
        // GET: api/<UserController>
        [HttpGet]
        public httpResponse<List<Manager>> Get()
        {
            List<Manager> users = managerDao.findAll();
            return new httpResponse<List<Manager>>("", (int)ResultCode.Success_ALL, users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public httpResponse<Manager> Get(string id)
        {
            Manager manager = managerDao.findByAccount(id);
            if (manager != null)
            {
                return new httpResponse<Manager>("查找成功", (int)ResultCode.Success_ALL, manager);
            }
            else
            {
                return new httpResponse<Manager>(
                    "不存在id为" + id + "的用户",
                    (int)ResultCode.Account_Not_Exist, null);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public httpResponse<Manager> Post([FromBody] Manager manager)
        {
            if (managerDao.findByAccount(manager.account) == null)
            {
                managerDao.createManager(manager);
                return new httpResponse<Manager>("成功创建用户", (int)ResultCode.Success_ALL, manager);
            }
            else
            {
                return new httpResponse<Manager>("账户已存在，创建失败", (int)ResultCode.Account_Already_Exist, manager);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public httpResponse<Manager> Put(string id, [FromBody] Manager manager)
        {
            if (managerDao.findByAccount(id.Trim()) != null)
            {
                manager.account = id;
                managerDao.updateManager(manager);
                return new httpResponse<Manager>("成功修改用户信息", (int)ResultCode.Success_ALL, manager);
            }
            else
            {
                return new httpResponse<Manager>("账户不存在", (int)ResultCode.Account_Not_Exist, manager);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public httpResponse<string> Delete(string id)
        {
            if (managerDao.findByAccount(id.Trim()) != null)
            {
                managerDao.deleteManager(id);
                return new httpResponse<string>("删除用户成功", (int)ResultCode.Success_ALL, id);
            }
            else
            {
                return new httpResponse<string>("用户" + id + "不存在,无法删除", (int)ResultCode.Account_Not_Exist, id);
            }
        }
    }
}
