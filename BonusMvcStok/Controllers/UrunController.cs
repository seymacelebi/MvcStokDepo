using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;

namespace BonusMvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(string p)
        {
            //var urunler = db.TBLURUNLER.Where(x=>x.durum==true).ToList();
            var urunler = db.TBLURUNLER.Where( x => x.durum == true);
            if (!string.IsNullOrEmpty(p))
            {
                //eğer benim gönderdiğim parametre değeri boş değilse
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum == true);
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.TBLKATEGORI.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER t)
        {
            var ktgr = db.TBLKATEGORI.Where(x => x.id == t.id).FirstOrDefault();
            t.TBLKATEGORI = ktgr;
            db.TBLURUNLER.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.TBLKATEGORI.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList() ;
            var ktgr = db.TBLURUNLER.Find(id);
            ViewBag.urunkategori = kat;
            return View("UrunGetir", ktgr);
        }
        public ActionResult UrunGuncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.id);
            urun.marka = p.marka;
            urun.satisfiyat = p.satisfiyat;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.ad = p.ad;
            var ktg = db.TBLKATEGORI.Where(x => x.id == p.TBLKATEGORI.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(TBLURUNLER p1)
        {
            var urunbul = db.TBLURUNLER.Find(p1.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}