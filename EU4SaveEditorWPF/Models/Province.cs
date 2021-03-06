﻿namespace EU4SaveEditorWPF.Models
{
    public class Province
    {
        private static int idCounter = 0;

        public Province(string name = "Useless", int positionInFile = 0, string owner = "Useless")
        {
            Id = idCounter++;
            Name = name;
            PositionInFile = positionInFile;
            Owner = owner;
        }

        public string Name;
        public int Id;
        public int PositionInFile;

        public string Owner;

        public PowerPoints Points { get; set; } = new PowerPoints();

        public string OriginalCulture { get; set; }
        public string CurrentCulture { get; set; }

        public string OriginalReligion { get; set; }
        public string CurrentReligion { get; set; }
    }
}
