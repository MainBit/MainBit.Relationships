using MainBit.Relationships.Models;
using Orchard;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Relationships.Services
{
    public interface IRelationshipGroupService : IDependency
    {
        List<RelationshipGroupRecord> Get();
        RelationshipGroupRecord Get(int id);
        void Create(RelationshipGroupRecord model);
        void Update(RelationshipGroupRecord model);
        void Delete(int id);
    }

    public class RelationshipGroupService : IRelationshipGroupService
    {
        private readonly IRepository<RelationshipGroupRecord> _relationshipGroupRepository;

        public RelationshipGroupService(
            IRepository<RelationshipGroupRecord> relationshipGroupRepository)
        {
            _relationshipGroupRepository = relationshipGroupRepository;
        }

        public List<RelationshipGroupRecord> Get()
        {
            return _relationshipGroupRepository.Table.ToList();
        }

        public RelationshipGroupRecord Get(int id)
        {
            return _relationshipGroupRepository.Get(id);
        }
            
        public void Create(RelationshipGroupRecord model) {
            _relationshipGroupRepository.Create(model);
        }

        public void Update(RelationshipGroupRecord model)
        {
            _relationshipGroupRepository.Update(model);
        }

        public void Delete(int id)
        {
            _relationshipGroupRepository.Delete(Get(id));
        }
    }
}