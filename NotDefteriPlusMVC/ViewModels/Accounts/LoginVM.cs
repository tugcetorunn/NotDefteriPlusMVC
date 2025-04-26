using System.ComponentModel.DataAnnotations;

namespace NotDefteriPlusMVC.ViewModels.Accounts
{
    /// <summary>
    /// login olurken kullanıcıya sorulacak bilgileri tutan model
    /// </summary>
    public class LoginVM
    {
        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre boş olamaz")]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
    }
}
