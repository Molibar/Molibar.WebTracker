using System;

namespace Molibar.WebTracking.Presentation.Models
{
    public class FormEventPostModel
    {
        public Guid VisitGuid { get; set; }
        public string Url { get; set; }
        public string PageId { get; set; }
        public string ElementId { get; set; }
        public string EventType { get; set; }
        public string ElementType { get; set; }
        public string ElementValue { get; set; }
        public bool ValueValid { get; set; }
        public long MillisSince1970 { get; set; }
    }
}
