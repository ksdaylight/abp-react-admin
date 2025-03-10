using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Miwen.Abp.OAuth.Github;

namespace Microsoft.AspNetCore.Authentication.Github;

public class GithubConnectOAuthHandler : OAuthHandler<GithubConnectOAuthOptions>
{
    protected AbpOAuthGithubOptionsFactory OAuthGithubOptionsFactory { get; }

    public GithubConnectOAuthHandler(
        IOptionsMonitor<GithubConnectOAuthOptions> options,
        AbpOAuthGithubOptionsFactory oAuthGithubOptionsFactory,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
        OAuthGithubOptionsFactory = oAuthGithubOptionsFactory;
    }

    protected override async Task InitializeHandlerAsync()
    {
        var options = await OAuthGithubOptionsFactory.CreateAsync();

        // 用配置项重写
        Options.ClientId = options.ClientId;
        Options.ClientSecret = options.ClientSecret;
        Options.TimeProvider ??= TimeProvider.System;

        await base.InitializeHandlerAsync();
    }

    /// <summary>
    /// 构建 GitHub 用户授权地址
    /// </summary>
    protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
    {
        var challengeUrl = base.BuildChallengeUrl(properties, redirectUri);
        return challengeUrl;
    }

    /// <summary>
    /// 用授权码换取 access_token
    /// </summary>
    protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(OAuthCodeExchangeContext context)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, Options.TokenEndpoint);
        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        requestMessage.Content = new FormUrlEncodedContent(new Dictionary<string, string>
{
    { "client_id", Options.ClientId },
    { "client_secret", Options.ClientSecret },
    { "code", context.Code },
    { "redirect_uri", context.RedirectUri }
});

        var response = await Backchannel.SendAsync(requestMessage, Context.RequestAborted);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Logger.LogError("GitHub OAuth 请求 access token 失败: {Status} {Body}",
                response.StatusCode, responseContent);

            return OAuthTokenResponse.Failed(new Exception("获取 GitHub access token 失败"));
        }

        // 直接解析 JSON
        var payload = JsonDocument.Parse(responseContent);
        return OAuthTokenResponse.Success(payload);

    }

    /// <summary>
    /// 使用 access_token 获取 GitHub 用户信息，并创建身份票据
    /// </summary>
    protected override async Task<AuthenticationTicket> CreateTicketAsync(
        ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
    {
        // 1. 调用 GitHub API 获取用户信息
        var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
        userInfoRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        var response = await Backchannel.SendAsync(userInfoRequest, Context.RequestAborted);
        if (!response.IsSuccessStatusCode)
        {
            Logger.LogError("获取 GitHub 用户信息失败: {Status} {Body}",
                response.StatusCode, await response.Content.ReadAsStringAsync());

            throw new HttpRequestException("获取 GitHub 用户信息失败");
        }

        var userInfoPayload = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var userJson = userInfoPayload.RootElement;

        // 2. 解析 GitHub 用户信息
        var githubId = userJson.GetProperty("id").GetInt64().ToString();
        var login = userJson.GetProperty("login").GetString();
        var name = userJson.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : null;
        var email = userJson.TryGetProperty("email", out var emailProp) ? emailProp.GetString() : null;
        var avatarUrl = userJson.TryGetProperty("avatar_url", out var avatarProp) ? avatarProp.GetString() : null;


        // 3. 添加 Claim
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, githubId ?? string.Empty, ClaimValueTypes.String, Options.ClaimsIssuer));
        identity.AddClaim(new Claim(ClaimTypes.Name, name ?? login ?? string.Empty, ClaimValueTypes.String, Options.ClaimsIssuer));

        if (!string.IsNullOrEmpty(email))
        {
            identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, Options.ClaimsIssuer));
        }

        if (!string.IsNullOrEmpty(avatarUrl))
        {
            identity.AddClaim(new Claim("urn:github:avatar", avatarUrl, ClaimValueTypes.String, Options.ClaimsIssuer));
        }

        // 4. 生成 OAuth 票据
        var context = new OAuthCreatingTicketContext(
            new ClaimsPrincipal(identity),
            properties,
            Context,
            Scheme,
            Options,
            Backchannel,
            tokens,
            userJson);

        context.RunClaimActions();
        await Events.CreatingTicket(context);

        return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
    }

}