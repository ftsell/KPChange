using System.Text.RegularExpressions;
using System.Windows.Forms;
using KeePassLib;
using KeePassLib.Security;

namespace KPChange.PasswordChangers
{
    
    public abstract class AbstractPasswordChanger
    {

        protected abstract Regex _urlMatch { get; }
        public bool AutomaticSelect;

        protected AbstractPasswordChanger() {}

        /// <summary>
        /// Change the remote (web) password of this entry to the current password
        /// </summary>
        internal abstract void ChangePassword(PwEntry pwEntry);

        /// <summary>
        /// Does this IPasswordChanger instance handle the given password-entry?
        /// </summary>
        /// <returns>Priority with which this Handler handles the given entry. Lesser than 1 means it does not handle at all</returns>
        internal virtual int DoesHandleEntry(PwEntry pwEntry) => _urlMatch.IsMatch(GetUrl(pwEntry)) ? 10 : 0;
        
        /// <summary>
        /// Gets Called when the whole password changin process is initiated.
        /// Meaning only once at the beginning even when chaning 5 passwords in a row.
        /// </summary>
        internal virtual void OnBegin() {}
        
        /// <summary>
        /// Gets Called when the whole password changin process is completed.
        /// Meaning only once at the end even when chaning 5 passwords in a row.
        /// </summary>
        internal virtual void OnEnd() {}

        protected string GetUrl(PwEntry pwEntry) => pwEntry.Strings.Get("URL").ReadString();

        protected string GetUsername(PwEntry pwEntry) => pwEntry.Strings.Get("UserName").ReadString();

        protected ProtectedString GetPassword(PwEntry pwEntry) => pwEntry.Strings.Get("Password");

    }
}