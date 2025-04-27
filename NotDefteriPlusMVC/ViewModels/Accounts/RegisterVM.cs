using System.ComponentModel.DataAnnotations;

namespace NotDefteriPlusMVC.ViewModels.Accounts
{
    /// <summary>
    /// register olurken kullanıcıdan istenen bilgileri tutan model
    /// </summary>
    public class RegisterVM
    {
        [Required(ErrorMessage = "Ad boş olamaz")]
        [StringLength(30, ErrorMessage = "Ad en fazla 30 karakter olmalıdır")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad boş olamaz")]
        [StringLength(30, ErrorMessage = "Soyad en fazla 30 karakter olmalıdır")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-posta boş olamaz")]
        [StringLength(80, ErrorMessage = "E-posta en fazla 80 karakter olmalıdır")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş olamaz")]
        [StringLength(30, ErrorMessage = "Kullanıcı adı en fazla 30 karakter olmalıdır")]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Display(Name = "Bölümler")]
        public List<int> SecilenBolumler { get; set; }

        [Required(ErrorMessage = "Şifre boş olamaz")]
        [StringLength(50, ErrorMessage = "Şifre en az 6 karakter olmalıdır", MinimumLength = 6)]
        [MaxLength(20, ErrorMessage = "Şifre en fazla 20 karakter olmalıdır")]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifre boş olamaz")]
        [StringLength(50, ErrorMessage = "Şifre en az 6 karakter olmalıdır", MinimumLength = 6)]
        [MaxLength(20, ErrorMessage = "Şifre en fazla 20 karakter olmalıdır")]
        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor.")]
        [Display(Name = "Şifre Tekrarı")]
        public string SifreTekrari { get; set; }
    }
}
