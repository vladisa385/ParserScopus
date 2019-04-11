using System;
using System.Collections.Generic;

namespace ScopusModel
{
    public interface IParse : IDisposable
    {
        List<Person> ParseSpecificArticle(string url);

        string GetNextArticle(string url);

        int GetCountArticle(string url);

    }
}
