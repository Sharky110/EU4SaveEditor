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
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.labelCountries = new System.Windows.Forms.Label();
            this.labelProvs = new System.Windows.Forms.Label();
            this.labelChooseCountry = new System.Windows.Forms.Label();
            this.labelLoadedFile = new System.Windows.Forms.Label();
            this.ListBoxCountries = new System.Windows.Forms.ListBox();
            this.ListBoxProvinces = new System.Windows.Forms.ListBox();
            this.labelChooseProvince = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(12, 12);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(205, 23);
            this.OpenFileButton.TabIndex = 1;
            this.OpenFileButton.Text = "Открыть файл";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // labelCountries
            // 
            this.labelCountries.AutoSize = true;
            this.labelCountries.Location = new System.Drawing.Point(699, 201);
            this.labelCountries.Name = "labelCountries";
            this.labelCountries.Size = new System.Drawing.Size(73, 13);
            this.labelCountries.TabIndex = 3;
            this.labelCountries.Text = "labelCountries";
            // 
            // labelProvs
            // 
            this.labelProvs.AutoSize = true;
            this.labelProvs.Location = new System.Drawing.Point(699, 246);
            this.labelProvs.Name = "labelProvs";
            this.labelProvs.Size = new System.Drawing.Size(56, 13);
            this.labelProvs.TabIndex = 4;
            this.labelProvs.Text = "labelProvs";
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
            this.ListBoxCountries.TabIndex = 8;
            // 
            // ListBoxProvinces
            // 
            this.ListBoxProvinces.FormattingEnabled = true;
            this.ListBoxProvinces.Location = new System.Drawing.Point(226, 90);
            this.ListBoxProvinces.Name = "ListBoxProvinces";
            this.ListBoxProvinces.Size = new System.Drawing.Size(205, 342);
            this.ListBoxProvinces.Sorted = true;
            this.ListBoxProvinces.TabIndex = 10;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 446);
            this.Controls.Add(this.ListBoxProvinces);
            this.Controls.Add(this.labelChooseProvince);
            this.Controls.Add(this.ListBoxCountries);
            this.Controls.Add(this.labelLoadedFile);
            this.Controls.Add(this.labelChooseCountry);
            this.Controls.Add(this.labelProvs);
            this.Controls.Add(this.labelCountries);
            this.Controls.Add(this.OpenFileButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Label labelCountries;
        private System.Windows.Forms.Label labelProvs;
        private System.Windows.Forms.Label labelChooseCountry;
        private System.Windows.Forms.Label labelLoadedFile;
        private System.Windows.Forms.ListBox ListBoxCountries;
        private System.Windows.Forms.ListBox ListBoxProvinces;
        private System.Windows.Forms.Label labelChooseProvince;
    }
}

