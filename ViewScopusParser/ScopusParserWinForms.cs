using System;
using System.Windows.Forms;
using ScopusModel;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using OpenQA.Selenium;
using Action = System.Action;
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
            var firstUrl = URLTextBox.Text;
            var nextUrl = firstUrl;
            var countArticles = Convert.ToInt32(PagesCounTextBox.Text);
            ChangeControlInMainUi(progressBar1, () => progressBar1.Value = 0);
            ChangeControlInMainUi(progressBar1, () => progressBar1.Maximum = countArticles);
            for (; progressBar1.Value < countArticles && nextUrl != null; ChangeControlInMainUi(progressBar1, () => progressBar1.Value += 1))
            {
                ChangeControlInMainUi(PersentLabel, () => PersentLabel.Text = (progressBar1.Value) * 100 / progressBar1.Maximum + @"%");
                var result = parser.ParseSpecificArticle(nextUrl);
                foreach (var item in result)
                {
                    ChangeControlInMainUi(ReturnedEmailDataGrid, () => ReturnedEmailDataGrid.Rows.Add(item.Fio, item.Email));
                }
                nextUrl = parser.GetNextArticle(nextUrl);
            }
        }

        private void ChangeControlInMainUi(Control control, Action action)
        {
            control.Invoke(action);
        }

        private void StartParseButtonClickAsync()
        {
            IParse parser = null;
            try
            {
                parser = new ScopusParser(SupportedSeleniumBrowsers.Chrome);
                ParsePageFromUrlTextBox(parser);
            }
            catch (DriverServiceNotFoundException)
            {
                MessageBox.Show(@"При запуске парсера возникла проблема.У вас отсутствует выбранный браузер, попробуйте другой",
                    @"Ошибка соединения!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);

            }
            catch (NoSuchElementException)
            {
                MessageBox.Show(@"Проверьте Ваше интернет-соединение. Возможно, вы забыли подключить VPN или указали неверную ссылку.",
                    @"Ошибка соединения!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);

            }
            catch (WebDriverException)
            {
                MessageBox.Show(@"При запуске парсера возникла проблема. Вы указали неверный URL.",
                    @"Ошибка соединения!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show(@"При запуске парсера возникла проблема. Попробуйте заново запустить программу.",
                    @"Ошибка запуска!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
            }
            finally
            {
                parser?.Dispose();
                ChangeControlInMainUi(ProgressGroupBox, () => ProgressGroupBox.Visible = false);
                ChangeControlInMainUi(StartParseButton, () => StartParseButton.Enabled = true);
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
