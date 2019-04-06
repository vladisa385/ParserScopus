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
            GetCountArticle(url);
            return emails;
        }

        public string GetNextArticle(string url)
        {
            IWebElement nextLinkURL = _driver.FindElement(By.ClassName("nextLink"));
            IWebElement nextLink = nextLinkURL.FindElement(By.XPath("./a"));

            Console.WriteLine(value: nextLink.GetAttribute("href"));

            return nextLink.GetAttribute("href");
        }

        public int GetCountArticle(string url)
        {
            IWebElement Count = _driver.FindElement(By.ClassName("recordPageCount"));

            string ArticleCount = "";
            bool check = false;

            for (int i = 0; i < Count.Text.Length; i++)
            {
                if (Count.Text[i].ToString() == "з")
                {
                    check = true;
                    i++;
                }

                if (check == true)
                {
                    ArticleCount = ArticleCount + Count.Text[i].ToString();
                }
            }

            if (ArticleCount == "")
            {
                return 0;
            }
            else
            {
                //Console.WriteLine(ArticleCount);
                return Convert.ToInt32(ArticleCount);
            }
        }
    }
}
