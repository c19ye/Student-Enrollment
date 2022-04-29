using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Okul.Models;

namespace Okul.Data
{
    //Dbcontexten kalıtım yoluyla bu sınıfa aktarıyoruz özellikleri
    public class VeritabaniContext:DbContext
    {
        //bu context sınıfı doğrudan işlem yapmayacak bu yüzden base yazarak yukarıya pasladık
        //dbcontext options ile sınıfın içinde gerekli işlemleri yaptıracak
        public VeritabaniContext(DbContextOptions<VeritabaniContext>options):base(options)
        {
            
        }
        //ogrenci modelini ogrenciler isimli gelişmiş bir listeye aktarıyoruz.
        //ogrenciler dedik çünkü bu bir liste oluşturacak
        public DbSet<Ogrenci> ogrenciler { get; set; }
        //sınıf modelini sınıflar isimli gelişmiş bir listeye aktarıyoruz çünkü entityframework listesiyle işlemler yapacağız
        //siniflar adedik çünkü birden fazla sınıf olacak ve bu bir liste oluşturacak
        public DbSet<Sınıf> siniflar { get; set; }
        //Kullanici modelini kullanicilar isimli gelişmiş bir listeye aktarıyoruz çünkü entityframework listesiyle işlemler yapacağız
        //kullanicilar dedik çünkü birden fazla kullanici olacak ve bu bir liste oluşturacak
        public DbSet<Kullanici> kullanicilar { get; set; }
    }
}
