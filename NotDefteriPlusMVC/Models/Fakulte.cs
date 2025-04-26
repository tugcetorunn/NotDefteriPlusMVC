namespace NotDefteriPlusMVC.Models
{
    /// <summary>
    /// databasedeki fakulte tablosunu temsil eden model. 
    /// </summary>
    public class Fakulte
    {
        public int FakulteId { get; set; }
        public string FakulteAdi { get; set; }
        public ICollection<Bolum>? Bolumler { get; set; }
    }
}
