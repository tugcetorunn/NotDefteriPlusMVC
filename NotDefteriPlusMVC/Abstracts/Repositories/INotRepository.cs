using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Notlar;

namespace NotDefteriPlusMVC.Abstracts.Repositories
{
    /// <summary>
    /// notlar ile ilgili özel metodların imzalarının yazılacağı interface
    /// </summary>
    public interface INotRepository : IRepository<Not>
    {
        List<NotListeleVM> TumNotlar();
        List<NotListeleVM> UyeyeOzelListele(string kullaniciId);
        void NotEkle(NotEkleVM not, string kullaniciId);
        void NotGuncelle(NotGuncelleVM not);
        NotDetayVM DetayGetir(int id);
        NotGuncelleFormVM? GuncellemeFormuOlustur(int id, string kullaniciId, IEnumerable<KullaniciBolumVM> kullanicininBolumleri);
        NotEkleFormVM EklemeFormuOlustur(string kullaniciId, IEnumerable<KullaniciBolumVM> kullanicininBolumleri);

    }
}
