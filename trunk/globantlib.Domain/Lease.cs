using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class Lease : IResponse
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Month { get; set; }

        [DataMember]
        public int StartDate { get; set; }

        [DataMember]
        public int EndDate { get; set; }

        [DataMember]
        public string Email { get; set; }

    }
}
