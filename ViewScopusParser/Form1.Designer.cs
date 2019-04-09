namespace ViewScopusParser
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.URLTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StarParsebutton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CurrentPagelabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MaxPagelabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PagesCounttextBox = new System.Windows.Forms.TextBox();
            this.ExportExcelbutton = new System.Windows.Forms.Button();
            this.FIOColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FIOColumn,
            this.EmailColumn});
            this.dataGridView1.Location = new System.Drawing.Point(12, 87);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(426, 191);
            this.dataGridView1.TabIndex = 0;
            // 
            // URLTextBox
            // 
            this.URLTextBox.Location = new System.Drawing.Point(43, 22);
            this.URLTextBox.Name = "URLTextBox";
            this.URLTextBox.Size = new System.Drawing.Size(270, 20);
            this.URLTextBox.TabIndex = 1;
            this.URLTextBox.Text = resources.GetString("URLTextBox.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL";
            // 
            // StarParsebutton
            // 
            this.StarParsebutton.Location = new System.Drawing.Point(363, 19);
            this.StarParsebutton.Name = "StarParsebutton";
            this.StarParsebutton.Size = new System.Drawing.Size(75, 23);
            this.StarParsebutton.TabIndex = 3;
            this.StarParsebutton.Text = "Начать";
            this.StarParsebutton.UseVisualStyleBackColor = true;
            this.StarParsebutton.Click += new System.EventHandler(this.StarParsebutton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 313);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(426, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // CurrentPagelabel
            // 
            this.CurrentPagelabel.AutoSize = true;
            this.CurrentPagelabel.Location = new System.Drawing.Point(12, 294);
            this.CurrentPagelabel.Name = "CurrentPagelabel";
            this.CurrentPagelabel.Size = new System.Drawing.Size(13, 13);
            this.CurrentPagelabel.TabIndex = 5;
            this.CurrentPagelabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "из";
            // 
            // MaxPagelabel
            // 
            this.MaxPagelabel.AutoSize = true;
            this.MaxPagelabel.Location = new System.Drawing.Point(56, 294);
            this.MaxPagelabel.Name = "MaxPagelabel";
            this.MaxPagelabel.Size = new System.Drawing.Size(13, 13);
            this.MaxPagelabel.TabIndex = 7;
            this.MaxPagelabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Кол-во статей для парса:";
            // 
            // PagesCounttextBox
            // 
            this.PagesCounttextBox.Location = new System.Drawing.Point(150, 48);
            this.PagesCounttextBox.Name = "PagesCounttextBox";
            this.PagesCounttextBox.Size = new System.Drawing.Size(49, 20);
            this.PagesCounttextBox.TabIndex = 9;
            // 
            // ExportExcelbutton
            // 
            this.ExportExcelbutton.Location = new System.Drawing.Point(337, 51);
            this.ExportExcelbutton.Name = "ExportExcelbutton";
            this.ExportExcelbutton.Size = new System.Drawing.Size(101, 23);
            this.ExportExcelbutton.TabIndex = 10;
            this.ExportExcelbutton.Text = "Экспорт в Excel";
            this.ExportExcelbutton.UseVisualStyleBackColor = true;
            this.ExportExcelbutton.Click += new System.EventHandler(this.ExportExcelbutton_Click);
            // 
            // FIOColumn
            // 
            this.FIOColumn.HeaderText = "Фамилия\\Имя";
            this.FIOColumn.Name = "FIOColumn";
            this.FIOColumn.Width = 180;
            // 
            // EmailColumn
            // 
            this.EmailColumn.HeaderText = "Email";
            this.EmailColumn.Name = "EmailColumn";
            this.EmailColumn.Width = 180;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 355);
            this.Controls.Add(this.ExportExcelbutton);
            this.Controls.Add(this.PagesCounttextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MaxPagelabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CurrentPagelabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.StarParsebutton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.URLTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Scopus Parser";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox URLTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StarParsebutton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label CurrentPagelabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label MaxPagelabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PagesCounttextBox;
        private System.Windows.Forms.Button ExportExcelbutton;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIOColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailColumn;
    }
}

