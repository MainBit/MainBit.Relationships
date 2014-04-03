using System.Collections.Generic;
using System.Globalization;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;

namespace MainBit.Relationships.Settings
{
    public class RelationshipFieldEditorEvents : ContentDefinitionEditorEventsBase
    {

        public override IEnumerable<TemplateViewModel> PartFieldEditor(ContentPartFieldDefinition definition) {
            if (definition.FieldDefinition.Name == "RelationshipField")
            {
                var model = definition.Settings.GetModel<RelationshipFieldSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> PartFieldEditorUpdate(ContentPartFieldDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.FieldType != "RelationshipField")
            {
                yield break;
            }

            var model = new RelationshipFieldSettings();
            if (updateModel.TryUpdateModel(model, "RelationshipFieldSettings", null, null))
            {
                builder.WithSetting("RelationshipFieldSettings.Hint", model.Hint);
                builder.WithSetting("RelationshipFieldSettings.Required", model.Required.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("RelationshipFieldSettings.Multiple", model.Multiple.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("RelationshipFieldSettings.RelationshipGroupRecord_Id", model.RelationshipGroupRecord_Id.ToString(CultureInfo.InvariantCulture));
            }

            yield return DefinitionTemplate(model);
        }
    }
}