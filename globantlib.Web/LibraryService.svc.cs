using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace globantlib.Web
{
    public class LibraryService : ILibraryService
    {

        #region ILibraryService Members

        public string GetLibraryContent()
        {
            return "<xml/>";
        }

        public string GetDevices()
        {
            return "<xml/>";
        }

        public string GetUsers()
        {
            return "<xml/>";
        }

        #endregion
    }
}
