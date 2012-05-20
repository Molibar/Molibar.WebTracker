namespace Molibar.WebTracking.Domain.Model
{
    public class FormEvent : Event
    {
        public string ElementValue { get; set; }
        private bool _valueValid = true;
        public bool ValueValid { get { return _valueValid; } set { _valueValid = value; } }
    }
}