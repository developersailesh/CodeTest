
namespace AppServer.Utilities
{
    using AutoMapper;
    using AppServer.Dtos;
    using AppServer.Models;
    public class PetEntityMappingProfile : Profile
    {
        /// <summary>
        /// Automapping 
        /// </summary>
        public PetEntityMappingProfile()
        {
            CreateMap<PetDetailsDto, People>()
                    .ForMember(x => x.gender, opt => opt.MapFrom(src => src.gender))
                    .ForMember(dest => dest.petsCollection,
                               opt => opt.MapFrom(
                                      src => src.petsCollection))
                    .ForAllMembers(opt => opt.Ignore());
        }
    }
}