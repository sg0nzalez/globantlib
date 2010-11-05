using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace globantlib.Domain
{

    [DataContract(Namespace = "")]
    public class Types : IResponse
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public List<Item> Items { get; set; }
    }
}