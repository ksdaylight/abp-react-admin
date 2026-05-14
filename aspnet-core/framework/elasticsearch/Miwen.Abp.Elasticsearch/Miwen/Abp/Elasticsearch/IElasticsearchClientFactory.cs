using Elastic.Clients.Elasticsearch;

namespace Miwen.Abp.Elasticsearch
{
    public interface IElasticsearchClientFactory
    {
        ElasticsearchClient Create();
    }
}
