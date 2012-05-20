using System;

namespace Molibar.WebTracking.Domain.Model
{
    public class Event
    {
        public string Id { get; set; }
        public Guid VisitGuid { get; set; }
        public string Url { get; set; }

        public string PageId { get; set; }
        public string ElementId { get; set; }
        public string EventType { get; set; }
        public string ElementType { get; set; }

        public DateTime ClientDateTime { get; set; }

        private DateTime _dateTime = DateTime.UtcNow;
        public DateTime DateTime { get { return _dateTime; } set { _dateTime = value; } }
    }
}