using MvcWhatsUp.Repositories;
using MvcWhatsUp.Models;
using System.Security.Cryptography;
using System.Text;
namespace MvcWhatsUp.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public List<User> GetAll()
        {
            return _usersRepository.GetAll();
        }
        public User? GetById(int userId)
        {
            return _usersRepository.GetById(userId);
        }
        public void Add(User user)
        {
            //check if user already exists
            if (_usersRepository.EmailAddressExists(user.EmailAddress))
            {
                throw new Exception("Email address is already in use!");
            }
            //hash password
            user.Password = HashPassword(user.Password);

            _usersRepository.Add(user);
        }
        public void Update(User user)
        {
            //check if user already exists
            
            if (_usersRepository.EmailAddressExists(user.EmailAddress))
            {
                throw new Exception("Email address is already in use!");
            }
            _usersRepository.Update(user);
        }
        public void Delete(int userId)
        {
            _usersRepository.Delete(userId);
        }
        public User? GetByLoginCredentials(string userName, string password)
        {
            return _usersRepository.GetByLoginCredentials(userName, HashPassword(password));
        }

        public string HashPassword(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] hasBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hasBytes);
            }
        }
    }
}
