using System;
using AutoMapper;
using Molibar.WebTracking.Domain.Model;
using MongoDB.Bson;

namespace Molibar.WebTracking.External.Models.Profiles
{
    public class FormEventFormEventDataModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<FormEventDataModel, FormEvent>()
                .ForMember(dest => dest.VisitGuid, opt => opt.MapFrom(
                    src => (src.VisitGuid == null) ? Guid.Empty : Guid.Parse(src.VisitGuid)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<FormEvent, FormEventDataModel>()
                .ForMember(dest => dest.VisitGuid, opt => opt.MapFrom(src => src.VisitGuid.ToString()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(
                    src => (src.Id == null) ? ObjectId.GenerateNewId() : ObjectId.Parse(src.Id)));
        }
    }
}