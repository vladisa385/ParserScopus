using System;
using System.Collections.Generic;

namespace ScopusModel
{
    public interface IParse : IDisposable
    {
        List<ResultEmail> ParseSpecificArticle(string url);

        string GetNextArticle(string url);

        int GetCountArticle(string url);

    }
}
