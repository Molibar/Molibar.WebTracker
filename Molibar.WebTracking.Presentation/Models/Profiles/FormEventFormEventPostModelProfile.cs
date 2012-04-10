using System;
using Molibar.WebTracking.Domain.Model;

namespace Molibar.WebTracking.Presentation.Models.Profiles
{
    public class FormEventFormEventPostModelProfile : EventEventPostModelProfile
    {
        protected override void Configure()
        {
            CreateMap<FormEventPostModel, FormEvent>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ClientDateTime,
                opt => opt.ResolveUsing(src => JavascriptTicksToDate(src.MillisSince1970)));

            CreateMap<FormEvent, FormEventPostModel>()
                .ForMember(dest => dest.MillisSince1970,
                opt => opt.ResolveUsing(src => DateToJavascriptTicks(src.ClientDateTime)));
        }
    }
}
