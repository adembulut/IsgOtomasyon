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
    public class YonetimController : BaseController
    {
        //
        // GET: /Yonetim/
        dkxisgContext db = new dkxisgContext();
        Yonetici ben;
        public ActionResult Index()
        {
            return View();
        }

        #region Yönetici islemleri
        public ActionResult YoneticiEkle()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public String YoneticiEkle(Yonetici yonetici)
        {
            if (yonetici != null)
            {
                if (!String.IsNullOrEmpty(yonetici.KullaniciAdi) && !String.IsNullOrEmpty(yonetici.Parola) && !String.IsNullOrEmpty(yonetici.AdiSoyadi))
                {
                    if (
                                        yonetici.AdiSoyadi.Length > 0 && yonetici.AdiSoyadi.Length < 51 &&
                                        yonetici.KullaniciAdi.Length > 0 && yonetici.KullaniciAdi.Length < 51 &&
                                        yonetici.Parola.Length > 0 && yonetici.Parola.Length < 51)
                    {
                        Yonetici temp = db.Yoneticis.FirstOrDefault(x => x.KullaniciAdi == yonetici.KullaniciAdi);
                        if (temp == null)
                        {
                            yonetici.AktifMi = true;
                            db.Yoneticis.Add(yonetici);
                            db.SaveChanges();
                            return "1";
                        }
                        else
                        {
                            return "2";
                        }

                    }
                }
                else
                {
                    return "3";
                }

            }
            return "0";
        }
        public ActionResult Yoneticiler()
        {
            ViewBag.yonetici = Session["yonetici"];
            var result = db.Yoneticis.ToList();
            return View(result);
        }
        public ActionResult YoneticiDetay(int? id)
        {
            if (id > 0)
            {
                Yonetici yonetici = db.Yoneticis.Find(id);
                if (yonetici != null)
                {
                    return View(yonetici);
                }
            }
            return RedirectToAction("Yoneticiler");
        }
        [HttpPost]
        public JavaScriptResult pasifyap(int? yoneticiid)
        {
            if (yoneticiid < 1) return hata("İşlem tamamlanırken bir hata meydana geldi");
            Yonetici yonetici = db.Yoneticis.Find(yoneticiid);
            if (yonetici == null) return hata("İşlem tamamlanırken bir hata meydana geldi");
            if (yoneticiid == ben.Id) return hata("Kendi durumunuzu değiştiremezsiniz");
            if (yonetici.AktifMi == false) return hata("Bu kullanıcı zaten pasif durumda");
            try
            {
                yonetici.AktifMi = false;
                db.SaveChanges();
                return onayyenile("Yönetici başarıyla pasifleştirildi");
            }
            catch
            {
                return hata("İşlem tamamlanırken bir hata meydana geldi");
            }
        }
        [HttpPost]
        public JavaScriptResult aktifyap(int? yoneticiid)
        {
            if (yoneticiid < 1) hata("İşlem tamamlanırken bir hata meydana geldi");
            Yonetici yonetici = db.Yoneticis.Find(yoneticiid);
            if (yonetici == null) hata("İşlem tamamlanırken bir hata meydana geldi");
            if (yoneticiid == ben.Id) return hata("Kendi durumunuzu değiştiremezsiniz");
            if (yonetici.AktifMi == true) return hata("Bu Kullanıcı zaten aktif durumda");
            try
            {
                yonetici.AktifMi = true;
                db.SaveChanges();
                return onayyenile("Yönetici başarıyla aktifleştirildi");
            }
            catch
            {
                return hata("İşlem tamamlanırken bir hata meydana geldi");
            }
        }

        [HttpPost]
        public JavaScriptResult SifreDegistir(FormCollection f)
        {
            try
            {
                var id = Convert.ToInt32(f.Get("Id"));
                var eskiparola = f.Get("EskiParola").Trim();
                var yeniparola = f.Get("YeniParola").Trim();
                var yeniparolaonay = f.Get("YeniParolaOnay").Trim();

                if (eskiparola.Length < 1 || yeniparola.Length < 1 || yeniparolaonay.Length < 1) return hata("Lütfen alanları doldurun");
                if (yeniparola != yeniparolaonay) return hata("Parolalar eşleşmiyor");

                Yonetici yonetici = db.Yoneticis.Find(id);
                if (yonetici == null) return hata("Bir hata oluştu");
                if (yonetici.Parola != eskiparola) return hata("Eski parolayı hatalı girdiniz");
                yonetici.Parola = yeniparola;
                db.SaveChanges();
                return onayyenile("Şifre başarıyla değiştirildi");

            }
            catch (Exception)
            {
                return hata("Bir hata oluştu");
            }
        }

        [HttpPost]
        public JavaScriptResult DetayProfilKaydet(Yonetici yonetici)
        {
            try
            {
                if (yonetici == null) return hata("Bir hata oluştu");
                Yonetici eski = db.Yoneticis.Find(yonetici.Id);
                if (eski == null) return hata("Bir hata oluştu");
                //if (String.IsNullOrEmpty(yonetici.KullaniciAdi) || String.IsNullOrEmpty(yonetici.AdiSoyadi)) return hata("Lütfen tüm gerekli alanları doldurun");
                //if (eski.KullaniciAdi != yonetici.KullaniciAdi)
                //{
                //    if (db.Yoneticis.FirstOrDefault(X => X.KullaniciAdi == yonetici.KullaniciAdi) != null) return hata("Bu kullanıcı adı zaten kayıtlı");
                //}
                //eski.KullaniciAdi = yonetici.KullaniciAdi; Kullanıcı adı değiştirilmemeli
                eski.AdiSoyadi = yonetici.AdiSoyadi;
                eski.Telefon = yonetici.Telefon;
                eski.Mail = yonetici.Mail;
                eski.Adres = yonetici.Adres;
                db.SaveChanges();
                return onayyenile("Bilgiler başarıyla güncellendi");

            }
            catch
            {
                return hata("Profil kaydedilirken bir hata meydana geldi");
            }
        }
        #endregion
        #region Doktor islemleri
        public ActionResult Doktorlar()
        {
            var result = db.Doktors.ToList();
            return View(result);
        }

        public ActionResult DoktorEkle()
        {
            return View();
        }
        [HttpPost]
        public JavaScriptResult DoktorEkle(Doktor doktor)
        {
            try
            {
                if (doktor == null) return hata("Doktor kaydedilirken bir hata oluştu");
                if (String.IsNullOrEmpty(doktor.AdiSoyadi) || String.IsNullOrEmpty(doktor.KullaniciAdi) || String.IsNullOrEmpty(doktor.Parola)) return hata("Lütfen gerekli alanları doldurun");
                if (doktor.AdiSoyadi.Length < 1 || doktor.KullaniciAdi.Length < 1 || doktor.Parola.Length < 1) return hata("Doktor kaydedilirken bir haya oluştu");
                if (db.Doktors.FirstOrDefault(x => x.KullaniciAdi == doktor.KullaniciAdi) != null) return hata("Bu kullanıcı adı zaten kayıtlı");
                if (db.Doktors.FirstOrDefault(x => x.Mail == doktor.Mail) != null) return hata("Bu mail zaten kayıtlı.");
                doktor.EkleyenID = (Session["yonetici"] as Yonetici).Id;
                doktor.EklenmeTarihi = DateTime.Now;
                doktor.AktifMi = true;
                db.Doktors.Add(doktor);
                db.SaveChanges();
                return onayYonlendir("Doktor başarıyla kaydedildi. Doktor listesine yönlendirileceksiniz", "/Yonetim/Doktorlar");
            }
            catch
            {
                return hata("Doktor eklenirken bir hata meydana geldi");
            }
        }
        public ActionResult DoktorDetay(int? id)
        {
            if (id < 1) return RedirectToAction("Doktorlar");
            Doktor doktor = db.Doktors.Find(id);
            if (doktor == null) return RedirectToAction("Doktorlar");

            return View(doktor);
        }

        [HttpPost]
        public JavaScriptResult DetayDoktorProfilKaydet(Doktor doktor)
        {
            try
            {
                if (doktor == null) return hata("Doktor kaydedilirken bir hata oluştu");
                Doktor eski = db.Doktors.Find(doktor.Id);
                if (eski == null) return hata("Doktor kaydedilirken bir hata oluştu");
                //if (String.IsNullOrEmpty(doktor.KullaniciAdi) || String.IsNullOrEmpty(doktor.AdiSoyadi)) return hata("Lütfen gerekli alanları eksiksiz doldurun");
                //if (eski.KullaniciAdi != doktor.KullaniciAdi)
                //{
                //    if (db.Doktors.FirstOrDefault(x => x.KullaniciAdi == doktor.KullaniciAdi) != null) return hata("Bu kullanıcı adı zaten bir doktorda kayıtlı. Lütfen farklı bir kullanıcı adı belirleyin");

                //}
                eski.AdiSoyadi = doktor.AdiSoyadi;
                ///eski.KullaniciAdi = doktor.KullaniciAdi; Kullanıcı adı değiştirilmemeli
                eski.Adres = doktor.Adres;
                eski.Mail = doktor.Mail;
                eski.DiplomaNo = doktor.DiplomaNo;
                eski.Telefon = doktor.Telefon;
                db.SaveChanges();
                return onayyenile("Doktor bilgileri başarıyla güncellendi");
            }
            catch
            {
                return hata("Doktor bilgileri güncellenirken bir hata meydana geldi");
            }
        }


        [HttpPost]
        public JavaScriptResult DoktorSifreDegistir(FormCollection f)
        {
            try
            {
                if (f == null) return hata("Şifre güncellenirken bir hata meydana geldi");
                var id = Convert.ToInt32(f.Get("Id"));
                var parola = f.Get("YeniParola");
                var parolaonay = f.Get("YeniParolaOnay");
                if (String.IsNullOrEmpty(parola) || String.IsNullOrEmpty(parolaonay)) return hata("Şifre güncellenirken bir hata meydana geldi");
                if (parola != parolaonay) return hata("Parolalar uyuşmuyor. İki alana da aynı parolayı girin");
                if (id < 1) return hata("Şifre güncellenirken bir hata meydana geldi");
                Doktor doktor = db.Doktors.Find(id);
                if (doktor == null) return hata("Şifre güncellenirken bir hata meydana geldi");
                doktor.Parola = parola;
                db.SaveChanges();
                return onayyenile("Doktor parolası başarıyla güncellendi");
            }
            catch
            {
                return hata("Şifre güncellenirken bir hata meydana geldi");
            }
        }
        [HttpPost]
        public JavaScriptResult pasifyapdoktor(int? doktorid)
        {
            if (doktorid < 1) return hata("İşlem tamamlanırken bir hata meydana geldi");
            Doktor doktor = db.Doktors.Find(doktorid);
            if (doktor == null) return hata("İşlem tamamlanırken bir hata meydana geldi");
            if (doktor.AktifMi == false) return hata("Bu kullanıcı zaten pasif durumda");
            try
            {
                doktor.AktifMi = false;
                db.SaveChanges();
                return onayyenile("Doktor başarıyla pasifleştirildi");
            }
            catch
            {
                return hata("İşlem tamamlanırken bir hata meydana geldi");
            }
        }
        [HttpPost]
        public JavaScriptResult aktifyapdoktor(int? doktorid)
        {
            if (doktorid < 1) return hata("İşlem tamamlanırken bir hata meydana geldi");
            Doktor doktor = db.Doktors.Find(doktorid);
            if (doktor == null) return hata("İşlem tamamlanırken bir hata meydana geldi");
            if (doktor.AktifMi == true) return hata("Bu kullanıcı zaten aktif durumda");
            try
            {
                doktor.AktifMi = true;
                db.SaveChanges();
                return onayyenile("Doktor başarıyla aktifleştirildi");
            }
            catch
            {
                return hata("İşlem tamamlanırken bir hata meydana geldi");
            }
        }

        #endregion
        #region Müşteri işlemleri
        public ActionResult MusteriEkle()
        {

            return View();
        }

        [HttpPost]
        public JavaScriptResult MusteriEkle(Musteri musteri)
        {
            if (musteri == null) return hata("Müşteri eklenemedi");
            if (String.IsNullOrEmpty(musteri.AdiSoyadi) || String.IsNullOrEmpty(musteri.KullaniciAdi) || String.IsNullOrEmpty(musteri.Parola)) return hata("Gerekli yerleri doldurmalısınız");
            try
            {
                if (db.Musteris.FirstOrDefault(x => x.KullaniciAdi == musteri.KullaniciAdi) != null) { return hata("Müşteri kullanıcı adı zaten kayıtlı. Lütfen farklı bir kullanıcı adı belirleyin"); }
                musteri.EkleyenID = (Session["yonetici"] as Yonetici).Id;
                musteri.EklenmeTarihi = DateTime.Now;
                musteri.AktifMi = true;
                db.Musteris.Add(musteri);
                db.SaveChanges(); return onayYonlendir("Müşteri başarıyla eklendi", "/Yonetim/Musteriler");
            }
            catch
            {
                return hata("Müşteri eklenirken bir hata meydana geldi");
            }
        }

        public ActionResult Musteriler()
        {
            var result = db.Musteris.ToList();
            return View(result);
        }
        [HttpPost]
        public JavaScriptResult pasifyapmusteri(int? musteriid)
        {
            if (musteriid < 1) return hata("İşlem tamamlanırken bir hata meydana geldi");
            Musteri musteri = db.Musteris.Find(musteriid);
            if (musteri == null) return hata("İşlem tamamlanırken bir hata meydana geldi");
            if (musteri.AktifMi == false) return hata("Bu müşteri zaten pasif durumda");
            try
            {
                musteri.AktifMi = false;
                db.SaveChanges();
                return onayyenile("Müşteri başarıyla pasifleştirildi");
            }
            catch
            {
                return hata("İşlem tamamlanırken bir hata meydana geldi");
            }
        }
        [HttpPost]
        public JavaScriptResult aktifyapmusteri(int? musteriid)
        {
            if (musteriid < 1) return hata("İşlem tamamlanırken bir hata meydana geldi");
            Musteri musteri = db.Musteris.Find(musteriid);
            if (musteri == null) return hata("İşlem tamamlanırken bir hata meydana geldi");
            if (musteri.AktifMi == true) return hata("Bu müşteri zaten aktif durumda");
            try
            {
                musteri.AktifMi = true;
                db.SaveChanges();
                return onayyenile("Müşteri başarıyla pasifleştirildi");
            }
            catch
            {
                return hata("İşlem tamamlanırken bir hata meydana geldi");
            }
        }

        public ActionResult MusteriDetay(int? id)
        {
            if (id < 1) return RedirectToAction("Musteriler");

            Musteri musteri = db.Musteris.Find(id);
            if (musteri == null) return RedirectToAction("Musteriler");
            return View(musteri);
        }

        [HttpPost]
        public JavaScriptResult DetayMusteriProfilKaydet(Musteri musteri)
        {
            try
            {
                if (musteri == null) return hata("Müşteri bilgileri güncellenirken bir hata oluştu");
                Musteri eski = db.Musteris.Find(musteri.Id);
                if (eski == null) return hata("Müşteri bilgileri güncellenirken  bir hata oluştu");
                //if (String.IsNullOrEmpty(musteri.KullaniciAdi) || String.IsNullOrEmpty(musteri.AdiSoyadi)) return hata("Lütfen gerekli alanları eksiksiz doldurun");
                //if (eski.KullaniciAdi != musteri.KullaniciAdi)
                //{
                //    if (db.Musteris.FirstOrDefault(x => x.KullaniciAdi == musteri.KullaniciAdi) != null) return hata("Bu kullanıcı adı zaten bir doktorda kayıtlı. Lütfen farklı bir kullanıcı adı bvelirleyin");

                //}
                eski.AdiSoyadi = musteri.AdiSoyadi;
                //eski.KullaniciAdi = musteri.KullaniciAdi; Kullanıcı adı değişmemeli
                eski.Adres = musteri.Adres;
                eski.Mail = musteri.Mail;
                eski.Telefon = musteri.Telefon;
                db.SaveChanges();
                return onayyenile("Müşteri bilgileri başarıyla güncellendi");
            }
            catch
            {
                return hata("Müşteri bilgileri güncellenirken bir hata meydana geldi");
            }
        }

        #endregion
        #region ISGUzmani işlemleri

        public ActionResult ISGUzmaniEkle()
        {
            return View();
        }

        [HttpPost]
        public JavaScriptResult ISGUzmaniEkle(Uzman uzman)
        {
            try
            {
                if (uzman == null) return hata("UZman kaydedilirken bir hata oluştu");
                if (String.IsNullOrEmpty(uzman.AdiSoyadi) || String.IsNullOrEmpty(uzman.KullaniciAdi) || String.IsNullOrEmpty(uzman.Parola)) return hata("Lütfen gerekli alanları doldurun");
                if (uzman.AdiSoyadi.Length < 1 || uzman.KullaniciAdi.Length < 1 || uzman.Parola.Length < 1) return hata("Uzman kaydedilirken bir haya oluştu");
                if (db.Uzmen.FirstOrDefault(x => x.KullaniciAdi == uzman.KullaniciAdi) != null) return hata("Bu kullanıcı adı zaten kayıtlı");
                if (db.Uzmen.FirstOrDefault(x => x.Mail == uzman.Mail) != null) return hata("Bu mail zaten kayıtlı.");
                if (db.Uzmen.FirstOrDefault(x => x.Telefon == uzman.Telefon) != null) return hata("Bu telefon zaten kayıtlı");
                uzman.EkleyenID = (Session["yonetici"] as Yonetici).Id;
                uzman.EklenmeTarihi = DateTime.Now;
                uzman.AktifMi = true;
                db.Uzmen.Add(uzman);
                db.SaveChanges();
                return onayYonlendir("Uzman başarıyla kaydedildi. Uzman listesine yönlendirileceksiniz", "/Yonetim/ISGUzmanlari");
            }
            catch
            {
                return hata("Uzman eklenirken bir hata meydana geldi");
            }
        }
        public ActionResult ISGUzmanlari()
        {
            var result = db.Uzmen.ToList();
            return View(result);
        }
        [HttpPost]
        public JavaScriptResult aktifyapuzman(int? uzmanid)
        {
            if (uzmanid < 1) return hata("İşlem tamamlanırken bir hata meydana geldi");
            Uzman uzman = db.Uzmen.Find(uzmanid);
            if (uzman == null) return hata("İşlem tamamlanırken bir hata meydana geldi");
            if (uzman.AktifMi == true) return hata("Bu kullanıcı zaten aktif durumda");
            try
            {
                uzman.AktifMi = true;
                db.SaveChanges();
                return onayyenile("Uzman başarıyla aktifleştirildi");
            }
            catch
            {
                return hata("İşlem tamamlanırken bir hata meydana geldi");
            }
        }
        [HttpPost]
        public JavaScriptResult pasifyapuzman(int? uzmanid)
        {
            if (uzmanid < 1) return hata("İşlem tamamlanırken bir hata meydana geldi");
            Uzman uzman = db.Uzmen.Find(uzmanid);
            if (uzman == null) return hata("İşlem tamamlanırken bir hata meydana geldi");
            if (uzman.AktifMi == false) return hata("Bu kullanıcı zaten pasif durumda");
            try
            {
                uzman.AktifMi = false;
                db.SaveChanges();
                return onayyenile("Uzman başarıyla pasifleştirildi");
            }
            catch
            {
                return hata("İşlem tamamlanırken bir hata meydana geldi");
            }
        }

        public ActionResult ISGUzmanDetay(int? id)
        {
            if (id < 1) return RedirectToAction("ISGUzmanlari");
            Uzman uzman = db.Uzmen.Find(id);
            if (uzman == null) return RedirectToAction("ISGUzmanlari");

            return View(uzman);
        }
        [HttpPost]
        public JavaScriptResult DetayuzmanProfilKaydet(Uzman uzman)
        {
            try
            {
                if (uzman == null) return hata("uzman kaydedilirken bir hata oluştu");
                Uzman eski = db.Uzmen.Find(uzman.Id);
                if (eski == null) return hata("uzman kaydedilirken bir hata oluştu");
                //if (String.IsNullOrEmpty(uzman.KullaniciAdi) || String.IsNullOrEmpty(uzman.AdiSoyadi)) return hata("Lütfen gerekli alanları eksiksiz doldurun");
                //if (eski.KullaniciAdi != uzman.KullaniciAdi)
                //{
                //    if (db.Uzmen.FirstOrDefault(x => x.KullaniciAdi == uzman.KullaniciAdi) != null) return hata("Bu kullanıcı adı zaten bir uzmanda kayıtlı. Lütfen farklı bir kullanıcı adı bvelirleyin");

                //}
                eski.AdiSoyadi = uzman.AdiSoyadi;
                //eski.KullaniciAdi = uzman.KullaniciAdi; Kullanıcı adı değiştirilmemeli
                eski.Adres = uzman.Adres;
                eski.Mail = uzman.Mail;
                eski.Telefon = uzman.Telefon;
                db.SaveChanges();
                return onayyenile("Uzman bilgileri başarıyla güncellendi");
            }
            catch
            {
                return hata("Uzman bilgileri güncellenirken bir hata meydana geldi");
            }
        }


        [HttpPost]
        public JavaScriptResult UzmanSifreDegistir(FormCollection f)
        {
            try
            {
                if (f == null) return hata("Şifre güncellenirken bir hata meydana geldi");
                var id = Convert.ToInt32(f.Get("Id"));
                var parola = f.Get("YeniParola");
                var parolaonay = f.Get("YeniParolaOnay");
                if (String.IsNullOrEmpty(parola) || String.IsNullOrEmpty(parolaonay)) return hata("Şifre güncellenirken bir hata meydana geldi");
                if (parola != parolaonay) return hata("Parolalar uyuşmuyor. İki alana da aynı parolayı girin");
                if (id < 1) return hata("Şifre güncellenirken bir hata meydana geldi");
                Uzman uzman = db.Uzmen.Find(id);
                if (uzman == null) return hata("Şifre güncellenirken bir hata meydana geldi");
                uzman.Parola = parola;
                db.SaveChanges();
                return onayyenile("Doktor parolası başarıyla güncellendi");
            }
            catch
            {
                return hata("Şifre güncellenirken bir hata meydana geldi");
            }
        }
        #endregion

        #region Firma işlemleri
        public ActionResult Firmalar(int? id)
        {
            if (id == null && id == 0)
            {
                return View(db.Firmas.ToList());
            }

            if (id < 1) return View(db.Firmas.ToList());
            Sektor sektor = db.Sektors.Find(id);
            if (sektor == null) return View(db.Firmas.ToList());

            return View(sektor.Firmas.ToList());
        }
        public ActionResult Sektorler()
        {
            var result = db.Sektors.ToList();

            return View(result);
        }
        [HttpPost]
        public JavaScriptResult SektorEkle(Sektor sektor)
        {
            if (sektor == null) hata("Lütfen bir sektör ismi giriniz");
            if (String.IsNullOrEmpty(sektor.Adi)) return hata("Sektör adı girmelisiniz");
            if (sektor.Adi.Length < 2 || sektor.Adi.Length > 50) return hata("Lütfen 3 ile 50 karakter arasında giriniz");
            if (db.Sektors.FirstOrDefault(x => x.Adi == sektor.Adi) != null) return hata("Bu sektör zaten kayıtlı. Lütfen farklı bir isim verin");
            try
            {
                db.Sektors.Add(sektor);
                db.SaveChanges();
                return onayyenile("Sektör kaydedildi");

            }
            catch
            {
                return hata("Sektör eklenirken bir hata meydana geldi");
            }
        }
        public ActionResult __SektorDuzenle(int? id)
        {
            if (id < 1) return RedirectToAction("Sektorler");
            Sektor sektor = db.Sektors.Find(id);
            if (sektor == null) return null;

            return PartialView(sektor);

        }
        [HttpPost]
        public JavaScriptResult __SektorDuzenle(Sektor sektor)
        {
            if (sektor == null) return hata("Sektör düzenlenemedi");
            if (String.IsNullOrEmpty(sektor.Adi) || sektor.Adi.Length < 3) return hata("Sektör adı boş veya 3 karakterden az olamaz");
            Sektor eski = db.Sektors.Find(sektor.Id);
            if (eski == null) return hata("Sektör bulunamadı");
            eski.Adi = sektor.Adi;
            db.SaveChanges();
            return sadeceYonlendir("/Yonetim/Sektorler");
        }
        [HttpPost]
        public ActionResult __SektorSil(int? id)
        {
            if (id < 1) return hata("Sektör bulunamadı");
            Sektor sektor = db.Sektors.Find(id);
            if (sektor == null) return hata("Sektör bulunamadı");
            if (sektor.Firmas.ToList().Count > 0) return hata("Bu sektöre ait firmalar olduğu için sektör silinemedi");
            db.Sektors.Remove(sektor);
            db.SaveChanges();
            return onayyenile("Sektör başarıyla silindi");

        }
        public ActionResult FirmaDetay(int? id)
        {
            if (id < 1) return RedirectToAction("Firmalar");
            Firma firma = db.Firmas.Find(id);
            if (firma == null) return RedirectToAction("Firmalar");

            return View(firma);
        }
        [HttpPost]
        public string IlceleriGetir(int? id)
        {
            try
            {
                Il il = db.Ils.Find(id);
                StringBuilder sb = new StringBuilder();
                foreach (var item in il.Ilces.ToList())
                {
                    sb.Append("<option value=\"" + item.Id + "\">" + item.IlceAdi + "</option>");
                }
                return sb.ToString();
            }
            catch { return "<option disabled selected>Tanımsız</option>"; }
        }
        public ActionResult FirmaEkle()
        {
            ViewBag.iller = db.Ils.ToList();
            ViewBag.musteriler = db.Musteris.ToList();
            ViewBag.sektorler = db.Sektors.ToList();
            return View();
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
        [HttpPost]
        public ActionResult FirmaEkle(Firma firma, HttpPostedFileBase ResimYolu)
        {
            ViewBag.iller = db.Ils.ToList();

            ViewBag.musteriler = db.Musteris.ToList();
            ViewBag.sektorler = db.Sektors.ToList();


            try
            {

                firma.EkleyenID = benyonetici.Id;
                firma.EklenmeTarihi = DateTime.Now;
                firma.TamamlandiMi = false;
                if (db.Firmas.FirstOrDefault(x => x.Adi == firma.Adi) != null) return View();
                if (ResimYolu != null)
                {
                    WebImage img = new WebImage(ResimYolu.InputStream);
                    img.Resize(300, 300, false, false);
                    FileInfo fotoinfo = new FileInfo(ResimYolu.FileName);
                    if (resimMi(fotoinfo))
                    {
                        string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        img.Save("~/Content/images/firmalar/" + newfoto);
                        firma.ResimYolu = "~/Content/images/firmalar/" + newfoto;
                    }
                }
                db.Firmas.Add(firma);
                db.SaveChanges();
                return RedirectToAction("FirmaDetay", new { id = firma.Id });

            }
            catch (Exception ex)
            {
                string hata = ex.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult ajaxSektorKaydet(Sektor sektor)
        {
            try
            {
                if (db.Sektors.FirstOrDefault(x => x.Adi == sektor.Adi) != null) { return Json(new { Sonuc = 0, mesaj = "Bu sektör zaten var" }); }
                db.Sektors.Add(sektor);
                db.SaveChanges();
                StringBuilder sb = new StringBuilder();
                foreach (var item in db.Sektors.ToList())
                {
                    if (item.Adi == sektor.Adi)
                    {
                        sb.Append("<option value=\"" + item.Id + "\" selected>" + item.Adi + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value=\"" + item.Id + "\">" + item.Adi + "</option>");
                    }
                }
                return Json(new
                {
                    Sonuc = 1,
                    html = sb.ToString(),
                    mesaj = "Sektör başarıyla kaydedildi"
                });
            }
            catch
            {
                return Json(new { Sonuc = 0, mesaj = "Sektör eklenirken bir hata meydana geldi" });
            }

        }
        [HttpPost]
        public ActionResult ajaxYeniMusteriKaydet(Musteri musteri)
        {
            if (musteri == null) return Json(new { Sonuc = 0, mesaj = "Müşteri eklenemedi" });
            if (String.IsNullOrEmpty(musteri.AdiSoyadi) || String.IsNullOrEmpty(musteri.KullaniciAdi) || String.IsNullOrEmpty(musteri.Parola)) return Json(new { Sonuc = 0, mesaj = "Gerekli yerleri doldurmalısınız" });
            try
            {
                if (db.Musteris.FirstOrDefault(x => x.KullaniciAdi == musteri.KullaniciAdi) != null) { return Json(new { Sonuc = 0, mesaj = "Bu kullanıcı adı zaten kayıtlı. Lütfen farklı bir kullanıcı adı belirleyin" }); }
                musteri.EkleyenID = (Session["yonetici"] as Yonetici).Id;
                musteri.EklenmeTarihi = DateTime.Now;
                musteri.AktifMi = true;
                db.Musteris.Add(musteri);
                db.SaveChanges();
                StringBuilder sb = new StringBuilder();
                foreach (var item in db.Musteris.ToList())
                {
                    if (item.KullaniciAdi == musteri.KullaniciAdi)
                    {
                        sb.Append("<option value=\"" + item.Id + "\" selected>" + item.AdiSoyadi.ToUpper() + " - " + item.KullaniciAdi + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value=\"" + item.Id + "\">" + item.AdiSoyadi.ToUpper() + " - " + item.KullaniciAdi + "</option>");
                    }
                }
                return Json(new { Sonuc = 1, html = sb.ToString(), mesaj = "Müşteri başarıyla eklendi" });

            }
            catch
            {
                return Json(new { Sonuc = 0, mesaj = "Müşteri eklenirken bir hata meydana geldi" });
            }
        }
        public ActionResult FirmaDuzenle(int? id)
        {
            try
            {
                ViewBag.iller = db.Ils.ToList();
                ViewBag.musteriler = db.Musteris.ToList();
                ViewBag.sektorler = db.Sektors.ToList();
                Firma firma = db.Firmas.Find(id);
                if (firma == null) return RedirectToAction("Firmalar");
                return View(firma);
            }
            catch
            {
                return RedirectToAction("Firmalar");
            }
        }
        [HttpPost]
        public JavaScriptResult FirmaDuzenle(Firma firma, HttpPostedFileBase ResimYolu)
        {
            try
            {
                if (firma == null) return hata("Firma güncellenemedi. Lütfen gerekli yerleri doldurduğunuzdan emin olup tekrar deneyin");
                Firma eski = db.Firmas.Find(firma.Id);
                if (eski == null) return hata("Firma güncellenemedi. Lütfen gerekli yerleri doldurduğunuzdan emin olup tekrar deneyin");

                eski.IlceID = firma.IlceID;
                eski.Adresi = firma.Adresi;
                eski.Telefonu = firma.Telefonu;
                eski.Sinifi = firma.Sinifi;
                eski.VergiDairesi = firma.VergiDairesi;
                eski.VergiNumarasi = firma.VergiNumarasi;
                eski.LatLng = firma.LatLng;
                eski.Web = firma.Web;
                eski.Mail = firma.Mail;
                eski.CalisanSayisi = firma.CalisanSayisi;
                eski.MusteriID = firma.MusteriID;
                eski.SektorId = firma.SektorId;
                if (ResimYolu != null)
                {
                    string eskiyol = eski.ResimYolu;
                    WebImage img = new WebImage(ResimYolu.InputStream);
                    img.Resize(300, 300, false, false);
                    FileInfo fotoinfo = new FileInfo(ResimYolu.FileName);
                    if (resimMi(fotoinfo))
                    {
                        string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        img.Save("~/Content/images/firmalar/" + newfoto);
                        eski.ResimYolu = "~/Content/images/firmalar/" + newfoto;
                    }
                    try
                    {
                        if (System.IO.File.Exists(Server.MapPath(eskiyol)))
                        {
                            System.IO.File.Delete(Server.MapPath(eskiyol));
                        }
                    }
                    catch { }

                }


                db.SaveChanges();
                return onayYonlendir("Firma başarıyla güncellendi", "/Yonetim/FirmaDetay/" + eski.Id);
            }
            catch
            {
                return hata("Firma güncellenemedi. Lütfen gerekli yerleri doldurduğunuzdan emin olup tekrar deneyin");
            }



            //try
            //{
            //    if (firma == null) return hata("Firma güncellenemedi. Lütfen gerekli yerleri doldurduğunuzdan emin olup tekrar deneyin");
            //    Firma eski = db.Firmas.Find(firma.Id);
            //    if (eski == null) return hata("Firma güncellenemedi. Lütfen gerekli yerleri doldurduğunuzdan emin olup tekrar deneyin");

            //    eski.IlceID = firma.IlceID;
            //    eski.Adresi = firma.Adresi;
            //    eski.Telefonu = firma.Telefonu;
            //    eski.Sinifi = firma.Sinifi;
            //    eski.VergiDairesi = firma.VergiDairesi;
            //    eski.VergiNumarasi = firma.VergiNumarasi;
            //    eski.LatLng = firma.LatLng;
            //    eski.Web = firma.Web;
            //    eski.Mail = firma.Mail;
            //    eski.CalisanSayisi = firma.CalisanSayisi;
            //    eski.MusteriID = firma.MusteriID;
            //    eski.SektorId = firma.SektorId;


            //    db.SaveChanges();
            //    return onayyenile("Firma başarıyla güncellendi");
            //}
            //catch
            //{
            //    return hata("Firma güncellenemedi. Lütfen gerekli yerleri doldurduğunuzdan emin olup tekrar deneyin");
            //}
        }
        #endregion

        #region Firma Yetkiİşlemleri
        public PartialViewResult _FirmaDoktorlari(int? id)
        {
            if (id < 1) return null;
            var result = db.FirmaDoktors.Where(x => x.FirmaID == id);

            ViewBag.doktorlar = db.Doktors.Where(x => x.AktifMi == true).ToList();
            ViewBag.firma = db.Firmas.Find(id);
            return PartialView(result);
        }
        public PartialViewResult _FirmaUzmanlari(int? id)
        {
            if (id < 1) return null;
            IEnumerable<FirmaUzman> result = db.FirmaUzmen.Where(x => x.FirmaID == id);
            ViewBag.firma = db.Firmas.Find(id);
            List<Uzman> uzmanlar = db.Uzmen.Where(x => x.AktifMi == true).ToList();
            ViewBag.uzmanlar = uzmanlar;
            return PartialView(result);
        }

        [HttpPost]
        public int FirmaDoktorYetkilendir(int? firmaid, int? doktorid)
        {
            try
            {
                Firma firma = db.Firmas.Find(firmaid);
                Doktor doktor = db.Doktors.Find(doktorid);
                FirmaDoktor eski = db.FirmaDoktors.FirstOrDefault(x => x.FirmaID == firma.Id && x.DoktorID == doktor.Id);
                if (eski != null) return 0;
                FirmaDoktor fd = new FirmaDoktor()
                {
                    DoktorID = doktor.Id,
                    FirmaID = firma.Id,
                    AtayanID = (Session["yonetici"] as Yonetici).Id,
                    AtanmaTarihi = DateTime.Now
                };
                db.FirmaDoktors.Add(fd); db.SaveChanges(); return 1;
            }
            catch
            {
                return 0;
            }
        }
        [HttpPost]
        public int FirmaDoktorYetkiyiSil(int? firmaid, int? doktorid)
        {
            try
            {
                FirmaDoktor fd = db.FirmaDoktors.FirstOrDefault(x => x.FirmaID == firmaid && x.DoktorID == doktorid);
                if (fd != null)
                {
                    db.FirmaDoktors.Remove(fd);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch
            {
            }
            return 0;
        }
        [HttpPost]
        public int FirmaUzmanYetkilendir(int? firmaid, int? uzmanid)
        {
            try
            {
                Firma firma = db.Firmas.Find(firmaid);
                Uzman uzman = db.Uzmen.Find(uzmanid);
                FirmaUzman fd = new FirmaUzman()
                {
                    UzmanID = uzman.Id,
                    FirmaID = firma.Id,
                    AtayanID = (Session["yonetici"] as Yonetici).Id,
                    AtanmaTarihi = DateTime.Now
                };
                db.FirmaUzmen.Add(fd); db.SaveChanges(); return 1;
            }
            catch
            {
                return 0;
            }
        }
        [HttpPost]
        public int FirmaUzmanYetkiSil(int? firmaid, int? uzmanid)
        {
            try
            {
                FirmaUzman fu = db.FirmaUzmen.FirstOrDefault(x => x.FirmaID == firmaid && x.UzmanID == uzmanid);
                if (fu != null)
                {
                    db.FirmaUzmen.Remove(fu);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch { }
            return 0;
        }
        public ActionResult FirmaDoktorYonet(int? id)
        {
            try
            {
                FirmaDoktor firmaDoktor = db.FirmaDoktors.Find(id);
                if (firmaDoktor != null)
                {
                    return View(firmaDoktor);
                }
                return RedirectToAction("Index");

            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult FirmaUzmanYonet(int? id)
        {
            try
            {
                FirmaUzman fu = db.FirmaUzmen.Find(id);
                if (fu != null)
                {

                    return View(fu);
                }
                return RedirectToAction("Firmalar");
            }
            catch (Exception)
            {

                return RedirectToAction("Firmalar");
            }


        }

        #endregion

        #region Çalışan İşlemleri


        #endregion
        #region Bildirim işleri
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
        #endregion

        #region Çalışan İşlemleri
        public ActionResult CalisanOlustur()
        {
            List<Firma> firmalar = db.Firmas.ToList();
            return View(firmalar);
        }

        [HttpPost]
        public JavaScriptResult CalisanOlustur(Calisan gelen)
        {
            try
            {
                
                gelen.EklenmeTarihi = DateTime.Now;
                db.Calisans.Add(gelen);
                db.SaveChanges();
                Firma firma = db.Firmas.Find(db.IsyeriBolumus.Find(gelen.IsyeriBolumID).FirmaID);
                return onayYonlendir("Personel başarıyla kaydedildi.", "/Yonetim/FirmaDetay/" + firma.Id);
            }
            catch
            {
            }
            return hata("Personel kaydolmadı. Tüm alanları doldurduğunuzdan emin olup tekrar deneyin");
        }
        [HttpPost]
        public String BolumGetir(int? id)
        {
            try
            {
                Firma firma = db.Firmas.Find(id);
                StringBuilder sb = new StringBuilder();
                foreach (var item in firma.IsyeriBolumus.ToList())
                {
                    sb.Append("<option value=\"" + item.Id + "\">" + item.Adi + "</option>");
                }
                return sb.ToString();
            }
            catch { }
            return "";
        }
        [HttpPost]
        public String FirmaBolumEkleVeGetir(int? firmaid, string bolumadi)
        {
            try
            {
                Firma firma = db.Firmas.Find(firmaid);

                IsyeriBolumu bolum = new IsyeriBolumu()
                {
                    FirmaID = firma.Id,
                    Adi = bolumadi
                };
                db.IsyeriBolumus.Add(bolum);
                db.SaveChanges();
                return BolumGetir(firma.Id);
            }
            catch { }
            return "";
        }

        #endregion



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ben == null)
                ben = Session["yonetici"] as Yonetici;

            base.OnActionExecuting(filterContext);
        }
        public PartialViewResult _TopBar()
        {
            return PartialView();
        }

    }
}