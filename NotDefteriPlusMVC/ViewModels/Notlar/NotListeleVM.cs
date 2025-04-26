using System.ComponentModel.DataAnnotations;

namespace NotDefteriPlusMVC.ViewModels.Notlar
{
    /// <summary>
    /// notlar listelenirken kullanıcının göreceği ve view de kullanılacak olan propertyler
    /// </summary>
    public class NotListeleVM
    {
        public int NotId { get; set; }

        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Display(Name = "Fakülte")]
        public string Fakulte { get; set; }

        [Display(Name = "Bölüm")]
        public string Bolum { get; set; }

        [Display(Name = "Ders")]
        public string Ders { get; set; }

        [Display(Name = "Ekleyen Kullanıcı")]
        public string KullaniciAd { get; set; }
        public string KullaniciId { get; set; }
    }
}
