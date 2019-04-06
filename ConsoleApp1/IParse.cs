using System;
using System.Collections.Generic;

namespace ParserScopus
{
    interface IParse : IDisposable
    {
        List<ResultEmail> ParseSpecificArticle(string url);

        string GetNextArticle(string url);

        int GetCountArticle(string url);

    }
}
