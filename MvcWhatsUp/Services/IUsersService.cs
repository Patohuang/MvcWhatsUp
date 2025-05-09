using MvcWhatsUp.Models;

namespace MvcWhatsUp.Services
{
    public interface IUsersService
    {
        List<User> GetAll();
        User? GetById(int userId);
        void Add(User user);
        void Update(User user);
        void Delete(int userId);
        User? GetByLoginCredentials(string userName, string password);
        string HashPassword(string password);
    }
}
