using System;
using System.Text.RegularExpressions;
using KeePassLib;

namespace KPChange.PasswordChangers
{
    public class GoogleChanger : AbstractSeleniumChanger
    {
        
        protected override Regex _urlMatch => new Regex(@"https://.*\.google\.com");

        public override void ChangePassword(PwEntry pwEntry)
        {
            Console.WriteLine(pwEntry.Strings.Get("Title").ReadString() + ": " + this.GetType());
            //Driver.Url = "www.google.de";
        }

    }
}