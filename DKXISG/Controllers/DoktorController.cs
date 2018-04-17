using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DKXISG.Models;
using System.Text;
namespace DKXISG.Controllers
{
    public class DoktorController : BaseController
    {
        // GET: Doktor
        dkxisgContext db = new dkxisgContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Firmalar()
        {
            Doktor ben = Session["doktor"] as Doktor;
            List<Firma> firmalar = new List<Firma>();
            List<FirmaDoktor> fds = db.FirmaDoktors.Where(x => x.DoktorID == ben.Id).ToList();
            return View(fds);
        }





        public ActionResult Ziyaretlerim()
        {
            Doktor ben = Session["doktor"] as Doktor;
            return View(db.DoktorZiyarets.Where(x => x.DoktorID == ben.Id).ToList());
        }
        public ActionResult ZiyaretOlustur()
        {
            Doktor ben = Session["doktor"] as Doktor;
            List<Firma> firmalar = new List<Firma>();
            foreach (var item in db.FirmaDoktors.Where(x => x.DoktorID == ben.Id).ToList())
            {
                firmalar.Add(item.Firma);
            }
            return View(firmalar);
        }
        [HttpPost]
        public JavaScriptResult ZiyaretOlustur(DoktorZiyaret dz)
        {
            try
            {
                Doktor ben = (Session["doktor"] as Doktor);
                FirmaDoktor fd = db.FirmaDoktors.FirstOrDefault(x => x.FirmaID == dz.FirmaID && x.DoktorID == ben.Id);
                if (fd == null) return hata("Sizin bu firmada bir yetkiniz bulunmuyor");
                DoktorZiyaret eskidz = db.DoktorZiyarets.FirstOrDefault(x => x.FirmaID == dz.FirmaID && x.DoktorID == ben.Id);
                if (eskidz != null) return onayYonlendir("Bu firma için açılmış bir kaydınız zaten bulunuyor. Firma işlemlerine yönlendirileceksiniz", "/Doktor/ZiyaretIslemleri/" + eskidz.Id);
                dz.Tarih = DateTime.Now;
                dz.DoktorID = ben.Id;
                db.DoktorZiyarets.Add(dz);
                db.SaveChanges();
                return onayYonlendir("Ziyaret başarıyla tamamlandı. İşlemlere yönlendirileceksiniz", "/Doktor/ZiyaretIslemleri/" + dz.Id);
            }
            catch
            {
                return hata("Bir hata oluştu");
            }
        }
        public ActionResult ZiyaretIslemleri(int? id)
        {
            try
            {
                Doktor ben = Session["doktor"] as Doktor;
                DoktorZiyaret dz = db.DoktorZiyarets.Find(id);
                if (dz.DoktorID != ben.Id) return RedirectToAction("Ziyaretlerim");
                Firma firma = dz.Firma;
                FirmaDoktor fd = db.FirmaDoktors.FirstOrDefault(x => x.FirmaID == firma.Id && x.DoktorID == ben.Id);
                if (fd == null) return RedirectToAction("Ziyaretlerim");
                return View(dz);
            }
            catch
            {


            }
            return RedirectToAction("Ziyaretlerim");
        }
        [HttpPost]
        public JavaScriptResult YeniNotOlustur(DoktorNot dn)
        {
            try
            {
                Doktor ben = Session["doktor"] as Doktor;
                DoktorZiyaret dz = db.DoktorZiyarets.Find(dn.DoktorZiyaretID);
                dn.EklenmeTarihi = DateTime.Now;
                db.DoktorNots.Add(dn);
                db.SaveChanges();
                return ScriptVeOnay("$('#mdlYeniNot').modal('hide');", "Not başarıyla kaydedildi.");
            }
            catch (Exception)
            {
                return hata("Not kaydedilemedi. Lütfen tüm alanları doldurun");
            }
        }
        [HttpPost]
        public JavaScriptResult NotSil(int? notid)
        {
            try
            {
                Doktor ben = Session["doktor"] as Doktor;
                DoktorNot dn = db.DoktorNots.FirstOrDefault(x => x.Id == notid);
                DoktorZiyaret dz = dn.DoktorZiyaret;
                if (dz.DoktorID == ben.Id)
                {
                    db.DoktorNots.Remove(dn);
                    db.SaveChanges();
                    return onayyenile("Not başarıyla silindi");
                }
            }
            catch
            {


            }
            return hata("Bu not sizin yetki alanınızda görünmüyor");
        }
        public ActionResult YeniRapor(int? id)
        {
            try
            {
                DoktorZiyaret dz = db.DoktorZiyarets.Find(id);

                Doktor ben = Session["doktor"] as Doktor;
                FirmaDoktor fd = db.FirmaDoktors.FirstOrDefault(x => x.FirmaID == dz.FirmaID && dz.DoktorID == ben.Id);

                if (fd != null)
                {
                    return View(dz);
                }
            }
            catch
            {
            }
            return RedirectToAction("Ziyaretlerim");
        }
        [HttpPost]
        public JavaScriptResult RaporOlustur(SaglikRaporu rapor)
        {
            try
            {
                DoktorZiyaret dz = db.DoktorZiyarets.Find(rapor.ZiyaretID);
                Doktor ben = Session["doktor"] as Doktor;
                if (dz.DoktorID == ben.Id)
                {
                    rapor.Tarih = DateTime.Now;
                    db.SaglikRaporus.Add(rapor);
                    db.SaveChanges();
                    return onayYonlendir("Rapor başarıyla oluşturuldu. Rapor içerik sayfasına yönlendiriliyorsunuz.", "/Doktor/RaporIcerik/" + rapor.Id);
                }
            }
            catch
            {
            }
            return hata("Rapor oluşturulamadı. Lütfen tüm bilgilerin doğruluğundan ve yetkinizin olduğundan emin olup tekrar deneyin");
        }
        public ActionResult RaporIcerik(int? id)
        {
            try
            {
                SaglikRaporu rapor = db.SaglikRaporus.Find(id);
                DoktorZiyaret dz = rapor.DoktorZiyaret;
                Doktor ben = Session["doktor"] as Doktor;
                if (dz.DoktorID == ben.Id)
                {
                    return View(rapor);
                }
            }
            catch
            {
            }
            return RedirectToAction("Raporlarim");
        }

