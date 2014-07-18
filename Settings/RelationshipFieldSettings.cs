using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Relationships.Settings
{
    public class RelationshipFieldSettings
    {
        public string Hint { get; set; }
        public bool Required { get; set; }
        public bool Multiple { get; set; }
        public int RelationshipGroupRecord_Id { get; set; }
        public bool HideEditor { get; set; }
    }
}