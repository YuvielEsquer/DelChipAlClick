using ApiTallerDelChipAlClick.DtoModels;
using ApiTallerDelChipAlClick.Models;
using AutoMapper;

namespace ApiTallerDelChipAlClick.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //LedsMappers
            CreateMap<LedsInsertDto, LedsModel>();
            CreateMap<LedsModel, LedsDto>();
            CreateMap<LedsUpdateDto, LedsModel>();

            //CommonModulesMappers
            CreateMap<CommonModulesInsertDto, CommonModulesModel>();
            CreateMap<CommonModulesModel, CommonModulesDto>();
            CreateMap<CommonModulesUpdateDto, CommonModulesModel>();

        }
    }
}
