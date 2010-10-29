using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    [CollectionDataContract(Name = "BookRequests",ItemName = "BookRequest" , Namespace = "")]
    public class BookRequestCollection : List<BookRequest>, IResponse
    {
        public BookRequestCollection()
            : base()
        { }

        public BookRequestCollection(IEnumerable<BookRequest> collection)
            : base(collection)
        { }
    }
}
