using System.Collections.Generic;
using System.Linq;

namespace Miwen.Abp.SettingManagement;

public class SettingGroupResult
{
    public IList<SettingGroupDto> Items { get; }

    public SettingGroupResult()
    {
        Items = new List<SettingGroupDto>();
    }

    public void AddGroup(SettingGroupDto group)
    {
        if (group.Settings.Any(g => g.Details.Any()))
        {
            Items.Add(group);
        }
    }
}
