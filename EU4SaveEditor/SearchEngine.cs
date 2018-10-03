using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        public static void FindAllProvinces(string[] FileRows, ref List<Province> Provinces, List<Country> Countries)
        {
            //string[] test = new string[50000];
            //Array.Copy(FileRows,198330,test,0,49000);
            string ProvinceName;
            int OwnerId = 0;
            Regex ProvRegEx = new Regex("name=\"[A-Z][a-z]{0,}\"$", RegexOptions.Singleline);
            int index = 1;
            foreach (string str in FileRows)
            {
                if (ProvRegEx.IsMatch(str))
                {
                    ProvinceName = str.Split('\"')[1];
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
                        string OwnerName = SetCountryForProvince(index, FileRows[index]);
                        if (OwnerName != "Not Province")
                        {
                            for(int i =0; i<Countries.Count; i++)
                            {
                                if(Countries[i].Name == OwnerName)
                                {
                                    OwnerId = Countries[i].Id;
                                    break;
                                }
                            }
                            Provinces.Add(new Province(ProvinceName, index, OwnerName, OwnerId));
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

        public static void FindProvinceParameters(string[] FileRows, int index, Province province)
        {
            Regex RegExTax = new Regex("base_tax=");
            Regex RegExProd = new Regex("base_production=");
            Regex RegExManPow = new Regex("base_manpower=");
            List<string> ProvinceInfo = new List<string>();
            for (int i = index; i < index + 50; i++)
            {
                if (RegExTax.IsMatch(FileRows[i]))
                {
                    province.Tax = FileRows[i].Split('=')[1];
                    province.TaxId = i;
                }
                if (RegExProd.IsMatch(FileRows[i]))
                {
                    province.Prod = FileRows[i].Split('=')[1];
                    province.ProdId = i;
                }
                if (RegExManPow.IsMatch(FileRows[i]))
                {
                    province.ManPow = FileRows[i].Split('=')[1];
                    province.ManPowId = i;
                }
            }
        }
    }
}
