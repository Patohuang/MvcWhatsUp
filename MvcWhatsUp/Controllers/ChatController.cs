using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.ViewModels;
using MvcWhatsUp.Services;

namespace MvcWhatsUp.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatsService _chatsService;
        private readonly IUsersService _usersService;

        public ChatController(IChatsService chatsService, IUsersService usersService)
        {
            _chatsService = chatsService;
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult AddMessage(int? id)
        {
            //receive user id (parameter) must be available
            if (id == null)
            {
                return RedirectToAction("Index", "Users");
            }

            //user needs to be logged in
            //(for now, id of logged in user is stored in a cookie)
            User? loggedInuserId = HttpContext.Session.GetObject<User>("LoggedInUser");
            if (loggedInuserId == null)
            {
                return RedirectToAction("Index", "Users");
            }

            // get the receiving User so we can display the name in the view
            User? receiver = _usersService.GetById((int)id);
            ViewData["ReceiverUser"] = receiver;

            Message message = new Message();
            message.SenderUserId = loggedInuserId.UserId;
            message.ReceiverUserId = (int)id;
            return View(message);
        }

        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
            try
            {
                message.SendAt = DateTime.Now;
                _chatsService.AddMessage(message);

                //confirm
                TempData["ConfirmMessage"] = "Messagge has been added successfully!";

                //go to chat with the other user
                return RedirectToAction("DisplayChat", new { id = message.ReceiverUserId });
            }
            catch (Exception ex)
            {
                //if something goes wrong, return the view with the message
                //and display the error message
                ViewBag.ErrorMessage = $"Exeption occured: {ex.Message}!";
                return View(message);
            }
        }

        [HttpGet]
        public IActionResult DisplayChat(int? id)
        {
            //receive user id (parameter) must be available
            if (id == null)
            {
                return RedirectToAction("Index", "Users");
            }

            //user needs to be logged in
            //(for now, id of logged in user is stored in a cookie)
            User? loggedInuserId = HttpContext.Session.GetObject<User>("LoggedInUser");
            if (loggedInuserId == null)
            {
                return RedirectToAction("Index", "Users");
            }

            // get the receiving User so we can display the name in the view
            User? receivingUser = _usersService.GetById((int)id);
            if (receivingUser == null)
            {
                return RedirectToAction("Index", "Users");
            }

            //get all messages between the two users
            List<Message> chatMessages = _chatsService.GetMessages(loggedInuserId.UserId, receivingUser.UserId);

            //store data in the chat model
            ChatViewModel chatViewModel = new ChatViewModel(chatMessages, loggedInuserId, receivingUser);

            return View(chatViewModel);
        }
    }
}
