using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailParserView.LogSaver;
using OpenQA.Selenium;
using ParserModel;
using ScopusParserImplementation;
using WebOfScienceParserImplementation;
using Action = System.Action;
using TextBox = System.Windows.Forms.TextBox;

namespace EmailParserView
{
    public partial class EmailParserWinForms : Form
    {
        private readonly List<Person> _persons;
        private int _countForAutoSave;
        public EmailParserWinForms()
        {
            _persons = new List<Person>();
            _countForAutoSave = 0;
            InitializeComponent();
            SeedAllComboboxValuesAndText();

        }

        private void StartParseButton_Click(object sender, EventArgs e)
        {
            ProgressGroupBox.Visible = true;
            StartParseButton.Enabled = false;
            ReturnedEmailDataGrid.Rows.Clear();
            _persons.Clear();
            var organization = ((ItemComboBox<TypeOrganization>)TypeOrganizationCombobox.SelectedItem).Value;
            var typeParser = ((ItemComboBox<ParserType>)TypeSiteCombobox.SelectedItem).Value;
            Task.Run(() => HandleErrorsAndBeginParsing(organization, typeParser));
        }

        private void HandleErrorsAndBeginParsing(TypeOrganization typeOrganization, ParserType typeParser)
        {
            IParse parser = null;
            try
            {
                parser = GetParserFabricMethod(typeParser, typeOrganization);
                StartParsing(parser);
            }
            catch (DriverServiceNotFoundException)
            {
                ShowErrorToUser("При запуске парсера возникла проблема.У вас отсутствует GoogleChrome");
            }
            catch (NoSuchElementException ex)
            {
                ShowErrorToUser($@"Проверьте Ваше интернет-соединение. Возможно, вы забыли подключить VPN или указали неверную ссылку.{ex.Message}{ex.StackTrace}");
            }
            catch (WebDriverException ex)
            {
                ShowErrorToUser($@"При запуске парсера возникла проблема. Возможно,вы указали неверный URL.{ex.Message}{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                ShowErrorToUser($@"{ex.Message}{ex.StackTrace}");
            }
            finally
            {
                parser?.Dispose();
                ChangeControlInMainUi(ProgressGroupBox, () => ProgressGroupBox.Visible = false);
                ChangeControlInMainUi(StartParseButton, () => StartParseButton.Enabled = true);
                _countForAutoSave = 0;
            }

        }

        private IParse GetParserFabricMethod(ParserType typeParser, TypeOrganization organization)
        {
            IParse baseParser = null;
            var settings = new ParserSettings(SupportedSeleniumBrowsers.Chrome, organization);
            switch (typeParser)
            {
                case ParserType.Scopus:
                    baseParser = new ScopusParser(settings);
                    break;
                case ParserType.WebOfSciense:
                    baseParser = new WebOfScienceParser(settings);
                    break;
            }
            var delay = uint.Parse(delayTextBox.Text) * 1000;
            var countAttempts = Properties.Settings.Default.CountAttempt;
            var loggerParser = new LoggerParser(baseParser);
            baseParser = new SleepRetryerParser(loggerParser, countAttempts, delay);
            return baseParser;
        }

        private ILogSave GetSaverFabricMethod(TypeSaver typeSaver)
        {
            ILogSave resultSaver = null;
            switch (typeSaver)
            {
                case TypeSaver.Excel:
                    resultSaver = new ExcelLogSaver(ReturnedEmailDataGrid);
                    break;
                case TypeSaver.Txt:
                    resultSaver = new TxtLogSaver();
                    break;
            }
            return resultSaver;
        }

        private void StartParsing(IParse parser)
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
                ChangeControlInMainUi(URLTextBox, () => URLTextBox.Text = nextUrl);
                if (result != null)
                {
                    _persons.AddRange(result);
                    foreach (var item in result)
                    {
                        ChangeControlInMainUi(ReturnedEmailDataGrid, () => ReturnedEmailDataGrid.Rows.Add(item.Fio, item.Email));
                    }
                }
                nextUrl = parser.GetNextArticle(nextUrl);
            }
        }

