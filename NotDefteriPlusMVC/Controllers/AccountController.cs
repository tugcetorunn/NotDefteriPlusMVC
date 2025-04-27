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

        /// <summary>
        /// Giriş yapma sayfasını döner
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Giriş sayfasına girilen bilgileri çeker ve kullanıcıyı kontrol eder. Buna göre giriş işlemi yapar.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Task<IActionResult></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            var loginResult = await accountService.Login(vm);
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

        /// <summary>
        /// Kayıt olma sayfasını döner
        /// </summary>
        /// <returns>Task<IActionResult></returns>
        public async Task<IActionResult> Register()
        {
            return View(await accountService.RegisterFormOlustur());
        }

        /// <summary>
        /// Kayıt olma sayfasında girilen bilgileri alır ve bilgileri kontrol eder. Buna göre kayıt işlemi yapar.
        /// </summary>
        /// <param name="kullanici"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM kullanici)
        {
            if (ModelState.IsValid)
            {
                var registerResult = await accountService.Register(kullanici);
                if (registerResult.Hatalar == null)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var item in registerResult.Hatalar)
                    {
                        ModelState.AddModelError("", item);
                    }
                    RegisterFormVM frm = await accountService.RegisterFormOlustur();
                    return View(frm);
                }
            }
            // modelstate geçerli değilse
            // var bolumler = await accountService.KullaniciBolumleriniGetir(User);
            var form = await accountService.RegisterFormOlustur();
            return View(form);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
