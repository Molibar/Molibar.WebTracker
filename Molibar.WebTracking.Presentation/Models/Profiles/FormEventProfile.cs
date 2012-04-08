using AutoMapper;
using Molibar.WebTracking.Domain.Model;

namespace Molibar.WebTracking.Presentation.Models.Profiles
{
    public class FormEventProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<FormEventPostModel, FormEvent>();
        }
    }
}
