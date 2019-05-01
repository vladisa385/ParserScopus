using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        public Task Save(List<Person> resultPersons, string pathToFile, bool isOnlyEmail)
        {
            return Task.Run(() =>
                {
                    try
                    {
                        using (var w = File.AppendText(pathToFile))
                        {
                            foreach (var person in resultPersons)
                                w.WriteLine(person.ToString(isOnlyEmail));
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(
                           $@"Во время записи в txt файл произошла ошибка {e.Message} {e.StackTrace}");

                    }
                });

        }
    }
}
