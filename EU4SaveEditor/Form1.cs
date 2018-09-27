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
        List<Country> Countries = new List<Country>(1);
        List<Province> Provinces = new List<Province>(1);
        string[] FileRows;
        bool duplicate = false;
        string FilePath = "";
        int CurrentProvince = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void FindAllCountries(string[] FileRows)
        {
            SearchEngine.FindAllCountries(FileRows, ref Countries);
        }

        public void FindAllProvinces(string[] FileRows)
        {
            SearchEngine.FindAllProvinces(FileRows, ref Provinces, Countries);
            for (int i = 0; i < Provinces.Count; i++)
            {
                for (int j = 0; j < i; j++)
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
                    SearchEngine.FindProvinceParameters(FileRows, Provinces[i].Id, Provinces[i]);
                    textBoxAdm.Text = Provinces[i].Tax;
                    textBoxDip.Text = Provinces[i].Prod;
                    textBoxMil.Text = Provinces[i].ManPow;
                    CurrentProvince = i;
                    break;
                }
            }

        }

        private void openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Europa Universalis 4 save (*.eu4)|*.eu4";
            OpenFile.ShowDialog();
            if (OpenFile.FileName != string.Empty)
            {
                FilePath = OpenFile.FileName;
                labelLoadedFile.Text = OpenFile.FileName;
                string SourceFile = File.ReadAllText(OpenFile.FileName);
                FileRows = SourceFile.Split('\n');
                FindAllCountries(FileRows);
                FindAllProvinces(FileRows);
                labelCountriesCount.Text = ListBoxCountries.Items.Count.ToString();
            }
        }
        
        private void saveFile_Click(object sender, EventArgs e)
        {
            if (FilePath != string.Empty)
            {
                SaveFileDialog SaveFile = new SaveFileDialog();
                SaveFile.Filter = "Europa Universalis 4 save (*.eu4)|*.eu4";
                SaveFile.ShowDialog();
                if (SaveFile.FileName != string.Empty)
                {
                    File.WriteAllLines(SaveFile.FileName, FileRows);
                }
            }
            else
            {
                MessageBox.Show("File not opened","Error!");
            }
        }

        private void textBoxAdm_TextChanged(object sender, EventArgs e)
        {
            if (FilePath != string.Empty)
            {
                FileRows[Provinces[CurrentProvince].TaxId] = "		base_tax=" + textBoxAdm.Text;
            }
        }

        private void textBoxDip_TextChanged(object sender, EventArgs e)
        {
            if (FilePath != string.Empty)
            {
                FileRows[Provinces[CurrentProvince].ProdId] = "		base_production=" + textBoxDip.Text;
            }
        }

        private void textBoxMil_TextChanged(object sender, EventArgs e)
        {
            if (FilePath != string.Empty)
            {
                FileRows[Provinces[CurrentProvince].ManPowId] = "		base_manpower=" + textBoxMil.Text;
            }
        }
    }
}
