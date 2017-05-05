using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BUF.Code.Forms.Models
{

    [DataContract(Name = "target")]
    public class RedirectionTarget
    {

        [DataMember(Name = "result")]
        public string Result
        {
            get;
            set;
        }

        [DataMember(Name = "node")]
        public int Node
        {
            get;
            set;
        }

    }
}