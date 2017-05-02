using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Attributes;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Providers.WorkflowTypes;

namespace BUF.Code.Forms.Workflows
{
    public class ConditionalSendEmail : SendEmail
    {
        public ConditionalSendEmail()
        {
            Name = "Conditional Send Email";
            Id = new Guid("8C499E9C-7112-41F5-8F72-A997149FDB54");
            Description = "Send the result of the form to an email address conditionally based on form input";
        }

        [Setting("Filter Fields", description = "Specify fields and their expected values to filter this email on.  If any of the filters aren't matched the email will not be sent.", view = "~/App_Plugins/UmbracoFormExtensions/SettingTypes/fieldfilter.html")]
        public string FilterFields
        {
            get; set;
        }

        [Setting("Negate", description = "Invert the Filter", view = "checkbox")]
        public string Negate
        {
            get; set;
        }

        [Setting("CcList", description = "Specify email addresses to be cc'd separated by a comma", view = "TextField")]
        public string CcList { get; set; }

        public override List<Exception> ValidateSettings()
        {
            var exceptions = new List<Exception>(base.ValidateSettings());
            if (string.IsNullOrWhiteSpace(this.FilterFields))
            {
                exceptions.Add(new Exception("'Filter Fields' have not been specified"));
            }
            return exceptions;
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            var filters = GetFilters();
            var values = new Dictionary<string, string>();
            bool allowEmail = true;
            foreach (var filter in filters)
            {
                var rf = record.RecordFields[new Guid(filter.Field)];
                allowEmail &= (rf.ValuesAsString(true) == filter.Value);
                if (!allowEmail) break;
            }

            if (Negate == true.ToString())
                allowEmail = !allowEmail;

            if (allowEmail)
            {
                return base.Execute(record, e);
            }
            return WorkflowExecutionStatus.Cancelled;
        }

        private List<FieldFilter> GetFilters()
        {
            List<FieldFilter> mappings = new List<FieldFilter>();
            if (!string.IsNullOrEmpty(this.FilterFields))
            {
                mappings = JsonConvert.DeserializeObject<IEnumerable<FieldFilter>>(this.FilterFields).ToList<FieldFilter>();
            }

            return mappings;
        }


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
}