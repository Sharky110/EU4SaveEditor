namespace EU4SaveEditor
{
    class Country
    {
        public Country(string vName = "Useless", int vId = 0)
        {
            CountryName = vName;
            CountryId = vId;
        }

        public string CountryName { get; set; }
        public int CountryId { get; set; }
    }

    class Province
    {
        public Province(string vName = "Useless", int vId = 0, int vIndex = 0, string owner = "Useless", int ownId = 0)
        {
            ProvinceName = vName;
            ProvinceId = vId;
            ProvinceIndex = vIndex;
            OwnerName = owner;
            OwnerId = ownId;
        }

        public string ProvinceName { get; set; }
        public int ProvinceId { get; set; } //Province position in file
        public int ProvinceIndex { get; set; } //Province index from 0 to ...

        public string OwnerName { get; set; }
        public int OwnerId { get; set; }

        public string Tax { get; set; }
        public int TaxId { get; set; }

        public string Prod { get; set; }
        public int ProdId { get; set; }

        public string ManPow { get; set; }
        public int ManPowId { get; set; }

        public string OrigCltr { get; set; }
        public string Cltr { get; set; }

        public string OrigRlgn { get; set; }
        public string Rlgn { get; set; }
    }
}
