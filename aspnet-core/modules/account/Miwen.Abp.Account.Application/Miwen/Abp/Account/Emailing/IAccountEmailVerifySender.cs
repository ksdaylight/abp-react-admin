using System.Threading.Tasks;

namespace Miwen.Abp.Account.Emailing;

public interface IAccountEmailVerifySender
{
    Task SendMailLoginVerifyCodeAsync(
        string code,
        string userName,
        string emailAddress);
}
