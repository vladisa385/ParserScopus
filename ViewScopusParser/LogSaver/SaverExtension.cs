using System.Windows.Forms;

namespace EmailParserView.LogSaver
{
    public static class SaverExtension
    {
        public static ILogSave GetSaverFabricMethod(this TypeSaver typeSaver, DataGridView returnedEmailDataGrid)
        {
            ILogSave resultSaver = null;
            switch (typeSaver)
            {
                case TypeSaver.Excel:
                    resultSaver = new ExcelLogSaver(returnedEmailDataGrid);
                    break;
                case TypeSaver.Txt:
                    resultSaver = new TxtLogSaver();
                    break;
            }
            return resultSaver;
        }
    }
}
