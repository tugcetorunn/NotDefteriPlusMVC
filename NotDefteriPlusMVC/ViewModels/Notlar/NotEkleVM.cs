using System.ComponentModel.DataAnnotations;

namespace NotDefteriPlusMVC.ViewModels.Notlar
{
    /// <summary>
    /// not ekleme formu için kullanıcıdan alınacak bilgileri tutan view model
    /// </summary>
    public class NotEkleVM
    {
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Display(Name = "Bölüm")]
        public int BolumId { get; set; }

        [Display(Name = "Ders")]
        public int DersId { get; set; }

        [Display(Name = "İçerik")]
        public string Icerik { get; set; }
        public IFormFile Dosya { get; set; }
    }
}