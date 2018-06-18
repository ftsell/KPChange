using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KPChange.PasswordChangers
{
    public abstract class AbstractSeleniumChanger : AbstractPasswordChanger
    {

        protected static readonly IWebDriver Driver = new ChromeDriver();

        public override void OnBegin()
        {
            base.OnBegin();
        }

        public override void OnEnd()
        {
            base.OnEnd();
            Driver.Quit();
        }
    }
}