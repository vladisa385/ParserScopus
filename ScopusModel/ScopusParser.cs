using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ScopusModel
{
    public class ScopusParser : IParse
    {
        private readonly IWebDriver _driver;


        public ScopusParser(IWebDriver driver)
        {
            _driver = driver;
        }


        public List<ResultEmail> ParseSpecificArticle(string url)
        {
            _driver.Navigate().GoToUrl(url);
            IWebElement authorsList = _driver.FindElement(By.Id("authorlist"));
            var emails = new List<ResultEmail>();
            foreach (var element in authorsList.FindElements(By.TagName("li")))
            {

                try
                {
                    var elementWithEmail = element.FindElement(By.ClassName("correspondenceEmail"));
                    var email = elementWithEmail.GetAttribute("href").Substring(7);
                    var elementWithFio = element.FindElement(By.ClassName("anchorText"));
                    var fio = RemoveBadSymbols(elementWithFio.Text);
                    emails.Add(new ResultEmail(fio, email));
                }
                catch (NoSuchElementException)
                {
                    // ignored
                }
            }
            return emails;
        }

        public string GetNextArticle(string url)
        {
            try
            {
                IWebElement nextLinkUrl = _driver.FindElement(By.ClassName("nextLink"));
                IWebElement nextLink = nextLinkUrl.FindElement(By.XPath("./a"));
                return nextLink.GetAttribute("href");
            }
            catch (Exception)
            {
                return null;
            }

        }

        public int GetCountArticle(string url)
        {
            IWebElement count = _driver.FindElement(By.ClassName("recordPageCount"));

            string articleCount = "";
            bool check = false;

            for (int i = 0; i < count.Text.Length; i++)
            {
                if (count.Text[i].ToString() == "з" || count.Text[i].ToString() == "f")
                {
                    check = true;
                    i++;
                }

                if (check)
                {
                    articleCount = articleCount + count.Text[i];
                }
            }

            return articleCount == "" ? 0 : Convert.ToInt32(articleCount);
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        private string RemoveBadSymbols(string rawString)
        {
            int numberLastUpperSymbol = rawString.Length;
            foreach (var i in rawString.Reverse())
            {

                if (char.IsUpper(i))
                {
                    return rawString.Substring(0, numberLastUpperSymbol);
                }
                numberLastUpperSymbol -= 1;
            }
            return rawString;
        }

    }
}
