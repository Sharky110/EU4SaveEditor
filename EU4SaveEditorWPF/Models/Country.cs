using System.Collections.Generic;

namespace EU4SaveEditorWPF.Models
{
    public class Country
    {
        public Country(string vName = "Useless", int vId = 0)
        {
            Name = vName;
            Id = vId;
        }

        public string Name { get; set; }
        public int Id { get; set; }

        public static readonly Dictionary<string, string> CountryNames = new Dictionary<string, string>()
        {
            {"MOS", "Moscovia (MOS)"},
            {"RUS", "Russia (RUS)"}
        };
    }
}
