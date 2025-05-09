using MvcWhatsUp.Repositories;
using MvcWhatsUp.Models;
namespace MvcWhatsUp.Services
{
    public class ChatsService : IChatsService
    {
        private readonly IChatsRepository _chatsRepository;
        public ChatsService(IChatsRepository chatsRepository)
        {
            _chatsRepository = chatsRepository;
        }
        public void AddMessage(Message message)
        {
            _chatsRepository.AddMessage(message);
        }
        public List<Message> GetMessages(int senderId, int receiverId)
        {
            return _chatsRepository.GetMessages(senderId, receiverId);
        }
    }
}
