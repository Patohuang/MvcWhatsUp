using MvcWhatsUp.Models;
namespace MvcWhatsUp.Repositories
{
    public interface IChatsRepository
    {
        void AddMessage(Message message);
        List<Message> GetMessages(int senderId, int receiverId);
    }
}
