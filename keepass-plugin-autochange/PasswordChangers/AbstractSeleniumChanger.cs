using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoChange.PasswordChangers
{
    public abstract class AbstractSeleniumChanger : AbstractPasswordChanger
    {

        protected static readonly IWebDriver Driver = new ChromeDriver();

        internal override void OnBegin()
        {
            base.OnBegin();
            Driver.Manage().Window.Size = new Size(980, 980);
        }

        internal override void OnEnd()
        {
            base.OnEnd();
            //Driver.Quit();
        }

        protected IWebElement WaitForElement(OpenQA.Selenium.By by)
        {
            // We dont use do-while to avoid a selenium staleReferenceException
            
            while (!Driver.FindElement(by).Displayed)
                Thread.Sleep(100);

            return Driver.FindElement(by);
        }

        protected ReadOnlyCollection<IWebElement> WaitForElements(OpenQA.Selenium.By by)
        {
            WaitForElement(by);

            return Driver.FindElements(by);
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