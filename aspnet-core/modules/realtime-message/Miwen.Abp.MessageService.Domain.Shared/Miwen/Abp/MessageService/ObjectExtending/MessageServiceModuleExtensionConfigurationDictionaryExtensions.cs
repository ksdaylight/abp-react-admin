using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Miwen.Abp.MessageService.ObjectExtending;

public static class MessageServiceModuleExtensionConfigurationDictionaryExtensions
{
    public static ModuleExtensionConfigurationDictionary ConfigureMessage(
        this ModuleExtensionConfigurationDictionary modules,
        Action<MessageServiceModuleExtensionConfiguration> configureAction)
    {
        return modules.ConfigureModule(
            MessageServiceModuleExtensionConsts.ModuleName,
            configureAction
        );
    }
}
