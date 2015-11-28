using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;
using System.Linq;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IWorldRepository _worldRepository;

        public AppController(IMailService mailService, IWorldRepository worldRepository)
        {
            _mailService = mailService;
            _worldRepository = worldRepository;
        }

        public IActionResult Index()
        {
            var trips = _worldRepository.GetAllTrips();
            ViewBag.trips = trips;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Couldt not send email, configuration error!");
                }

                if (_mailService.SendMail(email,
                    email,
                    $"Contact Page From {model.Name} ({model.Email})",
                    model.Message))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Sent, Thanks!!";
                }
                
            }

            return View();
        }
    }
}
