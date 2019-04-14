using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ParserModel;

namespace EmailParserView.LogSaver
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
            try
            {
                using (TextWriter tw = new StreamWriter(pathToFile))
                {
                    foreach (var person in resultPersons)
                        tw.WriteLine(person.ToString(isOnlyEmail));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                   $@"Во время записи в txt файл произошла ошибка {e.Message} {e.StackTrace}");

            }

        }
    }
}
