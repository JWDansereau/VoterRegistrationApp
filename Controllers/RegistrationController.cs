using Microsoft.AspNetCore.Mvc;
using VoterRegistrationApp.Models;
using System.Linq;

namespace VoterRegistrationApp.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ ALL
        public IActionResult Index()
        {
            var registrations = _context.Registrations.ToList();
            return View(registrations);
        }

        // CREATE GET
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(
    VoterRegistration reg,
    string Username,
    string Email,
    string PasswordHash)
        {
            if (ModelState.IsValid)
            {
                // Create user account
                User user = new User
                {
                    Username = Username,
                    Email = Email,
                    PasswordHash = PasswordHash,
                    Role = "Citizen"
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // VERY IMPORTANT
                // Link registration to this user
                reg.UserID = user.UserID;

                // Save voter registration
                _context.Registrations.Add(reg);
                _context.SaveChanges();

                // Auto-login user
                HttpContext.Session.SetString("User", user.Username);
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetInt32("UserID", user.UserID);

                return RedirectToAction("Status");
            }

            return View(reg);
        }

        // READ SINGLE
        public IActionResult Status()
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var reg = _context.Registrations
                .FirstOrDefault(r => r.UserID == userId.Value);

            if (reg == null)
            {
                return View("NoRegistration");
            }

            return View(reg);
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var reg = _context.Registrations.Find(id);

            if (reg == null)
                return NotFound();

            return View(reg);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(VoterRegistration reg)
        {
            if (ModelState.IsValid)
            {
                _context.Registrations.Update(reg);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(reg);
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            var reg = _context.Registrations.Find(id);

            if (reg != null)
            {
                _context.Registrations.Remove(reg);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}