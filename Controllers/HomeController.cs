using Microsoft.AspNetCore.Mvc;

namespace VoterRegistrationApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
