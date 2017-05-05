using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BUF.Code.Forms.Models
{

    [DataContract(Name = "fieldFilter")]
    public class FieldFilter
    {

        [DataMember(Name = "field")]
        public string Field
        {
            get;
            set;
        }

        [DataMember(Name = "value")]
        public string Value
        {
            get;
            set;
        }
    }
}