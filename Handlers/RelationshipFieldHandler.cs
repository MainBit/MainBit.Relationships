using System.Linq;
using MainBit.Relationships.Services;
using MainBit.Relationships.Settings;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using MainBit.Relationships.Fields;

namespace MainBit.Relationships.Handlers
{
    public class RelationshipFieldHandler : ContentHandler {
        private readonly IRelationshipService _relationshipsService;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public RelationshipFieldHandler(
            IRelationshipService relationshipsService, 
            IContentDefinitionManager contentDefinitionManager) {

            _relationshipsService = relationshipsService;
            _contentDefinitionManager = contentDefinitionManager;
        }

        protected override void Loading(LoadContentContext context) {
            base.Loading(context);

            var fields = context.ContentItem.Parts.SelectMany(x => x.Fields.Where(f => f.FieldDefinition.Name == typeof(RelationshipField).Name)).Cast<RelationshipField>();
            
            // define lazy initializer for Relationship.ContentItems
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(context.ContentType);
            if (contentTypeDefinition == null) {
                return;
            }
            
            foreach (var field in fields) {
                var settings = field.PartFieldDefinition.Settings.GetModel<RelationshipFieldSettings>();
                field._contentItems.Loader(x => _relationshipsService.Get(context.ContentItemRecord.Id, settings.RelationshipGroupRecord_Id));
            }
        }
    }
}