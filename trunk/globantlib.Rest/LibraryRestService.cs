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
    [ServiceKnownType(typeof(Content))]
    [ServiceKnownType(typeof(Response))]
    [ServiceKnownType(typeof(Error))]
    [ServiceKnownType(typeof(Reviews))]
    [ServiceKnownType(typeof(Review))]
    [ServiceKnownType(typeof(User))]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
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
            
            int pages = 1 + (int)(count / page_size);
            for (int i = 0; i < pages; i++)
            {
                resp.Pages.Add(new Page() { number = i+1, current = false });
            }
            if (count > 0)
            {
                resp.Pages[actual_page].current = true;
                result = resp;
            }
            else
                result = new Error() { Message = "Your query didn't generate any results." };            

            return result;
        }

        [IncludeXmlDeclaration]
        [WebInvoke(UriTemplate = "", Method = "POST")]
        public IResponse Create(Content instance)
        {
            libEntities.Create(instance);
            return instance;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "{id}")]
        public IResponse Get(string id)
        {
            int i = int.Parse(id);
            IResponse result = libEntities.GetContent(i);

            if (result == null)
                result = new Error() { Message = "The content you're trying to reach doesn't exist or has been removed." };

            return result;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public IResponse Update(string id, Content instance)
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

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "Review?ContentId={id}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        public IResponse GetReviews(String id)
        {
            IResponse result = null;
            
            int i = 0;
            int.TryParse(id, out i);
            Reviews reviews = new Reviews(libEntities.GetReviews(i));
            result = reviews;
            return result;
        }

        [IncludeXmlDeclaration]
        [WebInvoke(UriTemplate = "SubmitReview?ContentId={id}", Method = "POST")]
        public IResponse Submit(String id, Review instance)
        {
            IResponse result = null;

            int i = 0;
            if(int.TryParse(id, out i))
                result = libEntities.SubmitReview(i, instance);

            if (result == null)
                result = new Error() { Message = "The content you're trying to reach doesn't exist or has been removed." };

            return result;
        }
    }
}
