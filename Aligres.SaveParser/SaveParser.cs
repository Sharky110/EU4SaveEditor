using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aligres.SaveParser
{
    public class SaveParser
    {
        #region ----- Variables -------------------------------------------------------------------

        private readonly List<Country> Countries;
        public readonly List<Province> Provinces;
        public readonly List<Province> ProvincesOfCountry;
        public readonly List<int> SelectedProvincesId;
        public int CurrentProvince;
        public string[] SaveFile;

        #endregion --------------------------------------------------------------------------------

        #region ----- Singleton -------------------------------------------------------------------

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

        #endregion --------------------------------------------------------------------------------

        public void FindAllCountries()
        {
            var countryRegEx = new Regex("country=\"[A-Z]{3}\"");

            var countries = SaveFile.Select((value, index) => new {i = index, s = value})
                .Where(t => countryRegEx.IsMatch(t.s))
                .Select(r => new Country { Name = r.s.Split('\"')[1], Id = r.i + 1})
                .GroupBy(r => r.Name)
                .Select(r => r.First())
                .ToList();

            Countries.AddRange(countries);
        }

        public void FindAllProvinces()
        {
            var index = 0;
            var provinceCounter = 0;

            string provinceName;

            var provRegEx = new Regex("name=\"[A-Z]*[a-z]*"+ @"\s" + "*[A-Z][a-z]*\"", RegexOptions.Singleline);

            foreach (string str in SaveFile)
            {
                index++;

                if (!provRegEx.IsMatch(str))
                    continue;

                provinceName = str.Split('\"')[1];

                var isProvinceAlreadyExists = Provinces.Any(p => p.Name == provinceName);
                if (isProvinceAlreadyExists)
                    continue;

                var ownerString = SaveFile[index];
                var ownerName = GetOwnerName(ownerString);

                if (ownerName == "Not Province")
                    continue;

                Provinces.Add(new Province(provinceName, index, provinceCounter, ownerName));
                provinceCounter++;
            }
        }

        public List<string> GetCountries()
        {
            var list = Provinces.Select(p => p.OwnerName).Distinct().ToList();
            list.Sort();
            return list;
        }

        private string GetOwnerName(string targetString)
        {
            var ownerPattern = new Regex("owner=\"[A-Z]{3}\"");
            var noOwnerPattern = new Regex("previ");

            if (ownerPattern.IsMatch(targetString))
                return targetString.Split('\"')[1];
            else if (noOwnerPattern.IsMatch(targetString))
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
                    currentProvince.Adm = SaveFile[i].Split('=')[1];
                    currentProvince.AdmId = i;

                    closeId = i;
                    break;
                }
            }

            for (int i = closeId; i < closeId + 10; i++)
            {
                if (regExProd.IsMatch(SaveFile[i]))
                {
                    currentProvince.Dip = SaveFile[i].Split('=')[1];
                    currentProvince.DipId = i;
                    break;
                }
            }

            for (int i = closeId; i < closeId + 10; i++)
            {
                if (regExManPow.IsMatch(SaveFile[i]))
                {
                    currentProvince.Mil = SaveFile[i].Split('=')[1];
                    currentProvince.MilId = i;
                    break;
                }
            }
        }

        public List<string> GetProvincesOfContry(string selectedCountry)
        {
            ProvincesOfCountry.Clear();
            ProvincesOfCountry.AddRange(Provinces.Where(province => selectedCountry == province.OwnerName));
            return ProvincesOfCountry.Select(p => p.Name).ToList();
        }

        public Province GetProvince(string provinceName)
        {
            Province tempProvince = new Province();
            foreach (var province in ProvincesOfCountry.Where(province => province.Name == provinceName))
            {
                ChangeProvinceParameters(province);
                CurrentProvince = province.Id;
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