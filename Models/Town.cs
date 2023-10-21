namespace AddressAPI.Models
{
    public class Town
    {
        public string Name { get; set; }
        public List<District> Districts { get; set; }
    }
}