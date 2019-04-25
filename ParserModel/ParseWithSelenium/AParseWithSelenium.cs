using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ParserModel.ParseWithSelenium
{
    public abstract class AParseWithSelenium : IParse
    {

        protected readonly IWebDriver Driver;

        protected AParseWithSelenium(ParserSettings settings)
        {
            Driver = CreateIWebDriverFabricMethod(settings);
        }

        public void Dispose()
        {
            Driver?.Dispose();
        }

        protected IWebDriver CreateIWebDriverFabricMethod(ParserSettings settings)
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

        public abstract List<Person> ParseSpecificArticle(string url);
        public abstract string GetNextArticle(string url);
        public abstract int GetCountArticle(string url);
    }
}
