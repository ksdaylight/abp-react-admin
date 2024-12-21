using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.TextTemplating;

namespace Miwen.Abp.TextTemplating;

public interface ITemplateDefinitionStoreCache
{
    string CacheStamp { get; set; }

    SemaphoreSlim SyncSemaphore { get; }

    DateTime? LastCheckTime { get; set; }

    Task FillAsync(
        List<TextTemplateDefinition> templateDefinitionRecords,
        IReadOnlyList<TemplateDefinition> templateDefinitions);

    TemplateDefinition GetOrNull(string name);

    IReadOnlyList<TemplateDefinition> GetAll();
}
