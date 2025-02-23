namespace Miwen.Abp.Authentication.Github;

public static class AbpAuthenticationGithubConsts
{
    public static string AuthenticationScheme { get; set; } = "github";
    public static string DisplayName { get; set; } = "Github Connect";
    public static string CallbackPath { get; set; } = "/signin-callback-github";
}
