namespace ScopusParser
{
    public class ScopusParserSettings
    {
        public readonly SupportedSeleniumBrowsers Browser;
        public readonly TypeOrganization TypeOrganization;

        public ScopusParserSettings(SupportedSeleniumBrowsers browser, TypeOrganization typeOrganization)
        {
            Browser = browser;
            TypeOrganization = typeOrganization;
        }
    }
}
