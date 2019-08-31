namespace Aligres.SaveParser
{
    public class Country
    {
        public Country(string vName = "Useless", int vId = 0)
        {
            CountryName = vName;
            CountryId = vId;
        }

        public string CountryName { get; set; }
        public int CountryId { get; set; }
    }
}
