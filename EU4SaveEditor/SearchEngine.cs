using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Linq;

namespace EU4SaveEditor
{
    class SearchEngine
    {
        #region ----- Объявление переменных -------------------------------------------------------

        /// <summary>
        /// Список стран
        /// </summary>
        private List<Country> Countries;

        /// <summary>
        /// Список провинций
        /// </summary>
        private List<Province> Provinces;

        private List<Province> ProvincesOfCountry;

        private List<int> SelectedProvincesId;

        private int CurrentProvince;

        private string FilePath { get; set; }

        /// <summary>
        /// Массив всех строк в файле сохранения
        /// </summary>
        private string[] SavedDataFile;

        private bool duplicate;

        #endregion --------------------------------------------------------------------------------
        
        public SearchEngine()
        {
            Countries = new List<Country>();
            Provinces = new List<Province>();
            ProvincesOfCountry = new List<Province>();
            SelectedProvincesId = new List<int>();

            CurrentProvince = 0;
            duplicate = false;
        }

        public void FindAllCountries()
        {
            Regex countryRegEx = new Regex("country=\"[A-Z]{3}\"");
            
            var countries = SavedDataFile.Select((s, i) => new { i, s })
                                .Where(t => countryRegEx.IsMatch(t.s))
                                .Select(r => new Country { CountryName = r.s.Split('\"')[1] , CountryId =  r.i + 1})
                                .GroupBy(r => r.CountryName)
                                .Select(r => r.First())
                                .ToList();
            
            this.Countries.AddRange(countries);
        }

        public void FindAllProvinces(ref ListBox lbCountries)
        {
            int index = 1;
            int ownerId = 0;
            int provinceCounter = 0;

            string provinceName = string.Empty;
            string ownerName = string.Empty;

            bool isProvinceAlreadyExists;

            Regex ProvRegEx = new Regex("name=\"[A-Z][a-z]*" + @"\s" + "*[A-Z]*[a-z]*\"$", RegexOptions.Singleline);
            
            foreach (string str in SavedDataFile)
            {
                if (ProvRegEx.IsMatch(str))
                {
                    provinceName = str.Split('\"')[1];

                    isProvinceAlreadyExists = Provinces
                                                  .Where(p => p.ProvinceName == provinceName)
                                                  .LastOrDefault() != null;
                    
                    if (!isProvinceAlreadyExists)
                    {
                        ownerName = SetCountryForProvince(SavedDataFile[index]);

                        if (ownerName != "Not Province")
                        {
                            for (int i = 0; i < Countries.Count; i++)
                            {
                                if (Countries[i].CountryName == ownerName)
                                {
                                    ownerId = Countries[i].CountryId;
                                    break;
                                }
                            }

                            Provinces.Add(new Province(provinceName, index, provinceCounter, ownerName, ownerId));

                            provinceCounter++;
                        }
                    }
                }

                index++;
            }

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
                    lbCountries.Items.Add(Provinces[i].OwnerName);

                duplicate = false;
            }
        }

        public string SetCountryForProvince(string targetString)
        {
            string countryName = "Not Province";

            Regex countryRegEx = new Regex("owner=\"[A-Z]{3}\"");
            Regex countryRegEx2 = new Regex("previ");

            if (countryRegEx.IsMatch(targetString))
                countryName = targetString.Split('\"')[1];
            else if (countryRegEx2.IsMatch(targetString))
                countryName = "_No Owner";

            return countryName;
        }

        public void FindProvinceParameters(ref List<Province> currentProvinces, int currentProv)
        {
            int StartSearchId = currentProvinces[currentProv].ProvinceId;
            int closeId = 0;

            Regex RegExTax = new Regex("base_tax=");
            Regex RegExProd = new Regex("base_production=");
            Regex RegExManPow = new Regex("base_manpower=");

            for (int i = StartSearchId; i < StartSearchId + 100; i++)
            {
                currentProvinces[currentProv].OrigCltr = SavedDataFile[i].Contains("original_culture") ? SavedDataFile[i].Split('=')[1] : currentProvinces[currentProv].OrigCltr;
                currentProvinces[currentProv].Cltr = SavedDataFile[i].Contains("\tculture") ? SavedDataFile[i].Split('=')[1] : currentProvinces[currentProv].Cltr;
                currentProvinces[currentProv].OrigRlgn = SavedDataFile[i].Contains("original_religion") ? SavedDataFile[i].Split('=')[1] : currentProvinces[currentProv].OrigRlgn;
                currentProvinces[currentProv].Rlgn = SavedDataFile[i].Contains("\treligion") ? SavedDataFile[i].Split('=')[1] : currentProvinces[currentProv].Rlgn;
                
                if (RegExTax.IsMatch(SavedDataFile[i]))
                {
                    currentProvinces[currentProv].Tax = SavedDataFile[i].Split('=')[1];
                    currentProvinces[currentProv].TaxId = i;

                    closeId = i;

                    break;
                }
            }
            for (int i = closeId; i < closeId + 10; i++)
            {
                if (RegExProd.IsMatch(SavedDataFile[i]))
                {
                    currentProvinces[currentProv].Prod = SavedDataFile[i].Split('=')[1];
                    currentProvinces[currentProv].ProdId = i;

                    break;
                }
            }
            for (int i = closeId; i < closeId + 10; i++)
            {
                if (RegExManPow.IsMatch(SavedDataFile[i]))
                {
                    currentProvinces[currentProv].ManPow = SavedDataFile[i].Split('=')[1];
                    currentProvinces[currentProv].ManPowId = i;

                    break;
                }
            }
        }

