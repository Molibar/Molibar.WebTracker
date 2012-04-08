using System;

namespace Molibar.WebTracking.Web.Models.Demo
{
    public class DemoPostModel
    {
        public string StringValue { get; set; }
        public bool Checkbox { get; set; }
        public string Selected { get; set; }
        public Guid VisitGuid { get; set; }
    }
}