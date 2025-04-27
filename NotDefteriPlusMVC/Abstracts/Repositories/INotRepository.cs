using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Notlar;

namespace NotDefteriPlusMVC.Abstracts.Repositories
{
    /// <summary>
    /// Notlar ile ilgili özel metodların imzalarının yazılacağı interface
    /// </summary>
    public interface INotRepository : IRepository<Not>
    {
        Task<List<NotListeleVM>> TumNotlar();
        Task<List<NotListeleVM>> UyeyeOzelListele(string kullaniciId);
        Task NotEkle(NotEkleVM not, string kullaniciId);
        Task NotGuncelle(NotGuncelleVM not);
        Task<NotDetayVM?> DetayGetir(int id);
        Task<NotGuncelleFormVM?> GuncellemeFormuOlustur(int id, string kullaniciId, IEnumerable<KullaniciBolumVM> kullanicininBolumleri);
        NotEkleFormVM EklemeFormuOlustur(string kullaniciId, IEnumerable<KullaniciBolumVM> kullanicininBolumleri);
    }
}
