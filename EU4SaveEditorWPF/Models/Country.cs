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
    }
}
