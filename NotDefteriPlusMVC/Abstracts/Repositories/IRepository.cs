namespace NotDefteriPlusMVC.Abstracts.Repositories
{
    /// <summary>
    /// Generic repository için generic interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        Task EkleAsync(TEntity entity);
        Task GuncelleAsync(TEntity entity);
        Task SilAsync(int id);
        Task<TEntity> BulAsync(int id);
        Task<List<TEntity>> ListeleAsync();
        Task<bool> DegisiklikleriKaydetAsync();
    }
}
