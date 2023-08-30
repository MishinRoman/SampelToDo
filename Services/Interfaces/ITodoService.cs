using SampelToDo.Models;

namespace SampelToDo.Services.Interfaces
{
    public interface ITodoService
    {
        TodoModel Create(TodoModel model);
        TodoModel Update(TodoModel model);
        IEnumerable<TodoModel> GetAll();
        TodoModel GetById(Guid id);
        IEnumerable<TodoModel> GetByName(string name);
        bool Delete(Guid id);

    }
}
