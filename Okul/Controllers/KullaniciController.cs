using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Okul.Data;
using Okul.Models;

namespace Okul.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly VeritabaniContext context;

        public KullaniciController(VeritabaniContext context)
        {
            //dependency injection için kullanıldı
            //veritbanından referansa ihtiyac vardı 
            this.context = context;
        }
        //get
        public IActionResult giris()
        {
            return View();
        }
        //girişe basınca burası çalışacak
        [HttpPost]
        public IActionResult giris(Kullanici kullanici)
        {
            //veritabanı ile yazılan birbirine uyuyorsa onu bir değişkene atadık
            var girisyapankullanici = context.kullanicilar
                .Where(x => x.Kadi == kullanici.Kadi && x.Sifre == kullanici.Sifre).SingleOrDefault();
            if (girisyapankullanici != null)//giris yapan kullanıcı bos değilse veya varsa
            {
                //çekilen kullanıcı bilgilerini sesionsa yüklüyoruz 
                HttpContext.Session.SetString("Kadi",girisyapankullanici.Kadi);
                HttpContext.Session.SetString("Turu",girisyapankullanici.Turu);
            }

            return RedirectToAction("Index","Sınıf");//ve daha sonrasında sınıf kategorisindeki index acitonuna yönlendirecek
        }
        //çıkış actionu
        public IActionResult cikis()
        {
            HttpContext.Session.Clear();//yüklenenen sessionları temizler

            return RedirectToAction("Index","Sınıf"); //temizledikten sonra bizi sınıfın indexine yönlendirme yapacak
        }
    }
}