using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;

namespace BUF.Code.Forms.Workflows
{
    public class RecordSessionWorkflow : WorkflowType
    {
        public RecordSessionWorkflow()
        {
            Id = new Guid("01B44E1B-149C-4090-B9F2-2790364C9C88");
            Name = "Save Record To Session";

        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            HttpContext.Current.Session["uRecord"] = record;

            return WorkflowExecutionStatus.Completed;
        }

        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }
    }
}