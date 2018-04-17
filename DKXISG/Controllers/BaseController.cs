using DKXISG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DKXISG.Controllers
{
    public class BaseController : Controller
    {
        dkxisgContext db = new dkxisgContext();
        public Uzman benuzman;
        public Yonetici benyonetici;
        public Doktor bendoktor;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (bendoktor == null) bendoktor = db.Doktors.FirstOrDefault(d => d.KullaniciAdi == "dkadem");
            if (benyonetici == null) benyonetici = db.Yoneticis.Find(1);
            if (benuzman == null) benuzman = db.Uzmen.Find(5);
            Session["yonetici"] = benyonetici;
            Session["doktor"] = bendoktor;
            Session["uzman"] = benuzman;
            //if (Session["Admin"] == null)
            //{
                
            //    filterContext.Result = new RedirectResult("~/Login/Index");
            //}

            base.OnActionExecuting(filterContext);
        }
	}
}