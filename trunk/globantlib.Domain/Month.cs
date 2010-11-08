using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class Month : IResponse
    {
        [DataMember]
        public string Name;

        [DataMember]
        public List<Date> Dates;

    }
}
