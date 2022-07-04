using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public static class StoreToekn
    {
        public static String token; 

        public static bool login = false;
    }
    public class VehicleController : Controller
    {
        string baseAddress = "https://localhost:7238/";
       
     
        string userToken = "bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmFtZSI6Inh5ekBnbWFpbC5jb20iLCJuYmYiOjE2NTY5MjQ4NzAsImV4cCI6MTY1NzAxMTI3MCwiaWF0IjoxNjU2OTI0ODcwfQ.Q9Kgi-I3UV2f4b_ohRHD7gq-iVJKV_RR9eWattMK4_VRr8byD4IuSl1VpqOWrQp2FLUhhpOzv2bJgpUHDLEbDQ";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult E1()
        {
            return View();
        }

        public async Task<ActionResult> List()
        {

            if(StoreToekn.login == false)
            {
                 return RedirectToAction("E1");  
            }
            var data = new ServiceResponse<List<GetVehicleDto>>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoreToekn.token);
                HttpResponseMessage getData = await client.GetAsync("Vehicle/GETALL");
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

        public async Task<ActionResult> Details(int id)
        {
            var data = new ServiceResponse<GetVehicleDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoreToekn.token);
                HttpResponseMessage getData = await client.GetAsync("Vehicle/" + id);
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<ServiceResponse<GetVehicleDto>>(results);

                }
                else
                {
                    Console.WriteLine("Error in consuming web API.");
                }
                ViewData.Model = data;
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddVehicleDto addVehicle)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoreToekn.token);
                    HttpResponseMessage getData = await client.PostAsJsonAsync<AddVehicleDto>("Vehicle/AddVehicle", addVehicle);
                    if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        Console.WriteLine("Error........");
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

       public ActionResult Delete(int id  )
       {
            return View();

        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id , GetVehicleDto vehicleDto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoreToekn.token);
                    HttpResponseMessage getData = await client.DeleteAsync("Vehicle/" + id);
                    if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        Console.WriteLine("Error........");
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( GetVehicleDto vehicle)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StoreToekn.token);
                    HttpResponseMessage getData = await client.PutAsJsonAsync("Vehicle/UpdateVehicle" , vehicle);
                    if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        Console.WriteLine("Error........");
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginDto user)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    HttpResponseMessage getData = await client.PostAsJsonAsync<LoginDto>("/Owner/LogIn", user);
                    if (getData.IsSuccessStatusCode)
                    {
                        StoreToekn.login = true;
                        
                        string results = getData.Content.ReadAsStringAsync().Result;
                       
                        var data =  JsonConvert.DeserializeObject<ServiceResponse<string>>(results);
                         StoreToekn.token = data.Data; 
                        
                        
                        return RedirectToAction(nameof(List));
                    }
                    else
                    {
                        Console.WriteLine("Error........");
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterDto user)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                   
                    HttpResponseMessage getData = await client.PostAsJsonAsync<RegisterDto>("/Owner/Register", user);
                    if (getData.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    else
                    {
                        Console.WriteLine("Error........");
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        



    }


}

