using System.Collections.Generic;
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
            return new List<Person>();
        }

        public override string GetNextArticle(string url)
        {
            return null;
        }

        public override int GetCountArticle(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
