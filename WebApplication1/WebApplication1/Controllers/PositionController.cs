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
    /// <summary>
    /// 此api为测试api，实际应用中不需要使用
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        PositionDao positionDao = new PositionDao();
        [HttpGet]
        public httpResponse<List<Position>> Get()
        {
            List<Position> positions = positionDao.findAll();
            return new httpResponse<List<Position>>("", (int)ResultCode.Success_ALL, positions);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public httpResponse<Position> Get(int id)
        {
            Position position = positionDao.findByID(id);
            if (position != null)
            {
                return new httpResponse<Position>("查找成功", (int)ResultCode.Success_ALL, position);
            }
            else
            {
                return new httpResponse<Position>(
                    "不存在id为" + id + "的位置",
                    (int)ResultCode.Position_Not_Found, null);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public httpResponse<Position> Post([FromBody] Position position)
        {

            position.PositionID = positionDao.addPosition(position);
            return new httpResponse<Position>("成功创建书架", (int)ResultCode.Success_ALL,position);

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public httpResponse<Position> Put(int id, [FromBody] Position position)
        {
            if (positionDao.findByID(id) != null)
            {
                position.PositionID = id;
                positionDao.updatePosition(position) ;
                return new httpResponse<Position>("成功修改位置信息", (int)ResultCode.Success_ALL, position);
            }
            else
            {
                return new httpResponse<Position>("位置不存在", (int)ResultCode.Position_Not_Found, position);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public httpResponse<Position> Delete(int id)
        {
            if (positionDao.findByID(id) != null)
            {
                positionDao.deleteByID(id);
                return new httpResponse<Position>("删除位置成功", (int)ResultCode.Success_ALL, null);
            }
            else
            {
                return new httpResponse<Position>("位置" + id + "不存在,无法删除", (int)ResultCode.Position_Not_Found, null);
            }
        }
    }
}
