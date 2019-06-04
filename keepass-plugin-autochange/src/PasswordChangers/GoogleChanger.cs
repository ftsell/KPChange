using System;
using System.Text.RegularExpressions;
using KeePassLib;

namespace AutoChange.PasswordChangers
{
    public class GoogleChanger : AbstractSeleniumChanger
    {
        
        protected override Regex _urlMatch => new Regex(@"https://.*\.google\.com");

        internal override void ChangePassword(PwEntry pwEntry)
        {
            Console.WriteLine(pwEntry.Strings.Get("Title").ReadString() + ": " + this.GetType());

            Driver.Url = "https://myaccount.google.com/intro/signinoptions/password?hl=en";
            WaitForElement(By.Content("Continue to sign in")).Click();
            
            WaitForElement(By.Attrubute("type", "email")).SendKeys(GetUsername(pwEntry));
            Driver.FindElement(By.Id("identifierNext")).Click();
            
            WaitForElement(By.Attrubute("type", "password")).SendKeys(GetPassword(pwEntry).ReadString());
            Driver.FindElement(By.Id("passwordNext")).Click();

            GenerateNewPassword();

        }

    }
}