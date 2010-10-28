using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace="")]
    public class Response : IResponse
    {
        [DataMember(Order = 0)]
        public List<Page> Pages { get; set; }

        [DataMember(Order = 1)]
        public List<Content> ArrayOfContents { get; set; }        
    }
}