        [HttpPost]
        public JavaScriptResult YeniTetkik(SaglikRaporuTetkik st)
        {
            try
            {
                SaglikRaporu sr = db.SaglikRaporus.Find(st.SaglikRaporuID);
                Doktor ben = Session["doktor"] as Doktor;
                if (sr.DoktorZiyaret.DoktorID == ben.Id)
                {
                    st.Tarih = DateTime.Now;
                    db.SaglikRaporuTetkiks.Add(st);
                    db.SaveChanges();
                    return ScriptVeOnay("$('#mdlYeniTetkik').modal('hide');", "Tetkik başarıyla kaydedildi.");
                }
            }
            catch { }
            return hata("Tetkik kaydedilemedi. Lütfen gerekli alanları doldurduğunuzdan ve yetkiniz olduğundan emin olup tekrar deneyin");
        }

        public ActionResult TetkikDetay(int? id)
        {
            try
            {
                SaglikRaporuTetkik tetkik = db.SaglikRaporuTetkiks.Find(id);
                Doktor ben = Session["doktor"] as Doktor;
                if (tetkik.SaglikRaporu.DoktorZiyaret.DoktorID == ben.Id) { return View(tetkik); }
            }
            catch { }
            return RedirectToAction("Raporlarim");
        }
        public ActionResult RaporDetay(int? id)
        {
            try
            {
                SaglikRaporu rapor = db.SaglikRaporus.Find(id);
                Doktor ben = Session["doktor"] as Doktor;
                if (rapor.DoktorZiyaret.DoktorID == ben.Id)
                {
                    return View(rapor);
                }
            }
            catch
            {
            }
            return RedirectToAction("Raporlarim");
        }
        public ActionResult Raporlarim()
        {
            Doktor ben = Session["doktor"] as Doktor;
            var result = db.DoktorZiyarets.Where(x => x.DoktorID == ben.Id).ToList();
            List<SaglikRaporu> raporlar = new List<SaglikRaporu>();
            foreach (var item in result)
            {
                raporlar.AddRange(item.SaglikRaporus.ToList());
            }
            return View(raporlar.OrderBy(x => x.Tarih).ToList());
        }
        [HttpPost]
        public JavaScriptResult TetkikDegistir(FormCollection f)
        {
            try
            {
                Doktor ben = Session["doktor"] as Doktor;
                var Id = f.Get("Id");
                var SaglikRaporuID = f.Get("SaglikRaporuID");
                var Tetkik = f.Get("Tetkik").Trim();
                var Sonuc = f.Get("Sonuc").Trim();
                if (String.IsNullOrEmpty(Tetkik) || String.IsNullOrEmpty(Sonuc)) return hata("Tetkik ve sonuç boş olamaz");
                SaglikRaporuTetkik tetkik = new SaglikRaporuTetkik()
                {
                    Id = Convert.ToInt32(Id),
                    SaglikRaporuID = Convert.ToInt32(SaglikRaporuID),
                    Tetkik = Tetkik,
                    Sonuc = Sonuc
                };
                SaglikRaporuTetkik te = db.SaglikRaporuTetkiks.Find(tetkik.Id);
                if (te.SaglikRaporu.DoktorZiyaret.DoktorID == ben.Id)
                {
                    te.Tetkik = tetkik.Tetkik;
                    te.Sonuc = tetkik.Sonuc;
                    db.SaveChanges();
                    return ScriptVeOnay("$('#mdlSonucDegistir').modal('hide');", "Tetkik başarıyla kaydedildi");
                }

            }
            catch { }
            return hata("Kaydedilemedi. Lütfen gerekli alanların dolu olduğundan ve yetkininizin olduğundan emip olup tekrar deneyin");
        }

