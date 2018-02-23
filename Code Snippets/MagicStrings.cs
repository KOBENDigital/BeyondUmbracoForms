// 1.  Custom Workflow to save Record to session for later use


// 2. Use Forms ParsePlaceHolders() extension method to parse the magic strings.

@inherits Umbraco.Web.Mvc.UmbracoTemplatePage
@using Umbraco.Forms.Core.Extensions
@{
    Layout = "Master.cshtml";
}
@{ 
    var content = Html.GetGridHtml(Model.Content, "content", "fanoe");

    var parsedContent = content.ToString().ParsePlaceHolders();
}

@Html.Raw(parsedContent);

// 3. Use Forms StringHelper methods to parse the magic strings after retrieving from session.
@inherits Umbraco.Web.Mvc.UmbracoTemplatePage
@using Umbraco.Forms.Data
@{
    Layout = "Master.cshtml";
}
@{ 
    var record = HttpContext.Current.Session["uRecord"] as global::Umbraco.Forms.Core.Record;

    var content = Html.GetGridHtml(Model.Content, "content", "fanoe");

    var parsedContent = global::Umbraco.Forms.Data.StringHelper.ParsePlaceHolders(record, content.ToString());
}
