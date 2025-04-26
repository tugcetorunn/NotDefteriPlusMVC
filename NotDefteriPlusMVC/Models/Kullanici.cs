using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotDefteriPlusMVC.Models
{
    /// <summary>
    /// databasedeki aspnetuser tablosunu temsil eden model
    /// </summary>
    public class Kullanici : IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [NotMapped]
        public string AdSoyad => $"{Ad} {Soyad}";
        public ICollection<Not>? Notlar { get; set; }
        public ICollection<KullaniciBolum>? Bolumler { get; set; } // çift ana dal mesela
    }
}