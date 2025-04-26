using NotDefteriPlusMVC.Models;

namespace NotDefteriPlusMVC.Abstracts.Repositories
{
    /// <summary>
    /// bolumders özel metotları içeren repository interface i. interface üzerinden constructor injection ile erişilecek
    /// </summary>
    public interface IBolumDersRepository : IRepository<BolumDers>
    {
    }
}
