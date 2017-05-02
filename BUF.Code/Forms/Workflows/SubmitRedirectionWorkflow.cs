using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;

namespace BUF.Code.Forms.Workflows
{
    public class SubmitRedirectionWorkflow : WorkflowType
    {
        public SubmitRedirectionWorkflow()
        {

        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            // Access the form via the event args and update the target page or content.

            throw new NotImplementedException();
        }

        public override List<Exception> ValidateSettings()
        {
            throw new NotImplementedException();
        }
    }
}