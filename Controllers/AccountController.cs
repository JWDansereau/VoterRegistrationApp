using Microsoft.AspNetCore.Mvc;
using VoterRegistrationApp.Models;
using System.Linq;

namespace VoterRegistrationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // REGISTER GET
        public IActionResult Register()
        {
            return View();
        }

        // REGISTER POST
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        // LOGIN GET
        public IActionResult Login()
        {
            return View();
        }

        // LOGIN POST
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var existingUser = _context.Users
                .FirstOrDefault(u =>
                    u.Username == username &&
                    u.PasswordHash == password);

            if (existingUser != null)
            {
                HttpContext.Session.SetString("User", existingUser.Username);

                HttpContext.Session.SetString("Role", existingUser.Role);

                // VERY IMPORTANT
                HttpContext.Session.SetInt32("UserID", existingUser.UserID);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password";

            return View();
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}