        public ActionResult Asilar()
        {
            return View(db.Asis.ToList());
        }
        [HttpPost]
        public JavaScriptResult YeniAsiKaydet(Asi asi)
        {
            try
            {
                asi.EklenmeTarihi = DateTime.Now;
                db.Asis.Add(asi);
                db.SaveChanges();
                return onayyenile("Aşı başarıyla kaydedildi");
            }
            catch { }
            return hata("Aşı kaydedilemedi. Lütfen gerekli alanların doldurulduğundan emin olup tekrar deneyin");
        }

        [HttpPost]
        public ActionResult AsiDuzenleKaydet(FormCollection f)
        {
            try
            {
                int id = Convert.ToInt32(f.Get("eId"));
                string adi = f.Get("eAdi");
                if (String.IsNullOrEmpty(adi)) return hata("Aşı düzenlenemedi. Lütfen gerekli alanların doldurulduğundan emin olup tekrar deneyin");
                string kontrendikasyon = f.Get("eKontrendikasyon");
                int minyas = Convert.ToInt32(f.Get("eMinUygulamaYasi"));
                int maxyas = Convert.ToInt32(f.Get("eMaxUygulamaYasi"));
                int periyod = Convert.ToInt32(f.Get("eIslemPeriyodu"));
                DateTime degistirmeTarihi = DateTime.Now;
                Asi eski = db.Asis.Find(id);
                eski.Adi = adi;
                eski.Kontrendikasyon = kontrendikasyon;
                eski.MinUygulamaYasi = minyas;
                eski.MaxUygulamaYasi = maxyas;
                eski.IslemPeriyodu = periyod;
                eski.DuzenlenmeTarihi = degistirmeTarihi;
                db.SaveChanges();
                return onayyenile("Aşı başarıyla düzenlendi");
            }
            catch
            {

            }
            return hata("Aşı düzenlenemedi. Lütfen gerekli alanların doldurulduğundan emin olup tekrar deneyin");
        }


        public string selectFirmaDondur()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in db.FirmaDoktors.Where(x => x.DoktorID == bendoktor.Id))
            {
                sb.Append("<option value=\"" + item.Firma.Id + "\">" + item.Firma.Adi.ToUpper() + "</option>");
            }
            return sb.ToString();
        }

        [HttpPost]
        public string selecCalisanDoldur(int? id)
        {
            try
            {
                FirmaDoktor fd = db.FirmaDoktors.Where(x => x.DoktorID == bendoktor.Id && x.FirmaID == id).FirstOrDefault();
                if (fd != null)
                {
                    List<IsyeriBolumu> ib = fd.Firma.IsyeriBolumus.ToList();
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in ib)
                    {
                        foreach (var eleman in item.Calisans.ToList())
                        {
                            sb.Append("<option value=\"" + eleman.Id + "\">" + eleman.AdiSoyadi.ToUpper() + "</option>");
                        }
                    }
                    return sb.ToString();

                }
            }
            catch { }
            return null;
        }

        [HttpPost]
        public JavaScriptResult AsiKaydiKaydet(CalisanAsi ca)
        {
            try
            {
                ca.DoktorID = bendoktor.Id;
                ca.AsiTarihi = DateTime.Now;
                db.CalisanAsis.Add(ca);
                db.SaveChanges();
                return onayyenile("Aşı kaydı başarıyla oluşturuldu");
            }
            catch { }
            return hata("Lütfen gerekli alanları doldurup tekrar deneyin");
        }
        [HttpPost]
        public JavaScriptResult AsiSil(int?id)
        {
            try
            {
                Asi asi = db.Asis.Find(id);
                db.Asis.Remove(asi);
                db.SaveChanges();
                return onayyenile("Aşı başarıyla silindi");
            }
            catch { }
            return hata("Aşı silinirken bir hata meydana geldi. Bu aşıyı yaptığınız birisi varsa silemezsiniz. Önce o kayıtların silinmesi gerekiyor");
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



    }
}