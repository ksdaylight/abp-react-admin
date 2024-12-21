using System;
using System.Collections.Generic;

namespace Miwen.Abp.CachingManagement;

public class CacheValueDto
{
    public string Type { get; set; }
    public long Size { get; set; }
    public DateTime? Expiration { get; set; }
    public IDictionary<string, object> Values { get; set; } = new Dictionary<string, object>();
}
