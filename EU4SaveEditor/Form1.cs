using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace EU4SaveEditor
{
    public partial class Form1 : Form
    {
        List<Country> Countries = new List<Country>(1);
        List<Province> Provinces = new List<Province>(1);
        List<Province> CountryProvinces = new List<Province>(1);
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
            Countries.Clear();
            ListBoxCountries.Items.Clear();
            SearchEngine.FindAllCountries(FileRows, ref Countries);
        }

        public void FindAllProvinces(string[] FileRows)
        {
            Provinces.Clear();
            ListBoxProvinces.Items.Clear();
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
            CountryProvinces.Clear();
            for (int i = 0; i < Provinces.Count; i++)
            {
                if ((sender as ListBox).SelectedItem.ToString() == Provinces[i].OwnerName)
                {
                    ListBoxProvinces.Items.Add(Provinces[i].ProvinceName);
                    CountryProvinces.Add(Provinces[i]);
                }
            }
            labelProvincesCount.Text = ListBoxProvinces.Items.Count.ToString();
        }

        private void ListBoxProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                string ProvinceName = (sender as ListBox).SelectedItem.ToString();

                for (int i = 0; i < CountryProvinces.Count; i++)// раньше был province id и count, был правильные id
                {
                    if (ProvinceName == CountryProvinces[i].ProvinceName)
                    {
                        SearchEngine.FindProvinceParameters(FileRows, CountryProvinces[i].ProvinceId, CountryProvinces[i]);
                        textBoxAdm.Text = CountryProvinces[i].Tax;
                        textBoxDip.Text = CountryProvinces[i].Prod;
                        textBoxMil.Text = CountryProvinces[i].ManPow;
                        CurrentProvince = CountryProvinces[i].ProvinceIndex;
                        break;
                    }
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
                string SourceFile = File.ReadAllText(OpenFile.FileName, Encoding.GetEncoding(1252));
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
                FileRows.ToString();
                if (SaveFile.FileName != string.Empty)
                {
                    StreamWriter myfile = new StreamWriter(SaveFile.FileName, false, Encoding.GetEncoding(1252));
                    for (int i=0;i<FileRows.Length;i++)
                    {
                        myfile.Write(FileRows[i]+"\n");
                    }
                    myfile.Close();
                }
            }
            else
            {
                MessageBox.Show("File not opened.","Error!");
            }
        }

        private void textBoxAdm_TextChanged(object sender, EventArgs e)
        {
            if (FilePath != string.Empty && Provinces[CurrentProvince].TaxId != 0)
            {
                FileRows[Provinces[CurrentProvince].TaxId] = "		base_tax=" + textBoxAdm.Text;
            }
        }

        private void textBoxDip_TextChanged(object sender, EventArgs e)
        {
            if (FilePath != string.Empty && Provinces[CurrentProvince].ProdId != 0)
            {
                FileRows[Provinces[CurrentProvince].ProdId] = "		base_production=" + textBoxDip.Text;
            }
        }

        private void textBoxMil_TextChanged(object sender, EventArgs e)
        {
            if (FilePath != string.Empty && Provinces[CurrentProvince].ManPowId != 0)
            {
                FileRows[Provinces[CurrentProvince].ManPowId] = "		base_manpower=" + textBoxMil.Text;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 46) //цифры, клавиша BackSpace и точка в ASCII
            {
                e.Handled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
