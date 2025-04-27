using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Abstracts.Services;
using NotDefteriPlusMVC.Models;

namespace NotDefteriPlusMVC.Controllers
{
    /// <summary>
    /// domain/Home/ ile baþlayan url lerin yönlendirileceði controller. Not defteri uygulamasýnýn ana sayfasýný oluþturur.
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
        /// Domain girildiðinde ilk açýlan sayfa. Not defteri uygulamasýnýn ana sayfasý. Tüm notlarý listeler.
        /// </summary>
        /// <returns>Task<IActionResult></returns>
        public async Task<IActionResult> Index()
        {
            ViewBag.UserId = accountService.UserIdGetir(User); // listede not, giriþ yapan üyenin ise güncelle butonu getirilmesi için view e notu ekleyen üyenin id lerini gönderiyoruz.
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
