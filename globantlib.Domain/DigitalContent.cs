using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "", Name = "Digital")]
    public class DigitalContent
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public String Link { get; set; }

        [DataMember]
        public String Format { get; set; }
    }
}
