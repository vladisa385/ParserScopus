using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium;
using ParserModel;
using ParserModel.ParseWithSelenium;
using static System.Threading.Tasks.Task;

namespace WebOfScienceParserImplementation
{
    public class WebOfScienceParser : AParseWithSelenium
    {
        public WebOfScienceParser(ParserSettings settings) : base(settings)
        {
        }

        public override async Task<List<Person>> ParseSpecificArticle(string url)
        {

            Driver.Navigate().GoToUrl(url);
            IWebElement authorsList = await Run(() =>
               Driver.FindElement(By.ClassName("l-content")));
            var countEmails = await Run(() =>
                authorsList.FindElements(By.ClassName("snowplow-author-email-addresses")).Count);
            var emails = new List<Person>();
            foreach (var element in await Run(() =>
                authorsList.FindElements(By.ClassName("snowplow-author-email-addresses"))))
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

        public override async Task<string> GetNextArticle(string url)
        {

            try
            {
                IWebElement webElement = await Run(() =>
                    Driver
                        .FindElement(By.Id("paginationForm"))
                        .FindElement(By.ClassName("FR_rec_num"))
                        .FindElements(By.TagName("a"))[1]);
                return await Run(() => webElement.GetAttribute("href"));

            }
            catch (Exception)
            {
                return null;
            }
        }

        public override Task<int> GetCountArticle(string url)
        {
            throw new NotImplementedException();
        }
    }
}
