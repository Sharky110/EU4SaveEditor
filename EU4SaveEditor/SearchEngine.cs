using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EU4SaveEditor
{
    class SearchEngine
    {
        public List<string> Countries = new List<string>();
        public List<int> CountriesID = new List<int>();

        public List<string> Provs = new List<string>();
        public List<int> ProvsID = new List<int>();

        bool duplicate = false;

        public void FindAllCountries(string[] FileRows)
        {
            string strCountry;
            Regex countryRegEx = new Regex("country=\"[A-Z]{3}\"");
            int index = 1;

            foreach (string str in FileRows)
            {
                if (countryRegEx.IsMatch(str))
                {
                    strCountry = str.Split('\"')[1];
                    for (int i = 0; i < Countries.Count; i++)
                    {
                        if (strCountry == Countries[i])
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (duplicate == false)
                    {
                        Countries.Add(strCountry);
                        CountriesID.Add(index);
                    }
                    duplicate = false;
                }
                index++;
            }
        }

        public void FindAllProvinces(string[] FileRows)
        {
            string strProv;
            //name = "Borisoglebsk"
            //owner = "RYA"
            Regex ProvRegEx = new Regex("name=\"[A-Z][a-z]{0,}\"$", RegexOptions.Singleline);
            int index = 1;
            foreach (string str in FileRows)
            {
                if (ProvRegEx.IsMatch(str))
                {
                    strProv = str.Split('\"')[1];
                    //strCountry = strCountry.Remove(3);
                    for (int i = 0; i < Provs.Count; i++)
                    {
                        if (strProv == Provs[i])
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (duplicate == false)
                    {
                        Provs.Add(strProv);
                        ProvsID.Add(Convert.ToInt32(FileRows[index]));
                    }
                    duplicate = false;
                }
                index++;
            }
        }
    }
}
