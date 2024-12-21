using Elsa.Builders;
using System;
using System.Runtime.CompilerServices;

namespace Miwen.Abp.Elsa.Activities.Emailing;
public static class SendEmailingBuilderExtensions
{
    public static IActivityBuilder SendEmailing(
        this IBuilder builder,
        Action<ISetupActivity<SendEmailing>>? setup,
        [CallerLineNumber] int lineNumber = default,
        [CallerFilePath] string? sourceFile = default) =>
            builder.Then(setup, null, lineNumber, sourceFile);
}
