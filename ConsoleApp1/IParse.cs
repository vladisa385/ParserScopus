using System;
using System.Collections.Generic;
using System.Text;

namespace ParserScopus
{
    interface IParse
    {
        List<ResultEmail> ParseSpecificArticle(string url, out string nextUrl);

        string GetNextArticle(string url);

        int GetCountArticles(string url);
    }
}
