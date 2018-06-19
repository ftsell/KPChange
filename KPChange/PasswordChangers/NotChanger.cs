using System;
using System.Text.RegularExpressions;
using KeePassLib;

namespace KPChange.PasswordChangers
{
    /// <summary>
    /// This Changer handels absolutely everything with priority one (lowest possible).
    /// It doesnt actually do anything though
    /// </summary>
    public class NotChanger :AbstractPasswordChanger
    {
        
        protected override Regex _urlMatch => null;

        internal override void ChangePassword(PwEntry pwEntry)
        {
            Console.WriteLine(pwEntry.Strings.Get("Title").ReadString() + ": " + this.GetType());
        }

        internal override int DoesHandleEntry(PwEntry pwEntry) => 1;
    }
}