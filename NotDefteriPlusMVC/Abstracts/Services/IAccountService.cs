using NotDefteriPlusMVC.ResultViewModels;
using NotDefteriPlusMVC.ViewModels.Accounts;
using NotDefteriPlusMVC.ViewModels.Notlar;
using System.Security.Claims;

namespace NotDefteriPlusMVC.Abstracts.Services
{
    /// <summary>
    /// AccountService i soyutlamak üzere yazılan interface
    /// </summary>
    public interface IAccountService
    {
        Task<LoginResult> Login(LoginVM vm);
        Task<RegisterResult> Register(RegisterVM vm);
        string UserIdGetir(ClaimsPrincipal uye);
        Task<IEnumerable<KullaniciBolumVM>> KullaniciBolumleriniGetir(ClaimsPrincipal claims);
        Task<RegisterFormVM> RegisterFormOlustur();
    }
}
