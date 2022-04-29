using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Okul.Data;
using Okul.Models;

namespace Okul.Controllers
{
    public class OgrencisController : Controller
    {
        private readonly VeritabaniContext _context;

        private readonly IHostingEnvironment _hosting;
        //dependecy injecetion 
        public OgrencisController(VeritabaniContext context,IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        // GET: Ogrencis
        //bir ogrenci eklenince o sınfın ogrencilerini gostersin istiyoruz bu yüzden indexi sildik tumogrenciler gibi anlamlı bi isim yazdık
        public async Task<IActionResult> tumogrenciler()
        {
            return View(await _context.ogrenciler.ToListAsync());
        }
        //dropdownlistten seçilenlerin kategoriye taşıma actionu
        //int id sildik ve ıformcolletion yazdık çünkü name parametresinden gelen değerleri alıp işlem yapacak
        //submit edildiği zaman çalışacağı için httppost ekliyoruz
        [HttpPost]
        public async Task<IActionResult> toplutasi(IFormCollection collection)
        {
            //string keyler
            //collectionda oluşan seciliogrenciler dizisini lsiteleyeccek
            //bilgiler string ve birden fazla eleman geleceği için değişkene aktarıyoruz
            var seciliogrenciler = collection["seciliogrenciler"].ToList();
           var hedefsınıfid= Convert.ToInt32(collection["hedefsınıfid"].ToString());
           //seciliogrenciler ile işlem yapacağımız için döngüye ihtiyacımız var.seçtiğimiz öğrencielri alacak
           //veritabanı işlem yapılacağı için döngünün içine kodumuzu yazıyoruz
           //işimiz id numarası ile ise yani primary key ise where yerine find kullanabiliriz 
           foreach (var item in seciliogrenciler)
           {
               var ogrenci = await _context.ogrenciler.FindAsync(Convert.ToInt32(item));
               //sınıfid değiştirmek istediğimiz için eski sınıfı yeni sınıfa taşıyacağız
               ogrenci.sınıfid = hedefsınıfid;

           }
           //bu yaptıklarımızın gerçek veritabanına yazılması için
           await _context.SaveChangesAsync();
           //taşıdığımız sınıfa girmek için 
            return RedirectToAction(nameof(sınıfınogrencileri),new{id=hedefsınıfid});
        }
        //submit edildiği zaman çalışacağı için httppost ekliyoruz
        [HttpPost]
        public async Task<IActionResult> toplusil(IFormCollection collection)
        {
            //string keyler
            //collectionda oluşan seciliogrenciler dizisini lsiteleyeccek
            //bilgiler string ve birden fazla eleman geleceği için değişkene aktarıyoruz
            var seciliogrenciler = collection["seciliogrenciler"].ToList();
            //0 değerini verdik çünkü hic öğrenci yoksa returnde hata verir
            int sınıfid = 0;
            //seciliogrenciler ile işlem yapacağımız için döngüye ihtiyacımız var.seçtiğimiz öğrencielri alacak
            //veritabanı işlem yapılacağı için döngünün içine kodumuzu yazıyoruz
            //işimiz id numarası ile ise yani primary key ise where yerine find kullanabiliriz 
            foreach (var item in seciliogrenciler)
            {
                var ogrenci = await _context.ogrenciler.FindAsync(Convert.ToInt32(item));
                //içinde bulunan ogrencilerin sınıf id si aktarılacak
                sınıfid = ogrenci.sınıfid;
                //seçilen öğrencileri kaldırıyor
                _context.ogrenciler.Remove(ogrenci);
                

            }
            //bu yaptıklarımızın gerçek veritabanına yazılması için
            await _context.SaveChangesAsync();
            //sildiğimiz sınıfa girmek için 
            return RedirectToAction(nameof(sınıfınogrencileri), new { id = sınıfid});
        }
        //butona yazdığımız actionun ismiyle bir action oluşturmaya çalışıyoruz o yüzden ismini sınıfınogrencileri yaptık
        public async Task<IActionResult> sınıfınogrencileri(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

                
            var sınıf = await _context.siniflar
                //bağlam kurabilmesi için özellikle sorguya ifade eklememiz gerekiyordu
                //performans için dahil edilmiyor ama biz kendimiz dahil ettiriyoruz
                .Include(x => x.ogrencileri)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (sınıf == null)
            {
                return NotFound();
            }
            //sınfın öğrencilerini göstermesi için viewe sınıf.ogrenciler yazdık
            return View(sınıf);
        }

        // GET: Ogrencis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.ogrenciler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        // GET: Ogrencis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ogrencis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //create butonundan sınıfın id sini almıştık ve post işlemi gerçekleşince actiona ulaşacak
        //bind daki id siliyoruz işimiz yok ve yazdığımız id ile çakışıyor bu yüzden siliyoruz 
        public async Task<IActionResult> Create(int? id ,[Bind("Adi,Soyadi,Okulno,Dosya,Aciklama")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                //eğer resimler klasörü yoksa rootun içinde oluşturuyor
                var dosyaYolu = Path.Combine(_hosting.WebRootPath, "resimler");
                //eğer böyle bir klasör yoksa böyle bir klasör oluştur diyoruz
                if (!Directory.Exists(dosyaYolu))
                {
                    Directory.CreateDirectory(dosyaYolu);
                }
                //viewden gelen dosyanın içeriğini serverdaki filestreama kopyalanmasını sağlıyoruz
                using (var dosyaAkisi =
                    new FileStream(Path.Combine(dosyaYolu, ogrenci.Dosya.FileName), FileMode.Create))
                {
                    await ogrenci.Dosya.CopyToAsync(dosyaAkisi);
                }

                //dosya ismini de veritabanındaki ismi kısmına yazdırıyoruz
                ogrenci.Resim = ogrenci.Dosya.FileName;


                //bu ifleri kopyaladık çünkü id yoksa eklenmemesi için bulunamadı hatası döndürecek yani
                if (id == null)
                {
                    return NotFound();
                }

                var sınıf = await _context.siniflar.FindAsync(id);
                if (sınıf == null)
                {
                    return NotFound();
                }
                
                //buradaki oluşturulan sınıf ogrencinin sınıfına aktarıyor
                ogrenci.sinif = sınıf;
                _context.Add(ogrenci);
                await _context.SaveChangesAsync();
                //new id yazdık çünkü eklenen sınıfın id si e yönlendirecek yani aldığı id değerini id ye aktarıp o sınıfa yönlendirecek
                return RedirectToAction(nameof(sınıfınogrencileri),new {id=id});
            }
            return View(ogrenci);
        }

        // GET: Ogrencis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);
        }

        // POST: Ogrencis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //bindde sınıfid ekledik çünkü post edilirken o sınıf id sini bağlayacak
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,Soyadi,Okulno,sınıfid")] Ogrenci ogrenci)
        {
            if (id != ogrenci.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogrenci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgrenciExists(ogrenci.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //düzenlendiği zaman ait olduğu sınıfa yönlendirmesini istediğimiz için sınıfid sini alıp id ye aktarıyoruz
                return RedirectToAction(nameof(sınıfınogrencileri),new {id=ogrenci.sınıfid});
            }
            return View(ogrenci);
        }

        // GET: Ogrencis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.ogrenciler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        // POST: Ogrencis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ogrenci = await _context.ogrenciler.FindAsync(id);
            _context.ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(sınıfınogrencileri),new {id=ogrenci.sınıfid});
        }

        private bool OgrenciExists(int id)
        {
            return _context.ogrenciler.Any(e => e.Id == id);
        }
    }
}
