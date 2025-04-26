using NotDefteriPlusMVC.ResultViewModels;
using NotDefteriPlusMVC.ViewModels.Accounts;
using NotDefteriPlusMVC.ViewModels.Notlar;
using System.Security.Claims;

namespace NotDefteriPlusMVC.Abstracts.Services
{
    /// <summary>
    /// accountService i soyutlamak üzere yazılan interface
    /// </summary>
    public interface IAccountService
    {
        LoginResult Login(LoginVM vm);
        Task<bool> Register(RegisterVM vm);
        string UserIdGetir(ClaimsPrincipal uye); // controllerlarda user id gereken noktalarda kullanılmak üzere yazılan metod
        IEnumerable<KullaniciBolumVM> KullaniciBolumleriniGetir(ClaimsPrincipal claims); // kullanıcıya ait bölümleri getiren metod
        RegisterFormVM RegisterFormOlustur();
    }
}
