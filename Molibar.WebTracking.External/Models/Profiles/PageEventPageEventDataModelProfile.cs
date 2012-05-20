using AutoMapper;
using Molibar.WebTracking.Domain.Model;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.Models.Profiles
{
    public class PageEventPageEventDataModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PageEventDataModel, PageEvent>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<PageEvent, PageEventDataModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.Parse(src.Id)));
        }
    }
}