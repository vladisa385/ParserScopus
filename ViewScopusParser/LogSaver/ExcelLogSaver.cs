﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using ParserModel;
using static System.Threading.Tasks.Task;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace EmailParserView.LogSaver
{
    public class ExcelLogSaver : ILogSave
    {
        private readonly DataGridView _datagrid;
        public ExcelLogSaver(DataGridView datagrid)
        {
            _datagrid = datagrid;
            FileFormat = "xlsx";
            FileFilter = @"MS Excel documents (*.xlsx)|*.xlsx";
        }

        public string FileFormat { get; }

        public string FileFilter { get; }

        public async Task Save(List<Person> resultPersons, string pathToWrite, bool isOnlyEmail)
        {
            await Run(() =>
            {
                Application excelapp = new Application
                {
                    AlertBeforeOverwriting = false,
                    DisplayAlerts = false
                };

                try
                {
                    Workbook workbook = excelapp.Workbooks.Add();
                    Worksheet worksheet = workbook.ActiveSheet;

                    for (int i = 1; i < _datagrid.RowCount + 1; i++)
                    {
                        for (int j = 1; j < _datagrid.ColumnCount + 1; j++)
                        {
                            worksheet.Rows[i].Columns[j] = _datagrid.Rows[i - 1].Cells[j - 1].Value;
                        }
                    }

                    workbook.SaveAs(pathToWrite);
                    excelapp.Quit();
                }
                catch (Exception)
                {
                    //MessageBox.Show(
                    //    $@"Во время записи в excel файл произошла ошибка {exception.Message} {exception.StackTrace}");
                }
            });

        }
    }
}
