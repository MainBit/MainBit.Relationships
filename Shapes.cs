using MainBit.Relationships.Services;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Relationships
{
    public class Shapes : IShapeTableProvider
    {
        private readonly Work<IRelationshipGroupService> _relationshipGroupService;
        public Shapes(Work<IRelationshipGroupService> relationshipGroupService)
        {
            _relationshipGroupService = relationshipGroupService;
		}
 
		public void Discover(ShapeTableBuilder builder) {
            builder.Describe("RelationshipGroupPicker").OnDisplaying(context =>
            {
                context.Shape.RelationshipGroups = _relationshipGroupService.Value.Get();
			});
		}
    }
}