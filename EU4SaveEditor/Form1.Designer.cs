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
            this.lbCountries = new System.Windows.Forms.ListBox();
            this.lbProvinces = new System.Windows.Forms.ListBox();
            this.labelChooseProvince = new System.Windows.Forms.Label();
            this.labelCountriesCount = new System.Windows.Forms.Label();
            this.labelProvincesCount = new System.Windows.Forms.Label();
            this.groupBoxProvinceProsperity = new System.Windows.Forms.GroupBox();
            this.tbDip = new System.Windows.Forms.TextBox();
            this.tbAdm = new System.Windows.Forms.TextBox();
            this.tbMil = new System.Windows.Forms.TextBox();
            this.labelDiplomacy = new System.Windows.Forms.Label();
            this.labelMilitary = new System.Windows.Forms.Label();
            this.labelAdministrative = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSearchCountry = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOrigCltr = new System.Windows.Forms.TextBox();
            this.tbCltr = new System.Windows.Forms.TextBox();
            this.labelOrigCltr = new System.Windows.Forms.Label();
            this.labelCltr = new System.Windows.Forms.Label();
            this.labelRlgn = new System.Windows.Forms.Label();
            this.labelOrigRlgn = new System.Windows.Forms.Label();
            this.tbRlgn = new System.Windows.Forms.TextBox();
            this.tbOrigRlgn = new System.Windows.Forms.TextBox();
            this.groupBoxProvinceProsperity.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelChooseCountry
            // 
            this.labelChooseCountry.AutoSize = true;
            this.labelChooseCountry.Location = new System.Drawing.Point(12, 101);
            this.labelChooseCountry.Name = "labelChooseCountry";
            this.labelChooseCountry.Size = new System.Drawing.Size(84, 13);
            this.labelChooseCountry.TabIndex = 5;
            this.labelChooseCountry.Text = "Choose country:";
            // 
            // labelLoadedFile
            // 
            this.labelLoadedFile.AutoSize = true;
            this.labelLoadedFile.Location = new System.Drawing.Point(12, 28);
            this.labelLoadedFile.Name = "labelLoadedFile";
            this.labelLoadedFile.Size = new System.Drawing.Size(45, 13);
            this.labelLoadedFile.TabIndex = 7;
            this.labelLoadedFile.Text = "FilePath";
            // 
            // lbCountries
            // 
            this.lbCountries.FormattingEnabled = true;
            this.lbCountries.Location = new System.Drawing.Point(12, 116);
            this.lbCountries.Name = "lbCountries";
            this.lbCountries.Size = new System.Drawing.Size(205, 316);
            this.lbCountries.Sorted = true;
            this.lbCountries.TabIndex = 8;
            this.lbCountries.SelectedIndexChanged += new System.EventHandler(this.ListBoxCountries_SelectedIndexChanged);
            // 
            // lbProvinces
            // 
            this.lbProvinces.FormattingEnabled = true;
            this.lbProvinces.Location = new System.Drawing.Point(226, 116);
            this.lbProvinces.Name = "lbProvinces";
            this.lbProvinces.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbProvinces.Size = new System.Drawing.Size(205, 316);
            this.lbProvinces.Sorted = true;
            this.lbProvinces.TabIndex = 10;
            this.lbProvinces.SelectedIndexChanged += new System.EventHandler(this.ListBoxProvinces_SelectedIndexChanged);
            // 
            // labelChooseProvince
            // 
            this.labelChooseProvince.AutoSize = true;
            this.labelChooseProvince.Location = new System.Drawing.Point(226, 101);
            this.labelChooseProvince.Name = "labelChooseProvince";
            this.labelChooseProvince.Size = new System.Drawing.Size(90, 13);
            this.labelChooseProvince.TabIndex = 9;
            this.labelChooseProvince.Text = "Choose province:";
            // 
            // labelCountriesCount
            // 
            this.labelCountriesCount.AutoSize = true;
            this.labelCountriesCount.Location = new System.Drawing.Point(141, 101);
            this.labelCountriesCount.Name = "labelCountriesCount";
            this.labelCountriesCount.Size = new System.Drawing.Size(76, 13);
            this.labelCountriesCount.TabIndex = 11;
            this.labelCountriesCount.Text = "CountiesCount";
            this.labelCountriesCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelProvincesCount
            // 
            this.labelProvincesCount.AutoSize = true;
            this.labelProvincesCount.Location = new System.Drawing.Point(349, 101);
            this.labelProvincesCount.Name = "labelProvincesCount";
            this.labelProvincesCount.Size = new System.Drawing.Size(82, 13);
            this.labelProvincesCount.TabIndex = 12;
            this.labelProvincesCount.Text = "ProvincesCount";
            this.labelProvincesCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxProvinceProsperity
            // 
            this.groupBoxProvinceProsperity.Controls.Add(this.tbDip);
            this.groupBoxProvinceProsperity.Controls.Add(this.tbAdm);
            this.groupBoxProvinceProsperity.Controls.Add(this.tbMil);
            this.groupBoxProvinceProsperity.Controls.Add(this.labelDiplomacy);
            this.groupBoxProvinceProsperity.Controls.Add(this.labelMilitary);
            this.groupBoxProvinceProsperity.Controls.Add(this.labelAdministrative);
            this.groupBoxProvinceProsperity.Location = new System.Drawing.Point(477, 101);
            this.groupBoxProvinceProsperity.Name = "groupBoxProvinceProsperity";
            this.groupBoxProvinceProsperity.Size = new System.Drawing.Size(177, 140);
            this.groupBoxProvinceProsperity.TabIndex = 14;
            this.groupBoxProvinceProsperity.TabStop = false;
            this.groupBoxProvinceProsperity.Text = "Province prosperity";
            // 
            // tbDip
            // 
            this.tbDip.Location = new System.Drawing.Point(87, 63);
            this.tbDip.Name = "tbDip";
            this.tbDip.Size = new System.Drawing.Size(75, 20);
            this.tbDip.TabIndex = 20;
            this.tbDip.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.tbDip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // tbAdm
            // 
            this.tbAdm.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbAdm.Location = new System.Drawing.Point(87, 19);
            this.tbAdm.Name = "tbAdm";
            this.tbAdm.Size = new System.Drawing.Size(75, 20);
            this.tbAdm.TabIndex = 19;
            this.tbAdm.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.tbAdm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // tbMil
            // 
            this.tbMil.Location = new System.Drawing.Point(87, 108);
            this.tbMil.Name = "tbMil";
            this.tbMil.Size = new System.Drawing.Size(75, 20);
            this.tbMil.TabIndex = 18;
            this.tbMil.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.tbMil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
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
            // tbSearchCountry
            // 
            this.tbSearchCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbSearchCountry.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSearchCountry.Location = new System.Drawing.Point(12, 60);
            this.tbSearchCountry.Name = "tbSearchCountry";
            this.tbSearchCountry.Size = new System.Drawing.Size(205, 20);
            this.tbSearchCountry.TabIndex = 20;
            this.tbSearchCountry.TextChanged += new System.EventHandler(this.tbSearchCountry_TextChanged);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.BackColor = System.Drawing.Color.White;
            this.btnSaveFile.Location = new System.Drawing.Point(564, 267);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFile.TabIndex = 21;
            this.btnSaveFile.Text = "Save file";
            this.btnSaveFile.UseVisualStyleBackColor = false;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Search country:";
            // 
            // tbOrigCltr
            // 
            this.tbOrigCltr.Location = new System.Drawing.Point(564, 329);
            this.tbOrigCltr.Name = "tbOrigCltr";
            this.tbOrigCltr.ReadOnly = true;
            this.tbOrigCltr.Size = new System.Drawing.Size(150, 20);
            this.tbOrigCltr.TabIndex = 21;
            // 
            // tbCltr
            // 
            this.tbCltr.Location = new System.Drawing.Point(564, 355);
            this.tbCltr.Name = "tbCltr";
            this.tbCltr.ReadOnly = true;
            this.tbCltr.Size = new System.Drawing.Size(150, 20);
            this.tbCltr.TabIndex = 23;
            // 
            // labelOrigCltr
            // 
            this.labelOrigCltr.Location = new System.Drawing.Point(477, 329);
            this.labelOrigCltr.Name = "labelOrigCltr";
            this.labelOrigCltr.Size = new System.Drawing.Size(80, 20);
            this.labelOrigCltr.TabIndex = 21;
            this.labelOrigCltr.Text = "Original Culture";
            this.labelOrigCltr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCltr
            // 
            this.labelCltr.Location = new System.Drawing.Point(482, 355);
            this.labelCltr.Name = "labelCltr";
            this.labelCltr.Size = new System.Drawing.Size(75, 20);
            this.labelCltr.TabIndex = 24;
            this.labelCltr.Text = "Culture";
            this.labelCltr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRlgn
            // 
            this.labelRlgn.Location = new System.Drawing.Point(482, 417);
            this.labelRlgn.Name = "labelRlgn";
            this.labelRlgn.Size = new System.Drawing.Size(75, 20);
            this.labelRlgn.TabIndex = 28;
            this.labelRlgn.Text = "Religion";
            this.labelRlgn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOrigRlgn
            // 
            this.labelOrigRlgn.Location = new System.Drawing.Point(468, 391);
            this.labelOrigRlgn.Name = "labelOrigRlgn";
            this.labelOrigRlgn.Size = new System.Drawing.Size(89, 20);
            this.labelOrigRlgn.TabIndex = 25;
            this.labelOrigRlgn.Text = "Original Religion";
            this.labelOrigRlgn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbRlgn
            // 
            this.tbRlgn.Location = new System.Drawing.Point(564, 417);
            this.tbRlgn.Name = "tbRlgn";
            this.tbRlgn.ReadOnly = true;
            this.tbRlgn.Size = new System.Drawing.Size(150, 20);
            this.tbRlgn.TabIndex = 27;
            // 
            // tbOrigRlgn
            // 
            this.tbOrigRlgn.Location = new System.Drawing.Point(564, 391);
            this.tbOrigRlgn.Name = "tbOrigRlgn";
            this.tbOrigRlgn.ReadOnly = true;
            this.tbOrigRlgn.Size = new System.Drawing.Size(150, 20);
            this.tbOrigRlgn.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(784, 446);
            this.Controls.Add(this.labelRlgn);
            this.Controls.Add(this.labelOrigRlgn);
            this.Controls.Add(this.tbRlgn);
            this.Controls.Add(this.tbOrigRlgn);
            this.Controls.Add(this.labelCltr);
            this.Controls.Add(this.labelOrigCltr);
            this.Controls.Add(this.tbCltr);
            this.Controls.Add(this.tbOrigCltr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.tbSearchCountry);
            this.Controls.Add(this.groupBoxProvinceProsperity);
            this.Controls.Add(this.labelProvincesCount);
            this.Controls.Add(this.labelCountriesCount);
            this.Controls.Add(this.lbProvinces);
            this.Controls.Add(this.labelChooseProvince);
            this.Controls.Add(this.lbCountries);
            this.Controls.Add(this.labelLoadedFile);
            this.Controls.Add(this.labelChooseCountry);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
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
        private System.Windows.Forms.ListBox lbCountries;
        private System.Windows.Forms.ListBox lbProvinces;
        private System.Windows.Forms.Label labelChooseProvince;
        private System.Windows.Forms.Label labelCountriesCount;
        private System.Windows.Forms.Label labelProvincesCount;
        private System.Windows.Forms.GroupBox groupBoxProvinceProsperity;
        private System.Windows.Forms.TextBox tbDip;
        private System.Windows.Forms.TextBox tbAdm;
        private System.Windows.Forms.TextBox tbMil;
        private System.Windows.Forms.Label labelDiplomacy;
        private System.Windows.Forms.Label labelMilitary;
        private System.Windows.Forms.Label labelAdministrative;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox tbSearchCountry;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOrigCltr;
        private System.Windows.Forms.TextBox tbCltr;
        private System.Windows.Forms.Label labelOrigCltr;
        private System.Windows.Forms.Label labelCltr;
        private System.Windows.Forms.Label labelRlgn;
        private System.Windows.Forms.Label labelOrigRlgn;
        private System.Windows.Forms.TextBox tbRlgn;
        private System.Windows.Forms.TextBox tbOrigRlgn;
    }
}

