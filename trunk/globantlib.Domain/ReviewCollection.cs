using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    [CollectionDataContract(Name="Reviews", Namespace="")]
    public class ReviewCollection : List<Review>, IResponse
    {
        public ReviewCollection()
            : base()
        { }

        public ReviewCollection(IEnumerable<Review> collection)
            : base(collection)
        { }
    }
}
