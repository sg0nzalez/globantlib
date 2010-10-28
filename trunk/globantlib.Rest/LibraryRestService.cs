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
    [ServiceKnownType(typeof(Content))]
    [ServiceKnownType(typeof(Response))]
    [ServiceKnownType(typeof(Error))]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class LibraryRestService
    {
        LibraryManager libEntities;

        public LibraryRestService()
        {
            libEntities = new LibraryManager();
        }

        [WebGet(UriTemplate = "", ResponseFormat=WebMessageFormat.Xml)]
        public List<Content> GetCollection()
        {
            List<Content> t = libEntities.GetContents();
            return t;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "Search?Text={text}&Page={page}", RequestFormat= WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle=WebMessageBodyStyle.Bare)]
        public IResponse SearchCollection(String text, String page)
        {
            IResponse result;
            int page_size = 4;
            int actual_page, count;
            int.TryParse(page,out actual_page);
            
            if (actual_page == 0)
                actual_page = 1;

            actual_page -= 1;

            Response resp = new Response();
            resp.ArrayOfContents = libEntities.SearchContents(actual_page, page_size, text, out count);
            resp.Pages = new List<Page>();
            for (int i = 0; i < count / page_size; i++)
            {
                resp.Pages.Add(new Page() { number = i+1, current = false });
            }
            if (resp.Pages.Count > 0)
            {
                resp.Pages[actual_page].current = true;
                result = resp;
            }
            else
                result = new Error() { Message = "Not found" };            

            return result;
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public Content Create(Content instance)
        {
            //libEntities.Contents.AddObject(instance);
            //libEntities.SaveChanges();
            return instance;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "{id}")]
        public IResponse Get(string id)
        {
            int i = int.Parse(id);
            IResponse result = libEntities.GetContent(i);

            if (result == null)
                result = new Error() { Message = "Not Found" };

            return result;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public Content Update(string id, Content instance)
        {
            //libEntities.Attach(instance);
            //var stateEntry = libEntities.ObjectStateManager.GetObjectStateEntry(instance.ID);
            //var propertyNameList = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata.Select(pn => pn.FieldType.Name);
            //foreach (var propName in propertyNameList)
            //    stateEntry.SetModifiedProperty(propName);
            //libEntities.SaveChanges();
            return null;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            int i = int.Parse(id);          

        }

    }
}
