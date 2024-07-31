using Todo_App.Generic_Repository;
using Todo_App.Models;

namespace Todo_App.Repository
{
    public interface ITodoRepository: IGenericRepository<Todo>
    {
        IEnumerable<Todo> GetTodo(int id);
    }
}
