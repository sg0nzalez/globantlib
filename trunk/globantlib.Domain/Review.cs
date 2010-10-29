using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class Review
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Rate { get; set; }
        
        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String Submitted { get; set; }

        [DataMember]
        public String Comments { get; set; }

        [DataMember]
        public User User { get; set; }
    }
}
