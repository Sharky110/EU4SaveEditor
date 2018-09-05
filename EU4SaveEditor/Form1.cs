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
        List<string> countries = new List<string>();
        bool duplicate = false;

        public Form1()
        {
            InitializeComponent();
        }

        public void FindAllCountries(string[] FileRows)
        {
            Regex countryRegEx = new Regex("country=\"[A-Z]{3}\"");
            
            foreach (string str in FileRows)
            {
                if (countryRegEx.IsMatch(str))
                {
                    for(int i=0;i< countries.Count;i++)
                    {
                        if(str==countries[i])
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if(duplicate==false)
                    {
                        countries.Add(str.Remove(0, 8));
                        comboBoxCountries.Items.Add(str.Remove(0, 8));
                    }
                    duplicate = false;
                }
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            string SourceFile = File.ReadAllText(fileDialog.FileName);
            string[] FileRows = SourceFile.Split('\n');
            label1.Text = FileRows.Length.ToString();
            labelLoadedFile.Text = fileDialog.FileName;
            FindAllCountries(FileRows);
        }
    }
}
