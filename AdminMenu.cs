﻿using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace MainBit.Relationships
{
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Settings"),
                menu => menu.Add(T("Relationships"), "1.1", item => item.Action("Index", "RelationshipGroupAdmin", new { area = "MainBit.Relationships" })
                    .Permission(StandardPermissions.SiteOwner))
            );
        }
    }
}
