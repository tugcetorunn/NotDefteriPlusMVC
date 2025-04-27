namespace NotDefteriPlusMVC.Models
{
    /// <summary>
    /// Kullanici ve Bolum arasındaki çoka çok ilişkiyi temsil eden model
    /// </summary>
    public class KullaniciBolum
    {
        public string KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }
        public int BolumId { get; set; }
        public Bolum? Bolum { get; set; }
    }
}