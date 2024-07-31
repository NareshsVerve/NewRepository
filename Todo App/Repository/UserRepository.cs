using Microsoft.EntityFrameworkCore;
using Todo_App.Generic_Repository;
using Todo_App.Models;

namespace Todo_App.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext context,
            ILogger<UserRepository> logger) : base(context) 
        {
            _logger = logger;
        }
        public override IEnumerable<User> GetAll()
        {
            return _context.Users.Where(u=> u.IsActive).Include(u => u.TodoList).ToList();
        }
        public override User GetById(int id)
        {
            return _context.Users
                .Include(u => u.TodoList) 
                .FirstOrDefault(u => u.Id == id);
        }
        public override void Delete(int id)
        {
            var user = GetById(id);
            if(user == null)
            {
                _logger.LogError("User not Found");
            }
            else
            {

                if (user.TodoList.Any())
                {
                    _logger.LogError("User have todo task.");
                }
                else
                {

                    _context.Users.Remove(user);
                    base.Save();
                }
            }
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
