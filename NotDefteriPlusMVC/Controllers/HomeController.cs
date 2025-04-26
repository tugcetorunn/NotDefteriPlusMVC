using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Abstracts.Services;
using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.Repositories;
using NotDefteriPlusMVC.Services;

namespace NotDefteriPlusMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService accountService;
        private readonly INotRepository notRepository;

        public HomeController(ILogger<HomeController> logger, IAccountService _accountService, INotRepository notRepository)
        {
            _logger = logger;
            accountService = _accountService;
            this.notRepository = notRepository;
        }

        public IActionResult Index()
        {
            ViewBag.UserId = accountService.UserIdGetir(User); // listede not, giri� yapan �yenin ise g�ncelle butonu getirilmesi i�in view e notu ekleyen �yenin id lerini g�nderiyoruz.
            return View(notRepository.TumNotlar());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
