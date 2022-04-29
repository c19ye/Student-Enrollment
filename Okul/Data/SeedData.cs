using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Okul.Models;

namespace Okul.Data
{
    public static class SeedData
    {
        //yeni bir metod oluşturuyoruz ve bu metodda verileri tohumlama yapacağız
        //seed metodunda context elde etmemiz lazımdı bu yüzden veritabanıcontexti elde ettik
        public static void seed(IApplicationBuilder app)
        {
            VeritabaniContext context = app.ApplicationServices.GetRequiredService<VeritabaniContext>();
            //bekleyen migrationları yürürlüğe koymak için yazdık.yani update-database işini bizim yerimize yapacak ve bizim update-database yazmamıza gerek kalmayacak.
            context.Database.Migrate();
            
            //any diyerek eğer ogrenciler tablosu boşsa kayıtları eklesin demek 
            //normaldee context.ogrenciler.add diyerek ekleyebilirdik ama biz birden fazla kayıt eklemek istiyoruz
            if (!context.ogrenciler.Any())
            {
                var siniflar = new Sınıf[]
                {
                    //yeni bir sınıf modelini alarak adinin ne olması gerektiğini belirttik
                    new Sınıf{Adi = "1A"},
                    new Sınıf{Adi = "1B"},
                    new Sınıf{Adi = "1C"},

                };
                //birden çok ürün girdiğimiz için addrange dedik ve hangi aralıktaki kayıtları eklemek istediğimizi belirttik sınıflar değişkeni diyerek
                context.AddRange(siniflar);
                //birden fazla ogrenci yukleyecegımız icin bir dizi olusturduk ve onu ogrenciler degiskenine aktardık
                var ogrenciler = new Ogrenci[]
                {
                    //burada yeni bir ogrenci olustur diyoruz ve model sınıfından ogrencinin neye benzediğini bildiği için propertylerini karşımıza çıkardı
                    //c# tarafından id ile bağlayamacağımız için id yerine sınıf dizisinin referansını almamız lazım yani dizinin indisiyle bağladık
                    //0 indisi 1a 1 indisi 1b 2 indisi 1c yi temsil ediyor
                    new Ogrenci{Adi = "Yunus", Soyadi = "Emre" , Okulno = 1733150, sinif = siniflar[0]}, 
                    new Ogrenci{Adi = "Ahmet", Soyadi = "Çokçalışır" , Okulno = 1733151, sinif = siniflar[1]},
                    new Ogrenci{Adi = "Mehmet", Soyadi = "Hiçdurmaz" , Okulno = 1733152,sinif = siniflar[2]},

                };
                //contexte bu aralıktakileri al diyoruz.birden fazla kayıt oldugu için addrange dedik ve ogrencileri eklesin dedik
                context.AddRange(ogrenciler);
                //şuanda yapılanlar listeye aktarır ama savechanges dersek gerçek veritabanımıza kaydettirir yani sqlite deki veri.db ye
                context.SaveChanges();
            }

        }
    }
}
