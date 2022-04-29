using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Okul.Models
{
    public class Ogrenci
    {
        //her öğrencinin kendine ait bir id si olması lazım.İd property oluşturuyoruz
        //key data annotationsu tanımladık çünkü birincil olmasını istiyoruz
        [Key]
        public int Id { get; set; }
        //Öğrencinin adı olmalı
        public string Adi { get; set; }
        //Öğrencinin soyadı olmalı
        public string Soyadi { get; set; }
        //ve öğrenciye ait okul numarasını tanımlıyoruz
        public int Okulno { get; set; }
        //bir öğrenci bir sınıfta olabilir o yüzden sınıf modelinden bir property oluşturduk
        public Sınıf sinif { get; set; }
        //value ye verilen sınıf id si sınıf ile direkt referansla eşleşemediği için scaler id lazımdı
        //yani int şeklinde tanımlarsak scaler olur ama sınıfın ismini yazsaydık referansını almış olacaktık bu yüzden scaler sınıf id belirledik
        public int sınıfid { get; set; }
        //resmin ismini veritabanında tutacak resmi dğeil
        public string Resim { get; set; }
        //servere upload edilecek amanotmapped dersek veritabanına temsil edilmesini istemediğimizi belirtiriz
        [NotMapped]
        public IFormFile Dosya { get; set; }

        public string Aciklama { get; set; }





    }
}
