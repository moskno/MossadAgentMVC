using Microsoft.AspNetCore.Mvc;
using MossadAgentMVC.Models;

namespace MossadAgentMVC.Controllers
{
    public class MissionController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:44374/api");
        private readonly HttpClient _httpClient;

        public MissionController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Mission> missionLIst = new List<Mission>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/mission/Get");

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                missionLIst = JsonConvert.DeserializeObject<List<Mission>>(data);
            }
            return View(missionLIst);
        }
    }
}
