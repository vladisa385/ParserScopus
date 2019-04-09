using System;
using System.Collections.Generic;
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
                    var fio = elementWithFio.Text;
                    emails.Add(new ResultEmail(fio, email));
                }
                catch (Exception)
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
            IWebElement Count = _driver.FindElement(By.ClassName("recordPageCount"));

            string ArticleCount = "";
            bool check = false;

            for (int i = 0; i < Count.Text.Length; i++)
            {
                if (Count.Text[i].ToString() == "з" || Count.Text[i].ToString() == "f")
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

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
