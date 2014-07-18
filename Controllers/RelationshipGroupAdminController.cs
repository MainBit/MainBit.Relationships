using System.Web.Mvc;
using Orchard;
using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Admin;
using Orchard.UI.Notify;
using MainBit.Relationships.Services;
using MainBit.Relationships.Models;

namespace MainBit.Relationships.Controllers
{
    [ValidateInput(false), Admin]
    public class RelationshipGroupAdminController : Controller 
    {
        private readonly IRelationshipGroupService _relationshipGroupService;

        public RelationshipGroupAdminController(IRelationshipGroupService relationshipGroupService, IOrchardServices orchardServices)
        {
            _relationshipGroupService = relationshipGroupService;
            Services = orchardServices;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }

        [HttpGet]
        public ActionResult Index() {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit relationship groups")))
                return new HttpUnauthorizedResult();

            var listOfRecords = _relationshipGroupService.Get();
            if (listOfRecords == null || listOfRecords.Count == 0)
                ViewBag.EmptyMessage = T("No data");
            return View(listOfRecords);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit relationship groups")))
                return new HttpUnauthorizedResult();

            var model = new RelationshipGroupRecord();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RelationshipGroupRecord model)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit relationship groups")))
                return new HttpUnauthorizedResult();

            model.Id = 0;

            if (!ModelState.IsValid)
                return View();

            _relationshipGroupService.Create(model);
            Services.Notifier.Information(T("MainBit relationship group successfully added"));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int relationshipGroupId)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit relationship groups")))
                return new HttpUnauthorizedResult();

            return View(_relationshipGroupService.Get(relationshipGroupId));
        }

        [HttpPost]
        public ActionResult Edit(int relationshipGroupId, RelationshipGroupRecord model)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit relationship groups")))
                return new HttpUnauthorizedResult();

            model.Id = relationshipGroupId;

            if (!ModelState.IsValid)
                return View(model);

            _relationshipGroupService.Update(model);
            Services.Notifier.Information(T("MainBit relationship group successfully saved"));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int relationshipGroupId)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage MainBit relationship groups")))
                return new HttpUnauthorizedResult();

            _relationshipGroupService.Delete(relationshipGroupId);
            Services.Notifier.Information(T("MainBit relationship group successfully deleted"));

            return RedirectToAction("Index");
        }

    }
}