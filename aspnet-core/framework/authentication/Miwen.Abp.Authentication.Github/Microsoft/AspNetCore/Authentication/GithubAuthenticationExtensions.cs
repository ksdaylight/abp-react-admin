using Microsoft.AspNetCore.Authentication.Github;
using Microsoft.Extensions.DependencyInjection;
using Miwen.Abp.Authentication.Github;
using System;

namespace Microsoft.AspNetCore.Authentication;

public static class GithubAuthenticationExtensions
{
    /// <summary> 
    /// </summary>
    public static AuthenticationBuilder AddGithubConnect(
        this AuthenticationBuilder builder)
    {
        return builder
            .AddGithubConnect(
                AbpAuthenticationGithubConsts.AuthenticationScheme,
                AbpAuthenticationGithubConsts.DisplayName,
                options => { });
    }

    /// <summary> 
    /// </summary>
    public static AuthenticationBuilder AddGithubConnect(
        this AuthenticationBuilder builder,
        Action<GithubConnectOAuthOptions> configureOptions)
    {
        return builder
            .AddGithubConnect(
                AbpAuthenticationGithubConsts.AuthenticationScheme,
                configureOptions);
    }

    /// <summary> 
    /// </summary>
    public static AuthenticationBuilder AddGithubConnect(
        this AuthenticationBuilder builder,
        string authenticationScheme,
        Action<GithubConnectOAuthOptions>configureOptions)
    {
        return builder
            .AddGithubConnect(
                authenticationScheme,
                AbpAuthenticationGithubConsts.DisplayName,
                configureOptions);
    }

    /// <summary> 
    /// </summary>
    public static AuthenticationBuilder AddGithubConnect(
        this AuthenticationBuilder builder,
        string authenticationScheme,
        string displayName,
        Action<GithubConnectOAuthOptions> configureOptions)
    {
        return builder
            .AddOAuth<GithubConnectOAuthOptions, GithubConnectOAuthHandler>(
                authenticationScheme,
                displayName,
                configureOptions);
    }
}