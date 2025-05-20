using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Services;

using MvcWhatsUp.Models;
namespace MvcWhatsUp.ViewComponents
{
    public class MessageCountViewComponent : ViewComponent
    {
        private readonly IChatsService _chatService;
        public MessageCountViewComponent(IChatsService chatService)
        {
            _chatService = chatService;
        }
        public IViewComponentResult Invoke()
        {
            int? count = null;

            User? loggedInUser = HttpContext.Session.GetObject<User>("LoggedInUser");
            if (loggedInUser != null)
            {
                count = _chatService.GetLastMessages(loggedInUser.UserId).Count;
            }
            return View(count);
        }
    }
}
