using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DKXISG.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            

            bundles.Add(new StyleBundle("~/bundles/Genel").Include(
                "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                "~/Content/assets/global/plugins/bootstrap-sweetalert/sweetalert.css",
                "~/Content/assets/global/css/components-md.min.css",
                "~/Content/assets/global/css/plugins-md.min.css",
                "~/Content/assets/layouts/layout/css/layout.min.css",
                "~/Content/assets/layouts/layout/css/themes/darkblue.min.css",
                "~/Content/assets/layouts/layout/css/custom.min.css",
                "~/Scripts/dropzone/basic.min.css",
                "~/Scripts/dropzone/dropzone.min.css"
                ));
        }
    }
}