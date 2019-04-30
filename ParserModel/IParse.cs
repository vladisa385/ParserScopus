using System;
using System.Collections.Generic;

namespace ParserModel
{
    public interface IParse : IDisposable
    {
        List<Person> ParseSpecificArticle(string url);

        string GetNextArticle(string url);

        int GetCountArticle(string url);

        void Restart();

    }
}
