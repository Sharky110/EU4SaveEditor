using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aligres.SaveParser
{
    public class SaveParser
    {
        #region ----- Variables -------------------------------------------------------

        /// <summary>
        /// List of countries
        /// </summary>
        public readonly List<Country> Countries;

        /// <summary>
        /// List of provinces
        /// </summary>
        public readonly List<Province> Provinces;

        /// <summary>
        /// List of provinces of selected country
        /// </summary>
        public readonly List<Province> ProvincesOfCountry;

        /// <summary>
        /// List of selected provinces
        /// </summary>
        public readonly List<int> SelectedProvincesId;

        /// <summary>
        /// Selected province 
        /// </summary>
        public int CurrentProvince;

        /// <summary>
        /// Path to file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Array of rows in save file
        /// </summary>
        public string[] SavedDataFile;

        #endregion --------------------------------------------------------------------------------

        #region ----- Singleton -------------------------------------------------------

        private static SaveParser _instance;

        private SaveParser()
        {
            Countries = new List<Country>();
            Provinces = new List<Province>();
            ProvincesOfCountry = new List<Province>();
            SelectedProvincesId = new List<int>();

            CurrentProvince = 0;
        }

        public static SaveParser GetInstance()
        {
            return _instance ?? (_instance = new SaveParser());
        }

        #endregion

        public void FindAllCountries()
        {
            var countryRegEx = new Regex("country=\"[A-Z]{3}\"");

            var countries = SavedDataFile.Select((s, i) => new {i, s})
                .Where(t => countryRegEx.IsMatch(t.s))
                .Select(r => new Country {CountryName = r.s.Split('\"')[1], CountryId = r.i + 1})
                .GroupBy(r => r.CountryName)
                .Select(r => r.First())
                .ToList();

            Countries.AddRange(countries);
        }

        public List<string> FindAllProvinces()
        {
            var index = 1;
            var ownerId = 0;
            var provinceCounter = 0;

            var countries = new List<string>();

            string provinceName;

            var duplicate = false;

            var provRegEx = new Regex("name=\"[A-Z][a-z]*" + @"\s" + "*[A-Z]*[a-z]*\"$", RegexOptions.Singleline);

            foreach (string str in SavedDataFile)
            {
                if (provRegEx.IsMatch(str))
                {
                    provinceName = str.Split('\"')[1];

                    bool isProvinceAlreadyExists =
                        Provinces.LastOrDefault(p => p.ProvinceName == provinceName) != null;

                    if (!isProvinceAlreadyExists)
                    {
                        string ownerName = SetCountryForProvince(SavedDataFile[index]);

                        if (ownerName != "Not Province")
                        {
                            foreach (var country in Countries)
                            {
                                if (country.CountryName == ownerName)
                                {
                                    ownerId = country.CountryId;
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
                    countries.Add(Provinces[i].OwnerName);

                duplicate = false;
            }

            return countries;
        }

        private string SetCountryForProvince(string targetString)
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

        public void ChangeProvinceParameters(Province currentProvinces)
        {
            var startSearchId = currentProvinces.ProvinceId;
            var closeId = 0;

            var regExTax = new Regex("base_tax=");
            var regExProd = new Regex("base_production=");
            var regExManPow = new Regex("base_manpower=");

            for (int i = startSearchId; i < startSearchId + 100; i++)
            {
                currentProvinces.OriginalCulture = SavedDataFile[i].Contains("original_culture")
                    ? SavedDataFile[i].Split('=')[1]
                    : currentProvinces.OriginalCulture;
                currentProvinces.CurrentCulture = SavedDataFile[i].Contains("\tculture")
                    ? SavedDataFile[i].Split('=')[1]
                    : currentProvinces.CurrentCulture;
                currentProvinces.OriginalReligion = SavedDataFile[i].Contains("original_religion")
                    ? SavedDataFile[i].Split('=')[1]
                    : currentProvinces.OriginalReligion;
                currentProvinces.CurrentReligion = SavedDataFile[i].Contains("\treligion")
                    ? SavedDataFile[i].Split('=')[1]
                    : currentProvinces.CurrentReligion;

                if (regExTax.IsMatch(SavedDataFile[i]))
                {
                    currentProvinces.Tax = SavedDataFile[i].Split('=')[1];
                    currentProvinces.TaxId = i;

                    closeId = i;

                    break;
                }
            }

            for (int i = closeId; i < closeId + 10; i++)
            {
                if (regExProd.IsMatch(SavedDataFile[i]))
                {
                    currentProvinces.Prod = SavedDataFile[i].Split('=')[1];
                    currentProvinces.ProdId = i;

                    break;
                }
            }

            for (int i = closeId; i < closeId + 10; i++)
            {
                if (regExManPow.IsMatch(SavedDataFile[i]))
                {
                    currentProvinces.ManPow = SavedDataFile[i].Split('=')[1];
                    currentProvinces.ManPowId = i;

                    break;
                }
            }
        }

        public List<string> CountryChanged(string selectedCountry)
        {
            var newProvinces = new List<string>();

            ProvincesOfCountry.Clear();

            foreach (var province in Provinces.Where(province => selectedCountry == province.OwnerName))
            {
                newProvinces.Add(province.ProvinceName);
                ProvincesOfCountry.Add(province);
            }

            return newProvinces;
        }

        public Province ProvinceChanged(string provinceName)
        {
            Province tempProvince = new Province();
            foreach (var province in ProvincesOfCountry.Where(province => provinceName == province.ProvinceName))
            {
                ChangeProvinceParameters(province);

                CurrentProvince = province.ProvinceIndex;

                tempProvince = province;

                break;
            }

            return tempProvince;
        }

        public void ClearLists()
        {
            Countries.Clear();
            Provinces.Clear();
        }
    }
}