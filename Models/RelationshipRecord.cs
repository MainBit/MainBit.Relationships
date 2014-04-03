using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace MainBit.Relationships.Models
{
    public class RelationshipRecord
    {
        public virtual int Id { get; set; }
        public virtual int RelationshipGroupRecord_Id { get; set; }
        public virtual int ContentItemRecord1_Id { get; set; }
        public virtual int ContentItemRecord2_Id { get; set; }
    }
}