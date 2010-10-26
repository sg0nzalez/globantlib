﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class Content
    {
        [DataMember]
        public int ID { get; set; }
        
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public string Publisher { get; set; }

        [DataMember]
        public DateTime Released { get; set; }

        [DataMember]
        public int Pages { get; set; }

        [DataMember]
        public String Thumbnail { get; set; }

        [DataMember]
        public string hasDigital;

        [DataMember]
        public string hasPhysical;

        [DataMember]
        public List<DigitalContent> Digitals { get; set; }

        [DataMember]
        public List<PhysicalContent> Physicals { get; set; }
    }
}
