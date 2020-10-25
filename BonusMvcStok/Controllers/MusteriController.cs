using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace BonusMvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            // var musteriliste = db.TBLMUSTERI.ToList();
            var musteriliste = db.TBLMUSTERI.Where(x=>x.durum==true).ToList().ToPagedList(sayfa,3);
            return View(musteriliste);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERI p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.durum = true;
            db.TBLMUSTERI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(TBLMUSTERI p1)
        {
            var musteribul = db.TBLMUSTERI.Find(p1.id);
            musteribul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            //List<SelectListItem> kat = (from x in db.TBLMUSTERI.ToList()
            //                            select new SelectListItem
            //                            {
            //                                Text = x.ad,
            //                                Value = x.id.ToString()
            //                            }).ToList();
            var mus = db.TBLMUSTERI.Find(id);          
            return View("MusteriGetir", mus);
        }
        public ActionResult MusteriGuncelle(TBLMUSTERI t)
        {
            var mus = db.TBLMUSTERI.Find(t.id);
            mus.ad = t.ad;          
            mus.soyad = t.soyad;
            mus.sehir = t.sehir;
            mus.bakiye = t.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}