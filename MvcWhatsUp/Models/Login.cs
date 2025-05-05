using System.Xml.Linq;

namespace MvcWhatsUp.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Login()
        {
            UserName = "";
            Password = "";
        }
        public Login(string userName, string messageText)
        {
            UserName = userName;
            Password = messageText;
        }
    }
}
