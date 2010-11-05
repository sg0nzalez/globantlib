using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace globantlib.Domain
{

    [DataContract(Namespace = "")]
    public class Item : IResponse
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public bool Current;

        public string Name { get; set; }

        [DataMember]
        public List<Month> Lease;

    }
}