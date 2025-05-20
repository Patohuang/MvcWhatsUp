using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Services;

namespace MvcWhatsUp.ViewComponents
{
    public class TopMessagesViewComponent : ViewComponent
    {
        private readonly IChatsService _chatService;
        public TopMessagesViewComponent(IChatsService chatService)
        {
            _chatService = chatService;
        }
        public IViewComponentResult Invoke(int numberOfMessages)
        {
            List<Message> messages = null;
            User? loggedInUser = HttpContext.Session.GetObject<User>("LoggedInUser");
            if (loggedInUser != null)
            {
                messages = _chatService.GetLastMessages(loggedInUser.UserId);
            }

            return View(messages);
        }
    }
}
