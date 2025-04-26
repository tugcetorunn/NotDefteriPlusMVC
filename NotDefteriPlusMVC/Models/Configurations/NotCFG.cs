using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotDefteriPlusMVC.Models.Configurations
{
    /// <summary>
    /// Not tablosunun yapılandırmasını içeren sınıf
    /// </summary>
    public class NotCFG : IEntityTypeConfiguration<Not>
    {
        public void Configure(EntityTypeBuilder<Not> builder)
        {
            builder.Property(n => n.NotId).ValueGeneratedOnAdd();
            builder.Property(n => n.Baslik).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Icerik).IsRequired();
            builder.Property(n => n.OlusturulmaTarihi).IsRequired().HasColumnType("date");
            builder.Property(n => n.GuncellenmeTarihi).HasColumnType("date");
            builder.HasOne(n => n.Ders)
                .WithMany(d => d.Notlar)
                .HasForeignKey(n => n.DersId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(n => n.Kullanici)
                .WithMany(k => k.Notlar)
                .HasForeignKey(n => n.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
