using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;

namespace BUF.Code.Forms.Workflows
{
    public class NotifySubmitterWorkflow : WorkflowType
    {
        public NotifySubmitterWorkflow() {
            Icon = "icon-notify";
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