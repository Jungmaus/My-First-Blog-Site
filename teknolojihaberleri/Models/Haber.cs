using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace teknolojihaberleri.Models
{
    public class Haber
    {
        public string Id { get; set; }
        public string Baslik { get; set; }
        public string Konu { get; set; }
        public string Ozet { get; set; }
        public string Zaman { get; set; }
        public string Icerik { get; set; }
    }
}