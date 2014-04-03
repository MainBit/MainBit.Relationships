using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainBit.Relationships.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;

namespace MainBit.Relationships.Services
{
    public class RelationshipsService : IRelationshipsService
    {
        private readonly IRepository<RelationshipRecord> _relationshipRepository;
        private readonly IContentManager _contentManager;

        public RelationshipsService(
            IRepository<RelationshipRecord> relationshipRepository,
            IContentManager contentManager)
        {
            _relationshipRepository = relationshipRepository;
            _contentManager = contentManager;
        }

        public IEnumerable<ContentItem> Get(int contentItemId, int relationshipGroupRecordId) {
            var contentItemIds = _relationshipRepository
                .Fetch(r => r.RelationshipGroupRecord_Id == relationshipGroupRecordId 
                    && (r.ContentItemRecord1_Id == contentItemId || r.ContentItemRecord2_Id == contentItemId))
                .Select(p => p.ContentItemRecord1_Id == contentItemId ? p.ContentItemRecord2_Id : p.ContentItemRecord1_Id);
            return _contentManager.GetMany<ContentItem>(contentItemIds, VersionOptions.Published, QueryHints.Empty);
        }


        public bool Update(int contentItemId, int[] relContentItemIds, int relationshipGroupRecordId) {
            var relationships = _relationshipRepository
                .Fetch(r => r.RelationshipGroupRecord_Id == relationshipGroupRecordId
                            && (r.ContentItemRecord1_Id == contentItemId || r.ContentItemRecord2_Id == contentItemId)).ToList();

            for (var i = 0; i < relationships.Count(); i++ ) {
                var relationshipRecord = relationships[i];
                var relContentItemId = relationshipRecord.ContentItemRecord1_Id == contentItemId ?
                    relationshipRecord.ContentItemRecord2_Id : relationshipRecord.ContentItemRecord1_Id;

                if (!relContentItemIds.Any(r => r == relContentItemId)) {
                    _relationshipRepository.Delete(relationshipRecord);
                    relationships.Remove(relationshipRecord);
                    i--;
                }
            }

            for(var i = 0; i < relContentItemIds.Count(); i++) {
                var relContentItemId = relContentItemIds[i];
                var relationshipRecord = relationships.FirstOrDefault(r =>
                    r.ContentItemRecord1_Id == relContentItemId || r.ContentItemRecord2_Id == relContentItemId);
                if(relationshipRecord == null) {
                    relationshipRecord = new RelationshipRecord() {
                        ContentItemRecord1_Id = contentItemId,
                        ContentItemRecord2_Id = relContentItemId,
                        RelationshipGroupRecord_Id = relationshipGroupRecordId
                    };
                    _relationshipRepository.Create(relationshipRecord);
                }
            }

            return true;
        }
    }
}