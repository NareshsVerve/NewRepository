using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Todo_App.Models;
using Todo_App.Repository;

namespace Todo_App.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;

        private readonly IUserRepository _userRepository;
        private readonly ITodoRepository _todoRepository;
        public List<Todo> _todo;
        public TodoController(ILogger<UserController> logger,
            ApplicationDbContext context,
            IUserRepository userRepository,
            ITodoRepository todoRepository)
        {
            _logger = logger;
            _context = context;

            _userRepository = userRepository;
            _todoRepository = todoRepository;


            _todo = new List<Todo>()
              {
                new Todo
                {
                    Name = "Buy groceries",
                    Description = "Milk, Bread, Cheese",
                    Status = TodoStatus.NotStarted,
                    UserId = 31
                },
                      new Todo
                  {

                      Name = "Read Basic Dot Net",
                      Description = "Filestructure, MiddleWare, Routing",
                      Status = TodoStatus.NotStarted,
                      UserId = 31
                  }
                     /* new Todo
                {
                    Name = "Buy groceries",
                    Description = "Milk, Bread, Cheese",
                    Status = TodoStatus.NotStarted,
                    UserId = 2
                },
                      new Todo
                  {

                      Name = "Read Basic Dot Net",
                      Description = "Filestructure, MiddleWare, Routing",
                      Status = TodoStatus.NotStarted,
                      UserId = 6
                  }*/

              };
            /*AddTodoMany(_todo);*/
            /*Update(2);*/
            Update(4);
        }
        // Add More than one todo in the data base
        public void AddTodoMany(List<Todo> todo)
        {
            var validTodos = new List<Todo>();
           
            foreach (Todo _todo in todo)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(_todo, new ServiceProviderWithContext(_context), null);

                if (!Validator.TryValidateObject(_todo, validationContext, validationResults, true))
                {
                    foreach (var validationResult in validationResults)
                    {
                       
                        _logger.LogError(validationResult.ErrorMessage);
                    }
                }
                else
                {
                   
                    if (!IsTodoExist(_todo))
                    {
                        validTodos.Add(_todo);
                        
                    }
                }
            }

            _todoRepository.InsertMany(validTodos);
           
        }

        //Add One Todo 
        public void AddTodo(Todo todo)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(todo, new ServiceProviderWithContext(_context), null);

            if (!Validator.TryValidateObject(todo, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    _logger.LogError(validationResult.ErrorMessage);
                }
            }
            else
            {
                if (IsTodoExist(todo))
                {
                    _logger.LogError("Todo is already exist for this user.");
                }
                else
                {

                    _todoRepository.Insert(todo);
                    
                }
            }
        }
        // Retrive All todo of an Active user
        public IActionResult GetTodoByUser(int id)
        {
            var todos = _todoRepository.GetTodo(id);
            if(todos == null)
            {
                Console.WriteLine("Todo not exist for the given userid.");
            }
            foreach(var todo in todos)
            {
                Console.WriteLine($"Todo Name : {todo.Name}\n Description : {todo.Description}\n Created Date: {todo.CreatedDate} \n Last Updated Date: {todo.UpdatedDate}\n Status:{Enum.GetName(typeof(TodoStatus), todo.Status)}");
            }
            return Json(todos);
        }
        //Update Todo
        public void Update(int id)
        {
            var todo = _todoRepository.GetById(id);
           if(todo != null)
            {

                todo.Status = TodoStatus.InProgress;
                todo.UpdatedDate = DateTime.Now;
                _todoRepository.Update(todo);
                _logger.LogInformation("Todo updated Successfully");
            }
            else
            {
                _logger.LogError("Todo not found.");
            }
         
            
        }
        //Delete Todo
        public void Delete(int id)
        {
            _todoRepository.Delete(id);
           
            _logger.LogInformation("Todo deleted Successfully");
        }
        public IActionResult Index()
        {
            return View();
        }

        // Check the user have Existing todo or not
        public bool IsTodoExist(Todo todo)
        {
            var todoExist = _context.Todos.Any(t => todo.Name == t.Name && todo.UserId == t.UserId);
            if (todoExist)
            {
                return true;
            }
            return false;
        }
    }
}
