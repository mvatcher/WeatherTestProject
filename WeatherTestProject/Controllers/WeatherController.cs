using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeatherProject.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        [HttpGet("[action]/{city}")]
        public async Task<IActionResult> City(string city, string state)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=ab2ac8f50aa62fc7ddfdce05ec5b1fac&units=imperial");
                    var responseState = await client.GetAsync($"/data/2.5/weather?q={state}&appid=ab2ac8f50aa62fc7ddfdce05ec5b1fac&units=imperial");
                    response.EnsureSuccessStatusCode();
                    responseState.EnsureSuccessStatusCode();

                    var stringResultState = await responseState.Content.ReadAsStringAsync();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                    var rawWeatherState = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResultState);
                    return Ok(new
                    {
                        Temp = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        City = rawWeather.Name,
                        Wind = rawWeather.Wind.Speed,
                        
                        StateTemp = rawWeatherState.Main.Temp,
                        StateSummary = string.Join(",", rawWeatherState.Weather.Select(x => x.Main)),
                        State = rawWeatherState.Name,
                        StateWind = rawWeatherState.Wind.Speed

                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

    }  

}

