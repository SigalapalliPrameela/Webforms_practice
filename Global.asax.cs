using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebForm.App_Start;
using System.Web.Http;


namespace WebForm
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {

            GlobalConfiguration.Configure(
          WebForm.App_Start.WebApiConfig.Register);
            // Code that runs on application startup
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}