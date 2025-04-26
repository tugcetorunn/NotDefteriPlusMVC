using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotDefteriPlusMVC.Abstracts.Services;
using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Accounts;

namespace NotDefteriPlusMVC.Controllers
{
    /// <summary>
    /// view den kayıt, giriş, çıkış taleplerini karşılayan ve cevap dönen controller
    /// </summary>
    public class AccountController : Controller
    {
        private readonly SignInManager<Kullanici> signInManager; // browsera kaydedeceği özellikler olduğu için controllerdan yürütüyoruz.
        private readonly IAccountService accountService;
        public AccountController(SignInManager<Kullanici> _signInManager, IAccountService _accountService)
        {
            signInManager = _signInManager;
            accountService = _accountService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            var loginResult = accountService.Login(vm);
            if (loginResult.Kullanici != null)
            {
                await signInManager.SignInAsync(loginResult.Kullanici, isPersistent: false);
                return RedirectToAction("Index", "Home"); // login başarılı olduğunda tüm notların listelendiği sayfaya gider
            }
            else
            {
                ModelState.AddModelError("", loginResult.Mesaj);
                return View(vm);
            }
        }

        public IActionResult Register()
        {
            return View(accountService.RegisterFormOlustur());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM kullanici)
        {
            if (ModelState.IsValid)
            {
                if (await accountService.Register(kullanici))
                    return RedirectToAction("Login");
                else
                {
                    ModelState.AddModelError("", "Kayıt işlemi başarısız...");
                    return View(kullanici);
                }

            }
            return View(kullanici);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
