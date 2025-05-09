using MvcWhatsUp.Models;

namespace MvcWhatsUp.Services
{
    public interface IChatsService
    {
        void AddMessage(Message message);
        List<Message> GetMessages(int senderId, int receiverId);
    }
}
