using System.Collections;
using System.Collections.Generic;

namespace Miwen.Abp.BackgroundTasks;
public interface IJobDispatcherSelectorList : IList<JobTypeSelector>, ICollection<JobTypeSelector>, IEnumerable<JobTypeSelector>, IEnumerable
{

}
