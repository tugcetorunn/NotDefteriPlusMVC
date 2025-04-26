using System.ComponentModel.DataAnnotations.Schema;

namespace NotDefteriPlusMVC.Models
{
    /// <summary>
    /// databasedeki not tablosunu temsil eden model
    /// </summary>
    public class Not
    {
        public int NotId { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string DosyaYolu { get; set; }
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;
        public DateTime GuncellenmeTarihi { get; set; }
        [ForeignKey("Kullanici")]
        public string KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }
        public int BolumId { get; set; }
        public Bolum? Bolum { get; set; }
        public int DersId { get; set; }
        public Ders? Ders { get; set; }

    }
}
