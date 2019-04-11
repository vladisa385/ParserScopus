using System.Collections.Generic;
using ScopusModel;

namespace ViewScopusParser.LogSaver
{
    public interface ILogSave
    {
        string FileFormat { get; }
        void Save(List<Person> resultPersons, string pathToWrite, bool isOnlyEmail);
    }
}