using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.ViewModels.Notlar;
using static NuGet.Packaging.PackagingConstants;

namespace NotDefteriPlusMVC.Repositories
{
    /// <summary>
    /// notlar ile ilgili ortak metodlar için base repositoryden, kendi metodları için interface den miras alarak metod içlerinin doldurulacağı repository sınıfı
    /// </summary>
    public class NotRepository : BaseRepository<Not>, INotRepository
    {
        private readonly IDersRepository dersRepository;
        private readonly IBolumRepository bolumRepository;
        public NotRepository(NotDefteriDbContext context, IDersRepository _dersRepository, IBolumRepository _bolumRepository) : base(context)
        {
            dersRepository = _dersRepository;
            bolumRepository = _bolumRepository;
        }
        public List<NotListeleVM> TumNotlar()
        {
            // home sayfasında kullanıcı olan veya olmayan tüm ziyaretçilerin göreceği liste
            return table.Select(x => new NotListeleVM
            {
                NotId = x.NotId,
                Baslik = x.Baslik,
                Fakulte = x.Bolum.Fakulte.FakulteAdi,
                Bolum = x.Bolum.BolumAdi,
                Ders = x.Ders.DersAdi,
                KullaniciAd = x.Kullanici.AdSoyad, // kullanıcı adını göstermek için
                KullaniciId = x.KullaniciId, // oturum açan kullanıcı eğer bu notu ekleyen kullanıcı ise güncelle butonu gelir, değilse güncelle butonu gelmez.
                
            }).ToList();
        }
        public List<NotListeleVM> UyeyeOzelListele(string kullaniciId)
        {
            // kullanıcının kendi eklediği notların listelendiği metod
            return TumNotlar().Where(x => x.KullaniciId == kullaniciId).ToList();
        }
        public void NotEkle(NotEkleVM not, string kullaniciId)
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

            Ekle(yeniNot);
        }
        public void NotGuncelle(NotGuncelleVM not)
        {
            // güncellenecek notun bulunması
            var eskiNot = NotBulEagerLoad(not.NotId);

            // database de kayıtlıysa yeni değerlerin atanması
            if (eskiNot != null)
            {
                eskiNot.Baslik = not.Baslik;
                eskiNot.Icerik = not.Icerik;
                eskiNot.BolumId = not.BolumId;
                eskiNot.DosyaYolu = not.DosyaYolu;
                eskiNot.GuncellenmeTarihi = DateTime.Now;
                eskiNot.DersId = not.DersId;

                if (not.Dosya != null)
                    eskiNot.DosyaYolu = FileOperations.UploadImage(not.Dosya);

                context.SaveChanges();
            }
        }
        public NotDetayVM? DetayGetir(int id)
        {
            // hem ortak listede hem üyeye özel listede detay için çalışacak metod
            return table.Select(table => new NotDetayVM
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
            }).Where(n => n.NotId == id).SingleOrDefault();
        }
        public NotEkleFormVM EklemeFormuOlustur(string kullaniciId, IEnumerable<KullaniciBolumVM> kullanicininBolumleri)
        {
            // tüm derslerin forma aktarılması
            NotEkleFormVM form = new NotEkleFormVM
            {
                Dersler = DersIcinSelectListOlustur(), 
                Bolumler = BolumIcinSelectListOlustur(kullanicininBolumleri)
            };
            return form;
        }

        public NotGuncelleFormVM? GuncellemeFormuOlustur(int id, string kullaniciId, IEnumerable<KullaniciBolumVM> kullanicininBolumleri)
        {
            // güncellenmek üzere seçilmiş kitabın eski değerlerinin ve kategorilerin forma aktarılması
            var not = NotBulEagerLoad(id);
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

                    Dersler = BolumeOzelDersIcinSelectListOlustur(not.Kullanici.Bolumler),
                    Bolumler = BolumIcinSelectListOlustur(kullanicininBolumleri)
                };
                return form;
            }

            return null;
        }

        public Not NotBulEagerLoad(int id)
        {
            // güncelleme işlemi ve formu oluşturma metodları için tekrarı önlemek için oluşturulan yardımcı metod
            var not = table.Include(x => x.Ders)
                .Include(x => x.Kullanici) // ders bilgilerini de alabilmek için
                .Where(x => x.NotId == id).SingleOrDefault();
            return not;
        }

        public SelectList DersIcinSelectListOlustur()
        {             
            // derslerin listeleneceği selectlist için oluşturulan metod
            return new SelectList(dersRepository.Listele(), "DersId", "DersAdi"); // derslerin listesi dropdown için selectlist e aktarılıyor
        }

        public SelectList BolumIcinSelectListOlustur(IEnumerable<KullaniciBolumVM> kullanicininBolumleri) // accountservice te bu bölümler getirilecek, controllerda parametreye aktarılacak
        {
            // kullanıcıya ait bölümlerin listeleneceği selectlist için oluşturulan metod (çünkü kullanıcı kendi bölümüne ait derslere not ekleyebilir.)
            return new SelectList(kullanicininBolumleri, "BolumId", "BolumAdi"); 
        }

        public SelectList BolumeOzelDersIcinSelectListOlustur()
        {

        }
    }
}
