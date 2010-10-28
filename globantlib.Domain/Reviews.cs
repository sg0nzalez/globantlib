using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    //[DataContract(Namespace = "")]
    [CollectionDataContract(Name="Reviews", Namespace="")]
    public class Reviews : List<Review>, IResponse
    {
        public Reviews()
            : base()
        { }

        public Reviews(IEnumerable<Review> collection)
            : base(collection)
        { }
    }
}
