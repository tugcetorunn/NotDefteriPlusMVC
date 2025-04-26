using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotDefteriPlusMVC.Models.Configurations
{
    /// <summary>
    /// Ders tablosunun yapılandırmasını içeren sınıf
    /// </summary>
    public class DersCFG : IEntityTypeConfiguration<Ders>
    {
        public void Configure(EntityTypeBuilder<Ders> builder)
        {
            builder.HasKey(d => d.DersId);
            builder.Property(d => d.DersAdi).IsRequired().HasMaxLength(50);
            builder.HasMany(d => d.Bolumler).WithOne(b => b.Ders).HasForeignKey(b => b.DersId);

            // seed data
            builder.HasData(
                new Ders { DersId = 1, DersAdi = "Veri Yapıları" },
                new Ders { DersId = 2, DersAdi = "Algoritmalar" },
                new Ders { DersId = 3, DersAdi = "İstatistik" },
                new Ders { DersId = 4, DersAdi = "Mikroekonomi" },
                new Ders { DersId = 5, DersAdi = "Ceza Hukuku" },
                new Ders { DersId = 6, DersAdi = "Medeni Hukuk" },
                new Ders { DersId = 7, DersAdi = "Psikolojiye Giriş" },
                new Ders { DersId = 8, DersAdi = "Sosyolojiye Giriş" },
                new Ders { DersId = 9, DersAdi = "Biyolojiye Giriş" },
                new Ders { DersId = 10, DersAdi = "Kimyaya Giriş" },
                new Ders { DersId = 11, DersAdi = "Fizik" },
                new Ders { DersId = 12, DersAdi = "Matematik" }
            );
        }
    }
}
