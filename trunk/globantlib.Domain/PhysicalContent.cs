using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace="", Name="Physical")]
    public class PhysicalContent
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public String Type { get; set; }
    }
}
