using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskyJ.Globals.Data.DataObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskyJ.Business.API.AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskyJController : ControllerBase
    {
        private DBSessionJ _currentsession = null;

        public DBSessionJ CurrentSession
        {
            get
            {
                if (_currentsession == null)
                {
                    var refreshToken = Request.Cookies["refreshToken"];
                    if (string.IsNullOrEmpty(refreshToken))
                    {
                        //try with headers instead of cookies
                        refreshToken = Request.Headers["refreshToken"];
                    }
                    //check valid refresh token
                    if (!string.IsNullOrEmpty(refreshToken))
                        _currentsession = TaskyJManager.GetDBSessionJ(refreshToken);
                    //check original jwt token - only for header based, not for cookie based
                    if (_currentsession != null && !string.IsNullOrEmpty(Request.Headers["authorization"].ToString()))
                        if (Request.Headers["authorization"].ToString() != _currentsession.JwtToken)
                            _currentsession = null;
                }
                return _currentsession;
            }
        }

        [HttpGet("GetSYNC")]
        public IEnumerable<DBTaskJ> GetSYNC(bool? includedeleted = false)
        {
            if (CurrentSession == null)
                return (IEnumerable<DBTaskJ>)Unauthorized();
            return TaskyJManager.RetrieveTasks(CurrentSession).Union(
                    includedeleted.Value ? TaskyJManager.RetrieveDeletedTasks(CurrentSession) : new List<DBTaskJ>()
                );
        }

        // GET: api/<TaskyJController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //TODO async repos
            return Ok(TaskyJManager.RetrieveTasks(CurrentSession));
        }

        // GETMASTER: api/<TaskyJController>, no password needed
        [HttpGet("GetMaster")]
        public async Task<IActionResult> GetMaster()
        {
            //TODO async repos
            return Ok(TaskyJManager.RetrieveTasks(new DBSessionJ
            {
                UserName = "admin",
                IDUser = 1
            }));
        }

        // GETMASTER: api/<TaskyJController>, no password needed
        [HttpGet("Status")]
        public IActionResult Status()
        {
            //TODO async repos
            return Content("ALIVE");
        }

        // GET api/<TaskyJController>/5
        [HttpGet("GetSYNC/{id}")]
        public DBTaskJ GetSYNC(int id)
        {
            return TaskyJManager.GetTaskById(id);
        }

        // GET api/<CategoryJController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (CurrentSession == null)
                return Unauthorized();
            var task = TaskyJManager.RetrieveTasks(CurrentSession).FirstOrDefault(t => t.ID == id);
            if (task == null)
                return NotFound();
            else
                return Ok(task);
        }

        // POST api/<TaskyJController>
        [HttpPost]
        public IActionResult Post([FromBody] DBTaskJ value)
        {
            try
            {
                TaskyJManager.PushTask(value, CurrentSession);
                return Ok(value.ID);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<TaskyJController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DBTaskJ value)
        {
            try
            {
                value.ID = id;
                TaskyJManager.PushTask(value, CurrentSession);
                return Ok(id);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<TaskyJController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                TaskyJManager.RemoveTask(id);
                return Ok(id);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
