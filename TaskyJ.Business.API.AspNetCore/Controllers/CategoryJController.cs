using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Business.API.AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryJController : ControllerBase
    {
        // GET: api/<CategoryJController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //TODO async repos
            return Ok(Business.TaskyJManager.RetrieveCategories());
        }

        // GET api/<CategoryJController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(Business.TaskyJManager.GetCategoryById(id));
        }

        // POST api/<CategoryJController>
        [HttpPost]
        public void Post([FromBody] DBCategoryJ value)
        {
            Business.TaskyJManager.PushCategory(value);
        }

        // PUT api/<TaskyJController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DBCategoryJ value)
        {
            Business.TaskyJManager.PushCategory(value);
        }

        // DELETE api/<CategoryJController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Business.TaskyJManager.RemoveTask(id);
        }
    }
}
