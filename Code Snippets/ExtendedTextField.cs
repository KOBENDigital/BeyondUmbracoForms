// ExtendedTextField : Textfield

// Essentials:
Id = new Guid("34E3D1D2-B6EC-4ACE-AD59-50E8EBAF8D7D");

Name = "Extended Short Answer";
FieldTypeViewName = "FieldType.ExtendedTextField.cshtml";
Description = "Renders a more configurable Short Answer";

// Optional:
Category = "My Extensions";
SortOrder = 1;

// Attempts to render ~/App_Plugins/UmbracoForms/Backoffice/Common/FieldTypes/ExtendedTextField.html 
// when editing the form.
//RenderView = "ExtendedTextField";

        [Setting("Field Type", view = "dropdownlist", prevalues = "currency,email,numeric,tel,text,url")]
        public bool FieldType { get; set; }

        [Setting("Maximum Length")]
        public string MaxLength { get; set; }
        
        [Setting("CssClass", description = "Add a custom css class")]
        public string CssClass { get; set; }
