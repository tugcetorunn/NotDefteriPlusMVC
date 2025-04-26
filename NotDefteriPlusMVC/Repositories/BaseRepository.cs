using Microsoft.EntityFrameworkCore;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// tüm ortak metodları tek bir yerden yazıp tüm entity ler için kullanabileceğimiz generic repository sınıfı
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly NotDefteriDbContext context;
        protected DbSet<TEntity> table;
        public BaseRepository(NotDefteriDbContext _context)
        {
            context = _context;
            table = context.Set<TEntity>();
        }
        public void Ekle(TEntity entity)
        {
            table.Add(entity);
            context.SaveChanges();
        }
        public void Guncelle(TEntity entity)
        {
            table.Update(entity);
            context.SaveChanges();
        }
        public void Sil(int id)
        {
            var entity = Bul(id);
            if (entity != null)
            {
                table.Remove(entity);
                context.SaveChanges();
            }
        }
        public TEntity Bul(int id)
        {
            return table.Find(id);
        }
        public List<TEntity> Listele()
        {
            return table.ToList();
        }
    }
}
