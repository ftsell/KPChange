using System.Collections.Generic;
using System.Text.RegularExpressions;
using KeePassLib;

namespace KPChange.PasswordChangers
{
    public abstract class AbstractPasswordChanger
    {

        protected abstract Regex _urlMatch { get; }
        public bool AutomaticSelect;

        protected AbstractPasswordChanger(bool automaticSelect)
        {
            AutomaticSelect = automaticSelect;
        }

        protected AbstractPasswordChanger()
        {
        }

        /// <summary>
        /// Change the remote (web) password of this entry to the current password
        /// </summary>
        public abstract void ChangePassword(PwEntry pwEntry);

        /// <summary>
        /// Does this IPasswordChanger instance handle the given password-entry?
        /// </summary>
        /// <returns>Priority with which this Handler handles the given entry. Lesser than 1 means it does not handle at all</returns>
        public virtual int DoesHandleEntry(PwEntry pwEntry) => _urlMatch.IsMatch(GetUrl(pwEntry)) ? 10 : 0;

        protected string GetUrl(PwEntry pwEntry) => pwEntry.Strings.Get("URL").ReadString();
    }
}