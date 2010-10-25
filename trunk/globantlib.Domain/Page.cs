using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class Page
    {
        [DataMember]
        public int number { get; set; }

        [DataMember]
        public bool current { get; set; }
    }
}
