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
            GetCountArticle(url);
            return emails;
        }

        public string GetNextArticle(string url)
        {
            try
            {
                IWebElement nextLinkURL = _driver.FindElement(By.ClassName("nextLink"));
                IWebElement nextLink = nextLinkURL.FindElement(By.XPath("./a"));
                return nextLink.GetAttribute("href");
            }
            catch (Exception)
            {
                return null;
            }

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
