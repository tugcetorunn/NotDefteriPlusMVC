using System.ComponentModel.DataAnnotations;

namespace NotDefteriPlusMVC.ViewModels.Notlar
{
    /// <summary>
    /// tek bir not için olan bilgileri kullanıcıya gösterecek view model
    /// </summary>
    public class NotDetayVM
    {
        [Display(Name = "Not Id")]
        public int NotId { get; set; }

        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Display(Name = "Fakülte")]
        public string Fakulte { get; set; }

        [Display(Name = "Bölüm")]
        public string Bolum { get; set; }
        public string Ders { get; set; }

        [Display(Name = "İçerik")]
        public string Icerik { get; set; }

        [Display(Name = "Dosya")]
        public string DosyaYolu { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime OlusturulmaTarihi { get; set; }
        public string OlusturulmaTarihiStr => OlusturulmaTarihi.ToString("dd.MM.yyyy");

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime GuncellenmeTarihi { get; set; }
        public string GuncellenmeTarihiStr => GuncellenmeTarihi < OlusturulmaTarihi ? "" : GuncellenmeTarihi.ToString("dd.MM.yyyy"); // guncellenme tarihi not şlk oluşturulduğunda boş bırakılıyor bu sefer detayda 01.01.0001 olarak gözüküyor bu nedenle gunvellenme tarihi olusturulmadan küçükse str property sinde boşluk gözükecek güncelleme yapılırsa da tarihi alacak

        [Display(Name = "Ekleyen Kullanıcı")]
        public string KullaniciAd { get; set; }
        public string KullaniciId { get; set; }
    }
}
