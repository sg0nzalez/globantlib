using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace globantlib.Rest
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();

            //this.Context.Response.ContentType = "application/xml";
            //this.Context.Response.ContentType = "application/xhtml+xml";
        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("LibraryService", new WebServiceHostFactory(), typeof(LibraryRestService)));
            RouteTable.Routes.Add(new ServiceRoute("DeviceService", new WebServiceHostFactory(), typeof(DeviceService)));
        }
    }
}
