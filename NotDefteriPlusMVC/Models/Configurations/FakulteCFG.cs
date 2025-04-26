using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotDefteriPlusMVC.Models.Configurations
{
    /// <summary>
    /// Fakulte tablosunun yapılandırmasını içeren sınıf
    /// </summary>
    public class FakulteCFG : IEntityTypeConfiguration<Fakulte>
    {
        public void Configure(EntityTypeBuilder<Fakulte> builder)
        {
            builder.HasKey(f => f.FakulteId);
            builder.Property(f => f.FakulteAdi).IsRequired().HasMaxLength(50);
            builder.HasMany(f => f.Bolumler).WithOne(b => b.Fakulte).HasForeignKey(b => b.FakulteId);

            // Seed data
            builder.HasData(
                new Fakulte { FakulteId = 1, FakulteAdi = "Mühendislik Fakültesi" },
                new Fakulte { FakulteId = 2, FakulteAdi = "İktisadi ve İdari Bilimler Fakültesi" },
                new Fakulte { FakulteId = 3, FakulteAdi = "Edebiyat Fakültesi" },
                new Fakulte { FakulteId = 4, FakulteAdi = "Teknoloji Fakültesi"},
                new Fakulte { FakulteId = 5, FakulteAdi = "Güzel Sanatlar Fakültesi"}
            );
        }
    }
}
