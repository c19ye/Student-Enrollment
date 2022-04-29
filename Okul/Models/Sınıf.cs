using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Okul.Models
{
    public class Sınıf
    {
        //bir sınıfın eşsiz id si olması lazım.id tanımladık ve birincil olması için key data annotationsunu ekledik
        [Key]
        public int Id { get; set; }
        //Bir sınıfın adı olmalı bu yüzden adi propertysini ekliyoruz
        public string Adi { get; set; }

        //sınıfın birden fazla öğrencisi olmasını istiyoruz bu yüzden liste şekilde yapmamız lazım
        //bu şekilde sınıf ve ogrenciyi ilişkilendiriyoruz
        //bire çok ilişki yaptık
        public List<Ogrenci> ogrencileri { get; set; }



    }
}
