using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class Date : IResponse
    {
        [DataMember]
        public int Number;

        [DataMember]
        public string Username;

    }
}
