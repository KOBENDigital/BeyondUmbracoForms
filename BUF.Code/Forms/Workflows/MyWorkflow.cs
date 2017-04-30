using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;

namespace BUF.Code.Forms.Workflows
{
    public class MyWorkflow : WorkflowType
    {
        public MyWorkflow()
        {
            // Essentials:
            Id = new Guid("CD26D369-DBE7-4F08-963F-643988E39916");
            Name = "My Awesome Workflow";
            Description = "Does something with the Form submission";

            // Tip: To use the built-in Umbraco icons, use the Document Type Editor to 
            // inspect the available list.
            Icon = "icon-universal";
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override List<Exception> ValidateSettings()
        {
            throw new NotImplementedException();
        }
    }
}