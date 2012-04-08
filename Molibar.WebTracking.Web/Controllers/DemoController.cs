using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Molibar.WebTracking.Web.Models.Demo;

namespace Molibar.WebTracking.Web.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/

        public ActionResult Index()
        {
            return View(new DemoViewModel
            {
                FormName = "DemoForm",
                VisitGuid = Guid.NewGuid(),
                SelectList = GetSelectList()
            });
        }

        [HttpPost]
        public ActionResult Submit(DemoPostModel demoPostModel)
        {
            return View("Index", new DemoViewModel
            {
                FormName = "DemoForm",
                StringValue = demoPostModel.StringValue,
                CheckBox = demoPostModel.Checkbox,
                SelectList = GetSelectList(demoPostModel.Selected),
                Selected = demoPostModel.Selected,
                VisitGuid = demoPostModel.VisitGuid
            });
        }

        public SelectList GetSelectList(string selectedValue = "Undefined")
        {
            var selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem { Text = "-----", Value = "Undefined", Selected = true });
            selectListItems.Add(new SelectListItem { Text = "Yes", Value = "1" });
            selectListItems.Add(new SelectListItem { Text = "No", Value = "2" });
            selectListItems.Add(new SelectListItem { Text = "Maybe", Value = "3" });

            return new SelectList(selectListItems, "Value", "Text", selectedValue);
        }
    }
}
