using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using ParserModel;

namespace ScopusParser
{
    public class ScopusParser : IParse
    {
        private readonly IWebDriver _driver;


        public ScopusParser(ScopusParserSettings settings)
        {
            _driver = CreateIWebDriverFabricMethod(settings);
        }

        public List<Person> ParseSpecificArticle(string url)
        {
            _driver.Navigate().GoToUrl(url);
            IWebElement authorsList = _driver.FindElement(By.Id("authorlist"));
            var countEmails = _driver.FindElements(By.ClassName("correspondenceEmail")).Count;
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

        private IWebDriver CreateIWebDriverFabricMethod(ScopusParserSettings settings)
        {
            IWebDriver driver;
            var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            switch (settings.Browser)
            {
                case SupportedSeleniumBrowsers.Chrome:
                    var options = new ChromeOptions();
                    Proxy proxy = null;
                    switch (settings.TypeOrganization)
                    {
                        case TypeOrganization.Private:
                            proxy = new Proxy
                            {
                                Kind = ProxyKind.System
                            };
                            break;
                        case TypeOrganization.SFU:
                            proxy = new Proxy
                            {
                                Kind = ProxyKind.System
                            };
                            break;
                        case TypeOrganization.SibGau:
                            proxy = new Proxy
                            {
                                Kind = ProxyKind.System
                            };
                            break;

                    }
                    options.Proxy = proxy;
                    driver = new ChromeDriver(driverDirectory, options);
                    break;
                default:
                    driver = null;
                    break;
            }
            return driver;
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
