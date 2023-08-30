using SampelToDo.Data;
using SampelToDo.Models;
using SampelToDo.Services.Interfaces;

namespace SampelToDo.Services
{
    public class TodoService : ITodoService
    {
        private DefaultDataContext _context;
        public TodoService(DefaultDataContext context)
        {
            _context = context;   
        }


        public TodoModel Create(TodoModel model)
        {
           _context.todoModels.Add(model);
            return model;

        }

        public bool Delete(Guid id)
        {
            var RemooveModel = _context.todoModels.FirstOrDefault(x => x.Id == id);
            if (RemooveModel != null)
            {
                return _context.todoModels.Remove(RemooveModel);

            }
            return false;
        }

        public IEnumerable<TodoModel> GetAll()
        {
            return _context.todoModels;
        }

        public TodoModel GetById(Guid id)
        {
            return _context.todoModels.FirstOrDefault(x=>x.Id== id)??new TodoModel();
        }

        public IEnumerable<TodoModel> GetByName(string name)
        {
            return _context.todoModels.FindAll(x=>x.Header==name);
        }

        public TodoModel Update(TodoModel model)
            {
            var OldModel = _context.todoModels.FirstOrDefault(x=>x.Id == model.Id);
            if (OldModel != null)
            {
                OldModel.Header=model.Header;
                OldModel.Description=model.Description;
            }
            return OldModel;
        }
    }
}
