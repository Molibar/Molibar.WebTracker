using System.Web.Mvc;
using Molibar.Infrastructure.Mapper;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.Domain.Tracking;
using Molibar.WebTracking.Presentation.Models;

namespace Molibar.WebTracking.Web.Areas.Api.Controllers
{
    public class TrackingController : Controller
    {
        private readonly FormEventTracker _formEventTracker;
        private readonly IEntityMapper _entityMapper;

        public TrackingController(FormEventTracker formEventTracker,
            IEntityMapper entityMapper)
        {
            _formEventTracker = formEventTracker;
            _entityMapper = entityMapper;
        }

        [HttpGet]
        public JsonResult FormEvent()
        {
            return Json(new FormEventPostModel
                            {
                                
                            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FormEvent(FormEventPostModel formEventPostModel)
        {
            var formEvent = _entityMapper.Map<FormEvent>(formEventPostModel);
            var model = _formEventTracker.Add(formEvent);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
