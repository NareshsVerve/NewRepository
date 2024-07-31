using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Todo_App.Models;

namespace Todo_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public User user;
        public Todo todo;
        public List<User> _user;
        public List <Todo> _todo;
        public HomeController(ApplicationDbContext context) 
        {
            _context = context;
            /*user = new User
            {

                    Name = "Naresh Kumar Sahoo",
                    UserName = "Naresh",
                    Password = "password123"

                
            };*/
            /*user = new User
            {

                Name = "Alok Sahoo",
                UserName = "Alok",
                Password = "Alok123"


            };*/
            /*_user = new List<User>(){
                new User
                {

                    Name = "Rajesh Parida",
                    UserName = "Rajesh",
                    Password = "Rajesh123"

                },
                new User
                {

                    Name = "RaKesh Palar",
                    UserName = "Rakesh",
                    Password = "Rakesh123",
                    IsActive = false

                },
                new User
                {

                    Name = "Sipun Biswal",
                    UserName = "Sipun",
                    Password = "Sipun123"

                },

            };
*/

            _todo = new List<Todo>()
              {
                /*new Todo
                {
                    Name = "Buy groceries", 
                    Description = "Milk, Bread, Cheese",
                    Status = TodoStatus.NotStarted,
                    UserId = 1
                },*/
                      new Todo
                  {

                      Name = "Read Basic Dot Net",
                      Description = "Filestructure, MiddleWare, Routing",
                      Status = TodoStatus.NotStarted,
                      UserId = 1
                  }
                   /*   new Todo
                {
                    Name = "Buy groceries",
                    Description = "Milk, Bread, Cheese",
                    Status = TodoStatus.NotStarted,
                    UserId = 4
                },
                      new Todo
                  {

                      Name = "Read Basic Dot Net",
                      Description = "Filestructure, MiddleWare, Routing",
                      Status = TodoStatus.NotStarted,
                      UserId = 4
                  }*/

              };
            /* todo = new Todo()
             {
                 Name = "Read Basic Dot Net",
                 Description = "Filestructure, MiddleWare, Routing",
                 Status = TodoStatus.NotStarted,
                 UserId = 1
             };*/
            /*CreateUser();*/
            CreateTodo();
            /*UpdateUser(1);*/
            /*UpdateTodo(3);*/
            /* DeleteUser(2);*/
            /* DeleteTodo(2);*/
            /*GetAllUser();*/
            GetAllTodo(1);



        }
        public IActionResult GetAllTodo(int id)
        {
            var user = _context.Users.Where(u => u.IsActive && u.Id == id).SingleOrDefault();
            if(user != null)
            {

                var todos = _context.Todos.Where(t => t.UserId == id).ToList();
                int n = 1;
                foreach(var todo in todos)
                {
                    Console.WriteLine("Task no:" + n);
                    Console.WriteLine("Name:" + todo.Name);
                    Console.WriteLine("Description: " + todo.Description);
                    Console.WriteLine("Status:" + Enum.GetName(typeof(TodoStatus), todo.Status));
                    n++;
                }
                return Json(todos);
            }
            else
            {
                Console.WriteLine("User is Inactive");
            }
            return NotFound();
        }
        public void GetAllUser()
        {
           var users = _context.Users.Where(user => user.IsActive).ToList();
            foreach (var user in users) 
            {
                Console.WriteLine("User Deatils");
                Console.WriteLine("Name: "+user.Name);
                Console.WriteLine("Username: " + user.UserName);
                Console.WriteLine("Created date: "+user.CreateDate);
                Console.WriteLine("Last Update date: " + user.UpdateDate);
            }    
        }
        public void DeleteTodo(int id)
        {
           var _todo = _context.Todos.Where(todo => todo.Id == id).SingleOrDefault();
            _context.Todos.Remove(_todo);
            _context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var _user = _context.Users
           .Include(u => u.TodoList) 
           .Where(u => u.Id == id && u.IsActive)
           .SingleOrDefault();

            int todoCount = _user?.TodoList.Count ?? 0;
            if(todoCount == 0)
            {
                _context.Users.Remove(_user);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("The user have todo.");
            }
        }
        public void UpdateTodo(int id)
        {
            var _todo = _context.Todos.Where(todo => todo.Id == id).SingleOrDefault();
            _todo.Name = "Shopping";
            _todo.Status = TodoStatus.InProgress;
            _todo.UpdatedDate = DateTime.Now;
            _context.Todos.Update(_todo);
            _context.SaveChanges();
        }
        public void UpdateUser(int id) 
        { 
            var _user = _context.Users.Where(user => user.Id == id).SingleOrDefault();
           
            _user.UserName = "NareshSahoo";
            _user.UpdateDate = DateTime.Now;
            _context.Users.Update(_user);
            _context.SaveChanges();
        }

        public void CreateUser()
        {
            var userExist = _context.Users.Any(u => u.UserName == user.UserName);
            if (userExist)
            {
                Console.WriteLine("User Already Exist");
            }
            else
            {
                 _context.Users.Add(user);

            }

                /*_context.Users.AddRange(_user);*/
            
                _context.SaveChanges();
        }
        public void CreateTodo()
        {
            _context.Todos.AddRange(_todo);
            _context.SaveChanges();
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
