using System.Collections.Generic;
using ParserModel;

namespace ViewScopusParser.LogSaver
{
    public interface ILogSave
    {
        string FileFormat { get; }
        void Save(List<Person> resultPersons, string pathToWrite, bool isOnlyEmail);
    }
}