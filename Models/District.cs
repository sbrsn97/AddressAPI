namespace AddressAPI.Models
{
    public class District
    {
        public string Name { get; set; }
        public List<Quarter> Quarters { get; set; }
    }
}