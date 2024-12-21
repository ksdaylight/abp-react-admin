using System;

namespace Miwen.Platform.Datas;

public class DataCreateDto : DataCreateOrUpdateDto
{
    public Guid? ParentId { get; set; }
}
