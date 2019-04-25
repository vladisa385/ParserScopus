namespace ParserModel
{
    public class ParserSettings
    {
        public readonly SupportedSeleniumBrowsers Browser;
        public readonly TypeOrganization TypeOrganization;

        public ParserSettings(SupportedSeleniumBrowsers browser, TypeOrganization typeOrganization)
        {
            Browser = browser;
            TypeOrganization = typeOrganization;
        }
    }
}
