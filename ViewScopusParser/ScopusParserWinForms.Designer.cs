﻿namespace ViewScopusParser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopusParserWinForms));
            this.ReturnedEmailDataGrid = new System.Windows.Forms.DataGridView();
            this.FIOColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URLTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartParseButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.CountPagesLabel = new System.Windows.Forms.Label();
            this.PagesCounTextBox = new System.Windows.Forms.TextBox();
            this.ProgressGroupBox = new System.Windows.Forms.GroupBox();
            this.PersentLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.Задержка = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.IsExportOnlyEmailcheckBox = new System.Windows.Forms.CheckBox();
            this.AutoSaveComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnedEmailDataGrid)).BeginInit();
            this.ProgressGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // ReturnedEmailDataGrid
            // 
            this.ReturnedEmailDataGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.ReturnedEmailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReturnedEmailDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FIOColumn,
            this.EmailColumn});
            this.ReturnedEmailDataGrid.Location = new System.Drawing.Point(11, 140);
            this.ReturnedEmailDataGrid.Name = "ReturnedEmailDataGrid";
            this.ReturnedEmailDataGrid.ReadOnly = true;
            this.ReturnedEmailDataGrid.Size = new System.Drawing.Size(426, 191);
            this.ReturnedEmailDataGrid.TabIndex = 0;
            this.ReturnedEmailDataGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ReturnedEmailDataGrid_RowsAdded);
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
            this.URLTextBox.Size = new System.Drawing.Size(288, 20);
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
            this.StartParseButton.Location = new System.Drawing.Point(337, 20);
            this.StartParseButton.Name = "StartParseButton";
            this.StartParseButton.Size = new System.Drawing.Size(100, 23);
            this.StartParseButton.TabIndex = 3;
            this.StartParseButton.Text = "Начать";
            this.StartParseButton.UseVisualStyleBackColor = true;
            this.StartParseButton.Click += new System.EventHandler(this.StartParseButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 37);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(413, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 4;
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
            this.PagesCounTextBox.Location = new System.Drawing.Point(264, 55);
            this.PagesCounTextBox.Name = "PagesCounTextBox";
            this.PagesCounTextBox.Size = new System.Drawing.Size(61, 20);
            this.PagesCounTextBox.TabIndex = 9;
            this.PagesCounTextBox.Text = "1";
            this.PagesCounTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PagesCounTextBox_KeyPress);
            // 
            // ProgressGroupBox
            // 
            this.ProgressGroupBox.Controls.Add(this.PersentLabel);
            this.ProgressGroupBox.Controls.Add(this.progressBar1);
            this.ProgressGroupBox.Location = new System.Drawing.Point(12, 330);
            this.ProgressGroupBox.Name = "ProgressGroupBox";
            this.ProgressGroupBox.Size = new System.Drawing.Size(425, 66);
            this.ProgressGroupBox.TabIndex = 11;
            this.ProgressGroupBox.TabStop = false;
            this.ProgressGroupBox.Visible = false;
            // 
            // PersentLabel
            // 
            this.PersentLabel.AutoSize = true;
            this.PersentLabel.Location = new System.Drawing.Point(159, 21);
            this.PersentLabel.Name = "PersentLabel";
            this.PersentLabel.Size = new System.Drawing.Size(21, 13);
            this.PersentLabel.TabIndex = 9;
            this.PersentLabel.Text = "0%";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(449, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtToolStripMenuItem,
            this.excelToolStripMenuItem});
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.сохранитьToolStripMenuItem.Text = "Экспорт";
            // 
            // txtToolStripMenuItem
            // 
            this.txtToolStripMenuItem.Name = "txtToolStripMenuItem";
            this.txtToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.txtToolStripMenuItem.Text = "Txt";
            this.txtToolStripMenuItem.Click += new System.EventHandler(this.txtToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(264, 81);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(61, 20);
            this.delayTextBox.TabIndex = 16;
            this.delayTextBox.Text = "0";
            this.delayTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PagesCounTextBox_KeyPress);
            // 
            // Задержка
            // 
            this.Задержка.AutoSize = true;
            this.Задержка.Location = new System.Drawing.Point(15, 81);
            this.Задержка.Name = "Задержка";
            this.Задержка.Size = new System.Drawing.Size(243, 13);
            this.Задержка.TabIndex = 17;
            this.Задержка.Text = "Количество секунд задержки перед запросом";
            // 
            // IsExportOnlyEmailcheckBox
            // 
            this.IsExportOnlyEmailcheckBox.AutoSize = true;
            this.IsExportOnlyEmailcheckBox.Location = new System.Drawing.Point(18, 107);
            this.IsExportOnlyEmailcheckBox.Name = "IsExportOnlyEmailcheckBox";
            this.IsExportOnlyEmailcheckBox.Size = new System.Drawing.Size(174, 17);
            this.IsExportOnlyEmailcheckBox.TabIndex = 13;
            this.IsExportOnlyEmailcheckBox.Text = "Экспортировать только email";
            this.IsExportOnlyEmailcheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoSaveComboBox
            // 
            this.AutoSaveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AutoSaveComboBox.FormattingEnabled = true;
            this.AutoSaveComboBox.Items.AddRange(new object[] {
            "Автосохранения нет",
            "Автосохранение в txt",
            "Автосохранение в excel"});
            this.AutoSaveComboBox.Location = new System.Drawing.Point(264, 113);
            this.AutoSaveComboBox.Name = "AutoSaveComboBox";
            this.AutoSaveComboBox.Size = new System.Drawing.Size(173, 21);
            this.AutoSaveComboBox.TabIndex = 19;
            // 
            // ScopusParserWinForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 403);
            this.Controls.Add(this.AutoSaveComboBox);
            this.Controls.Add(this.Задержка);
            this.Controls.Add(this.delayTextBox);
            this.Controls.Add(this.IsExportOnlyEmailcheckBox);
            this.Controls.Add(this.ProgressGroupBox);
            this.Controls.Add(this.PagesCounTextBox);
            this.Controls.Add(this.CountPagesLabel);
            this.Controls.Add(this.StartParseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.URLTextBox);
            this.Controls.Add(this.ReturnedEmailDataGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScopusParserWinForms";
            this.Text = "Scopus Parser";
            ((System.ComponentModel.ISupportInitialize)(this.ReturnedEmailDataGrid)).EndInit();
            this.ProgressGroupBox.ResumeLayout(false);
            this.ProgressGroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ReturnedEmailDataGrid;
        private System.Windows.Forms.TextBox URLTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartParseButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label CountPagesLabel;
        private System.Windows.Forms.TextBox PagesCounTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIOColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailColumn;
        private System.Windows.Forms.GroupBox ProgressGroupBox;
        private System.Windows.Forms.Label PersentLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem txtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.Label Задержка;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.CheckBox IsExportOnlyEmailcheckBox;
        private System.Windows.Forms.ComboBox AutoSaveComboBox;
    }
}

