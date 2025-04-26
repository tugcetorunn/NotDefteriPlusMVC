using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// fakülteler ile ilgili ortak metodlar için base repositoryden, kendi metodları için interface den miras alarak metod içlerinin doldurulacağı repository sınıfı
    /// </summary>
    public class FakulteRepository : BaseRepository<Fakulte>, IFakulteRepository
    {
        public FakulteRepository(NotDefteriDbContext context) : base(context)
        {
        }
    }
}
