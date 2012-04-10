using System;
using Molibar.WebTracking.Domain.Model;

namespace Molibar.WebTracking.Presentation.Models.Profiles
{
    public class PageEventPageEventPostModelProfile : EventEventPostModelProfile
    {
        protected override void Configure()
        {
            CreateMap<PageEventPostModel, PageEvent>()
                .ForMember(dest => dest.DateTime, opt => opt.UseValue(DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ClientDateTime,
                opt => opt.ResolveUsing(src => JavascriptTicksToDate(src.MillisSince1970)));

            CreateMap<PageEvent, PageEventPostModel>()
                .ForMember(dest => dest.MillisSince1970,
                opt => opt.ResolveUsing(src => DateToJavascriptTicks(src.ClientDateTime)));
        }
    }
}