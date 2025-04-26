using Microsoft.AspNetCore.Mvc.Rendering;

namespace NotDefteriPlusMVC.ViewModels.Notlar
{
    /// <summary>
    /// not ekleme formu için gereken özellikleri modelleyen class
    /// </summary>
    public class NotEkleFormVM
    {
        public SelectList Bolumler { get; set; }
        public SelectList Dersler { get; set; }
        public NotEkleVM Not { get; set; }
    }
}
