using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SampelToDo.Models;
using SampelToDo.Services.Interfaces;

namespace SampelToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewTodoController:ControllerBase
    {
        private readonly IRepository<TodoModel> _repository;
        public NewTodoController(IRepository<TodoModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoModel>> GetAll()
        {
            var todos = await _repository.GetAllAsync();

            return todos;
        }

        [HttpGet("{id}")]
        public async Task<TodoModel?> GetById(Guid id)
        {
            var todo = await _repository.GetAsync(id);
            return todo;
        }

        [HttpPost]
        public async Task<TodoModel> Post([FromBody] TodoModel model)
        {
            await _repository.AddAsync(model);
            return model;

        }

        [HttpPatch]
        public async Task<ActionResult<TodoModel>> Update([FromBody] TodoModel model)
        {
            var todoForUpdate = await _repository.GetAsync(model.Id);//?? new TodoModel(){Id=id, Description=model.Description ,Header= model.Header};
            if (todoForUpdate == null)
            {
                return NotFound($"Пользователь с id: {model.Id} не найден");
            }

            todoForUpdate.Description = model.Description;
            todoForUpdate.Header=model.Header;

            await _repository.UpdateAsync(todoForUpdate);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoModel>> Delete(Guid id)
        {
            var user = await _repository.GetAsync(id);
            if (user == null)
            {
                return NotFound($"Пользователь с id: {id} не найден");
            }
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}
