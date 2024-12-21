using System;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.Saas.Editions;

public interface IEditionAppService :
    ICrudAppService<
        EditionDto,
        Guid,
        EditionGetListInput,
        EditionCreateDto,
        EditionUpdateDto>
{
}
