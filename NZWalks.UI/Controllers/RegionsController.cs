using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            //Get All Region from web api
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7271/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();
                
                 response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

            }
            catch (Exception ex)
            {
                //Log the exception
                throw;
            }
            return View(response);
        }

		[HttpGet]
		public async Task<IActionResult> Add()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Add(RegionCreateDto model)
		{
			//Get All Region from web api
			try
			{
				var client = _httpClientFactory.CreateClient();
                var httpRequest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri =new Uri("https://localhost:7271/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8,"application/json"),
                };
				var httpResponseMessage = await client.SendAsync(httpRequest);
				httpResponseMessage.EnsureSuccessStatusCode();

				var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response != null)
                {
                    return RedirectToAction("Index", "Regions");
                }
			}
			catch (Exception ex)
			{
				//Log the exception
				throw;
			}
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
            var client = _httpClientFactory.CreateClient();
            var httpResponse = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7271/api/regions/{id.ToString()}");
            if(httpResponse != null) 
            {
                return View(httpResponse);
            }
			return View(null);
		}
        [HttpPost]
		public async Task<IActionResult> Edit(RegionDto request)
        {
			var client = _httpClientFactory.CreateClient();
			var httpRequest = new HttpRequestMessage()
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri($"https://localhost:7271/api/regions/{request.Id}"),
				Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"),
			};
            var httpResponseMessage = await client.SendAsync(httpRequest);
			httpResponseMessage.EnsureSuccessStatusCode();
			var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
		
            if(response != null)
            {
                return RedirectToAction("Edit","Regions");
            }
			return View();
        }
		[HttpPost]
		public async Task<IActionResult> Delete(RegionDto request)
		{
			try
			{
				var client = _httpClientFactory.CreateClient();
				var httpResponse = await client.DeleteAsync($"https://localhost:7271/api/regions/{request.Id}");
				httpResponse.EnsureSuccessStatusCode();
				if (httpResponse != null)
				{
					return RedirectToAction("Index","Regions");
				}
			}
			catch (Exception ex)
			{
				//Log Exception
				throw;
			}
			return View("Edit");
		}
	}
}
