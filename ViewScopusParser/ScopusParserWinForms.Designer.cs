namespace ViewScopusParser
{
    partial class ScopusParserWinForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopusParserWinForms));
            this.ReturnedEmailDataGrid = new System.Windows.Forms.DataGridView();
            this.FIOColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URLTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartParseButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CurrentPagelabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MaxPagelabel = new System.Windows.Forms.Label();
            this.CountPagesLabel = new System.Windows.Forms.Label();
            this.PagesCounTextBox = new System.Windows.Forms.TextBox();
            this.ExportExcelButton = new System.Windows.Forms.Button();
            this.ProgressGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnedEmailDataGrid)).BeginInit();
            this.ProgressGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReturnedEmailDataGrid
            // 
            this.ReturnedEmailDataGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.ReturnedEmailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReturnedEmailDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FIOColumn,
            this.EmailColumn});
            this.ReturnedEmailDataGrid.Location = new System.Drawing.Point(12, 87);
            this.ReturnedEmailDataGrid.Name = "ReturnedEmailDataGrid";
            this.ReturnedEmailDataGrid.ReadOnly = true;
            this.ReturnedEmailDataGrid.Size = new System.Drawing.Size(426, 191);
            this.ReturnedEmailDataGrid.TabIndex = 0;
            // 
            // FIOColumn
            // 
            this.FIOColumn.HeaderText = "Фамилия\\Имя";
            this.FIOColumn.Name = "FIOColumn";
            this.FIOColumn.ReadOnly = true;
            this.FIOColumn.Width = 180;
            // 
            // EmailColumn
            // 
            this.EmailColumn.HeaderText = "Email";
            this.EmailColumn.Name = "EmailColumn";
            this.EmailColumn.ReadOnly = true;
            this.EmailColumn.Width = 180;
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
            // StartParseButton
            // 
            this.StartParseButton.Location = new System.Drawing.Point(363, 19);
            this.StartParseButton.Name = "StartParseButton";
            this.StartParseButton.Size = new System.Drawing.Size(75, 23);
            this.StartParseButton.TabIndex = 3;
            this.StartParseButton.Text = "Начать";
            this.StartParseButton.UseVisualStyleBackColor = true;
            this.StartParseButton.Click += new System.EventHandler(this.StartParseButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 37);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(426, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 4;
            // 
            // CurrentPagelabel
            // 
            this.CurrentPagelabel.AutoSize = true;
            this.CurrentPagelabel.Location = new System.Drawing.Point(69, 18);
            this.CurrentPagelabel.Name = "CurrentPagelabel";
            this.CurrentPagelabel.Size = new System.Drawing.Size(13, 13);
            this.CurrentPagelabel.TabIndex = 5;
            this.CurrentPagelabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "статья из";
            // 
            // MaxPagelabel
            // 
            this.MaxPagelabel.AutoSize = true;
            this.MaxPagelabel.Location = new System.Drawing.Point(132, 18);
            this.MaxPagelabel.Name = "MaxPagelabel";
            this.MaxPagelabel.Size = new System.Drawing.Size(13, 13);
            this.MaxPagelabel.TabIndex = 7;
            this.MaxPagelabel.Text = "0";
            // 
            // CountPagesLabel
            // 
            this.CountPagesLabel.AutoSize = true;
            this.CountPagesLabel.Location = new System.Drawing.Point(12, 51);
            this.CountPagesLabel.Name = "CountPagesLabel";
            this.CountPagesLabel.Size = new System.Drawing.Size(135, 13);
            this.CountPagesLabel.TabIndex = 8;
            this.CountPagesLabel.Text = "Кол-во статей для парса:";
            // 
            // PagesCounTextBox
            // 
            this.PagesCounTextBox.Location = new System.Drawing.Point(150, 48);
            this.PagesCounTextBox.Name = "PagesCounTextBox";
            this.PagesCounTextBox.Size = new System.Drawing.Size(49, 20);
            this.PagesCounTextBox.TabIndex = 9;
            this.PagesCounTextBox.Text = "1";
            this.PagesCounTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PagesCounTextBox_KeyPress);
            // 
            // ExportExcelButton
            // 
            this.ExportExcelButton.Location = new System.Drawing.Point(337, 51);
            this.ExportExcelButton.Name = "ExportExcelButton";
            this.ExportExcelButton.Size = new System.Drawing.Size(101, 23);
            this.ExportExcelButton.TabIndex = 10;
            this.ExportExcelButton.Text = "Экспорт в Excel";
            this.ExportExcelButton.UseVisualStyleBackColor = true;
            this.ExportExcelButton.Click += new System.EventHandler(this.ExportExcelbutton_Click);
            // 
            // ProgressGroupBox
            // 
            this.ProgressGroupBox.Controls.Add(this.label2);
            this.ProgressGroupBox.Controls.Add(this.progressBar1);
            this.ProgressGroupBox.Controls.Add(this.CurrentPagelabel);
            this.ProgressGroupBox.Controls.Add(this.label3);
            this.ProgressGroupBox.Controls.Add(this.MaxPagelabel);
            this.ProgressGroupBox.Location = new System.Drawing.Point(12, 277);
            this.ProgressGroupBox.Name = "ProgressGroupBox";
            this.ProgressGroupBox.Size = new System.Drawing.Size(425, 66);
            this.ProgressGroupBox.TabIndex = 11;
            this.ProgressGroupBox.TabStop = false;
            this.ProgressGroupBox.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Парсится";
            // 
            // ScopusParserWinForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 355);
            this.Controls.Add(this.ProgressGroupBox);
            this.Controls.Add(this.ExportExcelButton);
            this.Controls.Add(this.PagesCounTextBox);
            this.Controls.Add(this.CountPagesLabel);
            this.Controls.Add(this.StartParseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.URLTextBox);
            this.Controls.Add(this.ReturnedEmailDataGrid);
            this.Name = "ScopusParserWinForms";
            this.Text = "Scopus Parser";
            ((System.ComponentModel.ISupportInitialize)(this.ReturnedEmailDataGrid)).EndInit();
            this.ProgressGroupBox.ResumeLayout(false);
            this.ProgressGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ReturnedEmailDataGrid;
        private System.Windows.Forms.TextBox URLTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartParseButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label CurrentPagelabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label MaxPagelabel;
        private System.Windows.Forms.Label CountPagesLabel;
        private System.Windows.Forms.TextBox PagesCounTextBox;
        private System.Windows.Forms.Button ExportExcelButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIOColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailColumn;
        private System.Windows.Forms.GroupBox ProgressGroupBox;
        private System.Windows.Forms.Label label2;
    }
}

