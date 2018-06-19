using System;
using System.Text.RegularExpressions;
using KeePassLib;

namespace KPChange.PasswordChangers
{
    public class DropboxChanger : AbstractPasswordChanger
    {
        
        protected override Regex _urlMatch => new Regex(@"https://www\.dropbox\.com");
        
        internal override void ChangePassword(PwEntry pwEntry)
        {
            Console.WriteLine(pwEntry.Strings.Get("Title").ReadString() + ": " + this.GetType());
        }
        
    }
}