using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Okul.Models
{
    public class Kullanici
    {
        //primary key 
        [Key]
        public int Id { get; set; }
        //görüntü adını kullanıcı adı olarak göreceğiz
        //birşey girilmezse hata verecek
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı Girilmelidir")]
        public string Kadi { get; set; }
//göürntü ismi şifre olarak göreceğiz
//birşey girilmezse hata mesajı verilecek
//şifre girilirken yazılan yerine * şeklinde gözükecek yazılanlar
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre Girilmelidir")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
        //admin mi user mi o belirlenecek
        public string Turu { get; set; }

    }
}