        public void CountryChanged(string SelectedCountry, ref ListBox lbProvinces, ref Label ProvincesCount)
        {
            lbProvinces.Items.Clear();
            ProvincesOfCountry.Clear();

            for (int i = 0; i < Provinces.Count; i++)
            {
                if (SelectedCountry == Provinces[i].OwnerName)
                {
                    lbProvinces.Items.Add(Provinces[i].ProvinceName);
                    ProvincesOfCountry.Add(Provinces[i]);
                }
            }

            ProvincesCount.Text = lbProvinces.Items.Count.ToString();
        }

        public void ProvinceChanged(ref ListBox lbProvinces, ref GroupBox gbProvProsp, ref TextBox OrigCltr, 
                                    ref TextBox Cltr, ref TextBox OrigRlgn, ref TextBox Rlgn  )
        {
            if (lbProvinces.SelectedItems.Count < 2)
            {
                for (int currentProv = 0; currentProv < ProvincesOfCountry.Count; currentProv++)
                {
                    if (lbProvinces.SelectedItem.ToString() == ProvincesOfCountry[currentProv].ProvinceName)
                    {
                        FindProvinceParameters(ref ProvincesOfCountry, currentProv);

                        CurrentProvince = ProvincesOfCountry[currentProv].ProvinceIndex;

                        (gbProvProsp.Controls["tbAdm"] as TextBox).Text = ProvincesOfCountry[currentProv].Tax;
                        (gbProvProsp.Controls["tbDip"] as TextBox).Text = ProvincesOfCountry[currentProv].Prod;
                        (gbProvProsp.Controls["tbMil"] as TextBox).Text = ProvincesOfCountry[currentProv].ManPow;

                        OrigCltr.Text = ProvincesOfCountry[currentProv].OrigCltr;
                        Cltr.Text = ProvincesOfCountry[currentProv].Cltr;
                        OrigRlgn.Text = ProvincesOfCountry[currentProv].OrigRlgn;
                        Rlgn.Text = ProvincesOfCountry[currentProv].Rlgn;

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
            OpenFileDialog OpenFile = new OpenFileDialog
            {
                Filter = "Europa Universalis 4 save (*.eu4)|*.eu4"
            };

            OpenFile.ShowDialog();

            if (OpenFile.FileName != string.Empty)
            {
                FilePath = OpenFile.FileName;

                lblLoadedFile.Text = OpenFile.FileName;

                string SourceFile = File.ReadAllText(OpenFile.FileName, Encoding.GetEncoding(1252));

                SavedDataFile = SourceFile.Split('\n');

                Countries.Clear();
                Provinces.Clear();

                lbCountries.Items.Clear();
                lbProvinces.Items.Clear();

                FindAllCountries();
                FindAllProvinces(ref lbCountries);

                lblCountriesCount.Text = lbCountries.Items.Count.ToString();
            }
        }

        public void SaveFile()
        {
            if (FilePath != string.Empty)
            {
                SaveFileDialog SaveFile = new SaveFileDialog
                {
                    Filter = "Europa Universalis 4 save (*.eu4)|*.eu4"
                };

                SaveFile.ShowDialog();

                if (SaveFile.FileName != string.Empty)
                {
                    StreamWriter myfile = new StreamWriter(SaveFile.FileName, false, Encoding.GetEncoding(1252));
                    for (int i = 0; i < SavedDataFile.Length; i++)
                    {
                        myfile.Write(SavedDataFile[i] + "\n");
                    }
                    myfile.Close();
                }
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
                    switch ((sender as TextBox).Name)
                    {
                        case "tbAdm":
                            SavedDataFile[Provinces[CurrentProvince].TaxId] = "    base_tax=" + (sender as TextBox).Text;
                            break;
                        case "tbDip":
                            SavedDataFile[Provinces[CurrentProvince].ProdId] = "    base_production=" + (sender as TextBox).Text;
                            break;
                        case "tbMil":
                            SavedDataFile[Provinces[CurrentProvince].ManPowId] = "    base_manpower=" + (sender as TextBox).Text;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    SelectedProvincesId.Clear();
                    for (int i = 0; i < ProvincesOfCountry.Count; i++)
                    {
                        for (int j = 0; j < lbProvinces.SelectedItems.Count; j++)
                        {
                            if (lbProvinces.SelectedItems[j].ToString() == ProvincesOfCountry[i].ProvinceName)
                            {
                                FindProvinceParameters(ref ProvincesOfCountry, i);
                                SelectedProvincesId.Add(ProvincesOfCountry[i].ProvinceIndex);
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < SelectedProvincesId.Count; i++)
                    {
                        switch ((sender as TextBox).Name)
                        {
                            case "tbAdm":
                                SavedDataFile[Provinces[SelectedProvincesId[i]].TaxId] = "    base_tax=" + (sender as TextBox).Text;
                                break;
                            case "tbDip":
                                SavedDataFile[Provinces[SelectedProvincesId[i]].ProdId] = "    base_production=" + (sender as TextBox).Text;
                                break;
                            case "tbMil":
                                SavedDataFile[Provinces[SelectedProvincesId[i]].ManPowId] = "    base_manpower=" + (sender as TextBox).Text;
                                break;
                            default:
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
