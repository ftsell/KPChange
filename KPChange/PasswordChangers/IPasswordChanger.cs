using System.Collections.Generic;
using System.Text.RegularExpressions;
using KeePassLib;

namespace KPChange.PasswordChangers
{
    public interface IPasswordChanger
    {
        void ChangePassword(PwEntry pwEntry);

        bool DoesHandleEntry(PwEntry pwEntry);
    }
}