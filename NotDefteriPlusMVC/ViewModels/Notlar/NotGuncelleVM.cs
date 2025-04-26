using System.ComponentModel.DataAnnotations;

namespace NotDefteriPlusMVC.ViewModels.Notlar
{
    /// <summary>
    /// not güncelleme sayfasında önyüzde gereken propertyler için view model
    /// </summary>
    public class NotGuncelleVM
    {
        public int NotId { get; set; }

        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Display(Name = "Bölüm")]
        public int BolumId { get; set; }

        [Display(Name = "Ders")]
        public int DersId { get; set; }

        [Display(Name = "İçerik")]
        public string Icerik { get; set; }
        public IFormFile? Dosya { get; set; }
        public string DosyaYolu { get; set; } // güncelleme formunda eski dosyayı göstermek için

    }
}