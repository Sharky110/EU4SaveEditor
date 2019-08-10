using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Linq;

namespace EU4SaveEditor
{
    internal class SearchEngine
    {
        #region ----- Объявление переменных -------------------------------------------------------

        /// <summary>
        /// List of countries
        /// </summary>
        private readonly List<Country> _countries;

        /// <summary>
        /// List of provinces
        /// </summary>
        private readonly List<Province> _provinces;

        private List<Province> _provincesOfCountry;

        private readonly List<int> _selectedProvincesId;

        private int _currentProvince;

        private string FilePath { get; set; }

        /// <summary>
        /// Array of rows in save file
        /// </summary>
        private string[] _savedDataFile;
        #endregion --------------------------------------------------------------------------------

        public SearchEngine()
        {
            _countries = new List<Country>();
            _provinces = new List<Province>();
            _provincesOfCountry = new List<Province>();
            _selectedProvincesId = new List<int>();

            _currentProvince = 0;
        }

        public void FindAllCountries()
        {
            var countryRegEx = new Regex("country=\"[A-Z]{3}\"");

            var countries = _savedDataFile.Select((s, i) => new { i, s })
                                .Where(t => countryRegEx.IsMatch(t.s))
                                .Select(r => new Country { CountryName = r.s.Split('\"')[1], CountryId = r.i + 1 })
                                .GroupBy(r => r.CountryName)
                                .Select(r => r.First())
                                .ToList();

            _countries.AddRange(countries);
        }

        public void FindAllProvinces(ref ListBox lbCountries)
        {
            int index = 1;
            int ownerId = 0;
            int provinceCounter = 0;

            string provinceName;

            bool duplicate = false;

            var provRegEx = new Regex("name=\"[A-Z][a-z]*" + @"\s" + "*[A-Z]*[a-z]*\"$", RegexOptions.Singleline);

            foreach (string str in _savedDataFile)
            {
                if (provRegEx.IsMatch(str))
                {
                    provinceName = str.Split('\"')[1];

                    bool isProvinceAlreadyExists = _provinces.LastOrDefault(p => p.ProvinceName == provinceName) != null;

                    if (!isProvinceAlreadyExists)
                    {
                        string ownerName = SetCountryForProvince(_savedDataFile[index]);

                        if (ownerName != "Not Province")
                        {
                            foreach (var country in _countries)
                            {
                                if (country.CountryName == ownerName)
                                {
                                    ownerId = country.CountryId;
                                    break;
                                }
                            }

                            _provinces.Add(new Province(provinceName, index, provinceCounter, ownerName, ownerId));

                            provinceCounter++;
                        }
                    }
                }

                index++;
            }

            for (int i = 0; i < _provinces.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (_provinces[i].OwnerName == _provinces[j].OwnerName)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (duplicate == false)
                    lbCountries.Items.Add(_provinces[i].OwnerName);

                duplicate = false;
            }
        }

        public string SetCountryForProvince(string targetString)
        {
            string countryName = "Not Province";

            var countryRegEx = new Regex("owner=\"[A-Z]{3}\"");
            var countryRegEx2 = new Regex("previ");

            if (countryRegEx.IsMatch(targetString))
                countryName = targetString.Split('\"')[1];
            else if (countryRegEx2.IsMatch(targetString))
                countryName = "_No Owner";

            return countryName;
        }

        public void FindProvinceParameters(ref List<Province> currentProvinces, int currentProv)
        {
            int startSearchId = currentProvinces[currentProv].ProvinceId;
            int closeId = 0;

            var regExTax = new Regex("base_tax=");
            var regExProd = new Regex("base_production=");
            var regExManPow = new Regex("base_manpower=");

            for (int i = startSearchId; i < startSearchId + 100; i++)
            {
                currentProvinces[currentProv].OrigCltr = _savedDataFile[i].Contains("original_culture") ? _savedDataFile[i].Split('=')[1] : currentProvinces[currentProv].OrigCltr;
                currentProvinces[currentProv].Cltr = _savedDataFile[i].Contains("\tculture") ? _savedDataFile[i].Split('=')[1] : currentProvinces[currentProv].Cltr;
                currentProvinces[currentProv].OrigRlgn = _savedDataFile[i].Contains("original_religion") ? _savedDataFile[i].Split('=')[1] : currentProvinces[currentProv].OrigRlgn;
                currentProvinces[currentProv].Rlgn = _savedDataFile[i].Contains("\treligion") ? _savedDataFile[i].Split('=')[1] : currentProvinces[currentProv].Rlgn;

                if (regExTax.IsMatch(_savedDataFile[i]))
                {
                    currentProvinces[currentProv].Tax = _savedDataFile[i].Split('=')[1];
                    currentProvinces[currentProv].TaxId = i;

                    closeId = i;

                    break;
                }
            }
            for (int i = closeId; i < closeId + 10; i++)
            {
                if (regExProd.IsMatch(_savedDataFile[i]))
                {
                    currentProvinces[currentProv].Prod = _savedDataFile[i].Split('=')[1];
                    currentProvinces[currentProv].ProdId = i;

                    break;
                }
            }
            for (int i = closeId; i < closeId + 10; i++)
            {
                if (regExManPow.IsMatch(_savedDataFile[i]))
                {
                    currentProvinces[currentProv].ManPow = _savedDataFile[i].Split('=')[1];
                    currentProvinces[currentProv].ManPowId = i;

                    break;
                }
            }
        }

