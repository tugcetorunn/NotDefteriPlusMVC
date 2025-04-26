using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotDefteriPlusMVC.Models.Configurations
{
    /// <summary>
    /// Kullanici tablosunun yapılandırmasını içeren sınıf
    /// </summary>
    public class KullaniciCFG : IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.Property(k => k.UserName).IsRequired().HasMaxLength(50);
            builder.Property(k => k.PasswordHash).IsRequired().HasMaxLength(100);
            builder.Property(k => k.Email).IsRequired().HasMaxLength(80);
            builder.Property(k => k.Ad).IsRequired().HasMaxLength(30);
            builder.Property(k => k.Soyad).IsRequired().HasMaxLength(30);

            builder.HasMany(k => k.Notlar)
                .WithOne(n => n.Kullanici)
                .HasForeignKey(n => n.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde ilişkili notları da sil

            builder.HasMany(k => k.Bolumler)
                .WithOne(b => b.Kullanici)
                .HasForeignKey(b => b.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde ilişkili bölümleri de sil

        }
    }
}
