using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParserModel
{
    public interface IParse : IDisposable
    {
        Task<List<Person>> ParseSpecificArticle(string url);

        Task<string> GetNextArticle(string url);

        Task<int> GetCountArticle(string url);

        Task Restart();

    }
}
