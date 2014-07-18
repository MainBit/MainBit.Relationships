using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace MainBit.Relationships
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Route = new Route(
                        "Admin/Relationships/Groups",
                        new RouteValueDictionary {
                                                    {"area", "MainBit.Relationships"},
                                                    {"controller", "RelationshipGroupAdmin"},
                                                    {"action", "Index"}
                                                },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                                                    {"area", "MainBit.Relationships"}
                        },
                        new MvcRouteHandler())
                },

                new RouteDescriptor {
                    Route = new Route(
                        "Admin/Relationships/Groups/Create",
                        new RouteValueDictionary {
                                                    {"area", "MainBit.Relationships"},
                                                    {"controller", "RelationshipGroupAdmin"},
                                                    {"action", "Create"}
                                                },
                        new RouteValueDictionary (),
                        new RouteValueDictionary {
                                                    {"area", "MainBit.Relationships"}
                                                },
                        new MvcRouteHandler())
                },

                new RouteDescriptor {
                    Route = new Route(
                        "Admin/Relationships/Groups/{relationshipGroupId}/Edit",
                        new RouteValueDictionary {
                                                    {"area", "MainBit.Relationships"},
                                                    {"controller", "RelationshipGroupAdmin"},
                                                    {"action", "Edit"}
                                                },
                        new RouteValueDictionary (),
                        new RouteValueDictionary {
                                                    {"area", "MainBit.Relationships"}
                                                },
                        new MvcRouteHandler())
                },

                new RouteDescriptor {
                    Route = new Route(
                        "Admin/Relationships/Groups/{relationshipGroupId}/Delete",
                        new RouteValueDictionary {
                                                    {"area", "MainBit.Relationships"},
                                                    {"controller", "RelationshipGroupAdmin"},
                                                    {"action", "Delete"}
                                                },
                        new RouteValueDictionary (),
                        new RouteValueDictionary {
                                                    {"area", "MainBit.Relationships"}
                                                },
                        new MvcRouteHandler())
                },

            };
        }
    }
}