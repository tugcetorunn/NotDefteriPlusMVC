using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotDefteriPlusMVC.Models.Configurations
{
    /// <summary>
    /// bolum tablosunun yapılandırmasını içeren sınıf
    /// </summary>
    public class BolumCFG : IEntityTypeConfiguration<Bolum>
    {
        public void Configure(EntityTypeBuilder<Bolum> builder)
        {
            builder.HasKey(b => b.BolumId);
            builder.Property(b => b.BolumAdi).IsRequired().HasMaxLength(50);
            builder.HasIndex(b => b.BolumAdi).IsUnique();
            builder.HasOne(b => b.Fakulte).WithMany(f => f.Bolumler).HasForeignKey(b => b.FakulteId);
            builder.HasMany(b => b.Dersler).WithOne(d => d.Bolum).HasForeignKey(d => d.BolumId);
            builder.HasMany(b => b.Kullanicilar).WithOne(k => k.Bolum).HasForeignKey(k => k.BolumId);

            // seed data
            builder.HasData(
                new Bolum { BolumId = 1, BolumAdi = "Bilgisayar Mühendisliği", FakulteId = 1 },
                new Bolum { BolumId = 2, BolumAdi = "Endüstri Mühendisliği", FakulteId = 1 },
                new Bolum { BolumId = 3, BolumAdi = "Elektrik Elektronik Mühendisliği", FakulteId = 1 },
                new Bolum { BolumId = 4, BolumAdi = "Makine Mühendisliği", FakulteId = 1 },
                new Bolum { BolumId = 5, BolumAdi = "İşletme", FakulteId = 2 },
                new Bolum { BolumId = 6, BolumAdi = "İktisat", FakulteId = 2 },
                new Bolum { BolumId = 7, BolumAdi = "Hukuk", FakulteId = 3 },
                new Bolum { BolumId = 8, BolumAdi = "Psikoloji", FakulteId = 4 },
                new Bolum { BolumId = 9, BolumAdi = "Sosyoloji", FakulteId = 4 },
                new Bolum { BolumId = 10, BolumAdi = "Biyoloji", FakulteId = 5 },
                new Bolum { BolumId = 11, BolumAdi = "Kimya", FakulteId = 5 }
            );
        }

    }
}
