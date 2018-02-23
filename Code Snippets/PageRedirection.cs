// SubmitRedirectionWorkflow : WorkflowType

            Id = new Guid("F6ABE235-8E73-4C36-A4AD-DF7CFD39AFE8");

            Name = "Submit Page Redirection";
            Description = "Choose an umbraco page to go to on submit based on form choice.";
            Icon = "icon-directions-alt";

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            // Access the form via the event args and update the target page or content.
            if (string.IsNullOrWhiteSpace(Redirection))
                return WorkflowExecutionStatus.NotConfigured;

            var form = e.Form;

            var redirectionSettings = FieldRedirection.FromSettings(Redirection);
            if (redirectionSettings != null)
            {
                // Change the form's GoToPageOnSubmit parameter depending on the model.
                // Keep checking the validity as we go down the line.

                var sourceField = form.AllFields.FirstOrDefault(f => f.Id == redirectionSettings.Source);
                if (sourceField != null)
                {
                    object sourceValue = null;
                    if (record.RecordFields.ContainsKey(sourceField.Id))
                    {
                        sourceValue = record.RecordFields[sourceField.Id].Values.FirstOrDefault();
                    }

                    if (sourceValue != null)
                    {
                        var target = redirectionSettings.Targets.FirstOrDefault(t => t.Result == sourceValue.ToString());
                        if (target != null)
                        {
                            var umbraco = new UmbracoHelper(UmbracoContext.Current);
                            var node = umbraco.TypedContent(target.Node);

                            if (node != null && node.Id > -1)
                            {
                                // NOTE: This will actually update the form workflow definition loaded into memory (but not persisted to the filesystem)!
                                form.GoToPageOnSubmit = node.Id;

                            }
                        }
                    }
                }
            }
            return WorkflowExecutionStatus.Completed;
        }

        public override List<Exception> ValidateSettings()
        {
            var exceptions = new List<Exception>();
            if (string.IsNullOrWhiteSpace(Redirection))
            {
                exceptions.Add(new Exception("'Redirection' is not configured"));
            }
            return exceptions;
        }


        [Setting("Redirection", 
            description = "Specify the control to watch and the nodes to redirect to based on the control values", 
            view = "~/App_Plugins/UmbracoFormExtensions/SettingTypes/conditionalredirect.html")]
        public string Redirection
        {
            get; set;
        }
