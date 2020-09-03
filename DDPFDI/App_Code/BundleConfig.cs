using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

/// <summary>
/// Summary description for BundleConfig
/// </summary>
public class BundleConfig
{
    public BundleConfig()
    {
    }
    public static void RegisterBundles(BundleCollection bundles)
    {
        bundles.Add(new StyleBundle("~/bundles/HCLogin").Include(
           "~/assets/css/bootstrap.css",
           "~/assets/css/all.min.css",
           "~/assets/css/style.css",
           "~/assets/css/custom.css",
           "~/assets/css/responsive.css",
           "~/assets/css/font-awesome.min.css",
           "~/assets/css/font-awsome.css"
           ));
        bundles.Add(new StyleBundle("~/bundles/HCss").Include(
            "~/assets/css/bootstrap.css",
            "~/assets/css/all.min.css",
            "~/assets/css/style.css",
            "~/assets/css/select2.css",
            "~/assets/css/responsive.css",
            "~/assets/css/jquery-ui.css",
            "~/assets/css/aaryan.css",
            "~/assets/css/jquery.fancybox.min.css",
            "~/assets/css/multiselect.css",
            "~/assets/css/custom.css",
            "~/assets/css/summernote-bs4.css",
            "~/assets/css/fonts-googleapis.css",
            "~/assets/css/font-awesome.min.css",
            "~/assets/css/font-awsome.css"
         ));

        bundles.Add(new StyleBundle("~/bundles/HjsLogin").Include(
         "~/assets/js/bootstrap.min.js",
         "~/assets/js/custom.js"));

        bundles.Add(new StyleBundle("~/bundles/HJs").Include(
             "~/assets/js/jquery-ui.min.js",
             "~/assets/js/bootstrap.min.js",
             "~/assets/js/dataTables.bootstrap.min.js",
             "~/assets/js/jquery.dataTables.js",
             "~/assets/js/select2.min.js",
             "~/assets/js/jquery.fancybox.min.js",
             "~/assets/js/multiselect.min.js",
             "~/assets/js/summernote-bs4.js",
             "~/assets/js/custom.js"
         ));
        //BundleTable.EnableOptimizations = true;
    }
}
