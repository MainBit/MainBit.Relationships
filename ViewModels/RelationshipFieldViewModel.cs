using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainBit.Relationships.Fields;
using Orchard.ContentManagement;

namespace MainBit.Relationships.ViewModels
{
    public class RelationshipFieldViewModel
    {
        public ICollection<ContentItem> ContentItems { get; set; }
        public string SelectedIds { get; set; }
        public RelationshipField Field { get; set; }
    }
}