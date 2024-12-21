using System;
using Volo.Abp.Application.Dtos;

namespace Miwen.Platform.Datas;

public class DataItemDto : EntityDto<Guid>
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }

    public string Description { get; set; }

    public bool AllowBeNull { get; set; }

    public ValueType ValueType { get; set; }
}
