using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace EU4SaveEditor
{
    public partial class Form1 : Form
    {
        SearchEngine SE1 = new SearchEngine();

        public Form1()
        {
            InitializeComponent();
        }

        public void FindAllCountries(string[] FileRows)
        {
            SE1.FindAllCountries(FileRows);
            labelCountries.Text = SE1.Countries.Count.ToString();
            ListBoxCountries.Items.AddRange( SE1.Countries.ToArray());
        }

        public void FindAllProvinces(string[] FileRows)
        {
            labelProvs.Text = SE1.Provs.Count.ToString();
            ListBoxCountries.Items.AddRange(SE1.Provs.ToArray());
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            labelLoadedFile.Text = fileDialog.FileName;

            string SourceFile = File.ReadAllText(fileDialog.FileName);
            string[] FileRows = SourceFile.Split('\n');
            
            
            FindAllCountries(FileRows);
            FindAllProvinces(FileRows);
        }
    }
}
