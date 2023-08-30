using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SampelToDo.Models;
using SampelToDo.Services.Interfaces;

namespace SampelToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private ITodoService _todoService;
        public TodosController(ITodoService todoService)
        {
            _todoService= todoService;
        }

        [HttpPost]
        public TodoModel Create (TodoModel model)=>_todoService.Create (model);

        [HttpGet("{id}")]
        public TodoModel Get(Guid id) => _todoService.GetById(id);
        [HttpPatch]
        public TodoModel Updata(TodoModel model)=>_todoService.Update(model);

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)=>_todoService.Delete(id)?Ok():NotFound();

        [HttpGet]
        public IEnumerable<TodoModel> GetAll() => _todoService.GetAll();
        
    }
}
