using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class VehicleController : Controller
    {
        string baseAddress = "https://localhost:7238/";
        string userToken = "bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6Inh5ekBnbWFpbC5jb20iLCJuYmYiOjE2NTY2ODM5MDksImV4cCI6MTY1Njc3MDMwOSwiaWF0IjoxNjU2NjgzOTA5fQ.DPKYo4yWjTttDk8d9zKJ14C8TvOnbV09uNg11Ib_sHQ5-xRkrwgi7KSeZWouHPp9NaQumau6VFodPB5dwg0MFg";

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List()
        {
            var data = new ServiceResponse<List<GetVehicleDto>>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Authorization", userToken);
                HttpResponseMessage getData = await client.GetAsync("api/Vehicle/GETALL");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<ServiceResponse<List<GetVehicleDto>>>(results);
                }
                else
                {
                    Console.WriteLine("Error in consuming web API.");
                }
                ViewData.Model = data;
            }
            return View();
        }
    }
}