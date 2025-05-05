using MvcWhatsUp.Models;
namespace MvcWhatsUp.Repositories
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        User? GetById(int userId);
        void Add(User user);
        void Update(User user);
        void Delete(int userId);
        User? GetByLoginCredentials(string userName, string password);
    }
}
