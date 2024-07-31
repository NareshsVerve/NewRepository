using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Todo_App.Generic_Repository;
using Todo_App.Models;
using Todo_App.Repository;


namespace Todo_App.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;
    
        private readonly IUserRepository _userRepository;
       
        private User user;
        public UserController(ILogger<UserController> logger,
            ApplicationDbContext context,
            IUserRepository userRepository)
          
        {
            _logger = logger;
            _context = context;
           
            _userRepository = userRepository;
           

            user = new User
           {

                   Name = "Sipun",
                   UserName = "Sipun12*",
                   Password = "Sipun12*"

           };
            /*AddUser(user);*/
            /*GetUsers();*/

        }
        //Add User
        public IActionResult AddUser(User user)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user, new ServiceProviderWithContext(_context), null);

            if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
            {
                    foreach (var validationResult in validationResults)
                    {
                        /*Console.WriteLine(validationResult.ErrorMessage);*/
                        _logger.LogError(validationResult.ErrorMessage);
                    }    
            }
            else
            {

                _userRepository.Insert(user);
                
                _logger.LogInformation("User Added Successfully.");
                return Ok("User Added Successfully.");
            }
            return View(user);
            
        }
        //Update User
        public IActionResult Update(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                _logger.LogError("User not Found");
                return Content("User not Found");
            }
            else
            {
                user.Name = "Naresh Kumar Sahoo";
                user.UpdateDate = DateTime.Now;
                _userRepository.Update(user);
                
                _logger.LogInformation("User Update Successfully.");
                return Content("User Update Successfully.");
            }
        }

        // Delete User
        public void Delete(int id) 
        {
            _userRepository.Delete(id);
           
        }
        // Display user
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetAll();
            foreach(var user in users)
            {
                Console.WriteLine($"Name : {user.Name}, Username : {user.UserName}, Created Date: {user.CreateDate}, Last Updated Date: {user.UpdateDate}, Active: {(user.IsActive ? "Active" : "Inactive")}");

            }
            return Json(users);
        }

        public IActionResult Index()
        {
            return View();
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }



    public class ServiceProviderWithContext : IServiceProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public ServiceProviderWithContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(ApplicationDbContext))
            {
                return _dbContext;
            }

            return null;
        }
    }
}
