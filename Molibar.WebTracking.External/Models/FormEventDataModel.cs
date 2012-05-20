using System;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.Models
{
    public class FormEventDataModel : Event
    {
        public string ElementValue { get; set; }
        private bool _valueValid = true;
        public bool ValueValid { get { return _valueValid; } set { _valueValid = value; } }
    }
}
