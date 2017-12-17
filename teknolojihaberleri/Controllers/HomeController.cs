using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using teknolojihaberleri.Models;
using System.Drawing;

namespace teknolojihaberleri.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Request.QueryString["a"] != null && Request.QueryString["b"] != null)
            {
                string a = Request.QueryString["a"];
                string b = Request.QueryString["b"];
                var model = HaberListesiCek(a, b);
                return View(model);
            }
            else
            {
                var model = HaberListesiCek("1", "10");
                return View(model);
            }
        }

        public HaberSayfasiCek HaberListesiCek(string a, string b)
        {
            HaberSayfasiCek haberlistesi = new HaberSayfasiCek();
            Haber haber = new Haber();
            Yorum yorum = new Yorum();

            string sql = "Select * From kayitlar Where id>=@a AND id<=@b Order By id DESC";
            SqlCommand komut = new SqlCommand(sql, Baglan());
            komut.Parameters.AddWithValue("@a", a);
            komut.Parameters.AddWithValue("@b", b);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                haber.Id = dr["id"].ToString();
                haber.Baslik = dr["baslik"].ToString();
                haber.Konu = dr["konu"].ToString();
                haber.Ozet = dr["ozet"].ToString();
                haber.Icerik = dr["icerik"].ToString();
                haber.Zaman = dr["yayin_saat"].ToString();
                haberlistesi.Haber_Listesi.Add(haber);
                haber = new Haber();
            }

            string sqli = "Select * From yorumlar";
            komut = new SqlCommand(sqli, Baglan());
            dr = komut.ExecuteReader();
            while(dr.Read())
            {
                yorum.Icerik = dr["yorum"].ToString();
                yorum.Zaman = dr["zaman"].ToString();
                haberlistesi.Yorumlar.Add(yorum);
                yorum = new Yorum();
            }

            return haberlistesi;
        }

        public HaberSayfasiCek HaberCek(string haber_id)
        {
            HaberSayfasiCek habersayfasi = new HaberSayfasiCek();
            Haber haber = new Haber();
            Yorum yorum = new Yorum();

            string habersql = string.Empty;
            string yorumsql = string.Empty;
            SqlCommand komut;
            SqlDataReader dr;

            habersql = "Select * From kayitlar Where id=@id Order By id DESC";
            komut = new SqlCommand(habersql, Baglan());
            komut.Parameters.AddWithValue("@id", haber_id);
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                habersayfasi.Ilgili_Haber.Id = dr["id"].ToString();
                habersayfasi.Ilgili_Haber.Baslik = dr["baslik"].ToString();
                habersayfasi.Ilgili_Haber.Konu = dr["konu"].ToString();
                habersayfasi.Ilgili_Haber.Ozet = dr["ozet"].ToString();
                habersayfasi.Ilgili_Haber.Icerik = dr["icerik"].ToString();
                habersayfasi.Ilgili_Haber.Zaman = dr["yayin_saat"].ToString();
           }

            yorumsql = "Select * From yorumlar Order By id DESC";
            komut = new SqlCommand(yorumsql, Baglan());
            dr = komut.ExecuteReader();
            while(dr.Read())
            {
                yorum.Icerik = dr["yorum"].ToString();
                yorum.Zaman = dr["zaman"].ToString();
                habersayfasi.Yorumlar.Add(yorum);
                yorum = new Yorum();
            }

            return habersayfasi;
        }

        public ActionResult HaberGoster()
        {
            if (Request.QueryString["id"] != null)
            {
                string haber_id = Request.QueryString["id"];
                var model = HaberCek(haber_id);
                if (model != null)
                    return View(model);
                else
                    return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult BenKimim()
        {
            return View();
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult Admin_Login()
        {
            return View();
        }

        public ActionResult Haber_Yorum_Ekle(string txtyorum)
        {
            string yorum_icerik = txtyorum.ToString();
            string sql = "Insert Into yorumlar(yorum,zaman) Values (@yorum,@zaman)";
            SqlCommand komut = new SqlCommand(sql, Baglan());
            komut.Parameters.AddWithValue("@yorum", yorum_icerik);
            komut.Parameters.AddWithValue("@zaman", DateTime.Now);
            komut.ExecuteNonQuery();
            if (ViewBag["habergoster_id"] != null)
            {
                string ilgilihaber_id = ViewBag["habergoster_id"];
                return RedirectToAction("HaberGoster?id=" + ilgilihaber_id);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Index_Yorum_Ekle(string txtyorum)
        {
            string yorum_icerik = txtyorum.ToString();
            string sql = "Insert Into yorumlar(yorum,zaman) Values (@yorum,@zaman)";
            SqlCommand komut = new SqlCommand(sql, Baglan());
            komut.Parameters.AddWithValue("@yorum", yorum_icerik);
            komut.Parameters.AddWithValue("@zaman", DateTime.Now);
            komut.ExecuteNonQuery();

            return RedirectToAction("Index");
        }

        public SqlConnection Baglan()
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=mssql11.domainhizmetleri.com;Initial Catalog=sametsen_turk;User ID=sametsen_senturk;Password=iE1abd439");
            baglanti.Open();
            return baglanti;
        }

        public void Ekle_Iletisim(string ad, string email, string tel, string mesaj)
        {
            string sql = "Insert Into iletisim(ad,email,tel,mesaj) Values (@ad,@email,@tel,@mesaj)";
            SqlCommand komut = new SqlCommand(sql, Baglan());
            komut.Parameters.AddWithValue("@ad", ad);
            komut.Parameters.AddWithValue("@email", email);
            komut.Parameters.AddWithValue("@tel", tel);
            komut.Parameters.AddWithValue("@mesaj", mesaj);
            komut.ExecuteNonQuery();
        }

        public ActionResult Yonlendir_AnaSayfa()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Yonlendir_Ben()
        {
            return RedirectToAction("BenKimim");
        }

        public ActionResult Yonlendir_Iletisim()
        {
            return RedirectToAction("Iletisim");
        }

        public ActionResult Iletisim_Gonder(string txtad, string txtemail, string txttel, string txtmesaj)
        {
            string ad = txtad;
            string email = txtemail;
            string tel = txttel;
            string mesaj = txtmesaj;
            Ekle_Iletisim(ad, email, tel, mesaj);
            return RedirectToAction("Iletisim");
        }

    }
}