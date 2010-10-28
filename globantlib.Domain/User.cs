using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class User
    {
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String Thumbnail { get; set; }
    }
}
