using System;

namespace Molibar.WebTracking.Domain.Model
{
    public class FormEvent
    {
        public string Id { get; set; }
        public Guid VisitGuid { get; set; }
        public string Url { get; set; }
        public string FormName { get; set; }
        public string EventName { get; set; }
        public string ElementType { get; set; }
        public string ElementId { get; set; }
        public string ElementValue { get; set; }

        private bool _valueValid = true;
        public bool ValueValid { get { return _valueValid; } set { _valueValid = value; } }

        public int? TimeInMillis { get; set; }
        public DateTime DateTime { get; set; }
    }
}