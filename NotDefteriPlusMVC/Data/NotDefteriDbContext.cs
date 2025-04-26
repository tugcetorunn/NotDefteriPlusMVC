using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotDefteriPlusMVC.Models;
using System.Reflection;

namespace NotDefteriPlusMVC.Data
{
    public class NotDefteriDbContext : IdentityDbContext<Kullanici>
    {
        public NotDefteriDbContext()
        {
            
        }
        public NotDefteriDbContext(DbContextOptions<NotDefteriDbContext> options) : base(options)
        {
        }
        
        public DbSet<Not> Notlar { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<Bolum> Bolumler { get; set; }
        public DbSet<Fakulte> Fakulteler { get; set; }
        public DbSet<BolumDers> BolumDers { get; set; }
        public DbSet<KullaniciBolum> KullaniciBolum { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
