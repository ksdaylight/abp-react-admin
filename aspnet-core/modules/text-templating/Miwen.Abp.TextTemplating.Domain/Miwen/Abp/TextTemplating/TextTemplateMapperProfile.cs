using AutoMapper;

namespace Miwen.Abp.TextTemplating;
public class TextTemplateMapperProfile : Profile
{
    public TextTemplateMapperProfile()
    {
        CreateMap<TextTemplate, TextTemplateEto>();
        CreateMap<TextTemplateDefinition, TextTemplateDefinitionEto>();
    }
}
