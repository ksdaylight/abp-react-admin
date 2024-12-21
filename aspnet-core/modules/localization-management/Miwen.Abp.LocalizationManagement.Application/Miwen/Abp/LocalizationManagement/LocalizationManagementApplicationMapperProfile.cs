using AutoMapper;

namespace Miwen.Abp.LocalizationManagement;

public class LocalizationManagementApplicationMapperProfile : Profile
{
    public LocalizationManagementApplicationMapperProfile()
    {
        CreateMap<Language, LanguageDto>();
        CreateMap<Resource, ResourceDto>();
    }
}
