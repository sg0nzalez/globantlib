using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace globantlib.Domain
{
    [ServiceKnownType("GetKnownTypes", typeof(Helper))]
    [ServiceContract]
    public interface IResponse
    {
        
    }

    // This class has the method named GetKnownTypes that returns a generic IEnumerable.
    static class Helper
    {
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            System.Collections.Generic.List<System.Type> knownTypes =
                new System.Collections.Generic.List<System.Type>();
            // Add any types to include here.
            knownTypes.Add(typeof(Response));
            knownTypes.Add(typeof(Error));
            return knownTypes;
        }
    }

}
