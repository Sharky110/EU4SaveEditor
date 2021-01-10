namespace EU4SaveEditorWPF.Models
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

        public string Adm { get; set; }
        public int AdmId;

        public string Dip { get; set; }
        public int DipId;

        public string Mil { get; set; }
        public int MilId;

        public string OriginalCulture { get; set; }
        public string CurrentCulture { get; set; }

        public string OriginalReligion { get; set; }
        public string CurrentReligion { get; set; }
    }
}
