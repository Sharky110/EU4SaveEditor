namespace Aligres.SaveParser
{
    public class Province
    {
        public Province(string vName = "Useless", int vId = 0, int vIndex = 0, string owner = "Useless")
        {
            Name = vName;
            PositionInFile = vId;
            Id = vIndex;
            OwnerName = owner;
        }

        public string Name;
        public int Id;
        public int PositionInFile;

        public string OwnerName;

        public string Tax;
        public int TaxId;

        public string Prod;
        public int ProdId;

        public string ManPow;
        public int ManPowId;

        public string OriginalCulture;
        public string CurrentCulture;

        public string OriginalReligion;
        public string CurrentReligion;
    }
}
