using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotDefteriPlusMVC.Models.Configurations
{
    /// <summary>
    /// kullanici ve bolum arasindaki many-to-many iliskinin konfigürasyonu
    /// </summary>
    public class KullaniciBolumCFG : IEntityTypeConfiguration<KullaniciBolum>
    {
        public void Configure(EntityTypeBuilder<KullaniciBolum> builder)
        {
            builder.HasKey(kb => new { kb.BolumId, kb.KullaniciId });
            builder.HasOne(kb => kb.Bolum).WithMany(b => b.Kullanicilar).HasForeignKey(kb => kb.BolumId);
            builder.HasOne(kb => kb.Kullanici).WithMany(d => d.Bolumler).HasForeignKey(kb => kb.KullaniciId);
        }
    }
}
