using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Notlar;

namespace NotDefteriPlusMVC.Abstracts.Repositories
{
    /// <summary>
    /// bolume ait özel metodların imzalarını içeren interface
    /// </summary>
    public interface IBolumRepository : IRepository<Bolum>
    {
        /// <summary>
        /// Bölümün derslerini getirir. Ekleme ve güncelleme formlarında kullanılır.
        /// </summary>
        /// <param name="bolumId"></param>
        /// <returns></returns>
        IEnumerable<BolumDersVM> BolumDersleriniGetir(IEnumerable<KullaniciBolumVM> bolumler);
    }
}
