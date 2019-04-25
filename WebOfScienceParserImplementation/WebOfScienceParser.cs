using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using ParserModel;
using ParserModel.ParseWithSelenium;

namespace WebOfScienceParserImplementation
{
    public class WebOfScienceParser : AParseWithSelenium
    {
        public WebOfScienceParser(ParserSettings settings) : base(settings)
        {
        }

        public override List<Person> ParseSpecificArticle(string url)
        {
            Driver.Navigate().GoToUrl(url);
            IWebElement authorsList = Driver.FindElement(By.ClassName("l-content"));
            var countEmails = authorsList.FindElements(By.ClassName("snowplow-author-email-addresses")).Count;
            var emails = new List<Person>();
            foreach (var element in authorsList.FindElements(By.ClassName("snowplow-author-email-addresses")))
            {
                if (emails.Count == countEmails)
                    break;
                try
                {
                    var email = element.GetAttribute("href").Substring(7);
                    emails.Add(new Person("", email));
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
                IWebElement nextLinkUrl = Driver.FindElement(By.ClassName("paginationNext snowplow-navigation-nextpage-top"));
                //IWebElement nextLink = nextLinkUrl.FindElement(By.XPath("./a"));
                return nextLinkUrl.GetAttribute("href");
            }
            catch (Exception)
            {
                return null;
            }

        }

        public override int GetCountArticle(string url)
        {
            throw new NotImplementedException();
        }
    }
}
