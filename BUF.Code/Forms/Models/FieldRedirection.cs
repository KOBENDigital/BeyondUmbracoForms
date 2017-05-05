using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BUF.Code.Forms.Models
{

    [DataContract(Name = "fieldRedirection")]
    public class FieldRedirection
    {

        [DataMember(Name = "source")]
        public Guid Source
        {
            get;
            set;
        }

        [DataMember(Name = "targets")]
        public IEnumerable<RedirectionTarget> Targets
        {
            get;
            set;
        }

        public static FieldRedirection FromSettings(string redirects)
        {
            if (!string.IsNullOrEmpty(redirects))
            {
                return JsonConvert.DeserializeObject<FieldRedirection>(redirects);
            }

            return null;
        }

        public static FieldRedirection FromSettings(IDictionary<string, string> settings)
        {
            if (settings != null && settings.ContainsKey("Redirection"))
            {
                var settingValue = settings["Redirection"];
                if (!string.IsNullOrEmpty(settingValue))
                {
                    return JsonConvert.DeserializeObject<FieldRedirection>(settingValue);
                }

            }

            return null;
        }

    }
}