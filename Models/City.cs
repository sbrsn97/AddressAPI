namespace AddressAPI.Models
{
    public class City
    {
        public string Name { get; set; }
        public List<Town> Towns { get; set; }
    }
}
