using AutoMapper;
using Molibar.WebTracking.Domain.Model;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.Models.Profiles
{
    public class FormEventFormEventDobProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<FormEventDataModel, FormEvent>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<FormEvent, FormEventDataModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.Parse(src.Id)));
        }
    }
}