using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Abstracts.Services;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ResultViewModels;
using NotDefteriPlusMVC.ViewModels.Accounts;
using NotDefteriPlusMVC.ViewModels.Notlar;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotDefteriPlusMVC.Services
{
    /// <summary>
    /// userManager yardımıyla kullanıcı oluşturma, giriş yapma işlemlerini controller dan uzaklaştırmak üzere oluşturulmuş service
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly NotDefteriDbContext context;
        private readonly UserManager<Kullanici> userManager;
        private readonly IBolumRepository bolumRepository;
        public AccountService(UserManager<Kullanici> _userManager, IBolumRepository _bolumRepository, NotDefteriDbContext _context)
        {
            userManager = _userManager;
            bolumRepository = _bolumRepository;
            context = _context;
        }

        /// <summary>
        /// View den gelen kullanıcı adı ve şifre bilgilerini kontrol eder ve sonucu gönderir. Giriş yapma işlemi controllerda yapılıyor.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Task<LoginResult></returns>
        public async Task<LoginResult> Login(LoginVM vm)
        {
            LoginResult loginResult = new(); // üyeyi, sonucu ve mesajı döndürecek result class
            var user = await userManager.FindByNameAsync(vm.KullaniciAdi);
            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, vm.Sifre);
                if (result)
                {
                    // signin metodunu controller da çalıştıracağız.
                    loginResult.Kullanici = user;
                }
                else
                {
                    loginResult.Kullanici = null;
                    loginResult.Mesaj = "Şifre yanlış.";
                }
            }
            else
            {
                loginResult.Kullanici = null;
                loginResult.Mesaj = "Kullanıcı bulunamadı.";
            }

            return loginResult;
        }

        public async Task<RegisterResult> Register(RegisterVM vm)
        {
            var user = new Kullanici
            {
                UserName = vm.KullaniciAdi,
                Email = vm.Email,
                Ad = vm.Ad,
                Soyad = vm.Soyad
            };

            user.Bolumler = new List<KullaniciBolum>();
            foreach(var bolumId in vm.SecilenBolumler)
            {
                KullaniciBolum yeniBolum = new KullaniciBolum { BolumId = bolumId, Kullanici = user };
                user.Bolumler.Add(yeniBolum);
            }

            var result = await userManager.CreateAsync(user, vm.Sifre);
            // kullanıcı oluşturma işlemi başarılı ise
            if (result.Succeeded)
            {
                return new RegisterResult
                {
                    Hatalar = null
                };
            }
            else
            {
                // hata var, hataları döndür
                RegisterResult registerResult = new()
                {
                    Hatalar = result.Errors.Select(e => e.Description).ToList(),
                };
                return registerResult;
            }
        }

        public string UserIdGetir(ClaimsPrincipal uye)
        {
            var userId = userManager.GetUserId(uye);
            return userId;
        }

        public async Task<IEnumerable<KullaniciBolumVM>> KullaniciBolumleriniGetir(ClaimsPrincipal claims)
        {
            var userId = userManager.GetUserId(claims);

            var kullanici = await context.Users // usermanager da loading yapılmıyor...
                .Include(u => u.Bolumler)
                    .ThenInclude(ub => ub.Bolum)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (kullanici == null || kullanici.Bolumler == null)
                return Enumerable.Empty<KullaniciBolumVM>();

            var bolumler = kullanici.Bolumler
                .Where(b => b.Bolum != null)
                .Select(b => new KullaniciBolumVM
                {
                    BolumId = b.BolumId,
                    BolumAdi = b.Bolum.BolumAdi
                })
                .ToList();

            return bolumler;
        }


        public async Task<RegisterFormVM> RegisterFormOlustur()
        {
            RegisterFormVM form = new()
            {
                Bolumler = new SelectList(await bolumRepository.ListeleAsync(), "BolumId", "BolumAdi")
            };

            return form;
        }
    }
}
