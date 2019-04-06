using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
            IWebElement authorlist = _driver.FindElement(By.Id("authorlist"));
            var emails = new List<ResultEmail>();
            IWebElement lastElement = null;
            foreach (var element in authorlist.FindElements(By.TagName("li")))
            {

                try
                {
                    var elementWithEmail = element.FindElement(By.ClassName("correspondenceEmail"));
                    var email = elementWithEmail.GetAttribute("href");
                    var fio = lastElement?.Text;
                    emails.Add(new ResultEmail(fio, email));
                }
                catch (Exception e)
                {
                    // ignored
                }
                finally
                {
                    lastElement = element;
                }
            }
            nextUrl = GetNextArticle(url);
            return emails;
        }

        public string GetNextArticle(string url)
        {
            IWebElement nextLinkURL = _driver.FindElement(By.ClassName("nextLink"));
            IWebElement nextLink = nextLinkURL.FindElement(By.XPath("./a"));

            Console.WriteLine(value: nextLink.GetAttribute("href"));

            return nextLink.GetAttribute("href");
        }

        public int GetCountArticles(string url)
        {
            throw new NotImplementedException();
        }
    }
}
