using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using ParserModel;
using ParserModel.ParseWithSelenium;
using static System.Threading.Tasks.Task;

namespace ScopusParserImplementation
{
    public class ScopusParser : AParseWithSelenium
    {

        public ScopusParser(ParserSettings settings) : base(settings)
        { }


        public override async Task<List<Person>> ParseSpecificArticle(string url)
        {
            await Run(() => Driver.Navigate().GoToUrl(url));
            IWebElement authorsList = await Run(() =>
                Driver.FindElement(By.Id("authorlist")));
            var countEmails = await Run(() =>
                Driver.FindElements(By.ClassName("correspondenceEmail")).Count);
            var emails = new List<Person>();
            foreach (var element in await Run(() =>
                authorsList.FindElements(By.TagName("li"))))
            {
                if (emails.Count == countEmails)
                    break;
                try
                {
                    var elementWithEmail = await Run(() => element.FindElement(By.ClassName("correspondenceEmail")));
                    var email = await Run(() => elementWithEmail.GetAttribute("href").Substring(7));
                    var elementWithFio = await Run(() => element.FindElement(By.ClassName("anchorText")));
                    var fio = await Run(() => RemoveBadSymbols(elementWithFio.Text));
                    emails.Add(new Person(fio, email));
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
                var nextLinkUrl = await Run(() =>
                    Driver.FindElement(By.ClassName("nextLink")));
                var nextLink = await Run(() =>
                    nextLinkUrl.FindElement(By.XPath("./a")));
                return await Run(() =>
                   nextLink.GetAttribute("href"));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override async Task<int> GetCountArticle(string url)
        {

            IWebElement count = await Run(() =>
                Driver.FindElement(By.ClassName("recordPageCount")));
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
