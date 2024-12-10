using AutoMapper;
using Volo.Abp.Identity;

namespace Miwen.Abp.Identity;
public class IdentityDomainMappingProfile : Profile
{
    public IdentityDomainMappingProfile()
    {
        CreateMap<IdentitySession, IdentitySessionEto>();
    }
}
