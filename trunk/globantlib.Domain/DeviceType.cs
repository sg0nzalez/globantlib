using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace globantlib.Domain
{

    [DataContract(Namespace = "")]
    public class DeviceType
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Image { get; set; }
    }
}