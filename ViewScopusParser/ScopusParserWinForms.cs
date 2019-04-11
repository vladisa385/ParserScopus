using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ScopusModel;
using System.Threading;
using OpenQA.Selenium;
using ViewScopusParser.LogSaver;
using Action = System.Action;
using TextBox = System.Windows.Forms.TextBox;

namespace ViewScopusParser
{
    public partial class ScopusParserWinForms : Form
    {
        private readonly List<Person> _persons;
        private ILogSave _resultSaver;
        private int _countForAutoSave;
        public ScopusParserWinForms()
        {
            _persons = new List<Person>();
            _resultSaver = new TxtLogSaver();
            _countForAutoSave = 0;
            InitializeComponent();
            AutoSaveComboBox.SelectedIndex = 0;
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
                ChangeControlInMainUi(PersentLabel, () => PersentLabel.Text = $@"Парсится {progressBar1.Value} статья из {progressBar1.Maximum}({(progressBar1.Value) * 100 / progressBar1.Maximum}%)");
                var result = parser.ParseSpecificArticle(nextUrl);
                if (result != null)
                {
                    _persons.AddRange(result);
                    foreach (var item in result)
                    {
                        ChangeControlInMainUi(ReturnedEmailDataGrid, () => ReturnedEmailDataGrid.Rows.Add(item.Fio, item.Email));
                    }
                }

                nextUrl = parser.GetNextArticle(nextUrl);
                var url = nextUrl;
                ChangeControlInMainUi(URLTextBox, () => URLTextBox.Text = url);
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
                uint delay = uint.Parse(delayTextBox.Text) * 1000;
                uint countAttempts = Properties.Settings.Default.CountAttempt;
                var baseParser = new ScopusParser(SupportedSeleniumBrowsers.Chrome);
                parser = new SleepRetryerScopusParser(baseParser, countAttempts, delay);
                ParsePageFromUrlTextBox(parser);
            }
            catch (DriverServiceNotFoundException)
            {
                MessageBox.Show(@"При запуске парсера возникла проблема.У вас отсутствует выбранный браузер, попробуйте другой",
                    @"Ошибка соединения!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (NoSuchElementException)
            {
                MessageBox.Show(@"Проверьте Ваше интернет-соединение. Возможно, вы забыли подключить VPN или указали неверную ссылку.",
                    @"Ошибка соединения!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (WebDriverException)
            {
                MessageBox.Show(@"При запуске парсера возникла проблема. Вы указали неверный URL.",
                    @"Ошибка соединения!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{ex.Message}{ex.StackTrace}",
                    @"Ошибка работы парсера!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                parser?.Dispose();
                ChangeControlInMainUi(ProgressGroupBox, () => ProgressGroupBox.Visible = false);
                ChangeControlInMainUi(StartParseButton, () => StartParseButton.Enabled = true);
                _countForAutoSave = 0;
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
            _persons.Clear();
            Thread thread = new Thread(StartParseButtonClickAsync);
            thread.Start();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*",
                DefaultExt = "*.txt",
                FileName = "resultsFromParse",
                Title = "Укажите директорию и имя файла для сохранения"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            _resultSaver = new TxtLogSaver();
            Thread thread = new Thread(() => _resultSaver.Save(_persons, saveFileDialog.FileName, IsExportOnlyEmailcheckBox.Checked));
            thread.Start();

        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "MS Excel documents (*.xlsx)|*.xlsx",
                DefaultExt = "*.xlsx",
                FileName = "resultsFromParse",
                Title = "Укажите директорию и имя файла для сохранения"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            _resultSaver = new ExcelLogSaver(ReturnedEmailDataGrid);
            Thread thread = new Thread(() => _resultSaver.Save(_persons, saveFileDialog.FileName, IsExportOnlyEmailcheckBox.Checked));
            thread.Start();
        }

        private void ReturnedEmailDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (_persons.Count - _countForAutoSave < Properties.Settings.Default.AutoSaveStep)
                return;
            _countForAutoSave = _countForAutoSave += (int)Properties.Settings.Default.AutoSaveStep;
            switch (AutoSaveComboBox.SelectedIndex)
            {
                case 0:
                    return;
                case 1:
                    _resultSaver = new TxtLogSaver();
                    break;
                case 2:
                    _resultSaver = new ExcelLogSaver(ReturnedEmailDataGrid);
                    break;
            }

            string rootPath = Directory.GetCurrentDirectory();
            rootPath += $"\\resultParse.{_resultSaver.FileFormat}";
            if (!File.Exists(rootPath))
            {
                File.CreateText(rootPath);
            }
            Thread thread = new Thread(() => _resultSaver.Save(_persons.GetRange(0, _countForAutoSave), rootPath, IsExportOnlyEmailcheckBox.Checked));
            thread.Start();

        }
    }
}
