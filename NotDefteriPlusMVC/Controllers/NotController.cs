using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Abstracts.Services;
using NotDefteriPlusMVC.ViewModels.Notlar;

namespace NotDefteriPlusMVC.Controllers
{
    public class NotController : Controller
    {
        private readonly INotRepository notRepository;
        private readonly IAccountService accountService;

        public NotController(INotRepository _notRepository, IAccountService _accountService)
        {
            notRepository = _notRepository;
            accountService = _accountService;
        }

        [Authorize]
        public IActionResult Listele()
        {
            var userId = accountService.UserIdGetir(User);
            var notlar = notRepository.UyeyeOzelListele(userId);
            return View(notlar);
        }

        // tüm ziyaretçilerin göreceği detay sayfasını da getirdiği için sadece detay actionında authorize yok
        public IActionResult Detay(int id)
        {
            ViewBag.UserId = accountService.UserIdGetir(User);
            var notDetay = notRepository.DetayGetir(id);
            return View(notDetay);
        }

        [Authorize]
        public IActionResult Ekle()
        {
            return View(notRepository.EklemeFormuOlustur(accountService.UserIdGetir(User), accountService.KullaniciBolumleriniGetir(User)));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Ekle(NotEkleVM not)
        {
            if (ModelState.IsValid)
            {
                notRepository.NotEkle(not, accountService.UserIdGetir(User));
                return RedirectToAction("Listele");
            }

            var form = notRepository.EklemeFormuOlustur(accountService.UserIdGetir(User), accountService.KullaniciBolumleriniGetir(User));
            form.Not = not;
            return View(form);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Sil(int id)
        {
            notRepository.Sil(id);
            return RedirectToAction("Listele");
        }

        [Authorize]
        public IActionResult Guncelle(int id)
        {
            var form = notRepository.GuncellemeFormuOlustur(id, accountService.UserIdGetir(User), accountService.KullaniciBolumleriniGetir(User));
            if (form != null)
                return View(form);
            else
                return Content("Erişim yok"); // kitap null gelirse veya üyenin kitabı değilse logine atar
        }

        [HttpPost]
        [Authorize]
        public IActionResult Guncelle(NotGuncelleVM not)
        {
            var userId = accountService.UserIdGetir(User);
            var form = notRepository.GuncellemeFormuOlustur(not.NotId, userId, accountService.KullaniciBolumleriniGetir(User)); // güncellerken de notu ekleyen kişi ile oturumu açan aynı mı kontrolü yapılıyor çünkü kötü niyetli bir kullanıcı başka bir notun id sini yazıp güncelleme yapabilir.
            if (form == null)
            {
                return Forbid(); // yetkisiz kişiye yasak döner
            }

            if (ModelState.IsValid)
            {
                notRepository.NotGuncelle(not);
                return RedirectToAction("Listele");
            }

            return View(form);
        }
    }
}
