using System;

namespace Miwen.Abp.LocalizationManagement;

public class AbpLocalizationManagementOptions
{
    /// <summary>
    /// 保存本地化文本到数据库
    /// </summary>
    public bool SaveStaticLocalizationsToDatabase { get; set; } = true;

    /// <summary>
    /// 申请时间戳超时时间
    /// default: 2 minutes
    /// </summary>
    public TimeSpan LocalizationCacheStampTimeOut { get; set; }
    /// <summary>
    /// 时间戳过期时间
    /// default: 30 minutes
    /// </summary>
    public TimeSpan LocalizationCacheStampExpiration { get; set; }

    public AbpLocalizationManagementOptions()
    {
        LocalizationCacheStampTimeOut = TimeSpan.FromMinutes(2);
        // 30分钟过期重新刷新缓存
        LocalizationCacheStampExpiration = TimeSpan.FromMinutes(30);
    }
}
