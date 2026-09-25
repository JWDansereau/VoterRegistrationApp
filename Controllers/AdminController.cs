using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using VoterRegistrationApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VoterRegistrationApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Registrations.ToList());
        }

        public IActionResult Approve(int id)
        {
            var reg = _context.Registrations.Find(id);
            reg.Status = "Approved";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            var reg = _context.Registrations.Find(id);
            reg.Status = "Rejected";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role) || role != "Admin")
            {
                context.Result = RedirectToAction("Login", "Account");
            }

            base.OnActionExecuting(context);
        }
    }
}