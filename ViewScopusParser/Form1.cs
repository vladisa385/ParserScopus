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
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            IParse parser = new ScopusParser(driver);
            var emailWithAuthor = new List<ResultEmail>();
            string firstUrl = URLTextBox.Text;
            var nextUrl = firstUrl;
            var countArticles = Convert.ToInt32(PagesCounttextBox.Text);
            var currentArticle = 1;
            
            progressBar1.Maximum = countArticles;
            progressBar1.Value = 1;

            for (int i = 0; i < countArticles - 1 && nextUrl != null; i++)
            {
                List<ResultEmail> result;
                try
                {
                    result = parser.ParseSpecificArticle(nextUrl);
                }
                catch (Exception)
                {
                    MessageBox.Show("Подключите VPN");
                    parser.Dispose();
                    break;
                }
                
                if (countArticles > parser.GetCountArticle(firstUrl))
                {
                    countArticles = parser.GetCountArticle(firstUrl);
                    progressBar1.Maximum = countArticles ;
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
            CurrentPagelabel.Visible = false;


            parser.Dispose();
        }
    }
}
