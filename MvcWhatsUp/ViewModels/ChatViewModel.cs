
using MvcWhatsUp.Models;

namespace MvcWhatsUp.ViewModels
{
    public class ChatViewModel
    {
        public List<Message> Messages { get; set; }
        public User SendingUser { get; }
        public User ReceivingUser { get; }
        public ChatViewModel(List<Message> messages, User sendingUser, User receivingUser)
        {
            Messages = messages;
            SendingUser = sendingUser;
            ReceivingUser = receivingUser;
        }
    }
}