        private void ChangeControlInMainUi(Control control, Action action)
        {
            control.Invoke(action);
        }

        private void SeedAllComboboxValuesAndText()
        {
            var itemsForTypeOrganization = new System.Collections.Generic.List<ItemComboBox<TypeOrganization>>
            {
                new ItemComboBox<TypeOrganization>
                {
                    Text = "Личный ПК",
                    Value = TypeOrganization.Private
                },
                 new ItemComboBox<TypeOrganization>
                 {
                    Text = "СФУ",
                    Value = TypeOrganization.SFU
                },
               new ItemComboBox<TypeOrganization>
               {
                    Text = "СибГАУ",
                    Value = TypeOrganization.SibGau
                }
            };
            TypeOrganizationCombobox.Items.AddRange(items: itemsForTypeOrganization.ToArray());
            TypeOrganizationCombobox.SelectedIndex = 0;

            var itemsForAutoSaveMode = new System.Collections.Generic.List<ItemComboBox<TypeSaver>>
            {
                new ItemComboBox<TypeSaver>()
                {
                    Text = "Нет автосохранения",
                    Value = default(TypeSaver)
                },
                new  ItemComboBox<TypeSaver>()
                {
                    Text = "Автосохранение в excel",
                    Value = TypeSaver.Excel
                },
                new  ItemComboBox<TypeSaver>()
                {
                    Text = "Автосохранение в txt",
                    Value = TypeSaver.Txt
                }
            };
            AutoSaveComboBox.Items.AddRange(items: itemsForAutoSaveMode.ToArray());
            AutoSaveComboBox.SelectedIndex = 0;

            var itemsForTypeSite = new System.Collections.Generic.List<ItemComboBox<ParserType>>
            {
                new ItemComboBox<ParserType>()
                {
                    Text = "Scopus",
                    Value = ParserType.Scopus
                },
                new  ItemComboBox<ParserType>()
                {
                    Text = "WebOfScience",
                    Value = ParserType.WebOfSciense
                },
            };
            TypeSiteCombobox.Items.AddRange(items: itemsForTypeSite.ToArray());
            TypeSiteCombobox.SelectedIndex = 0;

        }

        private void ShowErrorToUser(string text)
        {
            MessageBox.Show(
                    text,
                    @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        private void PagesCounTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*",
                DefaultExt = "*.txt",
                FileName = "resultsFromParse",
                Title = "Укажите директорию и имя файла для сохранения"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            var saver = GetSaverFabricMethod(TypeSaver.Txt);
            Task.Run(() => saver.Save(_persons, saveFileDialog.FileName, IsExportOnlyEmailcheckBox.Checked));
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "MS Excel documents (*.xlsx)|*.xlsx",
                DefaultExt = "*.xlsx",
                FileName = "resultsFromParse",
                Title = "Укажите директорию и имя файла для сохранения"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            var saver = GetSaverFabricMethod(TypeSaver.Excel);
            Task.Run(() => saver.Save(_persons, saveFileDialog.FileName, IsExportOnlyEmailcheckBox.Checked));
        }

        private void ReturnedEmailDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (progressBar1.Value - _countForAutoSave < Properties.Settings.Default.AutoSaveStep)
                return;
            _countForAutoSave = _countForAutoSave += (int)Properties.Settings.Default.AutoSaveStep;

            try
            {
                var typeSaver = ((ItemComboBox<TypeSaver>)AutoSaveComboBox.SelectedItem).Value;
                var resultSaver = GetSaverFabricMethod(typeSaver);
                string rootPath = $"{Directory.GetCurrentDirectory()}\\resultParse.{resultSaver.FileFormat}";
                if (!File.Exists(rootPath))
                {
                    File.CreateText(rootPath);
                }
                Task.Run(() =>
                    resultSaver.Save(
                    _persons,
                    rootPath,
                    IsExportOnlyEmailcheckBox.Checked));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
