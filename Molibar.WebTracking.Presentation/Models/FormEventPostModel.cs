using System;

namespace Molibar.WebTracking.Presentation.Models
{
    public class FormEventPostModel
    {
        public Guid VisitGuid { get; set; }
        public string Url { get; set; }
        public string FormName { get; set; }
        public string EventName { get; set; }
        public string ElementType { get; set; }
        public string ElementId { get; set; }
        public string ElementValue { get; set; }
        public bool ValueValid { get; set; }
        public int? TimeInMillis { get; set; }
        private DateTime _dateTime = DateTime.UtcNow;
        public DateTime DateTime { get { return _dateTime; } set { _dateTime = value; } }
    }
}
