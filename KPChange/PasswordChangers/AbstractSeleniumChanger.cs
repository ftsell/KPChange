using System.Collections.ObjectModel;
using System.Drawing;
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
            Driver.Manage().Window.Size = new Size(980, 980);
        }

        public override void OnEnd()
        {
            base.OnEnd();
            //Driver.Quit();
        }
        
    }

    public class By : OpenQA.Selenium.By
    {
        public static OpenQA.Selenium.By Content(string content) 
            => XPath("//*[contains(text(), '" + content + "')]");

        public static OpenQA.Selenium.By Attrubute(string key, string value) 
            => XPath("//*[@" + key + "='" + value + "']");
    }
    
}