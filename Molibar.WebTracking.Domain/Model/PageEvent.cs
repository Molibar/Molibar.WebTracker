using System;

namespace Molibar.WebTracking.Domain.Model
{
    public class PageEvent
    {
        public string Id { get; set; }
        public Guid VisitGuid { get; set; }
        public string Url { get; set; }

        public string PageId { get; set; }
        public string ElementId { get; set; }
        public string EventType { get; set; }
        public string ElementType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public DateTime ClientDateTime { get; set; }
        public DateTime DateTime { get; set; }
    }
}