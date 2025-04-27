using Microsoft.EntityFrameworkCore;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using System.Threading.Tasks;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// Tüm ortak metodları tek bir yerden yazıp tüm entity ler için kullanabileceğimiz generic repository sınıfı
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
        public async Task EkleAsync(TEntity entity)
        {
            table.AddAsync(entity);
            await DegisiklikleriKaydetAsync();
        }
        public async Task GuncelleAsync(TEntity entity)
        {
            table.Update(entity);
            await DegisiklikleriKaydetAsync();
        }
        public async Task SilAsync(int id)
        {
            var entity = await BulAsync(id);
            if (entity != null)
            {
                table.Remove(entity);
                await DegisiklikleriKaydetAsync();
            }
        }
        public async Task<TEntity> BulAsync(int id)
        {
            return await table.FindAsync(id);
        }
        public async Task<List<TEntity>> ListeleAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<bool> DegisiklikleriKaydetAsync()
        {
            return await context.SaveChangesAsync() < 1 ? false : true; // kaydetme başarılıysa 1 değilse 0 döner.
        }
    }
}
