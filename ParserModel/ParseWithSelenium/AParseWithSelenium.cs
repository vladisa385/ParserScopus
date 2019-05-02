using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace ParserModel.ParseWithSelenium
{
    public abstract class AParseWithSelenium : IParse
    {

        protected IWebDriver Driver;
        private readonly ParserSettings _settings;
        protected AParseWithSelenium(ParserSettings settings)
        {
            _settings = settings;
            Driver = CreateIWebDriverFabricMethod();
        }

        public void Dispose()
        {
            Driver?.Dispose();
        }

        protected IWebDriver CreateIWebDriverFabricMethod()
        {
            IWebDriver driver;
            var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Proxy proxy = null;
            switch (_settings.TypeOrganization)
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
            switch (_settings.Browser)
            {
                case SupportedSeleniumBrowsers.Chrome:
                    var options = new ChromeOptions
                    {
                        Proxy = proxy
                    };
                    driver = new ChromeDriver(options);
                    break;
                case SupportedSeleniumBrowsers.FireFox:
                    var optionsFirefox = new FirefoxOptions
                    {
                        Proxy = proxy
                    };
                    driver = new FirefoxDriver(driverDirectory, optionsFirefox);
                    break;

                default:
                    driver = null;
                    break;
            }
            return driver;
        }

        public Task Restart()
        {
            return Task.Run(() =>
            {
                Driver?.Dispose();
                Driver = CreateIWebDriverFabricMethod();
            });
        }

        public abstract Task<List<Person>> ParseSpecificArticle(string url);
        public abstract Task<string> GetNextArticle(string url);
        public abstract Task<int> GetCountArticle(string url);
    }
}
