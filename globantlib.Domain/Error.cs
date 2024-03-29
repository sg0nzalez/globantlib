﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace globantlib.Domain
{
    [DataContract(Namespace = "")]
    public class Error : IResponse
    {
        public Error()
        {
            Message = "";
        }

        [DataMember(Order = 2)]
        public String Message { get; set; }
    }
}
