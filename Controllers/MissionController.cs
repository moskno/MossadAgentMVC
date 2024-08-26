using Microsoft.AspNetCore.Mvc;
using MossadAgentMVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MossadAgentMVC.Controllers
{
    public class MissionController : Controller
    {
        private readonly HttpClient _httpClient;

        public MissionController(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["ApiBaseAddress"]);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {   
            List<Mission> missionList = new List<Mission>();
            HttpResponseMessage response = await _httpClient.GetAsync("mission/Get");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                missionList = JsonConvert.DeserializeObject<List<Mission>>(data);
            }
            return View(missionList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            MissionDetails missionDetails = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"mission/GetDetails/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                missionDetails = JsonConvert.DeserializeObject<MissionDetails> (data);
            }
            return View(missionDetails);
        }
    }
}
