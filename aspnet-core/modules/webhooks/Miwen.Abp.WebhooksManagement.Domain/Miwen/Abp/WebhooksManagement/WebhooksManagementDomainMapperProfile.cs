using AutoMapper;
using Miwen.Abp.Webhooks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Miwen.Abp.WebhooksManagement;

public class WebhooksManagementDomainMapperProfile : Profile
{
    public WebhooksManagementDomainMapperProfile()
    {
        CreateMap<WebhookEventRecord, WebhookEventEto>();
        CreateMap<WebhookSendRecord, WebhookSendAttemptEto>();
        CreateMap<WebhookSubscription, WebhookSubscriptionEto>();

        CreateMap<WebhookEventRecord, WebhookEvent>();
        CreateMap<WebhookSendRecord, WebhookSendAttempt>();
    }
}
