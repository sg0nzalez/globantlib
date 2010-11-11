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
    [ServiceKnownType(typeof(Content))]
    [ServiceKnownType(typeof(Response))]
    [ServiceKnownType(typeof(Error))]
    [ServiceKnownType(typeof(ReviewCollection))]
    [ServiceKnownType(typeof(Review))]
    [ServiceKnownType(typeof(BookRequestCollection))]
    [ServiceKnownType(typeof(BookRequest))]
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

        [WebGet(UriTemplate = "", ResponseFormat = WebMessageFormat.Xml)]
        public List<Content> GetCollection()
        {
            List<Content> t = libEntities.GetContents();
            return t;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "?Text={text}", ResponseFormat = WebMessageFormat.Xml)]
        public IResponse GetFilteredCollection(String text)
        {
            IResponse result = null;

            Response t = new Response();
            t.ArrayOfContents = libEntities.GetContents(text);

            if (t.ArrayOfContents.Count == 0)
            {
                result = new Error() { Message = "Your query didn't generate any results." };
            }
            else
                result = t;

            return result;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "/ContentCalendar?Type={typeID}&Id={id}&Month={month}&Year={year}")]
        public List<Domain.Types> GetContentCalendar(String typeID, String id, String month, String year)
        {
            int Id = 0;
            int.TryParse(id, out Id);
            int Month = 0;
            int.TryParse(month, out Month);
            int Year = 0;
            int.TryParse(year, out Year);
            int ContentID = 0;
            int.TryParse(typeID, out ContentID);
            List<Domain.Types> l = libEntities.GetPhysicals(ContentID, Id, Month, Year);
            return l;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "Search?Text={text}&Page={page}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        public IResponse SearchCollection(String text, String page)
        {
            IResponse result;
            int page_size = 5;
            int actual_page, count;
            int.TryParse(page, out actual_page);

            if (actual_page == 0)
                actual_page = 1;

            actual_page -= 1;

            Response resp = new Response();
            resp.ArrayOfContents = libEntities.SearchContents(actual_page, page_size, text, out count);
            resp.Pages = new List<Page>();

            int pages = (int)Math.Ceiling((double)(count / page_size));
            if (count % page_size != 0)
                pages++;

            for (int i = 0; i < pages; i++)
            {
                resp.Pages.Add(new Page() { number = i + 1, current = false });
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
            int i = 0;
            int.TryParse(id, out i);
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
            libEntities.Update(instance);
            return null;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            int i = int.Parse(id);
            libEntities.Delete(i);
        }

        [WebInvoke(UriTemplate = "Digitals/{id}", Method = "DELETE")]
        public void DeleteDigital(string id)
        {
            int i = int.Parse(id);
            libEntities.DeleteDigital(i);
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "Review?ContentId={id}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        public IResponse GetReviews(String id)
        {
            IResponse result = null;

            int i = 0;
            int.TryParse(id, out i);
            ReviewCollection reviews = new ReviewCollection(libEntities.GetReviews(i));

            if (reviews.Count == 0)
                result = new Error() { Message = "There are no reviews to this book." };
            else
                result = reviews;

            return result;
        }

        [IncludeXmlDeclaration]
        [WebInvoke(UriTemplate = "SubmitReview?ContentId={id}", Method = "POST")]
        public IResponse Submit(String id, Review instance)
        {
            IResponse result = null;

            int i = 0;
            if (int.TryParse(id, out i))
                result = libEntities.SubmitReview(i, instance);

            if (result == null)
                result = new Error() { Message = "The content you're trying to reach doesn't exist or has been removed." };

            return result;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "BookRequests", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare)]
        public IResponse GetBookRequest()
        {
            IResponse result = null;

            BookRequestCollection bookreq = libEntities.GetBookRequests();

            if (bookreq.Count == 0)
                result = new Error() { Message = "." };
            else
                result = bookreq;



            return result;
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "BookRequest?Text={text}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void SubmitBookRequest(String text)
        {
            libEntities.SubmitBookRequest(text);   
        }

        [IncludeXmlDeclaration]
        [WebGet(UriTemplate = "SearchBookRequest?Text={text}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public Message SearchBookRequest(String text)
        {
            string result = "";

            // Create the web request  
            HttpWebRequest request = WebRequest.Create("http://books.google.com/books/feeds/volumes?q=" + text) as HttpWebRequest;
            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());

                // Read the whole contents and return as a string  
                result = reader.ReadToEnd();
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.InnerXml = @"<?xml version='1.0' encoding='utf-8'?><feed><entry></entry><title></title><entry></entry><entry></entry></feed>";

            xDoc.InnerXml = result;

            XmlElementBodyWriter writer = new XmlElementBodyWriter(xDoc.DocumentElement);

            Message msg = Message.CreateMessage(MessageVersion.None,
                OperationContext.Current.OutgoingMessageHeaders.Action, writer);

            return msg;
        }
    }

    public class XmlElementBodyWriter : BodyWriter
    {
        XmlElement xmlElement;

        public XmlElementBodyWriter(XmlElement xmlElement)
            : base(true)
        {
            //xmlElement.Attributes.RemoveAll();
            this.xmlElement = xmlElement;

        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            xmlElement.WriteTo(writer);
        }
    }
}
