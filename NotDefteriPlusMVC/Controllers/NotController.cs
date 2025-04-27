using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Abstracts.Services;
using NotDefteriPlusMVC.ViewModels.Notlar;
using System.Threading.Tasks;

namespace NotDefteriPlusMVC.Controllers
{
    /// <summary>
    /// Url de domain/Not/ ile başlayan linklerin yönlendirileceği controller. Not ile ilgili kullanıcıdan gelen CRUD isteklerini işler ve kullanıcıya bir çıktı döner.
    /// </summary>
    public class NotController : Controller
    {
        private readonly INotRepository notRepository;
        private readonly IAccountService accountService;

        public NotController(INotRepository _notRepository, IAccountService _accountService)
        {
            notRepository = _notRepository;
            accountService = _accountService;
        }

        /// <summary>
        /// Girişi başarılı olan kullanıcının kendi notlarını görmek istediğinde tetiklenecek action.
        /// </summary>
        /// <returns>IActionResult</returns>
        [Authorize]
        public async Task<IActionResult> Listele()
        {
            var userId = accountService.UserIdGetir(User);
            var notlar = await notRepository.UyeyeOzelListele(userId);
            return View(notlar);
        }

        /// <summary>
        /// Hem ziyaretçilerin hem kullanıcıların not detayını görmek istediğinde tetiklenecek action. Bu yüzden authorize attribute ü kullanılmadı.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> Detay(int id)
        {
            ViewBag.UserId = accountService.UserIdGetir(User);
            var notDetay = await notRepository.DetayGetir(id);
            return View(notDetay);
        }

        /// <summary>
        /// Girişi başarılı olan kullanıcının not eklemek istediğinde tetiklenecek action. Formu açar.
        /// </summary>
        /// <returns>IActionResult</returns>
        [Authorize]
        public IActionResult Ekle()
        {
            return View(notRepository.EklemeFormuOlustur(accountService.UserIdGetir(User), accountService.KullaniciBolumleriniGetir(User)));
        }

        /// <summary>
        /// Girişi başarılı olan kullanıcının not eklemek istediğinde tetiklenecek action. Formdan verileri çeker.
        /// </summary>
        /// <param name="not"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Ekle(NotEkleVM not)
        {
            if (ModelState.IsValid)
            {
                await notRepository.NotEkle(not, accountService.UserIdGetir(User));
                return RedirectToAction("Listele");
            }

            var form = notRepository.EklemeFormuOlustur(accountService.UserIdGetir(User), accountService.KullaniciBolumleriniGetir(User));
            form.Not = not;
            return View(form);
        }

        /// <summary>
        /// Girişi başarılı olan kullanıcının kendi notunu silmek istediğinde tetiklenecek action.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Sil(int id)
        {
            await notRepository.SilAsync(id);
            return RedirectToAction("Listele");
        }

        /// <summary>
        /// Girişi başarılı olan kullanıcının kendi notunu güncellemek istediğinde tetiklenecek action. Formu açar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [Authorize]
        public async Task<IActionResult> Guncelle(int id)
        {
            var form = await notRepository.GuncellemeFormuOlustur(id, accountService.UserIdGetir(User), accountService.KullaniciBolumleriniGetir(User));
            if (form != null)
                return View(form);
            else
                return Content("Erişim yok"); // kitap null gelirse veya üyenin kitabı değilse logine atar
        }

        /// <summary>
        /// Girişi başarılı olan kullanıcının kendi notunu güncellemek istediğinde tetiklenecek action. Formdan verileri çeker.
        /// </summary>
        /// <param name="not"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Guncelle(NotGuncelleVM not)
        {
            var userId = accountService.UserIdGetir(User);
            var form = await notRepository.GuncellemeFormuOlustur(not.NotId, userId, accountService.KullaniciBolumleriniGetir(User)); // güncellerken de notu ekleyen kişi ile oturumu açan aynı mı kontrolü yapılıyor çünkü kötü niyetli bir kullanıcı başka bir notun id sini yazıp güncelleme yapabilir.
            if (form == null)
            {
                return Forbid(); // yetkisiz kişiye yasak döner
            }

            if (ModelState.IsValid)
            {
                // Eğer yeni dosya yüklenmişse
                if (not.Dosya != null && not.Dosya.Length > 0)
                    not.DosyaYolu = FileOperations.UploadImage(not.Dosya);
                else
                    not.DosyaYolu = form.Not.DosyaYolu; // Yeni dosya yüklenmediyse, eski dosya yolunu koru

                await notRepository.NotGuncelle(not);
                return RedirectToAction("Listele");
            }

            return View(form);
        }
    }
}
