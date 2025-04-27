using Microsoft.EntityFrameworkCore;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Notlar;
using System.Collections.Generic;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// bolümler ile ilgili ortak metodlar için base repositoryden, kendi metodları için interface den miras alarak metod içlerinin doldurulacağı repository sınıfı
    /// </summary>
    public class BolumRepository : BaseRepository<Bolum>, IBolumRepository
    {
        public BolumRepository(NotDefteriDbContext context) : base(context)
        {
        }

        public IEnumerable<BolumDersVM> BolumDersleriniGetir(IEnumerable<KullaniciBolumVM> bolumler) // parametreye kullanıcının bölümleri id ve ad ile collection olarak gelir
        {
            // kullaniciBolumVm den bolumId leri çek 
            List<Bolum> kullanicininBolumleri = new List<Bolum>(); 
            foreach(var kullaniciBolumVm in bolumler) // VM içindeki bolumId lerden hangi bölümler olduğunu bulup birden fazla olabileceği için liste ye ekliyoruz.
            {
                Bolum? bolumEager = table?.Include(x => x.Dersler).ThenInclude(d => d.Ders).Where(x => x.BolumId == kullaniciBolumVm.BolumId).SingleOrDefault();
                kullanicininBolumleri.Add(bolumEager);
            }
            
            // getirilen bölümler içinde hangi dersler olduğunu bul
            List<BolumDersVM> dersler = new();
            foreach (var bolum in kullanicininBolumleri)
            {
                foreach (var ders in bolum.Dersler)
                {
                    dersler.Add(new BolumDersVM // bunları ders adi ve id tutan vm e atıyoruz
                    {
                        DersAdi = ders.Ders.DersAdi,
                        DersId = ders.DersId
                    });
                }
            }

            var distinctDersler = dersler.GroupBy(x => x.DersId).Select(g => g.First()).ToList(); // tekrar edenleri kaldır (distinct referansa bakıp aynı olanları çıkarır, fakat burada aynı id ve adda olan vm ler yeni obje olarak üretildikleri için aynı referansı tutmuyorlar. distinct işe yaramaz bu yüzden)
            return distinctDersler;
        }
    }
}
