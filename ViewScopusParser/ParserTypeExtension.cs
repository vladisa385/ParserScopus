using ParserModel;
using ParserModel.ParseWithSelenium;
using ScopusParserImplementation;
using WebOfScienceParserImplementation;

namespace EmailParserView
{
    public static class ParserTypeExtension
    {
        public static IParse GetParserFabricMethod(this ParserType typeParser, ParserSettings settings)
        {
            IParse baseParser = null;
            switch (typeParser)
            {
                case ParserType.Scopus:
                    baseParser = new ScopusParser(settings);
                    break;
                case ParserType.WebOfSciense:
                    baseParser = new WebOfScienceParser(settings);
                    break;
            }
            var delay = Properties.Settings.Default.Delay * 1000;
            var countAttempts = Properties.Settings.Default.CountAttempt;
            var loggerParser = new LoggerParser(baseParser);
            baseParser = new SleepRetryerParser(loggerParser, countAttempts, delay);
            return baseParser;
        }



    }
}
