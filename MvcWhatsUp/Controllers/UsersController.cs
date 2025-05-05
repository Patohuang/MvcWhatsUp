using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;

namespace MvcWhatsUp.Controllers
{
    public class UsersController : Controller
    {
        private IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepositories)
        {
            _usersRepository = usersRepositories;
        }

        public IActionResult Index()
        {
            User? loggedInUser = HttpContext.Session.GetObject<User>("LoggedInUser");

            ViewData["LoggedInUser"] = loggedInUser;

            List<User> users = _usersRepository.GetAll();
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
            User? user = _usersRepository.GetById(userId);
            return View(user);
        }

        [HttpGet("Users/Delete/{userId}")]
        public IActionResult Delete(int userId)
        {
            User? user = _usersRepository.GetById(userId);
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                _usersRepository.Add(user);
                TempData["ConfirmMessage"] = "User has been added successfully!";
                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View(ex);
            }
        }

        [HttpPost("Users/Edit")]
        public IActionResult Update(User user)
        {
            try
            {
                _usersRepository.Update(user);
                TempData["ConfirmMessage"] = "User has been updated successfully!";
                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View(ex);
            }
        }

        [HttpPost("Users/Delete")]
        public IActionResult DeleteConfirmed(int userId)
        {
            try
            {
                _usersRepository.Delete(userId);
                TempData["ConfirmMessage"] = "User has been deleted successfully!";
                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View(ex);
            }
        }
        [HttpPost("Users/Login")]
        public IActionResult Login(Login loginModel)
        {
            User? user = _usersRepository.GetByLoginCredentials(loginModel.UserName, loginModel.Password);
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
                return View(ex);
            }
        }

    }
}
