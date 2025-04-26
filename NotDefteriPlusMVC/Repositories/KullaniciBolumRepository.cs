using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// KullaniciBolum nesneleri için veri erişim işlemlerini gerçekleştiren bir repository sınıfıdır.
    /// </summary>
    public class KullaniciBolumRepository : BaseRepository<KullaniciBolum>, IKullaniciBolumRepository
    {
        public KullaniciBolumRepository(NotDefteriDbContext context) : base(context)
        {
        }
    }
}
