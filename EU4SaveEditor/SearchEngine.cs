using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace EU4SaveEditor
{
    class SearchEngine
    {
        static bool duplicate = false;

        public static void FindAllCountries(string[] FileRows, ref List<Country> Countries)
        {
            string CountryName;
            Regex countryRegEx = new Regex("country=\"[A-Z]{3}\"");
            int index = 1;

            foreach (string str in FileRows)
            {
                if (countryRegEx.IsMatch(str))
                {
                    CountryName = str.Split('\"')[1];
                    for (int i = 0; i < Countries.Count; i++)
                    {
                        if (CountryName == Countries[i].CountryName)
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (duplicate == false)
                    {
                        Countries.Add(new Country(CountryName, index));
                    }
                    duplicate = false;
                }
                index++;
            }
        }

        public static void FindAllProvinces(string[] FileRows, ref List<Province> Provinces, List<Country> Countries)
        {
            string ProvinceName;
            int OwnerId = 0;
            int ProvinceCounter = 0;
            Regex ProvRegEx = new Regex("name=\"[A-Z][a-z]*"+@"\s"+"*[A-Z]*[a-z]*\"$", RegexOptions.Singleline);
            int index = 1;
            foreach (string str in FileRows)
            {
                if (ProvRegEx.IsMatch(str))
                {
                    ProvinceName = str.Split('\"')[1];
                    for (int i = 0; i < Provinces.Count; i++)
                    {
                        if (ProvinceName == Provinces[i].ProvinceName)
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (duplicate == false)
                    {
                        string OwnerName = SetCountryForProvince(index, FileRows[index]);
                        if (OwnerName != "Not Province")
                        {
                            for (int i = 0; i < Countries.Count; i++)
                            {
                                if (Countries[i].CountryName == OwnerName)
                                {
                                    OwnerId = Countries[i].CountryId;
                                    break;
                                }
                            }
                            Provinces.Add(new Province(ProvinceName, index, ProvinceCounter, OwnerName, OwnerId));
                            ProvinceCounter++;
                        }
                    }
                    duplicate = false;
                }
                index++;
            }
        }

        public static string SetCountryForProvince(int index, string targetString)
        {
            string CountryName = "Not Province";
            Regex countryRegEx = new Regex("owner=\"[A-Z]{3}\"");
            Regex countryRegEx2 = new Regex("previ");
            if (countryRegEx.IsMatch(targetString))
            {
                CountryName = targetString.Split('\"')[1];
            }
            if (countryRegEx2.IsMatch(targetString))
            {
                CountryName = "_No Owner";
            }
            return CountryName;
        }

        public static void FindProvinceParameters(string[] FileRows, ref List<Province> currentProvinces, int currentProv)
        {
            int StartSearchId = currentProvinces[currentProv].ProvinceId;
            int closeId=0;
            Regex RegExTax = new Regex("base_tax=");
            Regex RegExProd = new Regex("base_production=");
            Regex RegExManPow = new Regex("base_manpower=");
            List<string> ProvinceInfo = new List<string>();
            for (int i = StartSearchId; i < StartSearchId + 100; i++)
            {
                if (RegExTax.IsMatch(FileRows[i]))
                {
                    currentProvinces[currentProv].Tax = FileRows[i].Split('=')[1];
                    currentProvinces[currentProv].TaxId = i;
                    closeId = i;
                    break;
                }
            }
            for (int i = closeId; i < closeId + 10; i++)
            {
                if (RegExProd.IsMatch(FileRows[i]))
                {
                    currentProvinces[currentProv].Prod = FileRows[i].Split('=')[1];
                    currentProvinces[currentProv].ProdId = i;
                    break;
                }
            }
            for (int i = closeId; i < closeId + 10; i++)
            {
                if (RegExManPow.IsMatch(FileRows[i]))
                {
                    currentProvinces[currentProv].ManPow = FileRows[i].Split('=')[1];
                    currentProvinces[currentProv].ManPowId = i;
                }
            }
        }
    }
}
