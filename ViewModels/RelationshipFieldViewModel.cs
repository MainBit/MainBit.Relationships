using System.Collections.Generic;
using Orchard.ContentManagement;
using MainBit.Relationships.Fields;

namespace MainBit.Relationships.ViewModels
{
    public class RelationshipFieldViewModel
    {
        public ICollection<ContentItem> ContentItems { get; set; }
        public string SelectedIds { get; set; }
        public RelationshipField Field { get; set; }
        public ContentPart Part { get; set; }
    }
}