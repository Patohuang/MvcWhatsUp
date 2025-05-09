using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Services;

namespace MvcWhatsUp.Controllers
{
    public class UsersController : BaseController
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public IActionResult Index()
        {
            User? loggedInUser = HttpContext.Session.GetObject<User>("LoggedInUser");

            ViewData["LoggedInUser"] = loggedInUser;

            List<User> users = _usersService.GetAll();
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpGet("Users/Edit/{userId}")]
        public IActionResult Edit(int userId)
        {
            User? user = _usersService.GetById(userId);
            return View(user);
        }

        [HttpGet("Users/Delete/{userId}")]
        public IActionResult Delete(int userId)
        {
            User? user = _usersService.GetById(userId);
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                _usersService.Add(user);
                TempData["ConfirmMessage"] = "User has been added successfully!";
                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View(user);
            }
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                _usersService.Update(user);
                TempData["ConfirmMessage"] = "User has been updated successfully!";
                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View(user);
            }
        }

        [HttpPost("Users/Delete")]
        public IActionResult DeleteConfirmed(int userId)
        {
            try
            {
                _usersService.Delete(userId);
                TempData["ConfirmMessage"] = "User has been deleted successfully!";
                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View();
            }
        }
        [HttpPost("Users/Login")]
        public IActionResult Login(Login loginModel)
        {
            User? user = _usersService.GetByLoginCredentials(loginModel.UserName, loginModel.Password);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View(loginModel);
            }
            else
            {
                HttpContext.Session.SetObject("LoggedInUser", user);
                TempData["ConfirmMessage"] = $"Welcome {user.UserName}";

                return RedirectToAction("Index", "Users");
            }
        }
        [HttpPost("Users/Logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("LoggedInUser");
                TempData["ConfirmMessage"] = "You have logged out successfully!";
                return RedirectToAction("Index", "Users"); // Redirect to the login page
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult SetPreferredTheme(string? theme)
        {
            if(theme != null)
            {
                CookieOptions options = new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddDays(30),
                    Path = "/",
                    Secure = true,
                    HttpOnly = true,
                    IsEssential = true,
                };
                Response.Cookies.Append("PreferredTheme", theme, options);
            }
            return RedirectToAction("Index", "Users");
        }

    }
}
