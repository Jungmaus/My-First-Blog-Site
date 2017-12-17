using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teknolojihaberleri.Models
{
    public class HaberSayfasiCek
    {
        public Haber Ilgili_Haber { get; set; }
        public List<Yorum> Yorumlar { get; set; }
        public List<Haber> Haber_Listesi { get; set; }
        public HaberSayfasiCek()
        {
            Ilgili_Haber = new Haber();
            Yorumlar = new List<Yorum>();
            Haber_Listesi = new List<Haber>();
        }
    }
}