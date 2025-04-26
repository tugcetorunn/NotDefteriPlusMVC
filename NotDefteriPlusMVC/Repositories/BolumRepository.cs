using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// bolümler ile ilgili ortak metodlar için base repositoryden, kendi metodları için interface den miras alarak metod içlerinin doldurulacağı repository sınıfı
    /// </summary>
    public class BolumRepository : BaseRepository<Bolum>, IBolumRepository
    {
        public BolumRepository(NotDefteriDbContext context) : base(context)
        {
        }
    }
}
