using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Notlar;
using System.Threading.Tasks;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// Notlar ile ilgili ortak metodlar için base repositoryden, kendi metodları için interface den miras alarak metod içlerinin doldurulacağı repository sınıfı
    /// </summary>
    public class NotRepository : BaseRepository<Not>, INotRepository
    {
        private readonly IBolumRepository bolumRepository;
        public NotRepository(NotDefteriDbContext context, IBolumRepository _bolumRepository) : base(context)
        {
            bolumRepository = _bolumRepository;
        }

        /// <summary>
        /// Database deki tüm notları özet bilgilerle liste halinde getirir. Yeniden eskiye sıralar.
        /// </summary>
        /// <returns>Task<List<NotListeleVM>></returns>
        public async Task<List<NotListeleVM>> TumNotlar()
        {
            // home sayfasında kullanıcı olan veya olmayan tüm ziyaretçilerin göreceği liste
            return await table.OrderByDescending(x => x.OlusturulmaTarihi)
                        .Select(x => new NotListeleVM
                        {
                            NotId = x.NotId,
                            Baslik = x.Baslik,
                            Fakulte = x.Bolum.Fakulte.FakulteAdi,
                            Bolum = x.Bolum.BolumAdi,
                            Ders = x.Ders.DersAdi,
                            KullaniciAd = x.Kullanici.AdSoyad, // kullanıcı adını göstermek için
                            KullaniciId = x.KullaniciId, // oturum açan kullanıcı eğer bu notu ekleyen kullanıcı ise güncelle butonu gelir, değilse güncelle butonu gelmez.
                
                        }).ToListAsync();
        }

        /// <summary>
        /// Üyenin kendi eklediği notları özet bilgilerle liste halinde getirir. Yeniden eskiye sıralar.
        /// </summary>
        /// <param name="kullaniciId"></param>
        /// <returns>Task<List<NotListeleVM>></returns>
        public async Task<List<NotListeleVM>> UyeyeOzelListele(string kullaniciId)
        {
            // kullanıcının kendi eklediği notların listelendiği metod
            return await table.Where(x => x.KullaniciId == kullaniciId)
                        .OrderByDescending(x => x.OlusturulmaTarihi)
                        .Select(x => new NotListeleVM
                        {
                            NotId = x.NotId,
                            Baslik = x.Baslik,
                            Fakulte = x.Bolum.Fakulte.FakulteAdi,
                            Bolum = x.Bolum.BolumAdi,
                            Ders = x.Ders.DersAdi,
                            KullaniciAd = x.Kullanici.AdSoyad, 
                            KullaniciId = x.KullaniciId, 

                        }).ToListAsync();
        }

        /// <summary>
        /// Kullanıcıdan gelen not bilgilerinin, yeni not oluşturularak kaydedilmesi.
        /// </summary>
        /// <param name="not"></param>
        /// <param name="kullaniciId"></param>
        /// <returns>Task</returns>
        public async Task NotEkle(NotEkleVM not, string kullaniciId)
        {
            // kullanıcıdan yeni not için verilerin alınması
            var yeniNot = new Not
            {
                Baslik = not.Baslik,
                Icerik = not.Icerik,
                BolumId = not.BolumId,
                DosyaYolu = FileOperations.UploadImage(not.Dosya), // dosya yükleme işlemi
                OlusturulmaTarihi = DateTime.Now,
                KullaniciId = kullaniciId,
                DersId = not.DersId
            };

            await EkleAsync(yeniNot);
        }

        /// <summary>
        /// Kullanıcıdan gelen yeni not bilgilerinin, güncellenmek üzere seçilen nota kaydedilmesi.
        /// </summary>
        /// <param name="not"></param>
        /// <returns>Task</returns>
        public async Task NotGuncelle(NotGuncelleVM not)
        {
            // güncellenecek notun bulunması
            var eskiNot = await NotBulEagerLoad(not.NotId);

            // database de kayıtlıysa yeni değerlerin atanması
            if (eskiNot != null)
            {
                eskiNot.Baslik = not.Baslik;
                eskiNot.Icerik = not.Icerik;
                eskiNot.BolumId = not.BolumId;
                eskiNot.DosyaYolu = not.DosyaYolu;
                eskiNot.GuncellenmeTarihi = DateTime.Now;
                eskiNot.DersId = not.DersId;

                await DegisiklikleriKaydetAsync();
            }
        }

        /// <summary>
        /// Nota ait tüm bilgilerin istenen nota göre getirilmesi.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<NotDetayVM?></returns>
        public async Task<NotDetayVM?> DetayGetir(int id)
        {
            return await table.Select(table => new NotDetayVM
            {
                NotId = table.NotId,
                Baslik = table.Baslik,
                Fakulte = table.Bolum.Fakulte.FakulteAdi, // fakülte bilgisi için
                Bolum = table.Bolum.BolumAdi,
                Ders = table.Ders.DersAdi,
                Icerik = table.Icerik,
                OlusturulmaTarihi = table.OlusturulmaTarihi,
                GuncellenmeTarihi = table.GuncellenmeTarihi,
                KullaniciAd = table.Kullanici.AdSoyad, // kullanıcı adını göstermek için
                KullaniciId = table.KullaniciId, // bu detay sayfasına gelen kullanıcı eğer bu notu ekleyen kullanıcı ise güncelle butonu gelir, değilse güncelle butonu gelmez.
                DosyaYolu = table.DosyaYolu
            }).Where(n => n.NotId == id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Not ekleme için kullanıcıya gösterilecek formu oluşturur. Kullanıcının bölümlerini ve dersleri forma aktarılır.
        /// </summary>
        /// <param name="kullaniciId"></param>
        /// <param name="kullanicininBolumleri"></param>
        /// <returns>NotEkleFormVM</returns>
        public NotEkleFormVM EklemeFormuOlustur(string kullaniciId, IEnumerable<KullaniciBolumVM> kullanicininBolumleri)
        {
            NotEkleFormVM form = new NotEkleFormVM
            {
                Dersler = BolumeGoreDersIcinSelecListOlustur(kullanicininBolumleri),
                Bolumler = BolumIcinSelectListOlustur(kullanicininBolumleri)
            };
            return form;
        }

        /// <summary>
        /// Not güncelleme için kullanıcıya gösterilecek formu oluşturur. Kitabın eski değerleri, kullanıcının bölümlerini ve dersleri forma aktarılır.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kullaniciId"></param>
        /// <param name="kullanicininBolumleri"></param>
        /// <returns>Task<NotGuncelleFormVM?></returns>
        public async Task<NotGuncelleFormVM?> GuncellemeFormuOlustur(int id, string kullaniciId, IEnumerable<KullaniciBolumVM> kullanicininBolumleri)
        {
            var not = await NotBulEagerLoad(id);
            if (not != null && not.Kullanici.Id == kullaniciId) // kendi kitabıysa güncelleyebilir 
            {
                var bolumler = table.Include(x => x.Kullanici).ThenInclude(k => k.Bolumler).ThenInclude(b => b.Bolum);
                NotGuncelleFormVM form = new NotGuncelleFormVM
                {
                    Not = new NotGuncelleVM
                    {
                        NotId = not.NotId,
                        Baslik = not.Baslik,
                        Icerik = not.Icerik,
                        BolumId = not.BolumId,
                        DersId = not.DersId,
                        DosyaYolu = not.DosyaYolu
                    },

                    Dersler = BolumeGoreDersIcinSelecListOlustur(kullanicininBolumleri),
                    Bolumler = BolumIcinSelectListOlustur(kullanicininBolumleri)
                };
                return form;
            }

            return null;
        }

        /// <summary>
        /// Include metodu ile eager loading işleminin tekrarı önlemek için metodlaştırılmış şekli. Yardımcı metoddur.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<Not></returns>
        public async Task<Not> NotBulEagerLoad(int id)
        {
            var not = await table.Include(x => x.Ders)
                .Include(x => x.Kullanici) // ders bilgilerini de alabilmek için
                .Where(x => x.NotId == id).SingleOrDefaultAsync();
            return not;
        }

        /// <summary>
        /// Kullanıcıya ait bölümlerin listeleneceği selectlist için oluşturulan metod. (Çünkü kullanıcı kendi bölümüne ait derslere not ekleyebilir.)
        /// </summary>
        /// <param name="kullanicininBolumleri"></param>
        /// <returns>SelectList</returns>
        public SelectList BolumIcinSelectListOlustur(IEnumerable<KullaniciBolumVM> kullanicininBolumleri) // accountservice te bu bölümler getirilecek, controllerda parametreye aktarılacak
        {
            return new SelectList(kullanicininBolumleri, "BolumId", "BolumAdi"); 
        }

        /// <summary>
        /// Kullanıcıya ait bölümlere göre derslerin listeleneceği selectlist için oluşturulan metod.
        /// </summary>
        /// <param name="kullanicininBolumleri"></param>
        /// <returns>SelectList</returns>
        public SelectList BolumeGoreDersIcinSelecListOlustur(IEnumerable<KullaniciBolumVM> kullanicininBolumleri)
        {
            return new SelectList(bolumRepository.BolumDersleriniGetir(kullanicininBolumleri), "DersId", "DersAdi");
        }
    }
}
