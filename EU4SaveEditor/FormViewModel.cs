using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using Aligres.SaveParser;

namespace EU4SaveEditor
{
    internal class FormViewModel
    {
        private readonly SaveParser _saveParser = SaveParser.GetInstance();

        private static void AddValsToListBox(IEnumerable<string> list, ref ListBox listBox)
        {
            foreach (var str in list)
            {
                listBox.Items.Add(str);
            }
        }

        public void CountryChanged(string country, ref ListBox lbProvinces, ref Label labelProvincesCount )
        {
            lbProvinces.Items.Clear();

            var newProvinces = _saveParser.CountryChanged(country);

            AddValsToListBox(newProvinces, ref lbProvinces);

            labelProvincesCount.Text = newProvinces.Count().ToString();
        }

        public void ProvinceChanged(ref ListBox lbProvinces, ref GroupBox gbProvinceProsperity, ref TextBox tbOriginalCulture,
            ref TextBox tbCurrentCulture, ref TextBox tbOriginalReligion, ref TextBox tbCurrentReligion)
        {
            var province = _saveParser.ProvinceChanged(lbProvinces.SelectedItem.ToString());

            ((TextBox)gbProvinceProsperity.Controls["tbAdm"]).Text = province.Tax;
            ((TextBox)gbProvinceProsperity.Controls["tbDip"]).Text = province.Prod;
            ((TextBox)gbProvinceProsperity.Controls["tbMil"]).Text = province.ManPow;

            tbOriginalCulture.Text = province.OriginalCulture;
            tbCurrentCulture.Text = province.CurrentCulture;
            tbOriginalReligion.Text = province.OriginalReligion;
            tbCurrentReligion.Text = province.CurrentReligion;
        }

        public void OpenFile(ref ListBox lbCountries, ref ListBox lbProvinces, 
            ref Label labelLoadedFile, ref Label labelCountriesCount)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = ConfigurationManager.AppSettings.Get("FileDialogFilter")
            };

            openFile.ShowDialog();

            if (openFile.FileName == string.Empty)
                return;

            _saveParser.FilePath = openFile.FileName;

            labelLoadedFile.Text = openFile.FileName;

            var sourceFile = File.ReadAllText(openFile.FileName, Encoding.GetEncoding(1252));

            _saveParser.SavedDataFile = sourceFile.Split('\n');

            _saveParser.ClearLists();

            lbCountries.Items.Clear();
            lbProvinces.Items.Clear();

            _saveParser.FindAllCountries();
            var listOfCountries = _saveParser.FindAllProvinces();

            AddValsToListBox(listOfCountries, ref lbCountries);

            labelCountriesCount.Text = lbCountries.Items.Count.ToString();
        }

        public void SaveFile()
        {
            if (!string.IsNullOrEmpty(_saveParser.FilePath))
            {
                var saveFile = new SaveFileDialog
                {
                    Filter = ConfigurationManager.AppSettings.Get("FileDialogFilter")
                };

                saveFile.ShowDialog();

                if (saveFile.FileName == string.Empty)
                    return;

                var streamWriter = new StreamWriter(saveFile.FileName, false, Encoding.GetEncoding(1252));
                foreach (var row in _saveParser.SavedDataFile)
                {
                    streamWriter.Write(row + "\n");
                }
                streamWriter.Close();
            }
            else
            {
                MessageBox.Show("File not opened.", "Error.");
            }
        }

        public void SetPoints(ref object sender, ListBox lbProvinces)
        {
            if (!string.IsNullOrEmpty(_saveParser.FilePath))
                return;

            if (lbProvinces.SelectedItems.Count == 1)
            {
                switch ((sender as TextBox)?.Name)
                {
                    case "tbAdm":
                        _saveParser.SavedDataFile[_saveParser.Provinces[_saveParser.CurrentProvince].TaxId] = 
                            "    base_tax=" + ((TextBox)sender).Text;
                        break;
                    case "tbDip":
                        _saveParser.SavedDataFile[_saveParser.Provinces[_saveParser.CurrentProvince].ProdId] = 
                            "    base_production=" + ((TextBox)sender).Text;
                        break;
                    case "tbMil":
                        _saveParser.SavedDataFile[_saveParser.Provinces[_saveParser.CurrentProvince].ManPowId] = 
                            "    base_manpower=" + ((TextBox)sender).Text;
                        break;
                }
            }
            else if (lbProvinces.SelectedItems.Count > 1)
            {
                _saveParser.SelectedProvincesId.Clear();
                foreach (var province in _saveParser.ProvincesOfCountry)
                {
                    if (lbProvinces.SelectedItems.Cast<object>().Any(provinceItem => provinceItem.ToString() == province.ProvinceName))
                    {
                        _saveParser.ChangeProvinceParameters(province);
                        _saveParser.SelectedProvincesId.Add(province.ProvinceIndex);
                    }
                }
                foreach (var provinceId in _saveParser.SelectedProvincesId)
                {
                    switch ((sender as TextBox)?.Name)
                    {
                        case "tbAdm":
                            _saveParser.SavedDataFile[_saveParser.Provinces[provinceId].TaxId] = 
                                "    base_tax=" + ((TextBox)sender).Text;
                            break;
                        case "tbDip":
                            _saveParser.SavedDataFile[_saveParser.Provinces[provinceId].ProdId] = 
                                "    base_production=" + ((TextBox)sender).Text;
                            break;
                        case "tbMil":
                            _saveParser.SavedDataFile[_saveParser.Provinces[provinceId].ManPowId] = 
                                "    base_manpower=" + ((TextBox)sender).Text;
                            break;
                    }
                }
            }
        }

        public void KeyPress(ref KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 46) //numbers, BackSpace and ASCII point
            {
                e.Handled = true;
            }
        }

        public void FindCountry(string text, ref ListBox lbCountries)
        {
            for (int i = 0; i < lbCountries.Items.Count; i++)
            {
                if (lbCountries.Items[i].ToString().StartsWith(text))
                {
                    lbCountries.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
