using SampelToDo.Models;

namespace SampelToDo.Data
{
    public class DefaultDataContext
    {
        public List<TodoModel> todoModels { get; set; }
        public DefaultDataContext()
        {
            todoModels = new List<TodoModel>();
        }

    }
}
