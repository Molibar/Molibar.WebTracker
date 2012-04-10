using System.Web.Mvc;
using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Tracking;
using Molibar.WebTracking.Presentation.Models;

namespace Molibar.WebTracking.Web.Areas.Api.Controllers
{
    public class TrackingController : Controller
    {
        private readonly IFormEventTrackingService _formEventTrackingService;
        private readonly IPageEventTrackingService _pageEventTrackingService;
        private readonly IEntityMapper _entityMapper;

        public TrackingController(IFormEventTrackingService formEventTrackingService,
            IPageEventTrackingService pageEventTrackingService,
            IEntityMapper entityMapper)
        {
            _formEventTrackingService = formEventTrackingService;
            _pageEventTrackingService = pageEventTrackingService;
            _entityMapper = entityMapper;
        }

        [HttpPost]
        public JsonResult FormEvent(FormEventPostModel formEventPostModel)
        {
            var formEvent = _entityMapper.Map<FormEvent>(formEventPostModel);
            var model = _formEventTrackingService.Add(formEvent);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PageEvent(PageEventPostModel pageEventPostModel)
        {
            var pageEvent = _entityMapper.Map<PageEvent>(pageEventPostModel);
            var model = _pageEventTrackingService.Add(pageEvent);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
