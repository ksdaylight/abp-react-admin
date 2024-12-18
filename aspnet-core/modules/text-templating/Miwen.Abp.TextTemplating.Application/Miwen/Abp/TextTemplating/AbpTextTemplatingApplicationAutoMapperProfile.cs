using AutoMapper;

namespace Miwen.Abp.TextTemplating;

public class AbpTextTemplatingApplicationAutoMapperProfile : Profile
{
    public AbpTextTemplatingApplicationAutoMapperProfile()
    {
        CreateMap<TextTemplateDefinition, TextTemplateDefinitionDto>();
    }
}
