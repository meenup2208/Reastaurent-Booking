using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RestaurentMVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) 
        {
            bundles.UseCdn = true;
          
            StyleBundle MyCssBundles = new StyleBundle("~/Content/MyCSS");
            MyCssBundles.Include("~/Content/Site.css",
                                "~/Content/bootstrap.min.css", 
                                "~/Content/DataTables/css/jquery.dataTables.min.css",
                                "~/Content/bootstrap-theme.min.css"
                               );
         
            ScriptBundle MyJsBundles = new ScriptBundle("~/Scripts/MyJS");
            MyJsBundles.Include("~/Scripts/jquery-3.5.1.min.js",
                                "~/Scripts/DataTables/jquery.dataTables.min.js", 
                                "~/Scripts/jquery.validate.min.js","~/Scripts/bootstrap.min.js", 
                                "~/Scripts/toastr.min.js",
                                "~/Scripts/DataTables/dataTables.buttons.min.js", 
                                "~/Scripts/jszip.min.js", 
                                "~/Scripts/DataTables/buttons.html5.min.js", 
                                "~/Scripts/DataTables/buttons.print.min.js"
                                );
           
            bundles.Add(MyCssBundles);
            bundles.Add(MyJsBundles);

            //BundleTable.EnableOptimizations = true;

        }
    }
}