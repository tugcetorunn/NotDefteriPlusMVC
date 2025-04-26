namespace NotDefteriPlusMVC.Abstracts.Repositories
{
    /// <summary>
    /// Generic repository için generic interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        void Ekle(TEntity entity);
        void Guncelle(TEntity entity);
        void Sil(int id);
        TEntity Bul(int id);
        List<TEntity> Listele();
    }
}
