using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teknolojihaberleri.Models
{
    public class Yorum
    {
        public string Id { get; set; }
        public string Icerik { get; set; }
        public string Ilgilihaber_id { get; set; }
        public string Zaman { get; set; }
    }
}