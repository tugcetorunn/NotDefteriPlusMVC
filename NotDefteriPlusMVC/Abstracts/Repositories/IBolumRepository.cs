using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Notlar;

namespace NotDefteriPlusMVC.Abstracts.Repositories
{
    /// <summary>
    /// bolume ait özel metodların imzalarını içeren interface
    /// </summary>
    public interface IBolumRepository : IRepository<Bolum>
    {
        ///// <summary>
        ///// Kullanıcıya ait bölümleri listele
        ///// </summary>
        ///// <param name="kullaniciId"></param>
        ///// <returns></returns>
        //List<KullaniciBolumVM> BolumleriGetir(string kullaniciId);
        ///// <summary>
        ///// Bölümün derslerini getir
        ///// </summary>
        ///// <param name="bolumId"></param>
        ///// <returns></returns>
        //List<BolumDersVM> BolumDersleriniGetir(int bolumId);
    }
}
