namespace NotDefteriPlusMVC.Models
{
    /// <summary>
    /// databasedeki bolum tablosunu temsil eden model. fakulte bolum arasında bire çok ilişki vardır. bolum ile ders arasında many to many ilişki vardır.
    /// </summary>
    public class Bolum
    {
        public int BolumId { get; set; }
        public string BolumAdi { get; set; }
        public int FakulteId { get; set; }
        public Fakulte? Fakulte { get; set; }
        public ICollection<BolumDers>? Dersler { get; set; }
        public ICollection<KullaniciBolum>? Kullanicilar { get; set; }
    }
}