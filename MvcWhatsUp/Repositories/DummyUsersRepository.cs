namespace MvcWhatsUp.Repositories
{
    public class DummyUsersRepository
    {
        List<Models.User> users =
        [
            new Models.User ( 1, "Peter Sauber", "06-87763419", "petersauber@gmail.com", "123", false ),
            new Models.User ( 2, "Bill hodges", "06-14022398", "bill.hodges@gmail.com", "123", false ),
            new Models.User ( 3, "Morris Bellamy", "06-56190265", "morrisbellamy@gmail.com", "123", false )
        ];

        public List<Models.User> GetAll()
        {
            Console.WriteLine($"Get all {users.Count}");
            return users;
        }

        public Models.User? GetById(int userId)
        {
            return users.FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(Models.User user)
        {
            users.Add(user);
            Console.WriteLine($"{users.Count}");
        }

        public void Update(Models.User user)
        {
            Models.User? existingUser = GetById(user.UserId);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.MobileNumber = user.MobileNumber;
                existingUser.EmailAddress = user.EmailAddress;
                existingUser.Password = user.Password;
            }
        }
        public void Delete(int userId)
        {
            var existingUser = users.FirstOrDefault(x => x.UserId == userId);
            if (existingUser != null)
            {
                users.Remove(existingUser);
            }
        }
    }
}
