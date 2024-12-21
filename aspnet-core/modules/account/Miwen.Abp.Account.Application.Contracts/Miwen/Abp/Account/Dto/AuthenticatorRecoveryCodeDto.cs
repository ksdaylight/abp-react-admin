using System.Collections.Generic;

namespace Miwen.Abp.Account;
public class AuthenticatorRecoveryCodeDto
{
    public List<string> RecoveryCodes { get; set; }
}
