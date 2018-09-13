using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EU4SaveEditor
{
    class SearchEngine
    {
        bool duplicate = false;

        public void FindAllCountries(string[] FileRows, ref List<Country> Countries)
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
                        if (CountryName == Countries[i].Name)
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

        public void FindAllProvinces(string[] FileRows, ref List<Province> Provinces, List<Country> Countries)
        {
            string ProvinceName;
            Regex ProvRegEx = new Regex("name=\"[A-Z][a-z]{0,}\"$", RegexOptions.Singleline);
            int index = 1;
            foreach (string str in FileRows)
            {
                if (ProvRegEx.IsMatch(str))
                {
                    ProvinceName = str.Split('\"')[1];
                    //strCountry = strCountry.Remove(3);
                    for (int i = 0; i < Provinces.Count; i++)
                    {
                        if (ProvinceName == Provinces[i].Name)
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (duplicate == false)
                    {
                        string id = SetCountryId(index, FileRows[index], Countries);
                        if (id != "Not Province")
                        {
                            Provinces.Add(new Province(ProvinceName, index, id));
                        }
                    }
                    duplicate = false;
                }
                index++;
            }
        }

        public string SetCountryId(int index, string targetString, List<Country> Countries)
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

        public void FindProvinceProsperity(string[] FileRows, int index, Province province)
        {
            Regex RegExTax = new Regex("base_tax=");
            Regex RegExProd = new Regex("base_production=");
            Regex RegExManPow = new Regex("base_manpower=");
            List<string> ProvinceInfo = new List<string>();
            for (int i = index; i < index + 40; i++)
            {
                if (RegExTax.IsMatch(FileRows[i]))
                {
                    province.Tax = FileRows[i].Split('=')[1];
                }
                if (RegExProd.IsMatch(FileRows[i]))
                {
                    province.Prod = FileRows[i].Split('=')[1];
                }
                if (RegExManPow.IsMatch(FileRows[i]))
                {
                    province.ManPow = FileRows[i].Split('=')[1];
                }
            }
        }
    }
}
