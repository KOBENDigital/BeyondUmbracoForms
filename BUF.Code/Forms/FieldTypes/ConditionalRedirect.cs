using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Umbraco.Forms.Core.Attributes;
using Umbraco.Forms.Core.Providers.FieldTypes;

namespace BUF.Code.Forms.FieldTypes
{
    public class ConditionalRedirect : HiddenField
    {
        public static readonly Guid ConditionalRedirectId = new Guid("98FB4A74-3CD5-4F44-A1F9-594885521171");

        public ConditionalRedirect()
        {
            Id = ConditionalRedirectId;
            Name = "Conditional Redirect";
            Description = "Manages the target page based on a linked field on the form";
            Icon = "icon-directions-alt";
            SortOrder = 10;
        }

        public new string DefaultValue { get; }

        public override string GetDesignView()
        {
            return "~/App_Plugins/UmbracoForms/Backoffice/Common/FieldTypes/HiddenField.html";
        }

        [Setting("Redirection", 
            description = "Specify the control to watch and the nodes to redirect to based on the control values", 
            view = "~/App_Plugins/UmbracoFormExtensions/SettingTypes/conditionalredirect.html")]
        public string Redirection
        {
            get; set;
        }


        public override bool StoresData
        {
            get
            {
                return false;
            }
        }
    }
}