using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace globantlib.Web
{
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        [WebGet]
        string GetLibraryContent();

        [OperationContract]
        [WebGet]
        string GetDevices();

        [OperationContract]
        [WebGet]
        string GetUsers();
    }
}
