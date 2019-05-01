using System.Collections.Generic;
using System.Threading.Tasks;
using ParserModel;

namespace EmailParserView.LogSaver
{
    public interface ILogSave
    {
        string FileFormat { get; }
        Task Save(List<Person> resultPersons, string pathToWrite, bool isOnlyEmail);
    }
}