using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using globantlib.Business;
using globantlib.Domain;

namespace globantlib.Rest
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class DeviceService
    {
        DeviceManager libEntities;

        public DeviceService()
        {
            libEntities = new DeviceManager();
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Xml)]
        public List<DeviceType> GetCollection()
        {
            List<DeviceType> l = libEntities.GetDeviceTypes();
            return l;
        }

        [IncludeXmlDeclaration]
        //[WebGet(UriTemplate = "View?Id={id}&Year={year}&Month={month}")]
        [WebGet(UriTemplate = "{id}")]
        public List<Device> Get(String id)
        {
            int typeID = int.Parse(id);
            //int monthID = int.Parse(month);
            //int yearID = int.Parse(year);
            //List<Device> l = libEntities.GetDevicesbyType(typeID, monthID, yearID);
            List<Device> l = libEntities.GetDevicesbyType(typeID);
            return l;
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public Device Create(Device instance)
        {
            return instance;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public Device Update(string id, Device instance)
        {
            return instance;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {

        }

        
       
    }
}