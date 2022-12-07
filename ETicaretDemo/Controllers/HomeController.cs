using ETicaretDemo.Dto;
using ETicaretDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ETicaretDemo.Controllers
{
    
    public class HomeController : Controller
    {
        Context con = new Context();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Home()
        {
            List<Urun> urunlerim = new List<Urun>();
            urunlerim = con.Urunler.ToList();

            List<Sepet> sepetim = new List<Sepet>();
            sepetim = con.Sepetim.ToList();

            HomeDto list = new HomeDto();
            list.Sepetim = sepetim;
            list.Urunlerim = urunlerim;

            return View(list);
        }

        [HttpPost]
        public IActionResult SepeteEkle(int id, int adet)
        {
            Urun urun = con.Urunler.Where(u => u.Id == id).FirstOrDefault();

            Sepet sepet = new Sepet();
            sepet.UrunAdi = urun.UrunAdi;
            sepet.Adet = adet;
            sepet.BirimFiyati = urun.BirimFiyati;
            sepet.Toplam = sepet.Adet * sepet.BirimFiyati;
            con.Sepetim.Add(sepet);
            con.SaveChanges();
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public IActionResult SepettekiUrunuSil(int id)
        {
            Sepet sepet = con.Sepetim.Where(u => u.Id == id).FirstOrDefault();
            con.Sepetim.Remove(sepet);
            con.SaveChanges();
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public IActionResult OdemeYap(int id)
        {
            var result = con.Sepetim.ToList();
            foreach (var item in result)
            {
                con.Sepetim.Remove(item);
                con.SaveChanges();
            }
            TempData["odeme"] = "Ödeme yapıldı";
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            return RedirectToAction("Home", "Home");
        }

    }
}