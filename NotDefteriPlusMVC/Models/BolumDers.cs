namespace NotDefteriPlusMVC.Models
{
    /// <summary>
    /// databasedeki bolum ile ders arasındaki many to many ilişkiyi temsil eden model
    /// </summary>
    public class BolumDers
    {
        public int BolumId { get; set; }
        public int DersId { get; set; }
        public Bolum? Bolum { get; set; }
        public Ders? Ders { get; set; }
    }
}