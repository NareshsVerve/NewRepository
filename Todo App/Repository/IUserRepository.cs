using Todo_App.Generic_Repository;
using Todo_App.Models;

namespace Todo_App.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetAllUsers();
    }
}
