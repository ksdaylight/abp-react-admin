using System;

namespace Miwen.Abp.Identity.Session;

public interface ISessionInfoProvider
{
    string SessionId { get; }

    IDisposable Change(string sessionId);
}
