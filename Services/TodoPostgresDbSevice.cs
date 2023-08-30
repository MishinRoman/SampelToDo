using SampelToDo.Data;
using SampelToDo.Models;
using SampelToDo.Services.Interfaces;
using SampelToDo.Data;
using SampelToDo.Models;
using SampelToDo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SampelToDo.Services
{
    public class TodoPostgresDbSevice : ITodoService
    {
        private PostgresDbContext _context;
        public TodoPostgresDbSevice(PostgresDbContext context)
        {
            _context = context;
        }
        public TodoModel Create(TodoModel model)
        {
            _context.Todos.Add(model);
            _context.SaveChanges();
            return model;

        }

        public bool Delete(Guid id)
        {
            var RemooveModel = _context.Todos.FirstOrDefault<TodoModel>(x => x.Id == id);
            if (RemooveModel != null)
            {
                _context.Todos.Remove(RemooveModel);
                _context.SaveChanges();
                return _context.ChangeTracker.HasChanges();


            }
            return false;
        }

        public IEnumerable<TodoModel> GetAll()
        {
            return _context.Todos;
        }

        public TodoModel GetById(Guid id)
        {
            return _context.Todos.FirstOrDefault(x => x.Id == id) ?? new TodoModel();
        }

        public IEnumerable<TodoModel> GetByName(string name)
        {
            return _context.Todos.Where(x => x.Header == name);
        }

        public TodoModel Update(TodoModel model)
        {
            var update = _context.Todos.FirstOrDefault(x => x.Id == model.Id);

            if (update != null)
            {
                update.Header = model.Header;
                update.Description = model.Description;
                _context.Todos.Update(update);
                _context.SaveChanges();
            }
            

            return model;

        }

    }
}

