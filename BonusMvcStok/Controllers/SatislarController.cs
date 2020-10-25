using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;
namespace BonusMvcStok.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index()
        {
            var satislar = db.TBLSATISLAR.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            //Ürünler
            List<SelectListItem> urun = (from x in db.TBLURUNLER.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop1 = urun;
            //Personel

            List<SelectListItem> per = (from x in db.TBLPERSONEL.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad + " " + x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop2 = per;
            //Müşteriler
            List<SelectListItem> must = (from x in db.TBLMUSTERI.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad + " " + x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop3 = must;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR p)
        {
            var urun = db.TBLURUNLER.Where(x => x.id == p.TBLURUNLER.id).FirstOrDefault();
            var musteri = db.TBLMUSTERI.Where(x => x.id == p.TBLMUSTERI.id).FirstOrDefault();
            var personel= db.TBLPERSONEL.Where(x => x.id == p.TBLPERSONEL.id).FirstOrDefault();
            p.TBLURUNLER = urun;
            p.TBLMUSTERI = musteri;
            p.TBLPERSONEL = personel;
            p.tarih =DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLSATISLAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}