        public void CountryChanged(string selectedCountry, ref ListBox lbProvinces, ref Label provincesCount)
        {
            lbProvinces.Items.Clear();
            _provincesOfCountry.Clear();

            foreach (var province in _provinces)
            {
                if (selectedCountry == province.OwnerName)
                {
                    lbProvinces.Items.Add(province.ProvinceName);
                    _provincesOfCountry.Add(province);
                }
            }

            provincesCount.Text = lbProvinces.Items.Count.ToString();
        }

        public void ProvinceChanged(ref ListBox lbProvinces, ref GroupBox gbProvProsp, ref TextBox origCltr,
                                    ref TextBox cltr, ref TextBox origRlgn, ref TextBox rlgn)
        {
            if (lbProvinces.SelectedItems.Count < 2)
            {
                for (int currentProv = 0; currentProv < _provincesOfCountry.Count; currentProv++)
                {
                    if (lbProvinces.SelectedItem.ToString() == _provincesOfCountry[currentProv].ProvinceName)
                    {
                        FindProvinceParameters(ref _provincesOfCountry, currentProv);

                        _currentProvince = _provincesOfCountry[currentProv].ProvinceIndex;

                        ((TextBox) gbProvProsp.Controls["tbAdm"]).Text = _provincesOfCountry[currentProv].Tax;
                        ((TextBox) gbProvProsp.Controls["tbDip"]).Text = _provincesOfCountry[currentProv].Prod;
                        ((TextBox) gbProvProsp.Controls["tbMil"]).Text = _provincesOfCountry[currentProv].ManPow;

                        origCltr.Text = _provincesOfCountry[currentProv].OrigCltr;
                        cltr.Text = _provincesOfCountry[currentProv].Cltr;
                        origRlgn.Text = _provincesOfCountry[currentProv].OrigRlgn;
                        rlgn.Text = _provincesOfCountry[currentProv].Rlgn;

                        break;
                    }
                }
            }
            else
            {

            }
        }

        public void OpenFile(ref ListBox lbCountries, ref ListBox lbProvinces, ref Label lblLoadedFile, ref Label lblCountriesCount)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "Europa Universalis 4 save (*.eu4)|*.eu4"
            };

            openFile.ShowDialog();

            if (openFile.FileName == string.Empty)
                return;

            FilePath = openFile.FileName;

            lblLoadedFile.Text = openFile.FileName;

            string sourceFile = File.ReadAllText(openFile.FileName, Encoding.GetEncoding(1252));

            _savedDataFile = sourceFile.Split('\n');

            _countries.Clear();
            _provinces.Clear();

            lbCountries.Items.Clear();
            lbProvinces.Items.Clear();

            FindAllCountries();
            FindAllProvinces(ref lbCountries);

            lblCountriesCount.Text = lbCountries.Items.Count.ToString();
        }

        public void SaveFile()
        {
            if (FilePath != string.Empty)
            {
                SaveFileDialog saveFile = new SaveFileDialog
                {
                    Filter = "Europa Universalis 4 save (*.eu4)|*.eu4"
                };

                saveFile.ShowDialog();

                if (saveFile.FileName == string.Empty)
                    return;

                var myfile = new StreamWriter(saveFile.FileName, false, Encoding.GetEncoding(1252));
                foreach (var row in _savedDataFile)
                {
                    myfile.Write(row + "\n");
                }
                myfile.Close();
            }
            else
            {
                MessageBox.Show("File not opened.", "Error.");
            }
        }

        public void SetPoints(ref object sender, ListBox lbProvinces)
        {
            if (FilePath != string.Empty)
            {
                if (lbProvinces.SelectedItems.Count < 2)
                {
                    switch ((sender as TextBox)?.Name)
                    {
                        case "tbAdm":
                            _savedDataFile[_provinces[_currentProvince].TaxId] = "    base_tax=" + ((TextBox) sender).Text;
                            break;
                        case "tbDip":
                            _savedDataFile[_provinces[_currentProvince].ProdId] = "    base_production=" + ((TextBox) sender).Text;
                            break;
                        case "tbMil":
                            _savedDataFile[_provinces[_currentProvince].ManPowId] = "    base_manpower=" + ((TextBox) sender).Text;
                            break;
                    }
                }
                else
                {
                    _selectedProvincesId.Clear();
                    for (int i = 0; i < _provincesOfCountry.Count; i++)
                    {
                        for (int j = 0; j < lbProvinces.SelectedItems.Count; j++)
                        {
                            if (lbProvinces.SelectedItems[j].ToString() == _provincesOfCountry[i].ProvinceName)
                            {
                                FindProvinceParameters(ref _provincesOfCountry, i);
                                _selectedProvincesId.Add(_provincesOfCountry[i].ProvinceIndex);
                                break;
                            }
                        }
                    }
                    foreach (var provinceId in _selectedProvincesId)
                    {
                        switch ((sender as TextBox)?.Name)
                        {
                            case "tbAdm":
                                _savedDataFile[_provinces[provinceId].TaxId] = "    base_tax=" + ((TextBox) sender).Text;
                                break;
                            case "tbDip":
                                _savedDataFile[_provinces[provinceId].ProdId] = "    base_production=" + ((TextBox) sender).Text;
                                break;
                            case "tbMil":
                                _savedDataFile[_provinces[provinceId].ManPowId] = "    base_manpower=" + ((TextBox) sender).Text;
                                break;
                        }
                    }
                }
            }
        }

        public void KeyPress(ref KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 46) //цифры, клавиша BackSpace и точка в ASCII
            {
                e.Handled = true;
            }
        }

        public void FindCountry(string Text, ref ListBox lbCountries)
        {
            for (int i = 0; i < lbCountries.Items.Count; i++)
            {
                if (lbCountries.Items[i].ToString().StartsWith(Text))
                {
                    lbCountries.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
