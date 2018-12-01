namespace EU4SaveEditor
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
            this.labelChooseCountry = new System.Windows.Forms.Label();
            this.labelLoadedFile = new System.Windows.Forms.Label();
            this.ListBoxCountries = new System.Windows.Forms.ListBox();
            this.ListBoxProvinces = new System.Windows.Forms.ListBox();
            this.labelChooseProvince = new System.Windows.Forms.Label();
            this.labelCountriesCount = new System.Windows.Forms.Label();
            this.labelProvincesCount = new System.Windows.Forms.Label();
            this.groupBoxProvinceProsperity = new System.Windows.Forms.GroupBox();
            this.textBoxDip = new System.Windows.Forms.TextBox();
            this.textBoxAdm = new System.Windows.Forms.TextBox();
            this.textBoxMil = new System.Windows.Forms.TextBox();
            this.labelDiplomacy = new System.Windows.Forms.Label();
            this.labelMilitary = new System.Windows.Forms.Label();
            this.labelAdministrative = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxProvinceProsperity.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelChooseCountry
            // 
            this.labelChooseCountry.AutoSize = true;
            this.labelChooseCountry.Location = new System.Drawing.Point(12, 74);
            this.labelChooseCountry.Name = "labelChooseCountry";
            this.labelChooseCountry.Size = new System.Drawing.Size(84, 13);
            this.labelChooseCountry.TabIndex = 5;
            this.labelChooseCountry.Text = "Choose country:";
            // 
            // labelLoadedFile
            // 
            this.labelLoadedFile.AutoSize = true;
            this.labelLoadedFile.Location = new System.Drawing.Point(223, 17);
            this.labelLoadedFile.Name = "labelLoadedFile";
            this.labelLoadedFile.Size = new System.Drawing.Size(0, 13);
            this.labelLoadedFile.TabIndex = 7;
            // 
            // ListBoxCountries
            // 
            this.ListBoxCountries.FormattingEnabled = true;
            this.ListBoxCountries.Location = new System.Drawing.Point(12, 90);
            this.ListBoxCountries.Name = "ListBoxCountries";
            this.ListBoxCountries.Size = new System.Drawing.Size(205, 342);
            this.ListBoxCountries.Sorted = true;
            this.ListBoxCountries.TabIndex = 8;
            this.ListBoxCountries.SelectedIndexChanged += new System.EventHandler(this.ListBoxCountries_SelectedIndexChanged);
            // 
            // ListBoxProvinces
            // 
            this.ListBoxProvinces.FormattingEnabled = true;
            this.ListBoxProvinces.Location = new System.Drawing.Point(226, 90);
            this.ListBoxProvinces.Name = "ListBoxProvinces";
            this.ListBoxProvinces.Size = new System.Drawing.Size(205, 342);
            this.ListBoxProvinces.Sorted = true;
            this.ListBoxProvinces.TabIndex = 10;
            this.ListBoxProvinces.SelectedIndexChanged += new System.EventHandler(this.ListBoxProvinces_SelectedIndexChanged);
            // 
            // labelChooseProvince
            // 
            this.labelChooseProvince.AutoSize = true;
            this.labelChooseProvince.Location = new System.Drawing.Point(226, 74);
            this.labelChooseProvince.Name = "labelChooseProvince";
            this.labelChooseProvince.Size = new System.Drawing.Size(90, 13);
            this.labelChooseProvince.TabIndex = 9;
            this.labelChooseProvince.Text = "Choose province:";
            // 
            // labelCountriesCount
            // 
            this.labelCountriesCount.AutoSize = true;
            this.labelCountriesCount.Location = new System.Drawing.Point(141, 74);
            this.labelCountriesCount.Name = "labelCountriesCount";
            this.labelCountriesCount.Size = new System.Drawing.Size(76, 13);
            this.labelCountriesCount.TabIndex = 11;
            this.labelCountriesCount.Text = "CountiesCount";
            this.labelCountriesCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelProvincesCount
            // 
            this.labelProvincesCount.AutoSize = true;
            this.labelProvincesCount.Location = new System.Drawing.Point(349, 74);
            this.labelProvincesCount.Name = "labelProvincesCount";
            this.labelProvincesCount.Size = new System.Drawing.Size(82, 13);
            this.labelProvincesCount.TabIndex = 12;
            this.labelProvincesCount.Text = "ProvincesCount";
            this.labelProvincesCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxProvinceProsperity
            // 
            this.groupBoxProvinceProsperity.Controls.Add(this.textBoxDip);
            this.groupBoxProvinceProsperity.Controls.Add(this.textBoxAdm);
            this.groupBoxProvinceProsperity.Controls.Add(this.textBoxMil);
            this.groupBoxProvinceProsperity.Controls.Add(this.labelDiplomacy);
            this.groupBoxProvinceProsperity.Controls.Add(this.labelMilitary);
            this.groupBoxProvinceProsperity.Controls.Add(this.labelAdministrative);
            this.groupBoxProvinceProsperity.Location = new System.Drawing.Point(456, 74);
            this.groupBoxProvinceProsperity.Name = "groupBoxProvinceProsperity";
            this.groupBoxProvinceProsperity.Size = new System.Drawing.Size(177, 140);
            this.groupBoxProvinceProsperity.TabIndex = 14;
            this.groupBoxProvinceProsperity.TabStop = false;
            this.groupBoxProvinceProsperity.Text = "Province prosperity";
            // 
            // textBoxDip
            // 
            this.textBoxDip.Location = new System.Drawing.Point(87, 63);
            this.textBoxDip.Name = "textBoxDip";
            this.textBoxDip.Size = new System.Drawing.Size(75, 20);
            this.textBoxDip.TabIndex = 20;
            this.textBoxDip.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxDip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // textBoxAdm
            // 
            this.textBoxAdm.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxAdm.Location = new System.Drawing.Point(87, 19);
            this.textBoxAdm.Name = "textBoxAdm";
            this.textBoxAdm.Size = new System.Drawing.Size(75, 20);
            this.textBoxAdm.TabIndex = 19;
            this.textBoxAdm.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxAdm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // textBoxMil
            // 
            this.textBoxMil.Location = new System.Drawing.Point(87, 108);
            this.textBoxMil.Name = "textBoxMil";
            this.textBoxMil.Size = new System.Drawing.Size(75, 20);
            this.textBoxMil.TabIndex = 18;
            this.textBoxMil.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxMil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // labelDiplomacy
            // 
            this.labelDiplomacy.Location = new System.Drawing.Point(5, 60);
            this.labelDiplomacy.Name = "labelDiplomacy";
            this.labelDiplomacy.Size = new System.Drawing.Size(75, 25);
            this.labelDiplomacy.TabIndex = 17;
            this.labelDiplomacy.Text = "Base\r\nProduction";
            this.labelDiplomacy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMilitary
            // 
            this.labelMilitary.Location = new System.Drawing.Point(5, 105);
            this.labelMilitary.Name = "labelMilitary";
            this.labelMilitary.Size = new System.Drawing.Size(75, 25);
            this.labelMilitary.TabIndex = 16;
            this.labelMilitary.Text = "Base\r\nManpower";
            this.labelMilitary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAdministrative
            // 
            this.labelAdministrative.Location = new System.Drawing.Point(5, 15);
            this.labelAdministrative.Name = "labelAdministrative";
            this.labelAdministrative.Size = new System.Drawing.Size(75, 25);
            this.labelAdministrative.TabIndex = 15;
            this.labelAdministrative.Text = "Base\r\nTax";
            this.labelAdministrative.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.openFileToolStripMenuItem.Text = "Open file";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFile_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.saveFileToolStripMenuItem.Text = "Save file";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFile_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 446);
            this.Controls.Add(this.groupBoxProvinceProsperity);
            this.Controls.Add(this.labelProvincesCount);
            this.Controls.Add(this.labelCountriesCount);
            this.Controls.Add(this.ListBoxProvinces);
            this.Controls.Add(this.labelChooseProvince);
            this.Controls.Add(this.ListBoxCountries);
            this.Controls.Add(this.labelLoadedFile);
            this.Controls.Add(this.labelChooseCountry);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Europa Universalis 4 Save Editor";
            this.groupBoxProvinceProsperity.ResumeLayout(false);
            this.groupBoxProvinceProsperity.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelChooseCountry;
        private System.Windows.Forms.Label labelLoadedFile;
        private System.Windows.Forms.ListBox ListBoxCountries;
        private System.Windows.Forms.ListBox ListBoxProvinces;
        private System.Windows.Forms.Label labelChooseProvince;
        private System.Windows.Forms.Label labelCountriesCount;
        private System.Windows.Forms.Label labelProvincesCount;
        private System.Windows.Forms.GroupBox groupBoxProvinceProsperity;
        private System.Windows.Forms.TextBox textBoxDip;
        private System.Windows.Forms.TextBox textBoxAdm;
        private System.Windows.Forms.TextBox textBoxMil;
        private System.Windows.Forms.Label labelDiplomacy;
        private System.Windows.Forms.Label labelMilitary;
        private System.Windows.Forms.Label labelAdministrative;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

