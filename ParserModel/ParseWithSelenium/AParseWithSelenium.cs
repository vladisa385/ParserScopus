using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

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
                    var x = 
                        $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\GoogleChromePortable\App\Chrome-bin\chrome.exe";
                    options.BinaryLocation = x;
                    driver = new ChromeDriver( options);
                    break;
                case SupportedSeleniumBrowsers.FireFox:
                    var optionsFirefox = new FirefoxOptions();
                    Proxy proxyFirefox = null;
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
                    optionsFirefox.Proxy = proxyFirefox;
                    driver = new FirefoxDriver(driverDirectory,optionsFirefox);
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
