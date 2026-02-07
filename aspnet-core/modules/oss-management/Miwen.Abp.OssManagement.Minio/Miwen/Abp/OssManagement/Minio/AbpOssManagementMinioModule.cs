using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Minio;
using Volo.Abp.Modularity;

namespace Miwen.Abp.OssManagement.Minio;

[DependsOn(
    typeof(AbpBlobStoringMinioModule),
    typeof(AbpOssManagementDomainModule))]
public class AbpOssManagementMinioModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IOssContainerFactory, MinioOssContainerFactory>();

        context.Services.AddTransient<IOssObjectExpireor>(provider =>
            provider
                .GetRequiredService<IOssContainerFactory>()
                .Create()
                .As<MinioOssContainer>());

        var configuration = context.Services.GetConfiguration();
        //context.Services.AddMinioHttpClient(); extension method ->TODO 
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureAll((containerName, containerConfiguration) =>
            {
                containerConfiguration.UseMinio(oss =>
                {
                    oss.EndPoint = configuration["Minio:EndPoint"];
                    oss.AccessKey = configuration["Minio:AccessKey"];
                    oss.SecretKey = configuration["Minio:SecretKey"];
                    oss.BucketName = configuration["Minio:BucketName"];
                    oss.WithSSL = bool.Parse(configuration["Minio:WithSSL"] ?? "false");

                    //configuration.GetSection("Minio").Bind(oss);
                });
            });
        });
        //context.Services.AddMinioContainer();
    }
}
