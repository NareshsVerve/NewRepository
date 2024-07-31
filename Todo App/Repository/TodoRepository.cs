using Todo_App.Generic_Repository;
using Todo_App.Models;

namespace Todo_App.Repository
{
    public class TodoRepository: GenericRepository<Todo>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext context): base(context) { }

        public IEnumerable<Todo> GetTodo(int id)
        {
            /* var ActiveUser = _context.Users.Where(u => u.Id == id && u.IsActive);
     
             var todos = new List<Todo>();
             if(ActiveUser != null)
             {
                 todos = _context.Todos.Where(t => t.UserId == id).ToList();
             }*/

            var todos = _context.Todos
                          .Where(t => t.UserId == id && _context.Users.Any(u => u.Id == id && u.IsActive))
                          .ToList();


            return todos;
        }
    }
}
