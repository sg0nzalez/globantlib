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

            this.Context.Response.ContentType = "application/xml";
            //this.Context.Response.ContentType = "application/xhtml+xml";
        }

        private void RegisterRoutes()
        {
            var factory = new WebServiceHostFactory();
            RouteTable.Routes.Add(new ServiceRoute("LibraryService.mvc", factory, typeof(LibraryRestService)));
            RouteTable.Routes.Add(new ServiceRoute("DeviceService.mvc", factory, typeof(DeviceService)));
        }
    }
}
