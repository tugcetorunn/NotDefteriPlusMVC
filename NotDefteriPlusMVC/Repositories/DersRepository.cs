using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Notlar;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// dersler ile ilgili ortak metodlar için base repositoryden, kendi metodları için interface den miras alarak metod içlerinin doldurulacağı repository sınıfı
    /// </summary>
    public class DersRepository : BaseRepository<Ders>, IDersRepository
    {
        public DersRepository(NotDefteriDbContext context) : base(context)
        {
        }
    }
}
