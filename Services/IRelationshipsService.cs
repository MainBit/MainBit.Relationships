using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.ContentManagement;

namespace MainBit.Relationships.Services
{
    public interface IRelationshipsService : IDependency
    {
        IEnumerable<ContentItem> Get(int contentItemId, int relationshipGroupRecordId);
        bool Update(int contentItemId, int[] relContentItemId, int relationshipGroupRecordId);
    }
}