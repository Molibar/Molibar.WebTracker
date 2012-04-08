using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Molibar.WebTracking.Web.Models.Demo
{
    public class DemoViewModel
    {
        public Guid VisitGuid { get; set; }
        public string FormName { get; set; }

        [Display(Name = "String Value")]
        [Required(ErrorMessage = "Please enter something")]
        public string StringValue { get; set; }
        [Display(Name = "Check Box")]
        [Required(ErrorMessage = "Please tick")]
        public bool CheckBox { get; set; }
        public SelectList SelectList { get; set; }
        [Display(Name = "Selection")]
        [RegularExpression("^(?!Undefined).*$", ErrorMessage = "Please select something")]
        public string Selected { get; set; }
    }
}