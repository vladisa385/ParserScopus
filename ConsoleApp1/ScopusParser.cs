using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ParserScopus
{
    public class ScopusParser : IParse
    {
        private readonly IWebDriver _driver;


        public ScopusParser(IWebDriver driver)
        {
            _driver = driver;
        }


        public List<ResultEmail> ParseSpecificArticle(string url, out string nextUrl)
        {
            _driver.Navigate().GoToUrl(url);
            ReadOnlyCollection<IWebElement> webElement = _driver.FindElements(By.ClassName("correspondenceEmail"));
            var emails = new List<ResultEmail>();
            foreach (var element in webElement)
            {
                var email = element.GetAttribute("href");
                emails.Add(new ResultEmail("", email));
            }
            nextUrl = GetNextArticle(url);
            return emails;
        }

        public string GetNextArticle(string url)
        {
            return null;
        }
    }
}
