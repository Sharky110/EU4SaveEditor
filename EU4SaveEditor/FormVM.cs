using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using Aligres.SaveParser;
using System.Threading.Tasks;
using System;

namespace EU4SaveEditor
{
    internal class FormVM
    {
        private readonly SaveParser _saveParser = SaveParser.GetInstance();
        
        [Obsolete]
        private static void AddValsToListBox(IEnumerable<string> list, ListBox listBox)
        {
            foreach (var str in list)
            {
                listBox.Items.Add(str);
            }
        }

        [Obsolete]
        public void CountryChanged(string country, ListBox lbProvinces, Label labelProvincesCount )
        {
            var newProvinces = _saveParser.GetProvincesOfContry(country);

            lbProvinces.Items.Clear();
            AddValsToListBox(newProvinces, lbProvinces);

            labelProvincesCount.Text = newProvinces.Count().ToString();
        }

        [Obsolete]
        public void ProvinceChanged(ListBox lbProvinces, GroupBox gbProvinceProsperity, TextBox tbOriginalCulture,
            TextBox tbCurrentCulture, TextBox tbOriginalReligion, TextBox tbCurrentReligion)
        {
            if (lbProvinces.SelectedItem == null)
                return;
            var province = _saveParser.GetProvince(lbProvinces.SelectedItem.ToString());

            ((TextBox)gbProvinceProsperity.Controls["tbAdm"]).Text = province.Adm;
            ((TextBox)gbProvinceProsperity.Controls["tbDip"]).Text = province.Dip;
            ((TextBox)gbProvinceProsperity.Controls["tbMil"]).Text = province.Mil;

            tbOriginalCulture.Text = province.OriginalCulture;
            tbCurrentCulture.Text = province.CurrentCulture;
            tbOriginalReligion.Text = province.OriginalReligion;
            tbCurrentReligion.Text = province.CurrentReligion;
        }

        [Obsolete]
        public async void OpenFile(ListBox lbCountries, ListBox lbProvinces, Label labelLoadedFile, Label labelCountriesCount)
        {
            string fileName;
            using (var openFileDialog = new OpenFileDialog() { Filter = ConfigurationManager.AppSettings.Get("FileDialogFilter") })
            {
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName == string.Empty)
                    return;
                fileName = openFileDialog.FileName;
            }

           // _saveParser.FilePath = fileName;

            labelLoadedFile.Text = fileName;

            var sourceFile = await Task.Run(() => File.ReadAllText(fileName, Encoding.GetEncoding(1252)));

            _saveParser.SaveFile = sourceFile.Split('\n');

            _saveParser.ClearLists();

            lbCountries.Items.Clear();
            lbProvinces.Items.Clear();

            await Task.Run(() => _saveParser.FindAllCountries());
            await Task.Run(() => _saveParser.FindAllProvinces());
            var listOfCountries = _saveParser.GetCountries();
            AddValsToListBox(listOfCountries, lbCountries);

            labelCountriesCount.Text = lbCountries.Items.Count.ToString();
        }

        [Obsolete]
        public void SaveFile()
        {
            //if (string.IsNullOrEmpty(_saveParser.FilePath))
           // {
           //     MessageBox.Show("File not opened.", "Error.");
           //     return;
           // }
            string saveFile;
            using (var saveFileDialog = new SaveFileDialog() { Filter = ConfigurationManager.AppSettings.Get("FileDialogFilter")})
            {
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName == string.Empty)
                    return;
                saveFile = saveFileDialog.FileName;
            }

            var streamWriter = new StreamWriter(saveFile, false, Encoding.GetEncoding(1252));
            foreach (var row in _saveParser.SaveFile)
            {
                streamWriter.Write(row + "\n");
            }
            streamWriter.Close();
        }

        public void SetPoints(object sender, ListBox lbProvinces)
        {
            //if (string.IsNullOrEmpty(_saveParser.FilePath))
           //     return;

            if (lbProvinces.SelectedItems.Count == 1)
            {
                switch ((sender as TextBox)?.Name)
                {
                    case "tbAdm":
                        _saveParser.SaveFile[_saveParser.Provinces[_saveParser.CurrentProvince].AdmId] = 
                            "    base_tax=" + ((TextBox)sender).Text;
                        break;
                    case "tbDip":
                        _saveParser.SaveFile[_saveParser.Provinces[_saveParser.CurrentProvince].DipId] = 
                            "    base_production=" + ((TextBox)sender).Text;
                        break;
                    case "tbMil":
                        _saveParser.SaveFile[_saveParser.Provinces[_saveParser.CurrentProvince].MilId] = 
                            "    base_manpower=" + ((TextBox)sender).Text;
                        break;
                }
            }
            else if (lbProvinces.SelectedItems.Count > 1)
            {
                _saveParser.SelectedProvincesId.Clear();
                foreach (var province in _saveParser.ProvincesOfCountry)
                {
                    if (lbProvinces.SelectedItems.Cast<object>().Any(provinceItem => provinceItem.ToString() == province.Name))
                    {
                        _saveParser.ChangeProvinceParameters(province);
                        _saveParser.SelectedProvincesId.Add(province.Id);
                    }
                }
                foreach (var provinceId in _saveParser.SelectedProvincesId)
                {
                    switch ((sender as TextBox)?.Name)
                    {
                        case "tbAdm":
                            _saveParser.SaveFile[_saveParser.Provinces[provinceId].AdmId] = 
                                "    base_tax=" + ((TextBox)sender).Text;
                            break;
                        case "tbDip":
                            _saveParser.SaveFile[_saveParser.Provinces[provinceId].DipId] = 
                                "    base_production=" + ((TextBox)sender).Text;
                            break;
                        case "tbMil":
                            _saveParser.SaveFile[_saveParser.Provinces[provinceId].MilId] = 
                                "    base_manpower=" + ((TextBox)sender).Text;
                            break;
                    }
                }
            }
        }

        public void KeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 46) //numbers, BackSpace and ASCII point
            {
                e.Handled = true;
            }
        }


        public void FindCountry(string text, ListBox lbCountries)
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
