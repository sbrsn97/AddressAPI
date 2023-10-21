using AddressAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;

namespace AddressAPI.Controllers
{
    
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private List<City> ReadFromJson()
        {
            List<City> addressData = new List<City>();
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(basePath, "AddressJson", "data.json");

            string jsonContent = System.IO.File.ReadAllText(filePath);
            addressData = JsonConvert.DeserializeObject<List<City>>(jsonContent);

            return addressData;
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            try
            {
                var data = ReadFromJson();
                var cities = data.Select(x => x.Name).ToList();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public ActionResult GetTowns(string city)
        {
            try
            {
                var data = ReadFromJson();
                var towns = data.FirstOrDefault(x => x.Name.ToLower() == city.ToLower())?.Towns;
                var townNames = towns.Select(x => x.Name).ToList();
                return Ok(townNames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public ActionResult GetDistricts(string town,string city)
        {
            try
            {
                var data = ReadFromJson();
                var districts = data
                .Where(x=>x.Name.ToLower() == city.ToLower())
                .SelectMany(x => x.Towns)
                .FirstOrDefault(t => t.Name.ToLower() == town.ToLower())?
                .Districts;
                var districtNames = districts.Select(x => x.Name).ToList();
                return Ok(districtNames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public ActionResult GetQuarters(string district,string town,string city)
        {
            try
            {
                var data = ReadFromJson();
                var quarters = data
                .Where(x => x.Name.ToLower() == city.ToLower())
                .SelectMany(x => x.Towns)
                .Where(x => x.Name.ToLower() == town.ToLower())
                .SelectMany(town => town.Districts)
                .Where(x => x.Name.ToLower() == district.ToLower())
                .FirstOrDefault(d => d.Name.ToLower() == district.ToLower())?
                .Quarters;
                var quartersNames = quarters.Select(x => x.Name).ToList();
                return Ok(quartersNames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
