using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// bolümders tablosu ile ilgili ortak metodlar için base repositoryden, kendi metodları için interface den miras alarak metod içlerinin doldurulacağı repository sınıfı
    /// </summary>
    public class BolumDersRepository : BaseRepository<BolumDers>, IBolumDersRepository
    {
        public BolumDersRepository(NotDefteriDbContext context) : base(context)
        {
        }
    }
}
