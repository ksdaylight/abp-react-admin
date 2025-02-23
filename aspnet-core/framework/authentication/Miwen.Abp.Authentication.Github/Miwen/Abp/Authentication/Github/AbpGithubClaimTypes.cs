namespace Miwen.Abp.Authentication.Github;


/// </summary>
public class AbpGithubClaimTypes
{
    /// <summary>
    /// 用户的唯一标识
    /// </summary>
    public static string Id { get; set; } = "urn:github:id";

    public static string Email { get; set; } = "urn:github:email";
    public static string Avatar { get; set; } = "urn:github:avatar";

}
