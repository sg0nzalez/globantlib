using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using globantlib.Business;
using globantlib.Domain;
using System.Runtime.Serialization;
using System.Net;
using System.Xml;
using System.IO;
using System.ServiceModel.Channels;

namespace globantlib.Rest
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceKnownType(typeof(DeviceType))]
    [ServiceKnownType(typeof(Lease))]
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

        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Xml)]
        public List<DeviceType> GetCollection()
        {
            List<DeviceType> l = libEntities.GetDeviceTypes();
            return l;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "/DeviceCalendar?Type={typeID}&Id={id}&Month={month}&Year={year}")]
        public List<Domain.Types> Get(String typeID, String id, String month, String year)
        {
            int  TypeID = 0;
            int.TryParse(typeID, out TypeID);
            int  Id = 0;
            int.TryParse(id, out Id);
            int  Month = 0;
            int.TryParse(month, out Month);
            int  Year = 0;
            int.TryParse(year, out Year);
            List<Domain.Types> l = libEntities.GetDevicesbyType(TypeID, Id, Month, Year);
            return l;
        }

        [IncludeXmlDeclaration]
        [WebInvoke(UriTemplate = "/LeaseSubmit", Method = "POST")]
        public IResponse Create(Lease instance)
        {
            libEntities.Create(instance);
            return instance;
        }

        [IncludeXmlDeclaration]
        [WebInvoke(UriTemplate = "/Device", Method = "POST")]
        public IResponse CreateDevice(Device instance)
        {
            libEntities.Create(instance);
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