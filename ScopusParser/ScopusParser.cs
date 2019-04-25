using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using ParserModel;
using ParserModel.ParseWithSelenium;

namespace ScopusParserImplementation
{
    public class ScopusParser : AParseWithSelenium
    {

        public ScopusParser(ParserSettings settings) : base(settings)
        { }


        public override List<Person> ParseSpecificArticle(string url)
        {
            Driver.Navigate().GoToUrl(url);
            IWebElement authorsList = Driver.FindElement(By.Id("authorlist"));
            var countEmails = Driver.FindElements(By.ClassName("correspondenceEmail")).Count;
            var emails = new List<Person>();
            foreach (var element in authorsList.FindElements(By.TagName("li")))
            {
                if (emails.Count == countEmails)
                    break;
                try
                {
                    var elementWithEmail = element.FindElement(By.ClassName("correspondenceEmail"));
                    var email = elementWithEmail.GetAttribute("href").Substring(7);
                    var elementWithFio = element.FindElement(By.ClassName("anchorText"));
                    var fio = RemoveBadSymbols(elementWithFio.Text);
                    emails.Add(new Person(fio, email));
                }
                catch (NoSuchElementException)
                {
                    // ignored
                }
            }
            return emails;
        }

        public override string GetNextArticle(string url)
        {
            try
            {
                IWebElement nextLinkUrl = Driver.FindElement(By.ClassName("nextLink"));
                IWebElement nextLink = nextLinkUrl.FindElement(By.XPath("./a"));
                return nextLink.GetAttribute("href");
            }
            catch (Exception)
            {
                return null;
            }

        }

        public override int GetCountArticle(string url)
        {
            IWebElement count = Driver.FindElement(By.ClassName("recordPageCount"));
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
