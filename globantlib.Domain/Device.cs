using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace globantlib.Domain
{

    [DataContract(Namespace = "")]
    public class Device
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int LeasableID { get; set; }

        [DataMember]
        public int TypeID { get; set; }

        [DataMember]
        public List<Domain.Lease> Leases { get; set; }
    }
}
