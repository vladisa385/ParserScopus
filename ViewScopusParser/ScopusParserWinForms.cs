using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ScopusModel;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using System.Threading;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;

namespace ViewScopusParser
{
    public partial class ScopusParserWinForms : Form
    {
        public ScopusParserWinForms()
        {
            InitializeComponent();
        }

        private void ParsePageFromUrlTextBox(IParse parser)
        {

            string firstUrl = URLTextBox.Text;
            var nextUrl = firstUrl;
            var countArticles = Convert.ToInt32(PagesCounTextBox.Text);

            progressBar1.Invoke((MethodInvoker)(() => progressBar1.Maximum = countArticles));
            progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = 0));
            MaxPagelabel.Invoke((MethodInvoker)(() => MaxPagelabel.Text = countArticles.ToString()));
            for (var currentArticle = 1; currentArticle <= countArticles && nextUrl != null; currentArticle++)
            {
                var result = parser.ParseSpecificArticle(nextUrl);

                if (countArticles > parser.GetCountArticle(firstUrl))
                {
                    countArticles = parser.GetCountArticle(firstUrl);
                    progressBar1.Invoke((MethodInvoker)(() => progressBar1.Maximum = countArticles));
                }

                foreach (var item in result)
                {
                    ReturnedEmailDataGrid.Invoke((MethodInvoker)(() => ReturnedEmailDataGrid.Rows.Add(item.Fio, item.Email)));
                }
                nextUrl = parser.GetNextArticle(nextUrl);
                CurrentPagelabel.Invoke((MethodInvoker)(() => CurrentPagelabel.Text = currentArticle.ToString()));
                progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = currentArticle));
            }


        }

        private void StartParseButtonClickAsync()
        {

            var driver = CreateIWebDriverFabricMethod();
            IParse parser = new ScopusParser(driver);
            try
            {
                ParsePageFromUrlTextBox(parser);
            }

            catch (NoSuchElementException)
            {
                MessageBox.Show(@"Проверьте Ваше интернет-соединение. Возможно, вы забыли подключить VPN или указали неверную ссылку.",
                    "Ошибка соединения!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);

            }
            catch (WebDriverException)
            {
                MessageBox.Show(@"При запуске парсера возникла проблема. Вы указали неверный URL.",
                    @"Ошибка соединения!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
            }
            //catch (Exception ee)
            //{
            //    MessageBox.Show(@"При запуске парсера возникла проблема. Попробуйте заново запустить программу.",
            //        @"Ошибка запуска!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
            //}
            finally
            {
                parser.Dispose();
                ProgressGroupBox.Invoke((MethodInvoker)(() => ProgressGroupBox.Visible = false));
                StartParseButton.Invoke((MethodInvoker)(() => StartParseButton.Enabled = true));
            }

        }

        private void ExportExcelbutton_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            excelapp.AlertBeforeOverwriting = false;
            excelapp.DisplayAlerts = false;

            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "MS Excel documents (*.xlsx)|*.xlsx",
                DefaultExt = "*.xlsx",
                FileName = "1",
                Title = "Укажите директорию и имя файла для сохранения"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Workbook workbook = excelapp.Workbooks.Add();
                    Worksheet worksheet = workbook.ActiveSheet;

                    for (int i = 1; i < ReturnedEmailDataGrid.RowCount + 1; i++)
                    {
                        for (int j = 1; j < ReturnedEmailDataGrid.ColumnCount + 1; j++)
                        {
                            worksheet.Rows[i].Columns[j] = ReturnedEmailDataGrid.Rows[i - 1].Cells[j - 1].Value;
                        }
                    }


                    workbook.SaveAs(sfd.FileName);
                    excelapp.Quit();
                }
                catch (Exception)
                {
                    //return;
                    excelapp.Quit();
                }
            }

        }

        private void PagesCounTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private IWebDriver CreateIWebDriverFabricMethod()
        {
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            return driver;
        }

        private void StartParseButton_Click(object sender, EventArgs e)
        {
            ProgressGroupBox.Visible = true;
            StartParseButton.Enabled = false;
            ReturnedEmailDataGrid.Rows.Clear();
            Thread thread = new Thread(StartParseButtonClickAsync);
            thread.Start();
        }
    }
}
