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
        List<Country> Countries = new List<Country>(1);
        List<Province> Provinces = new List<Province>(1);
        string[] FileRows;
        bool duplicate = false;

        public Form1()
        {
            InitializeComponent();
        }

        public void FindAllCountries(string[] FileRows)
        {
            SE1.FindAllCountries(FileRows, ref Countries);
            labelCountries.Text = Countries.Count.ToString();
            //for (int i = 0; i < Countries.Count; i++)
            //{
            //    ListBoxCountries.Items.Add(Countries[i].Name);
            //}
        }

        public void FindAllProvinces(string[] FileRows)
        {
            SE1.FindAllProvinces(FileRows, ref Provinces, Countries);
            labelProvs.Text = Provinces.Count.ToString();
            //ListBoxProvinces.Items.AddRange(SE1.Provs.ToArray());
            for (int i = 0; i < Provinces.Count; i++)
            {
                for (int j= 0; j < i; j++)
                {
                    if (Provinces[i].OwnerName == Provinces[j].OwnerName)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (duplicate == false)
                {
                    ListBoxCountries.Items.Add(Provinces[i].OwnerName);
                }
                duplicate = false;
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            labelLoadedFile.Text = fileDialog.FileName;

            string SourceFile = File.ReadAllText(fileDialog.FileName);
            FileRows = SourceFile.Split('\n');
            
            
            FindAllCountries(FileRows);
            FindAllProvinces(FileRows);
            labelCountriesCount.Text = ListBoxCountries.Items.Count.ToString();
            
        }

        private void ListBoxCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxProvinces.Items.Clear();
            for (int i = 0; i < Provinces.Count; i++)
            {
                if ((sender as ListBox).SelectedItem.ToString() == Provinces[i].OwnerName)
                {
                    ListBoxProvinces.Items.Add(Provinces[i].Name);
                }
            }
            labelProvincesCount.Text = ListBoxProvinces.Items.Count.ToString();
        }

        private void ListBoxProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProvinceName = (sender as ListBox).SelectedItem.ToString();

            for (int i = 0; i < Provinces.Count; i++)
            {
                if (ProvinceName == Provinces[i].Name)
                {
                    SE1.FindProvinceProsperity(FileRows, Provinces[i].Id, Provinces[i]);
                    textBoxAdm.Text = Provinces[i].Tax;
                    textBoxDip.Text = Provinces[i].Prod;
                    textBoxMil.Text = Provinces[i].ManPow;
                    break;
                }
            }
            
        }
    }
}
