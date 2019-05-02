using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailParserView.LogSaver;
using OpenQA.Selenium;
using ParserModel;
using ParserModel.ParseWithSelenium;
using static System.Threading.Tasks.Task;
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
            SeedComboboxBySpecificEnum(TypeOrganizationCombobox, typeof(TypeOrganization));
            SeedComboboxBySpecificEnum(AutoSaveComboBox, typeof(TypeSaver));
            SeedComboboxBySpecificEnum(TypeSiteCombobox, typeof(ParserType));
            SeedComboboxBySpecificEnum(SelectBrowserComboBox, typeof(SupportedSeleniumBrowsers));
        }

        public void SeedComboboxBySpecificEnum(ComboBox combobox, Type type)
        {
            combobox.DataSource = Enum.GetValues(type);
            combobox.SelectedIndex = 0;
        }

        private async void StartParseButton_Click(object sender, EventArgs e)
        {
            ProgressGroupBox.Visible = true;
            StartParseButton.Enabled = false;
            var organization = (TypeOrganization)TypeOrganizationCombobox.SelectedItem;
            var typeParser = (ParserType)TypeSiteCombobox.SelectedItem;
            var browser = (SupportedSeleniumBrowsers)SelectBrowserComboBox.SelectedItem;
            var settings = new ParserSettings(browser, organization);
            IParse parser = null;
            try
            {
                parser = typeParser.GetParserFabricMethod(settings);
                await Run(() => StartParsing(parser));
            }
            catch (DriverServiceNotFoundException ex)
            {
                await ShowErrorToUser($"При запуске парсера возникла проблема.У вас отсутствует нужный браузер{ex.Message}");
            }
            catch (NoSuchElementException ex)
            {
                await ShowErrorToUser($@"Проверьте Ваше интернет-соединение. Возможно, вы забыли подключить VPN или указали неверную ссылку.{ex.Message}");
            }
            catch (WebDriverException ex)
            {
                await ShowErrorToUser($@"При запуске парсера возникла проблема. Возможно,вы указали неверный URL.{ex.Message}");
            }
            catch (Exception ex)
            {
                await ShowErrorToUser($@"{ex.Message}");
            }
            finally
            {
                parser?.Dispose();
            }
            ProgressGroupBox.Visible = false;
            StartParseButton.Enabled = true;
        }

        private async Task StartParsing(IParse parser)
        {
            var firstUrl = URLTextBox.Text;
            var nextUrl = firstUrl;
            var countArticles = Convert.ToInt32(PagesCounTextBox.Text);
            ChangeControlInMainUi(progressBar1, () => progressBar1.Value = 0);
            ChangeControlInMainUi(progressBar1, () => progressBar1.Maximum = countArticles);

            for (; progressBar1.Value < countArticles && nextUrl != null; ChangeControlInMainUi(progressBar1, () => progressBar1.Value += 1))
            {
                ChangeControlInMainUi(PersentLabel, () =>
                 PersentLabel.Text = $@"Парсится {progressBar1.Value} статья из {progressBar1.Maximum}({(progressBar1.Value) * 100 / progressBar1.Maximum}%)");
                var result = await parser.ParseSpecificArticle(nextUrl);
                ChangeControlInMainUi(URLTextBox, () => URLTextBox.Text = nextUrl);
                if (result != null)
                {
                    _persons.AddRange(result);
                    foreach (var item in result)
                    {
                        ChangeControlInMainUi(ReturnedEmailDataGrid,
                            () => ReturnedEmailDataGrid.Rows.Add(item.Fio, item.Email));
                    }
                }
                nextUrl = await parser.GetNextArticle(nextUrl);
            }
        }

        private void ChangeControlInMainUi(Control control, Action action)
        {
            control.Invoke(action);
        }

        private async Task ShowErrorToUser(string text)
        {
            await Run(() => MessageBox.Show(text, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning));
        }

        private void PagesCounTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar != '.' || ((TextBox)sender).Text.IndexOf('.') <= -1) return;
            e.Handled = true;
        }

        private void txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToUserFilePath(TypeSaver.Txt);
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToUserFilePath(TypeSaver.Excel);
        }

        private async void SaveToUserFilePath(TypeSaver typeSaver)
        {
            var saver = typeSaver.GetSaverFabricMethod(ReturnedEmailDataGrid);
            var saveFileDialog = new SaveFileDialog
            {
                Filter = saver.FileFilter,
                DefaultExt = $"*.{saver.FileFormat}",
                FileName = "resultsFromParse",
                Title = @"Укажите директорию и имя файла для сохранения"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                await saver.Save(_persons, saveFileDialog.FileName, IsExportOnlyEmailcheckBox.Checked); return;
        }

        private void ReturnedEmailDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!IsDataReadyForAutoSave()) return;
            _countForAutoSave = _countForAutoSave += (int)Properties.Settings.Default.AutoSaveStep;
            BackupCurrentResults();
        }

        private async void BackupCurrentResults()
        {
            var typeSaver = (TypeSaver)AutoSaveComboBox.SelectedItem;
            var resultSaver = typeSaver.GetSaverFabricMethod(ReturnedEmailDataGrid);
            var rootPath = $"{Directory.GetCurrentDirectory()}\\Backup\\resultParse.{resultSaver.FileFormat}";
            await resultSaver.Save(_persons, rootPath, IsExportOnlyEmailcheckBox.Checked);
        }

        private bool IsDataReadyForAutoSave()
        {
            return progressBar1.Value - _countForAutoSave > Properties.Settings.Default.AutoSaveStep;
        }

        private void ClearPersonsButton_Click(object sender, EventArgs e)
        {
            _countForAutoSave = 0;
            ReturnedEmailDataGrid.Rows.Clear();
            _persons.Clear();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}