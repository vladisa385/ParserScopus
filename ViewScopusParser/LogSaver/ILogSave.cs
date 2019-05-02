using System.Collections.Generic;
using System.Threading.Tasks;
using ParserModel;

namespace EmailParserView.LogSaver
{
    public interface ILogSave
    {
        string FileFormat { get; }
        string FileFilter { get; }
        Task Save(List<Person> resultPersons, string pathToWrite, bool isOnlyEmail);
    }
}