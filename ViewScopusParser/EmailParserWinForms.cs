﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailParserView.LogSaver;
using OpenQA.Selenium;
using ParserModel;
using ParserModel.ParseWithSelenium;
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
            SeedAllCombobox();

        }

        private void StartParseButton_Click(object sender, EventArgs e)
        {
            ProgressGroupBox.Visible = true;
            StartParseButton.Enabled = false;
            var organization = (TypeOrganization)TypeOrganizationCombobox.SelectedItem;
            var typeParser = (ParserType)TypeSiteCombobox.SelectedItem;
            var browser = (SupportedSeleniumBrowsers)SelectBrowserComboBox.SelectedItem;
            var settings = new ParserSettings(browser, organization);
            Task.Run(() => HandleErrorsAndBeginParsing(typeParser, settings));
        }

        private void HandleErrorsAndBeginParsing(ParserType parserType, ParserSettings settings)
        {
            IParse parser = null;
            try
            {
                parser = parserType.GetParserFabricMethod(settings);
                StartParsing(parser);
            }
            catch (DriverServiceNotFoundException ex)
            {
                ShowErrorToUser($"При запуске парсера возникла проблема.У вас отсутствует нужный браузер{ex.Message}");
            }
            catch (NoSuchElementException ex)
            {
                ShowErrorToUser($@"Проверьте Ваше интернет-соединение. Возможно, вы забыли подключить VPN или указали неверную ссылку.{ex.Message}");
            }
            catch (WebDriverException ex)
            {
                ShowErrorToUser($@"При запуске парсера возникла проблема. Возможно,вы указали неверный URL.{ex.Message}");
            }
            catch (Exception ex)
            {
                ShowErrorToUser($@"{ex.Message}");
            }
            finally
            {
                parser?.Dispose();
                ChangeControlInMainUi(ProgressGroupBox, () => ProgressGroupBox.Visible = false);
                ChangeControlInMainUi(StartParseButton, () => StartParseButton.Enabled = true);
            }

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

        private void SeedAllCombobox()
        {
            SeedComboboxBySpecificEnum(TypeOrganizationCombobox, typeof(TypeOrganization));
            SeedComboboxBySpecificEnum(AutoSaveComboBox, typeof(TypeSaver));
            SeedComboboxBySpecificEnum(TypeSiteCombobox, typeof(ParserType));
            SeedComboboxBySpecificEnum(SelectBrowserComboBox, typeof(SupportedSeleniumBrowsers));
        }

        private void SeedComboboxBySpecificEnum(ComboBox combobox, Type type)
        {
            combobox.DataSource = Enum.GetValues(type);
            combobox.SelectedIndex = 0;
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
                Filter = @"Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*",
                DefaultExt = "*.txt",
                FileName = "resultsFromParse",
                Title = @"Укажите директорию и имя файла для сохранения"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            var saver = TypeSaver.Txt.GetSaverFabricMethod(ReturnedEmailDataGrid);
            Task.Run(() => saver.Save(_persons, saveFileDialog.FileName, IsExportOnlyEmailcheckBox.Checked));
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = @"MS Excel documents (*.xlsx)|*.xlsx",
                DefaultExt = "*.xlsx",
                FileName = "resultsFromParse",
                Title = @"Укажите директорию и имя файла для сохранения"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            var saver = TypeSaver.Excel.GetSaverFabricMethod(ReturnedEmailDataGrid);
            Task.Run(() => saver.Save(_persons, saveFileDialog.FileName, IsExportOnlyEmailcheckBox.Checked));
        }

        private void ReturnedEmailDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (progressBar1.Value - _countForAutoSave < Properties.Settings.Default.AutoSaveStep)
                return;
            _countForAutoSave = _countForAutoSave += (int)Properties.Settings.Default.AutoSaveStep;
            try
            {
                var typeSaver = (TypeSaver)AutoSaveComboBox.SelectedItem;
                var resultSaver = typeSaver.GetSaverFabricMethod(ReturnedEmailDataGrid);
                var rootPath = $"{Directory.GetCurrentDirectory()}\\Backup\\resultParse.{resultSaver.FileFormat}";

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

        private void ClearPersonsButton_Click(object sender, EventArgs e)
        {
            _countForAutoSave = 0;
            ReturnedEmailDataGrid.Rows.Clear();
            _persons.Clear();
        }
    }
}
