using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class BookRequest
    {
        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String UserName { get; set; }
    }
}
