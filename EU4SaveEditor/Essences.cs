using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4SaveEditor
{
    class Country
    {
        public Country(string Vname = "Useless", int Vid = 0)
        {
            CountryName = Vname;
            CountryId = Vid;
        }

        public string CountryName { get; set; }
        public int CountryId { get; set; }
    }

    class Province
    {
        public Province(string Vname = "Useless", int Vid = 0, int Vindex = 0, string owner = "Useless", int OwnId = 0)
        {
            ProvinceName = Vname;
            ProvinceId = Vid;
            ProvinceIndex = Vindex;
            OwnerName = owner;
            OwnerId = OwnId;
        }

        public string ProvinceName { get; set; }
        public int ProvinceId { get; set; } //Province position in save file
        public int ProvinceIndex { get; set; } //Province index

        public string OwnerName { get; set; }
        public int OwnerId { get; set; }

        public string Tax { get; set; }
        public int TaxId { get; set; }

        public string Prod { get; set; }
        public int ProdId { get; set; }

        public string ManPow { get; set; }
        public int ManPowId { get; set; }
    }
}
