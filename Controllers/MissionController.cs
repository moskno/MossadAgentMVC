using Microsoft.AspNetCore.Mvc;
using MossadAgentMVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text.Json.Nodes;

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
            List<MissionDetails> missionList = new List<MissionDetails>();
            HttpResponseMessage response = await _httpClient.GetAsync("mission/details");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<JObject>(data);

                if (responseObject != null && responseObject["message"] != null && responseObject["message"]["missionDetails"] != null)
                {
                    missionList = JsonConvert.DeserializeObject<List<MissionDetails>>(responseObject["message"]["missionDetails"].ToString());
                }
            }
            return View(missionList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            MissionDetails missionDetails = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"mission/details/{id}");
                
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data);

                if (jsonObject != null &&
                    jsonObject["message"] != null &&
                    jsonObject["message"]["missionDetails"] != null)
                {
                    missionDetails = jsonObject["message"]["missionDetails"].ToObject<MissionDetails>();
                }
            }

            if (missionDetails == null)
            {
                return NotFound();
            }

            return View(missionDetails);
        }

    }
}
