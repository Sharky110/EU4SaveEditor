namespace EU4SaveEditorWPF.Models
{
    public class Relation
    {
        public Relation(int index, string countryName, string opinion)
        {
            Index = index;
            CountryName = countryName;
            Opinion = opinion;
        }
        
        public int Index { get; set; }
        public string CountryName { get; set; }
        public string Opinion { get; set; }
    }
}