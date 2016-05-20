using Orchard.ContentManagement.MetaData.Models;
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
        public string DisplayedContentTypes { get; set; }
    }

    public static class RelationshipFieldSettingsExtensions
    {
        public static RelationshipFieldSettings GetRelationshipFieldSettings(this ContentPartFieldDefinition definition)
        {
            var settings = definition.Settings.GetModel<RelationshipFieldSettings>();
            string displayedContentTypes;
            if (definition.Settings.TryGetValue("RelationshipFieldSettings.DisplayedContentTypes", out displayedContentTypes))
            {
                settings.DisplayedContentTypes = displayedContentTypes;
            }
            return settings;
        }
    }
}