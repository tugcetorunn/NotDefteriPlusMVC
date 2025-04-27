using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Abstracts.Services;
using NotDefteriPlusMVC.Models;

namespace NotDefteriPlusMVC.Controllers
{
    /// <summary>
    /// domain/Home/ ile ba�layan url lerin y�nlendirilece�i controller. Not defteri uygulamas�n�n ana sayfas�n� olu�turur.
    /// </summary>
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

        /// <summary>
        /// Domain girildi�inde ilk a��lan sayfa. Not defteri uygulamas�n�n ana sayfas�. T�m notlar� listeler.
        /// </summary>
        /// <returns>Task<IActionResult></returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = accountService.UserIdGetir(User); // listede not, giri� yapan �yenin ise g�ncelle butonu getirilmesi i�in view e notu ekleyen �yenin id lerini g�nderiyoruz.
            return View(await notRepository.TumNotlar());
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
