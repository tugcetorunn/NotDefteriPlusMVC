using Microsoft.AspNetCore.Mvc.Rendering;

namespace NotDefteriPlusMVC.ViewModels.Accounts
{
    /// <summary>
    /// register formunda kullanılacak olan viewmodel, bolumlerin gelmesi için eklendi
    /// </summary>
    public class RegisterFormVM
    {
        public SelectList Bolumler { get; set; }
        public RegisterVM Kullanici { get; set; }
    }
}
