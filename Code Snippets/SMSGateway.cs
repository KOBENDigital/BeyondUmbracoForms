// Account details: 
// Sign up for a 15-day, 25 SMS trial at https://redoxygen.com/create-trial-account/
// You will need the details from the trial email for the pre-values of this workflow.

// SMSNotifyWorkflow : WorkflowType

            Id = new Guid("72A9FA8D-EA76-428E-8003-D431FCDA92D8");

            Name = "SMS Notification";
            Description = "Notify the form submitter or a specific phone number via SMS on submit";
            Icon = "icon-notify";

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {

            if (SendSMS(e.Form, record) == -1) {
                return WorkflowExecutionStatus.Failed;
            }

            return WorkflowExecutionStatus.Completed;
        }

        public int SendSMS(Form form, Record record)
        {
            WebClient client = new WebClient();

            string requestURL = "https://redoxygen.net/sms.dll?Action=SendSMS";

            // Replace Magic Strings in the NotificationMessage.
            // Can't use the string.ParsePlaceHolders() extension method, because it doesn't pass the record data through properly. 
            // Use the Umbraco.Forms.Data.StringHelper methods instead.
            var message = StringHelper.ParsePlaceHolders(record, NotificationMessage);

            // Get the Phone number from the form if it's not configured in the workflow.
            var recipient = SMSRecipient;
            if (string.IsNullOrWhiteSpace(recipient))
            {
                var phoneField = form.AllFields.FirstOrDefault(f => f.Alias == PhoneNumberField);
                if (phoneField != null)
                {
                    recipient = (string)record.RecordFields[phoneField.Id].Values.FirstOrDefault();
                }
            }

            if (string.IsNullOrWhiteSpace(recipient))
            {
                return -1;
            }

            var requestData = "AccountId=" + GatewayAccountId
                + "&Email=" + System.Web.HttpUtility.UrlEncode(GatewayEmail)
                + "&Password=" + System.Web.HttpUtility.UrlEncode(GatewayPassword)
                + "&Recipient=" + System.Web.HttpUtility.UrlEncode(recipient)
                + "&Message=" + System.Web.HttpUtility.UrlEncode(message);

            byte[] postData = Encoding.ASCII.GetBytes(requestData);
            byte[] response = client.UploadData(requestURL, postData);

            String result = Encoding.ASCII.GetString(response);
            int resultCode = Convert.ToInt32(result.Substring(0, 4));

            return resultCode;
        }

        public override List<Exception> ValidateSettings()
        {
            var exceptions = new List<Exception>();
            if (string.IsNullOrWhiteSpace(GatewayAccountId))
            {
                exceptions.Add(new Exception("'GatewayAccountId' is not configured"));
            }
            if (string.IsNullOrWhiteSpace(GatewayEmail))
            {
                exceptions.Add(new Exception("'GatewayEmail' is not configured"));
            }
            if (string.IsNullOrWhiteSpace(NotificationMessage))
            {
                exceptions.Add(new Exception("'NotificationMessage' is not configured"));
            }
            if (string.IsNullOrWhiteSpace(PhoneNumberField) || string.IsNullOrWhiteSpace(SMSRecipient))
            {
                exceptions.Add(new Exception("Please specify a Phone Number or the form field containing the recipient's phone number."));
            }
            return exceptions;
        }

        [Setting("SMS Gateway Account Id",
            description = "Specify the redOxygen Account Id")]
        public string GatewayAccountId
        {
            get; set;
        }

        [Setting("SMS Gateway Email Address")]
        public string GatewayEmail
        {
            get; set;
        }

        [Setting("SMS Gateway Password")]
        public string GatewayPassword
        {
            get; set;
        }

        [Setting("SMS Notification Message")]
        public string NotificationMessage
        {
            get;set;
        }

        [Setting("SMS Phone Number Field Name")]
        public string PhoneNumberField { get; set; }

        [Setting("SMS Phone Number")]
        public string SMSRecipient { get; set; }
