﻿using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EU4SaveEditorWPF.Models;

namespace EU4SaveEditorWPF.ViewModels
{
    public class SaveParser
    {
        #region ----- Variables -------------------------------------------------------------------

        private readonly List<Country> Countries;
        private string[] _saveFile;
        public readonly List<Province> Provinces;
        public readonly List<Province> ProvincesOfCountry;
        public readonly List<int> SelectedProvincesId;
        public string PlayerCountry;
        

        #endregion --------------------------------------------------------------------------------

        #region ----- Properties ------------------------------------------------------------------

        public string[] SaveFile
        {
            get { return _saveFile; }
            set
            {
                _saveFile = value;
                ClearLists();
            }
        }

        #endregion

        #region ----- Singleton -------------------------------------------------------------------

        private static SaveParser _instance;
        private SaveParser()
        {
            Countries = new List<Country>();
            Provinces = new List<Province>();
            ProvincesOfCountry = new List<Province>();
            SelectedProvincesId = new List<int>();
        }

        public static SaveParser GetInstance()
        {
            return _instance ?? (_instance = new SaveParser());
        }

        #endregion --------------------------------------------------------------------------------

        public void FindCountriesAndProvinces()
        {
            var index = 0;
            var countryRegEx = new Regex("country=\"[A-Z]{3}\"");
            var provRegEx = new Regex("-[0-9]{1,}=", RegexOptions.Singleline);

            foreach (string str in SaveFile)
            {
                index++;

                if (str.Length < 3)
                    continue;

                if(index < 20 && str.Contains(@"player="""))
                    PlayerCountry = str.Split('=')[1].Replace("\"", "");
                else if (countryRegEx.IsMatch(str))
                    AddCountry(str, index);
                else if (provRegEx.IsMatch(str))
                    AddProvince(str, index-1);
            }
        }

        private void AddCountry(string str, int index)
        {
            var countryName = str.Split('\"')[1];
            var isCountryAlreadyExists = Countries.Any(p => p.Name == countryName);
            if (isCountryAlreadyExists)
                return;

            var tempCountry = new Country { Name = countryName, Id = index + 1 };
            Countries.Add(tempCountry);
        }

        private void AddProvince(string str, int index)
        {
            var counter = 0;
            var endIndex = 0;
            for (int i = index; i < index + 1000; i++)
            {
                if (SaveFile[i].Contains('{'))
                    counter += 1;
                if (SaveFile[i].Contains('}'))
                    counter -= 1;
                if(counter == 0)
                {
                    endIndex = i;
                    break;
                }

            }

            var provinceName = string.Empty;
            var nameIndex = 0;
            for (int i = index; i < index+10; i++)
            {
                if (SaveFile[i].Contains("name"))
                {
                    provinceName = SaveFile[i].Split('\"')[1];
                    nameIndex = i;
                    break;
                }
            }

            var ownerString = SaveFile[nameIndex + 1];
            var ownerName = GetOwnerName(ownerString);
            if (ownerName == "Not Province")
                return;

            var tempProvince = new Province(provinceName, index, ownerName);
            Provinces.Add(tempProvince);
        }

        public List<string> GetCountries()
        {
            var list = Provinces
                .Select(p => p.Owner)
                .Distinct()
                .ToList();
            list.Sort();
            return list;
        }

        private string GetOwnerName(string targetString)
        {
            var ownerPattern = new Regex("owner=\"[A-Z]{3}\"");
            var noOwnerPattern = new Regex("institutions");
            var noOwnerPattern2 = new Regex("previous_controller");
            
            if (ownerPattern.IsMatch(targetString))
                return targetString.Split('\"')[1];
            else if (noOwnerPattern.IsMatch(targetString) || noOwnerPattern2.IsMatch(targetString))
                return "_No Owner";
            else
                return "Not Province";
        }

        public void ChangeProvinceParameters(Province currentProvince)
        {
            var startSearchId = currentProvince.PositionInFile;
            var closeId = 0;

            var regExTax = new Regex("base_tax=");
            var regExProd = new Regex("base_production=");
            var regExManPow = new Regex("base_manpower=");

            for (int i = startSearchId; i < startSearchId + 100; i++)
            {
                currentProvince.OriginalCulture = SaveFile[i].Contains("original_culture")
                    ? SaveFile[i].Split('=')[1]
                    : currentProvince.OriginalCulture;
                currentProvince.CurrentCulture = SaveFile[i].Contains("\tculture")
                    ? SaveFile[i].Split('=')[1]
                    : currentProvince.CurrentCulture;
                currentProvince.OriginalReligion = SaveFile[i].Contains("original_religion")
                    ? SaveFile[i].Split('=')[1]
                    : currentProvince.OriginalReligion;
                currentProvince.CurrentReligion = SaveFile[i].Contains("\treligion")
                    ? SaveFile[i].Split('=')[1]
                    : currentProvince.CurrentReligion;

                if (regExTax.IsMatch(SaveFile[i]))
                {
                    currentProvince.Points.Adm = SaveFile[i].Split('=')[1];
                    currentProvince.Points.AdmId = i;

                    closeId = i;
                    break;
                }
            }

            for (int i = closeId; i < closeId + 10; i++)
            {
                if (regExProd.IsMatch(SaveFile[i]))
                {
                    currentProvince.Points.Dip = SaveFile[i].Split('=')[1];
                    currentProvince.Points.DipId = i;
                    break;
                }
            }

            for (int i = closeId; i < closeId + 10; i++)
            {
                if (regExManPow.IsMatch(SaveFile[i]))
                {
                    currentProvince.Points.Mil = SaveFile[i].Split('=')[1];
                    currentProvince.Points.MilId = i;
                    break;
                }
            }
        }

        public List<string> GetProvincesOfContry(string selectedCountry)
        {
            ProvincesOfCountry.Clear();
            ProvincesOfCountry.AddRange(Provinces.Where(province => selectedCountry == province.Owner));
            return ProvincesOfCountry.Select(p => p.Name).ToList();
        }

        public List<Province> GetProvinces(string[] provinceNames)
        {
            var tempProvinces = new List<Province>();
            var list = ProvincesOfCountry.Where(province => provinceNames.Contains(province.Name));
            foreach (var province in list)
            {
                ChangeProvinceParameters(province);
                tempProvinces.Add(province);
            }

            return tempProvinces;
        }

        public void ClearLists()
        {
            Countries.Clear();
            Provinces.Clear();
        }
    }
}