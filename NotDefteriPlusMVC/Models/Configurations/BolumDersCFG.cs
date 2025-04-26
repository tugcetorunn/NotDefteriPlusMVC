using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotDefteriPlusMVC.Models.Configurations
{
    /// <summary>
    /// Ders ve Bolum arasındaki many-to-many ilişkiyi temsil eden BolumDers sınıfının yapılandırmasını içeren sınıf
    /// </summary>
    public class BolumDersCFG : IEntityTypeConfiguration<BolumDers>
    {
        public void Configure(EntityTypeBuilder<BolumDers> builder)
        {
            builder.HasKey(bd => new { bd.BolumId, bd.DersId });
            builder.HasOne(bd => bd.Bolum).WithMany(b => b.Dersler).HasForeignKey(bd => bd.BolumId);
            builder.HasOne(bd => bd.Ders).WithMany(d => d.Bolumler).HasForeignKey(bd => bd.DersId);

            // seed data
            builder.HasData(
                new BolumDers { BolumId = 1, DersId = 1 },
                new BolumDers { BolumId = 1, DersId = 2 },
                new BolumDers { BolumId = 2, DersId = 3 },
                new BolumDers { BolumId = 2, DersId = 4 },
                new BolumDers { BolumId = 3, DersId = 5 },
                new BolumDers { BolumId = 3, DersId = 6 },
                new BolumDers { BolumId = 4, DersId = 7 },
                new BolumDers { BolumId = 4, DersId = 8 },
                new BolumDers { BolumId = 5, DersId = 9 },
                new BolumDers { BolumId = 5, DersId = 10 },
                new BolumDers { BolumId = 1, DersId = 11 },
                new BolumDers { BolumId = 1, DersId = 12 },
                new BolumDers { BolumId = 2, DersId = 11 },
                new BolumDers { BolumId = 2, DersId = 12 },
                new BolumDers { BolumId = 3, DersId = 11 },
                new BolumDers { BolumId = 3, DersId = 12 },
                new BolumDers { BolumId = 4, DersId = 11 },
                new BolumDers { BolumId = 4, DersId = 12 }
            );
        }
    }
}
