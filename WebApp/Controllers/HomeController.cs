using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApp.Models;

namespace VehicleManagementMVC.Controllers
{
    public class HomeController : Controller
    {
        string baseURL = "https://localhost:7238/";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            var data = new ServiceResponse<List<Vehicle>>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //  client.DefaultRequestHeaders.Add("Authorization", "bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI1IiwibmFtZSI6IkFjaHl1dCIsIm5iZiI6MTY1NjU3Mzk0NSwiZXhwIjoxNjU2NjYwMzQ1LCJpYXQiOjE2NTY1NzM5NDV9.iOXew5TLSP86bppxbz6bZHlFhHtEZ-1YBfFR-6GZgNJCduVAqoXvcX81OyfdDLy6_YM-UZKdiGKy2rnLH1mPhA"


                HttpResponseMessage getData = await client.GetAsync("api/Vehicle/GetAll"); 

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<ServiceResponse<List<Vehicle>>>(results);
                }
                else
                {
                    Console.WriteLine("Error calling API");
                }
                ViewData.Model = data;
            }

            return View();
        }

        
    }
}