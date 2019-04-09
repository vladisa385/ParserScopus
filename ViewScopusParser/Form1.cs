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
namespace ViewScopusParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void StarParsebutton_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            try
            {
                IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                IParse parser = new ScopusParser(driver);
                var emailWithAuthor = new List<ResultEmail>();

                string firstUrl = URLTextBox.Text;
                var nextUrl = firstUrl;
                var countArticles = Convert.ToInt32(PagesCounttextBox.Text);
                var currentArticle = 1;

                progressBar1.Maximum = countArticles + 1;
                progressBar1.Value = 1;

                for (int i = 0; i < countArticles && nextUrl != null; i++)
                {
                    List<ResultEmail> result;
                    try
                    {
                        result = parser.ParseSpecificArticle(nextUrl);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Проверьте Ваше интернет-соединение. Возможно, вы забыли подключить VPN.", "Ошибка соединения!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
                        parser.Dispose();
                        break;
                    }

                    if (countArticles > parser.GetCountArticle(firstUrl))
                    {
                        countArticles = parser.GetCountArticle(firstUrl);
                        progressBar1.Maximum = countArticles;
                    }
                    foreach (var item in result)
                    {
                        dataGridView1.Rows.Add(item.Fio, item.Email);
                    }
                    nextUrl = parser.GetNextArticle(nextUrl);
                    if (countArticles == 0)
                        countArticles = parser.GetCountArticle(firstUrl);
                    CurrentPagelabel.Text = currentArticle.ToString();
                    MaxPagelabel.Text = countArticles.ToString();
                    progressBar1.Value++;
                    if (result.Count != 0)
                        emailWithAuthor.AddRange(result);
                    currentArticle += 1;
                }

                progressBar1.Visible = false;
                MaxPagelabel.Visible = false;
                label3.Visible = false;
                CurrentPagelabel.Visible = false;

                parser.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("При запуске парсера возникла проблема. Попробуйте заново запустить программу.", "Ошибка запуска!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
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

                    for (int i = 1; i < dataGridView1.RowCount + 1; i++)
                    {
                        for (int j = 1; j < dataGridView1.ColumnCount + 1; j++)
                        {
                            worksheet.Rows[i].Columns[j] = dataGridView1.Rows[i - 1].Cells[j - 1].Value;
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
    }
}
