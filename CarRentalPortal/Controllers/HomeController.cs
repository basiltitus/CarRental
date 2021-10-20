using CarRentalPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarRentalPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client;
        public async Task<List<CarTable>> RetrieveCarList()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.GetAsync("Car/getlist");
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                List<CarTable> carList = JsonConvert.DeserializeObject<List<CarTable>>(stringData);
                return carList;
            }
            return null;
        }
        public HomeController(ILogger<HomeController> logger, HttpClient client)
        {
            this.client = client;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserAuth item)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("auth/signin", item);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(stringData);
                    if (userProfile.token != "unavailable")
                    {
                        HttpContext.Session.SetString("_token", userProfile.token);
                        if (item.UserName.Contains("admin"))
                        {
                            HttpContext.Session.SetString("_userType", "Admin");
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            return RedirectToAction("AdminPortal");
                        }
                        else
                        {
                            HttpContext.Session.SetString("_userType", "Customer");
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            return RedirectToAction("UserPortal");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid User Credentials";
                        return View(item);
                    }
                }
                else
                {
                    return RedirectToAction("ErrorPage");
                }
            }
            else
                return View(item);
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserTable item)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("auth/signup", item);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(stringData);
                    if (userProfile.token != "unavailable")
                    {
                        HttpContext.Session.SetString("_token", userProfile.token);
                        if (item.UserName.Contains("admin"))
                        {
                            HttpContext.Session.SetString("_userType", "Admin");
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            return RedirectToAction("AdminPortal");
                        }
                        else
                        {
                            HttpContext.Session.SetString("_userType", "Customer");
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            return RedirectToAction("UserPortal");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User Already Exists!";
                        return View(item);
                    }

                }
                return RedirectToAction("Index");
            }
            return View(item);
        }
        public IActionResult AdminPortal()
        {
            if (HttpContext.Session.GetString("_userType") == "Admin")
                return View();
            else
                return RedirectToAction("UnauthorizedPage");
        }
        [HttpPost]
        public async Task<IActionResult> AdminPortal(CarTable cardetails)
        {
            if (ModelState.IsValid)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.PostAsJsonAsync("Car/adddetails", cardetails);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    bool carAdded = JsonConvert.DeserializeObject<Boolean>(stringData);
                    if (carAdded)
                    {
                        ViewBag.SuccessMessage = "Added";
                        ModelState.Clear();
                        return View();
                    }
                }
            }
            ViewBag.SuccessMessage = "";
            return View();
        }
        public async Task<IActionResult> UserPortal()
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            int pendingOrders = 0;
            int userId = (int)HttpContext.Session.GetInt32("_userId");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.GetAsync("Order/userId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                List<OrderTable> orderList = JsonConvert.DeserializeObject<List<OrderTable>>(stringData);
                orderList.ForEach((item) =>
                {
                    if (!item.Completed)
                    {
                        ++pendingOrders;
                    }
                });
                ViewBag.PendingOrders = pendingOrders;
                return View();
            }
            return View();
        }
        public async Task<IActionResult> CarList()
        {
            if (HttpContext.Session.GetString("_userType") == "Customer" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");

            List<CarTable> carList = await RetrieveCarList();
            return View(carList);

        }
        public async Task<IActionResult> EditCar(int id)
        {
            if (HttpContext.Session.GetString("_userType") == "Customer" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.GetAsync("Car/" + id);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                CarTable carDetails = JsonConvert.DeserializeObject<CarTable>(stringData);
                return View(carDetails);
            }
            return View();
        }
        public IActionResult ErrorPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditCar(CarTable car)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.PostAsJsonAsync("Car/updatecar", car);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                bool updated = JsonConvert.DeserializeObject<bool>(stringData);
                if (updated)
                {
                    return RedirectToAction("CarList");
                }
            }
            return RedirectToAction("ErrorPage");
        }

        public async Task<IActionResult> DeleteCar(int id)
        {

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.PostAsJsonAsync("Car/deletecar", id);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                bool deleted = JsonConvert.DeserializeObject<bool>(stringData);
                if (deleted)
                    return RedirectToAction("CarList");
            }
            return RedirectToAction("ErrorPage");
        }

        public async Task<IActionResult> RentCar()
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            int pendingOrders = 0;
            int userId = (int)HttpContext.Session.GetInt32("_userId");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.GetAsync("Order/userId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                List<OrderTable> orderList = JsonConvert.DeserializeObject<List<OrderTable>>(stringData);
                orderList.ForEach((item) =>
                {
                    if (!item.Completed)
                    {
                        ++pendingOrders;
                    }
                });
            }
            ViewBag.PendingOrders = pendingOrders;
            ViewBag.CarListed = (List<CarTable>)await RetrieveCarList();
            ViewBag.CarVarients = Enum.GetNames(typeof(CarVarient));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RentCar(OrderTable order)
        {
            if (ModelState.IsValid)
            {
                order.UserId = (int)HttpContext.Session.GetInt32("_userId");
                int noOfDays = (int)(order.ToDate - order.FromDate).TotalDays;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Car/" + order.CarId);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    CarTable carDetails = JsonConvert.DeserializeObject<CarTable>(stringData);
                    order.Total = noOfDays * carDetails.ChargePerDay;
                    order.Completed = false;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                    HttpResponseMessage orderResponse = await client.PostAsJsonAsync("Order/addorder", order);
                    if (orderResponse.IsSuccessStatusCode)
                    {
                        var orderStringData = orderResponse.Content.ReadAsStringAsync().Result;
                        int orderId = JsonConvert.DeserializeObject<int>(orderStringData);
                        if (orderId >= 100)
                        {
                            return RedirectToAction("PaymentPage", new { orderId = orderId, type = "order", total = 0, ExtraDays = 0 });
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("RentCar");
            }
            return RedirectToAction("ErrorPage");
        }
        public async Task<IActionResult> PaymentPage(int orderId, string type, int total, int ExtraDays)
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            if (type == "order")
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Order/" + orderId);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    OrderTable order = JsonConvert.DeserializeObject<OrderTable>(stringData);
                    ViewBag.OrderId = order.OrderId;
                    ViewBag.FromDate = order.FromDate;
                    ViewBag.ToDate = order.ToDate;
                    ViewBag.Total = order.Total;
                    return View();
                }
            }
            else if (type == "fine")
            {
                HttpResponseMessage response = await client.GetAsync("Order/ExtraDays?orderId=" + orderId + "&extraDays=" + ExtraDays);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    bool updated = JsonConvert.DeserializeObject<bool>(stringData);
                    if (updated)
                    {
                        ViewBag.OrderId = orderId;
                        ViewBag.Total = total;
                        return View();
                    }
                }
            }
            return RedirectToAction("ErrorPage");
        }
        public async Task<IActionResult> OrderSuccessfull(int orderId, int total)
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            PaymentTable payment = new PaymentTable
            {
                OrderId = orderId,
                UserId = (int)HttpContext.Session.GetInt32("_userId"),
                Total = total
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.PostAsJsonAsync("Payment/addPayment", payment);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                bool inserted = JsonConvert.DeserializeObject<bool>(stringData);
                if (inserted)
                {
                    return View();
                }
            }
            return RedirectToAction("ErrorPage");
        }
        public async Task<IActionResult> OrderHistory()
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            int userId = (int)HttpContext.Session.GetInt32("_userId");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.GetAsync("Order/userId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                List<OrderTable> orderList = JsonConvert.DeserializeObject<List<OrderTable>>(stringData);
                orderList.Reverse();
                if (orderList.Count == 0)
                    return RedirectToAction("ZeroHistoryPage");
                ViewBag.OrderList = orderList;
                return View(orderList);
            }
            return View();
        }
        public async Task<IActionResult> PaymentHistory()
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            int userId = (int)HttpContext.Session.GetInt32("_userId");
            HttpResponseMessage response = await client.GetAsync("Payment/" + userId);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                List<PaymentTable> paymentList = JsonConvert.DeserializeObject<List<PaymentTable>>(stringData);
                paymentList.Reverse();
                if (paymentList.Count == 0)
                    return RedirectToAction("ZeroHistoryPage");
                return View(paymentList);
            }
            return RedirectToAction("ErrorPage");
        }
        public async Task<IActionResult> CompleteTrip(int orderId)
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.GetAsync("Order/" + orderId);
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                OrderTable order = JsonConvert.DeserializeObject<OrderTable>(stringData);
                DateTime today = DateTime.Today;
                HttpResponseMessage carResponse = await client.GetAsync("Car/" + order.CarId);
                if (carResponse.IsSuccessStatusCode)
                {
                    var carStringData = carResponse.Content.ReadAsStringAsync().Result;
                    CarTable car = JsonConvert.DeserializeObject<CarTable>(carStringData);
                    int noOfDays;
                    if (order.ToDate < today)
                    {
                        noOfDays = (int)(today - order.ToDate).TotalDays;
                        ViewBag.ExtraDays = noOfDays;
                        ViewBag.OrderId = order.OrderId;
                        ViewBag.FineAmount = noOfDays * (car.ChargePerDay * 1.5);
                        return View();
                    }
                    else
                    {
                        HttpResponseMessage updateResponse = await client.GetAsync("Order/ExtraDays?orderId=" + order.OrderId + "&extraDays=0");
                        if (updateResponse.IsSuccessStatusCode)
                        {
                            var updateStringData = updateResponse.Content.ReadAsStringAsync().Result;
                            bool updated = JsonConvert.DeserializeObject<bool>(updateStringData);
                            if (updated)
                                return RedirectToAction("UserPortal");
                        }
                    }
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("_userType", "");
            HttpContext.Session.SetInt32("_userId", 0);
            HttpContext.Session.SetString("_token", "");
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UnauthorizedPage()
        {
            return View();
        }
        public IActionResult ZeroHistoryPage()
        {
            return View();
        }
    }
}
