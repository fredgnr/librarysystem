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
    public class UserController : ControllerBase
    {
        UserDao userDao = new UserDao();
        RequestDao requestDao = new RequestDao();
        // GET: api/<UserController>
        [HttpGet]
        public httpResponse<List<User>> Get()
        {
            List<User> users =userDao.findAll();
            return new httpResponse<List<User>>("",(int)ResultCode.Success_ALL,users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public httpResponse<User> Get(string id)
        {
            User user = userDao.findByAccount(id);
            if (user!=null)
            {
                return new httpResponse<User>("查找成功", (int)ResultCode.Success_ALL, user);
            }
            else
            {
                return new httpResponse<User>(
                    "不存在id为" + id + "的用户",
                    (int)ResultCode.Account_Not_Exist, null);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public httpResponse<User> Post([FromBody] User user)
        {
            if (userDao.findByAccount(user.account) == null)
            {
                userDao.createUser(user);
                return new httpResponse<User>("成功创建用户", (int)ResultCode.Success_ALL, user);
            }
            else
            {
                return new httpResponse<User>("账户已存在，创建失败", (int)ResultCode.Account_Already_Exist, user);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public httpResponse<User> Put(string id, [FromBody] User user)
        {
            if (userDao.findByAccount(id.Trim()) != null)
            {
                userDao.updateUser(user);
                return new httpResponse<User>("成功修改用户信息", (int)ResultCode.Success_ALL, user);
            }
            else
            {
                return new httpResponse<User>("账户不存在", (int)ResultCode.Account_Not_Exist, user);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public httpResponse<string> Delete(string id,string password)
        {
            if (userDao.findByAccount(id.Trim()) != null)
            {
                User user = userDao.findByAccount(id);
                if (user.Password != password)
                {
                    return new httpResponse<string>("密码错误，无法注销用户", (int)ResultCode.Password_Wrong, id);
                }
                List<Request> requests = requestDao.byuserundo(user.account);
                if (requests.Count >= 0)
                {
                    return new httpResponse<string>("用户还有书未归还", (int)ResultCode.Book_Not_Back, "");
                }

                userDao.deleteUser(id);

                return new httpResponse<string>("注销用户成功", (int)ResultCode.Success_ALL, id);
            }
            else
            {
                return new httpResponse<string>("用户"+id+"不存在,无法注销", (int)ResultCode.Account_Not_Exist, id);
            }
        }

        [HttpGet("login")]
        public httpResponse<User> Login(string account,string password)
        {
            User user = userDao.findByAccount(account);
            if (user == null)
            {
                return new httpResponse<User>("账不存在", (int)ResultCode.Account_Not_Exist, null);
            }
            else if (user.Password != password)
            {
                return new httpResponse<User>("密码错误", (int)ResultCode.Password_Wrong, null);
            }
            else
            {
                return new httpResponse<User>("成功登录", (int)ResultCode.Success_ALL, user);
            }
        }
    }
}
