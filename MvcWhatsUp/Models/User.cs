namespace MvcWhatsUp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public bool Deleted { get; set; }

        public User()
        {
            UserId = 0;
            UserName = "";
            MobileNumber = "";
            EmailAddress = "";
            Password = "";
            Deleted = false;

        }
        public User(int id, string name, string mobileNumber, string emailAddress, string password, bool deleted)
        {
            UserId = id;
            UserName = name;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
            Password = password;
            Deleted = deleted;
        }
    }
}
