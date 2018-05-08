using DKXISG.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DKXISG.Controllers
{
    public class UzmanController : BaseController
    {
        dkxisgContext db = new dkxisgContext();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Firmalar()
        {
            List<FirmaUzman> fu = db.FirmaUzmen.Where(x => x.UzmanID == benuzman.Id).ToList();
            List<Firma> firmalar = new List<Firma>();
            foreach (var item in fu)
            {
                firmalar.Add(item.Firma);
            }
            return View(firmalar);
        }
        public ActionResult FirmaDetay(int? id)
        {
            try
            {
                Firma firma = db.Firmas.Find(id);

                ViewBag.yapilacaklar = db.FirmaYapilacaks.Where(x => x.FirmaID == id && x.UzmanID == benuzman.Id).ToList();
                ViewBag.ben = benuzman;


                FirmaUzman fu = db.FirmaUzmen.FirstOrDefault(x => x.UzmanID == benuzman.Id && x.FirmaID == firma.Id);
                if (fu != null)
                {
                    return View(firma);
                }
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Firmalar");
        }
        [HttpPost]
        public PartialViewResult _YapilacakDetayGetir(int? id)
        {
            try
            {
                FirmaYapilacak fy = db.FirmaYapilacaks.Find(id);
                if (fy.UzmanID != benuzman.Id) return null;
                return PartialView(fy);
            }
            catch
            {

            }
            return null;
        }
        [HttpPost]
        public string YapilacakTamamlandi(int? id)
        {
            try
            {
                FirmaYapilacak fy = db.FirmaYapilacaks.Find(id);
                if (fy.UzmanID == benuzman.Id)
                {
                    fy.isTamam = true;
                    fy.okTarih = DateTime.Now;
                    db.SaveChanges();
                    return "1";
                }
            }
            catch { }
            return "0";
        }

        [HttpPost]
        public JavaScriptResult YeniYapilacak(FirmaYapilacak yap)
        {
            try
            {
                FirmaUzman fu = db.FirmaUzmen.FirstOrDefault(x => x.FirmaID == yap.FirmaID && x.UzmanID == benuzman.Id);
                if (fu == null) return hata("Bu işlem için yetkiniz bulunmuyor");
                yap.UzmanID = benuzman.Id;
                yap.Tarih = DateTime.Now;
                yap.isTamam = false;
                db.FirmaYapilacaks.Add(yap);
                db.SaveChanges();
                return onayyenile("Hatırlatmanız başarıyla kaydedildi");
            }
            catch { }
            return hata("Yapılacak kaydedilemedi. Lütfen tüm alanarı doldurduğunuzdan emin olup tekrar deneyin");
        }

        public ActionResult Egitimler()
        {
            List<Egitim> egitimler = db.Egitims.Where(x => x.EgitimEkleyen == benuzman.KullaniciAdi || x.EgitimYapan == benuzman.KullaniciAdi).ToList();
            ViewBag.firmauzman = db.FirmaUzmen.Where(x => x.UzmanID == benuzman.Id).ToList();
            return View(egitimler);
        }

        [HttpPost]
        public JavaScriptResult EgitimEkle(Egitim egitim)
        {
            try
            {
                egitim.EgitimEkleyen = benuzman.KullaniciAdi;
                egitim.EkleyenAdiSoyadi = benuzman.AdiSoyadi;
                egitim.EgitimYapan = benuzman.KullaniciAdi;
                egitim.EklenmeTarihi = DateTime.Now;
                if (egitim.EgitimTarihi < DateTime.Now) return hata("Eğitim tarihi ileri bir tarih olmalıdır. Geçmişe dönük eğitim girilemez");
                db.Egitims.Add(egitim);
                db.SaveChanges();
                return onayyenile("Eğitim başarıyla kaydedildi");
            }
            catch { }
            return hata("Eğitim eklenirken bir haya meydana geldi. Lütfen tüm bilgilerin girildiğinden emin olup tekrar deneyin");
        }
        [HttpPost]
        public JavaScriptResult EgitimYapildiKaydet(int? id)
        {
            try
            {
                Egitim egitim = db.Egitims.Find(id);
                if (egitim.EgitimYapan == benuzman.KullaniciAdi)
                {
                    egitim.isYapildi = true;
                    db.SaveChanges();
                    return onayyenile("Eğitiminiz için teşşekkür ederiz.");
                }
                else
                {
                    return hata("Bu eğitim size ait görünmüyor");
                }
            }
            catch { }
            return hata("İşlem yapılırken bir hata meydana geldi");
        }

        [HttpPost]
        public ActionResult ajaxEgitimGetir(int? id)
        {
            try
            {
                Egitim egitim = db.Egitims.Find(id);
                if (egitim.EgitimYapan == benuzman.KullaniciAdi)
                {
                    return Json(new
                    {
                        sonuc = 1,
                        firmaadi = egitim.Firma.Adi.ToUpper(),
                        adi = egitim.Adi.ToUpper(),
                        lokasyonu = egitim.Lokasyonu.ToUpper(),
                        egitimtarihi = egitim.EgitimTarihi.ToShortDateString(),
                        eklenmetarihi = egitim.EklenmeTarihi.ToShortDateString(),
                        ekleyenkisi = egitim.EkleyenAdiSoyadi.ToUpper()
                    });
                }
                else
                {
                    return Json(new { sonuc = 0, mesaj = "Bu eğitim size ait görünmüyor" });
                }
            }
            catch { }
            return Json(new { sonuc = 0, mesaj = "Eğitim detayları getirilirken bir hata meydana geldi" });
        }

        public ActionResult YeniSahaDenetimi()
        {
            List<Firma> firmalar = new List<Firma>();
            foreach (var item in db.FirmaUzmen.Where(x => x.UzmanID == benuzman.Id).ToList())
            {
                firmalar.Add(item.Firma);
            }
            return View(firmalar);
        }
        public ActionResult SahaDenetimi(int? id)
        {
            try
            {
                SahaDenetim sd = db.SahaDenetims.Find(id);
                if (sd.UzmanID == benuzman.Id)
                {
                    //var itemler = db.Items.Select(x => new {
                    //    x.TEDurum
                    //}).Distinct().ToList();

                    var itemler = db.Items.GroupBy(x => x.TEDurum).Select(x => x.FirstOrDefault()).ToList();
                    ViewBag.itemler = itemler;
                    return View(sd);
                }
            }
            catch { }

            return RedirectToAction("SahaDenetimleri");
        }
        [HttpPost]
        public JavaScriptResult SahaDenetimiItemKaydet(Item item)
        {
            try
            {

                if (item.TerminTarihi == null) return hata("Termin tarihi kesinlikle girilmelidir");
                if (item.TerminTarihi < DateTime.Now) return hata("Termin tarihi doğru girilmelidir");
                item.EklenmeTarihi = DateTime.Now;
                item.Risk = item.Olasilik * item.Siddet;
                db.Items.Add(item);
                db.SaveChanges();
                return onayYonlendir("Saha denetim içeriği başarıyla kaydedildi. Resim ekleyebilirsiniz.", "/Uzman/SahaDenetimiItemResim/" + item.Id);
            }
            catch { }
            return hata("Lütfen tüm alanları doldurduğunuzdan emin olun");
        }
        public ActionResult SahaDenetimiItemResim(int? id)
        {
            try
            {
                Item item = db.Items.Find(id);
                if (item.SahaDenetim.UzmanID == benuzman.Id)
                {
                    return View(item);
                }
            }
            catch { }
            return RedirectToAction("SahaDenetimi", new { id = id });
        }
        [HttpPost]
        public ActionResult ItemResimKaydet(int? ItemID)
        {
            bool isSavedSuccessfully = false;
            Item item = db.Items.Find(ItemID);
            if (item != null)
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(400, 400, false, false);
                    FileInfo fotoinfo = new FileInfo(file.FileName);
                    if (resimMi(fotoinfo))
                    {
                        string dosyaadi = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        string dosyayolu = "~/Content/images/olayyeri/" + dosyaadi;
                        if (img.Save(dosyayolu) != null)
                        {
                            OlayYeriFoto oyf = new OlayYeriFoto()
                            {
                                ItemID = item.Id,
                                EklenmeTarihi = DateTime.Now,
                                ResimYolu = dosyayolu
                            };
                            db.OlayYeriFotoes.Add(oyf);
                            db.SaveChanges();
                            return Json(new
                            {
                                ResimYolu = Url.Content(dosyayolu),
                                ResimId = oyf.Id,
                                Message = "Resim başarıyla kaydedildi",
                                JsonRequestBehavior.AllowGet
                            });
                        }
                        else
                        {
                            return Json(new { Message = "Dosya kaydedilirken bir hata meydana geldi. ISS ile iletişime geçmelisiniz.", JsonRequestBehavior.AllowGet });
                        }
                    }
                    else
                    {
                        return Json(new { Message = "Yüklemeye çalıştığınız dosya resim değil", JsonRequestBehavior.AllowGet });
                    }

                    isSavedSuccessfully = true;
                }
                if (isSavedSuccessfully)
                {
                    return Json(new { Message = "Resim Eklendi", JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { Message = "Resim eklenirken bir hata oluştu", JsonRequestBehavior.AllowGet });
                }

            }
            else
            {
                return Json(new { Message = "Item bulunamadı.", JsonRequestBehavior.AllowGet });

            }

        }
        [HttpPost]
        public JavaScriptResult ItemResimSil(int? id)
        {
            try
            {
                OlayYeriFoto foto = db.OlayYeriFotoes.Find(id);
                if (foto.Item.SahaDenetim.UzmanID == benuzman.Id)
                {
                    string resimyeri = foto.ResimYolu;
                    db.OlayYeriFotoes.Remove(foto);

                    db.SaveChanges();
                    if (System.IO.File.Exists(Server.MapPath(resimyeri)))
                    {
                        System.IO.File.Delete(Server.MapPath(resimyeri));
                    }
                    return onayyenile("Resim başarıyla silindi");
                }
                else
                {
                    return hata("Bu resmi silmeye yetkiniz bulunmuyor");
                }
            }
            catch { }
            return hata("Resim silinirken bir hata meydana geldi");
        }


        [HttpPost]
        public JsonResult ItemGetir(int? id)
        {
            try
            {
                Item item = db.Items.Find(id);
                return Json(new
                {
                    TehlikeSinifi = item.TehlikeSinifi,
                    TEDurum = item.TEDurum,
                    Riskler = item.Riskler,
                    Olasilik = item.Olasilik,
                    Siddet = item.Siddet,
                    OncelikSirasi = item.OncelikSirasi,
                    DOFaliyetler = item.DOFaliyetler,
                    TerminTarihi = item.TerminTarihi
                });
            }
            catch { }
            return Json(null);
        }
        [HttpPost]
        public ActionResult YeniSahaDenetimi(int? FirmaID, string Adi)
        {
            try
            {
                Firma firma = db.Firmas.Find(FirmaID);
                FirmaUzman fu = db.FirmaUzmen.FirstOrDefault(x => x.FirmaID == firma.Id && x.UzmanID == benuzman.Id);
                if (fu != null)
                {
                    SahaDenetim denetim = new SahaDenetim()
                    {
                        Adi = Adi,
                        FirmaID = firma.Id,
                        UzmanID = benuzman.Id,
                        EklenenTarih = DateTime.Now
                    };
                    db.SahaDenetims.Add(denetim);
                    db.SaveChanges();
                    return RedirectToAction("SahaDenetimi", new { id = denetim.Id });
                }
            }
            catch
            {

            }

            return RedirectToAction("YeniSahaDenetimi");
        }


        public ActionResult SahaDenetimleri(int? id)
        {
            try
            {
                if (id > 0)
                {
                    Firma firma = db.Firmas.Find(id);
                    return View(firma.SahaDenetims.Where(x=>x.UzmanID == benuzman.Id).ToList());
                }

            }
            catch { }
            var result = db.SahaDenetims.Where(x => x.UzmanID == benuzman.Id).ToList();
            return View(result);
        }


        public ActionResult SahaDenetimiIcerigi(int? id)
        {
            try
            {
                SahaDenetim sd = db.SahaDenetims.Find(id);
                if (sd.UzmanID == benuzman.Id)
                {
                    return View(sd);
                }
            }
            catch { }
            return RedirectToAction("SahaDenetimleri");
        }

        public ActionResult SahaDenetimiIcerigiDetay(int? id)
        {
            try
            {
                Item item = db.Items.Find(id);
                if (item.SahaDenetim.UzmanID == benuzman.Id)
                {
                    return View(item);
                }
            }
            catch { }
            return RedirectToAction("SahaDenetimleri");
        }
        public ActionResult SahaDenetimiSil(int? id)
        {
            try
            {
                SahaDenetim sd = db.SahaDenetims.Find(id);
                if (sd.UzmanID == benuzman.Id)
                {
                    return View(sd);
                }
            }
            catch { }
            return RedirectToAction("SahaDenetimleri");
        }
        public ActionResult SahaDenetimiRaporla(int? id)
        {
            try
            {
                SahaDenetim sd = db.SahaDenetims.Find(id);
                if (sd.UzmanID == benuzman.Id) { return View(sd); }
            }
            catch { }
            return RedirectToAction("SahaDenetimleri");
        }
        [HttpPost]
        public ActionResult SahaDenetimiAdDegistir(SahaDenetim sade)
        {
            try
            {
                SahaDenetim sd = db.SahaDenetims.Find(sade.Id);
                if (sd.UzmanID != benuzman.Id) return hata("Bu saha denetimi size ait görünmüyor");
                if (!String.IsNullOrEmpty(sade.Adi))
                {
                    sd.Adi = sade.Adi;
                    db.SaveChanges();
                    return ScriptVeOnay("$('mdlAdDegistir').modal('hide');", "Saha Denetimi başarıyla kaydedildi.");
                }
                else
                {
                    return hata("Lütfen bir isim veriniz");
                }

            }
            catch { }
            return hata("Saha denetimi kaydedilemedi. Lütfen izninizin olduğundan emin olup tekrar deneyin");
        }

        [HttpPost]
        public JavaScriptResult SahaDenetimiSil(int? id, string parola)
        {
            try
            {
                if (parola != benuzman.Parola) return hata("Yanlış bir parola girdiniz");
                SahaDenetim sd = db.SahaDenetims.Find(id);
                if (sd.UzmanID != benuzman.Id) return hata("Bu saha denetimi size ait görünmüyor.Lütfen sadece kendi yetkinizdeki denetimlerle ilgilenin");
                db.Items.RemoveRange(sd.Items.ToList());
                db.SahaDenetims.Remove(sd);
                db.SaveChanges();
                return onayYonlendir("Saha denetimi başarıyla silindi", "/Uzman/SahaDenetimleri");

            }
            catch { }
            return hata("Denetim silinemedi. Yetkiniz olduğundan emin olup tekrak deneyin");
        }

        [HttpPost]
        public JavaScriptResult SahaDenetimiIcerikSil(int? id)
        {
            try
            {

                Item item = db.Items.Find(id);
                SahaDenetim sd = item.SahaDenetim;
                if (sd.UzmanID == benuzman.Id)
                {
                    db.Items.Remove(item);
                    db.SaveChanges();
                    return onayYonlendir("Denetim içeriği başarıyla silindi", "/Uzman/SahaDenetimiIcerigi/" + sd.Id);
                }
            }
            catch { }
            return hata("İçerik silinirken bir hata meydana geldi. Lütfen yetkinizi kontrol edip tekrar deneyin");
        }


        public ActionResult CalismaPlanlari(int?id)
        {
            try
            {
                if (id > 0)
                {
                    Firma firma = db.Firmas.Find(id);
                    return View(firma.CalismaPlanis.Where(x => x.SorumluKisi == benuzman.KullaniciAdi).ToList());
                }
            }
            catch { }
            var result = db.CalismaPlanis.Where(x => x.SorumluKisi == benuzman.KullaniciAdi).ToList();
            return View(result);
        }

        [HttpPost]
        public JsonResult SiradakiCalismaTarihi(int? id)//Çalışma planları sayfasındaki sıradaki tarih kolonu için
        {
            try
            {
                DateTime tarih;
                CalismaPlani cp = db.CalismaPlanis.Find(id);
                if (cp == null) return Json(new { sonuc = 0, yazi = "Plan Bulunamadı" });
                Calisma soncalisma = cp.Calismas.ToList().Where(x => x.CalismayiYapanKisi == benuzman.KullaniciAdi).OrderByDescending(x => x.Tarih).Take(1).FirstOrDefault();
                int toplamgun = 0;
                DateTime ilktarih;
                if (soncalisma != null)
                {
                    ilktarih = soncalisma.Tarih.Value;
                }
                else
                {
                    ilktarih = cp.EklenmeTarihi;
                }

                int gun = 0;
                switch (cp.PeriyotTipi)
                {
                    case "Yıl": gun = 365; break;
                    case "Ay": gun = 30; break;
                    case "Hafta": gun = 7; break;
                    case "Gün": gun = 1; break;
                }
                toplamgun = gun * cp.PeriyotAraligi;
                tarih = ilktarih.AddDays(toplamgun);
                return Json(new { sonuc = 1, yazi = tarih.ToString() });
            }
            catch { }
            return Json(new { sonuc = 1, yazi = "Tanımsız" });
        }

        [HttpGet]
        public string selectFirmalariGetir()
        {
            List<FirmaUzman> fu = benuzman.FirmaUzmen.ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append("<option disabled selected>Firma Seçin</option>");
            foreach (var item in fu)
            {
                sb.Append("<option value=\"" + item.FirmaID + "\">" + item.Firma.Adi.ToUpper() + "</option>");
            }
            return sb.ToString();
        }

        [HttpPost]
        public ActionResult YeniFaliyetPlani(CalismaPlani cp)
        {
            try
            {
                if (cp.PeriyotAraligi < 1) return hata("Periyot aralığı 0 dan büyük olmalıdır");
                cp.EklenmeTarihi = DateTime.Now;
                cp.EkleyenKisi = benuzman.KullaniciAdi;
                cp.EkleyenAdi = benuzman.AdiSoyadi;
                cp.SorumluKisi = benuzman.KullaniciAdi;
                db.CalismaPlanis.Add(cp);
                db.SaveChanges();
                return onayyenile("Çalışma planı başarıyla kaydedildi");
            }
            catch { }
            return hata("Plan kaydedilirken bir hata oluştu. Gerekli alanları doldurup tekrar deneyin lütfen");
        }

        public ActionResult FaaliyetPlaniDetay(int? id)
        {
            try
            {
                CalismaPlani cp = db.CalismaPlanis.Find(id);
                if (cp.SorumluKisi != benuzman.KullaniciAdi) return RedirectToAction("CalismaPlanlari");
                return View(cp);
            }
            catch { }
            return RedirectToAction("CalismaPlanlari");
        }

        [HttpPost]
        public int CalismaEklenebilirMi(int? id)
        {
            try
            {
                CalismaPlani cp = db.CalismaPlanis.Find(id);
                if (cp.SorumluKisi != benuzman.KullaniciAdi) return -1;
                Calisma soncalisma = cp.Calismas.OrderByDescending(x => x.Tarih).Take(1).FirstOrDefault();

                int toplamgun = 0;
                DateTime ilktarih;
                DateTime gecerliTarih;
                if (soncalisma != null)
                {
                    ilktarih = soncalisma.Tarih.Value;
                }
                else
                {
                    ilktarih = cp.EklenmeTarihi;
                    return 1;
                }

                int gun = 0;
                switch (cp.PeriyotTipi)
                {
                    case "Yıl": gun = 365; break;
                    case "Ay": gun = 30; break;
                    case "Hafta": gun = 7; break;
                    case "Gün": gun = 1; break;
                }
                toplamgun = gun * cp.PeriyotAraligi;
                gecerliTarih = ilktarih.AddDays(toplamgun);
                if ((gecerliTarih.ToShortDateString() == DateTime.Now.ToShortDateString()) ||
                    gecerliTarih.AddDays(1).ToShortDateString() == DateTime.Now.ToShortDateString() ||
                    gecerliTarih.AddDays(-1).ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    return 1;
                }
                else
                {
                    return 2;
                }

            }
            catch { }
            return 0;
        }

        [HttpPost]
        public JavaScriptResult YeniCalismaKaydet(Calisma calisma)
        {
            try
            {
                CalismaPlani cp = db.CalismaPlanis.Find(calisma.CalismaPlaniID);
                int durum = CalismaEklenebilirMi(cp.Id);
                if (durum == 1)
                {
                    calisma.Tarih = DateTime.Now;
                    calisma.CalismayiYapanKisi = benuzman.KullaniciAdi;
                    calisma.CalismayiYapanAdiSoyadi = benuzman.AdiSoyadi;
                    db.Calismas.Add(calisma);
                    db.SaveChanges();
                    return onayyenile("Çalışmanız başarıyla tamamlandı");
                }
                else if (durum == -1)
                {
                    return hata("Yetkiniz yok");
                }
                else if (durum == 2)
                {
                    return hata("Son çalışmadan sonra periyota göre tarih henüz gelmemiş");
                }
            }
            catch { }
            return hata("Çalışma kaydedilemedi");
        }







        public JavaScriptResult hata(string icerik)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("swal('Hata','" + icerik + "','warning');");
            return JavaScript(sb.ToString());
        }

        public JavaScriptResult onayyenile(string icerik)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                @"swal({title:'Bilgi',text:'" + icerik + "',type:'success',confirmButtonClass:'btn-default'},function(){location.reload();});"
                );
            return JavaScript(sb.ToString());
        }
        public JavaScriptResult onayVeScript(string icerik, string kod)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                @"swal({title:'Bilgi',text:'" + icerik + "',type:'success',confirmButtonClass:'btn-default'},function(){});" + kod
                );
            return JavaScript(sb.ToString());
        }
        public JavaScriptResult ScriptVeOnay(string kod, string icerik)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                kod + @"swal({title:'Bilgi',text:'" + icerik + "',type:'success',confirmButtonClass:'btn-default'},function(){location.reload();});"
                );
            return JavaScript(sb.ToString());
        }
        public JavaScriptResult onayYonlendir(string icerik, string adres)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                @"swal({title:'Bilgi',text:'" + icerik + "',type:'success',confirmButtonClass:'btn-default'},function(){location.href ='" + adres + "';});"
                );
            return JavaScript(sb.ToString());
        }
        public JavaScriptResult sadeceYonlendir(string adres)
        {
            return JavaScript("location.href ='" + adres + "';");
        }

        bool resimMi(FileInfo fotoinfo)
        {
            if (fotoinfo.Extension == ".gif")
                return true;
            if (fotoinfo.Extension == ".jpg")
                return true;
            if (fotoinfo.Extension == ".png")
                return true;
            if (fotoinfo.Extension == ".jpeg")
                return true;
            if (fotoinfo.Extension == ".GIF")
                return true;
            if (fotoinfo.Extension == ".JPG")
                return true;
            if (fotoinfo.Extension == ".JPG")
                return true;
            if (fotoinfo.Extension == ".JPEG")
                return true;
            return false;
        }




    }
}