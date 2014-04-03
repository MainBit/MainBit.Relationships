using System;
using System.Collections.Generic;
using System.Linq;
using MainBit.Relationships.Fields;
using MainBit.Relationships.Settings;
using MainBit.Relationships.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.Utility.Extensions;
using MainBit.Relationships.Services;

namespace MainBit.Relationships.Drivers
{
    public class RelationshipFieldDriver : ContentFieldDriver<RelationshipField>
    {
        private readonly IRelationshipsService _relationshipsService;

        // EditorTemplates/Fields/Relationship.cshtml
        //private const string TemplateName = "Fields/Relationship";

        public RelationshipFieldDriver(IRelationshipsService relationshipsService)
        {
            _relationshipsService = relationshipsService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        private static string GetPrefix(RelationshipField field, ContentPart part)
        {
            return part.PartDefinition.Name + "." + field.Name;
        }

        private static string GetDifferentiator(RelationshipField field, ContentPart part)
        {
            return field.Name;
        }

        protected override DriverResult Display(ContentPart part, RelationshipField field, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Fields_Relationship", GetDifferentiator(field, part), () => shapeHelper.Fields_Relationship()),
                ContentShape("Fields_Relationship_SummaryAdmin", GetDifferentiator(field, part), () => shapeHelper.Fields_Relationship_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(ContentPart part, RelationshipField field, dynamic shapeHelper)
        {
            var settings = field.PartFieldDefinition.Settings.GetModel<RelationshipFieldSettings>();
            var model = new RelationshipFieldViewModel
            {
                Field = field,
                ContentItems = _relationshipsService.Get(part.ContentItem.Id, settings.RelationshipGroupRecord_Id).ToList(),
            };
            model.SelectedIds = string.Concat(",", model.ContentItems.Select(r => r.Id));

            return ContentShape("Fields_Relationship_Edit", GetDifferentiator(field, part),
                () => shapeHelper.EditorTemplate(TemplateName: "Fields/Relationship", Model: model, Prefix: GetPrefix(field, part)));
                    
        }

        protected override DriverResult Editor(ContentPart part, RelationshipField field, IUpdateModel updater, dynamic shapeHelper)
        {
            var model = new RelationshipFieldViewModel();

            updater.TryUpdateModel(model, GetPrefix(field, part), null, null);

            var settings = field.PartFieldDefinition.Settings.GetModel<RelationshipFieldSettings>();

            int[] ids;
            if (String.IsNullOrEmpty(model.SelectedIds))
            {
                ids = new int[0];
            }
            else
            {
                ids = model.SelectedIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }


            if (settings.Required && ids.Length == 0)
            {
                updater.AddModelError("Id", T("The field {0} is mandatory", field.Name.CamelFriendly()));
            }
            else {
                _relationshipsService.Update(part.ContentItem.Id, ids, settings.RelationshipGroupRecord_Id);
            }

            return Editor(part, field, shapeHelper);
        }
    }
}