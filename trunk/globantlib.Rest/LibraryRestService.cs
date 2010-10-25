﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using globantlib.Business;
using globantlib.Domain;
//using globantlib.DataAccess;

namespace globantlib.Rest
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
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

        [WebGet(UriTemplate = "Search?Text={text}", ResponseFormat = WebMessageFormat.Xml)]
        public List<Content> SearchCollection(String text)
        {
            List<Content> t = libEntities.SearchContents(0, 50, text);
            return t;
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public Content Create(Content instance)
        {
            //libEntities.Contents.AddObject(instance);
            //libEntities.SaveChanges();
            return instance;
        }

        [WebGet(UriTemplate = "{id}")]
        public Content Get(string id)
        {
            int i = int.Parse(id);
            return libEntities.GetContents().Where<Content>(x => x.ID == i).FirstOrDefault<Content>();
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
