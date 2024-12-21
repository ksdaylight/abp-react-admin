using System;
using System.Collections.Generic;

namespace Miwen.Abp.TaskManagement;

public class BackgroundJobInfoBatchInput
{
    public List<string> JobIds { get; set; } = new List<string>();
}
