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
            return new List<Person> { new Person("23", "23") };
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
