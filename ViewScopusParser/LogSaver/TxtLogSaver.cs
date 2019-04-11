using System.Collections.Generic;
using System.IO;
using ScopusModel;

namespace ViewScopusParser.LogSaver
{
    public class TxtLogSaver : ILogSave
    {
        public TxtLogSaver()
        {
            FileFormat = "txt";
        }

        public string FileFormat { get; }

        public void Save(List<Person> resultPersons, string pathToFile, bool isOnlyEmail)
        {
            using (TextWriter tw = new StreamWriter(pathToFile))
            {
                foreach (var person in resultPersons)
                    tw.WriteLine(person.ToString(isOnlyEmail));
            }
        }
    }
}
