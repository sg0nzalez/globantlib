using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace globantlib.Domain
{
    [DataContract]
    public class Content
    {
        [DataMember]
        public int ID { get; set; }
        
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
