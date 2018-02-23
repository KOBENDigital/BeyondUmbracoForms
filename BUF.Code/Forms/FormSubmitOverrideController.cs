using BUF.Code.Forms.FieldTypes;
using BUF.Code.Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Forms.Core;
using Umbraco.Forms.Mvc.Models;
using Umbraco.Forms.Web.Controllers;

namespace BUF.Code.Forms
{
    public class FormSubmitOverrideController : UmbracoFormsController
    {
        protected override void OnFormHandled(Form form, FormViewModel model)
        {
            // find the field in the AllFields collection and then perform some action
            var redirectField = form.AllFields.FirstOrDefault(f => f.FieldType.Name == "Conditional Redirect");

            //var redirectField = form.AllFields.FirstOrDefault(f => f.FieldTypeId == ConditionalRedirect.ConditionalRedirectId);

            if (redirectField != null)
            {
                var redirectionSettings = FieldRedirection.FromSettings(redirectField.Settings);
                if (redirectionSettings != null)
                {
                    // Change the form's GoToPageOnSubmit parameter depending on the model.
                    // Keep checking the validity as we go down the line.

                    var sourceField = form.AllFields.FirstOrDefault(f => f.Id == redirectionSettings.Source);
                    if (sourceField != null)
                    {
                        string sourceId = sourceField.Id.ToString();
                        object sourceValue = null;
                        if (model.FormState != null && model.FormState.ContainsKey(sourceId))
                        {
                            sourceValue = model.FormState[sourceId].FirstOrDefault();
                        }

                        if (sourceValue != null)
                        {
                            var target = redirectionSettings.Targets.FirstOrDefault(t => t.Result == sourceValue.ToString());
                            if (target != null)
                            {
                                var node = Umbraco.TypedContent(target.Node);

                                if (node != null && node.Id > -1)
                                {
                                    form.GoToPageOnSubmit = node.Id;
                                }
                            }
                        }

                    }
                }
            }

            base.OnFormHandled(form, model);
        }
    }
}