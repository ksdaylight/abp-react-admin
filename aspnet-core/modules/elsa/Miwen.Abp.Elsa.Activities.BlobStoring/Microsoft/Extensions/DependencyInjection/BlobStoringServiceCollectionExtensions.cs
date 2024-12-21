using Elsa.Options;
using Miwen.Abp.Elsa.Activities.BlobStoring;

namespace Microsoft.Extensions.DependencyInjection;

public static class BlobStoringServiceCollectionExtensions
{
    public static ElsaOptionsBuilder AddBlobStoringActivities(this ElsaOptionsBuilder options)
    {
        options
            .AddActivity<WriteBlob>()
            .AddActivity<ReadBlob>()
            .AddActivity<BlobExists>()
            .AddActivity<DeleteBlob>();

        return options;
    }
}
