namespace NotDefteriPlusMVC.Models
{
    /// <summary>
    /// databasedeki ders tablosunu temsil eden model
    /// </summary>
    public class Ders
    {
        public int DersId { get; set; }
        public string DersAdi { get; set; }
        public ICollection<Not>? Notlar { get; set; }
        public ICollection<BolumDers>? Bolumler { get; set; }
    }
}