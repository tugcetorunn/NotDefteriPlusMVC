using Microsoft.AspNetCore.Mvc.Rendering;

namespace NotDefteriPlusMVC.ViewModels.Notlar
{
    public class NotGuncelleFormVM
    {
        public SelectList Bolumler { get; set; }
        public SelectList Dersler { get; set; }
        public NotGuncelleVM Not { get; set; }
    }
}
