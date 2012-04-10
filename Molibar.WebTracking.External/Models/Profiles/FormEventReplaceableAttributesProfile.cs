using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Amazon.SimpleDB.Model;
using AutoMapper;
using Molibar.Common;
using Molibar.WebTracking.Domain.Model;
using Molibar.WebTracking.External.SimpleDb;

namespace Molibar.WebTracking.External.Models.Profiles
{
    public class FormEventReplaceableAttributesProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<List<ReplaceableAttribute>, FormEvent>().ConvertUsing(ReplaceableAttributeToFormEvent);
            CreateMap<FormEvent, List<ReplaceableAttribute>>().ConvertUsing(FormEventToReplaceableAttribute);
        }

        protected FormEvent ReplaceableAttributeToFormEvent(List<ReplaceableAttribute> replaceableAttributes)
        {
            return new FormEvent
                       {
                           Id = replaceableAttributes.First(x => x.Name.Equals("Id")).Value,
                           VisitGuid = Guid.Parse(replaceableAttributes.First(x => x.Name.Equals("VisitGuid")).Value),
                           Url = replaceableAttributes.First(x => x.Name.Equals("Url")).Value,
                           PageId = replaceableAttributes.First(x => x.Name.Equals("PageId")).Value,
                           EventType = replaceableAttributes.First(x => x.Name.Equals("EventType")).Value,
                           ElementId = replaceableAttributes.First(x => x.Name.Equals("ElementId")).Value,
                           ElementValue = replaceableAttributes.First(x => x.Name.Equals("ElementValue")).Value,
                           ValueValid = DataConverter.ToBoolean(replaceableAttributes.First(x => x.Name.Equals("ValueValid")).Value),
                           ClientDateTime = DataConverter.ToDateTime(replaceableAttributes.First(x => x.Name.Equals("ClientDateTime")).Value),
                           DateTime = DataConverter.ToDateTime(replaceableAttributes.First(x => x.Name.Equals("DateTime")).Value)
                       };
        }

        protected List<ReplaceableAttribute> FormEventToReplaceableAttribute(FormEvent formEvent)
        {
            return new List<ReplaceableAttribute>(
                new[]
                    {
                        new ReplaceableAttribute { Name = "Id", Value = formEvent.Id },
                        new ReplaceableAttribute { Name = "VisitGuid", Value = formEvent.VisitGuid.ToString() },
                        new ReplaceableAttribute { Name = "Url", Value = EnsureValue(formEvent.Url) },
                        new ReplaceableAttribute { Name = "PageId", Value = EnsureValue(formEvent.PageId) },
                        new ReplaceableAttribute { Name = "EventType", Value = EnsureValue(formEvent.EventType) },
                        new ReplaceableAttribute { Name = "ElementId", Value = EnsureValue(formEvent.ElementId) },
                        new ReplaceableAttribute { Name = "ElementValue", Value = EnsureValue(formEvent.ElementValue) },
                        new ReplaceableAttribute { Name = "ValueValid", Value = formEvent.ValueValid.ToString(CultureInfo.InvariantCulture) },
                        new ReplaceableAttribute { Name = "ClientDateTime", Value = formEvent.ClientDateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING) },
                        new ReplaceableAttribute { Name = "DateTime", Value = formEvent.DateTime.ToString(SimpleDbProxy.DATE_FORMAT_STRING) }
                    }
                );
        }

        internal static string EnsureValue(string value)
        {
            if (value == null) return "<null>";
            return value;
        }

        internal static string EnsurePaddedIntValue(int? value)
        {
            if (!value.HasValue) return "<null>";
            return StringTools.PadValue(value.Value, 7);
        }
    }
}
