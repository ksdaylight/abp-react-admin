using System;

namespace Miwen.Abp.BackgroundTasks;
public interface IJobExceptionTypeFinder
{
    JobExceptionType GetExceptionType(JobEventContext eventContext, Exception exception);
}

