using System;

namespace Molibar.WebTracking.Presentation.Models
{
    public class PageEventPostModel
    {
        public Guid VisitGuid { get; set; }
        public string Url { get; set; }
        public string PageId { get; set; }
        public string ElementId { get; set; }
        public string EventType { get; set; }
        public string ElementType { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public long MillisSince1970 { get; set; }
    }
}