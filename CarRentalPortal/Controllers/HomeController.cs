using CarRentalPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        public IActionResult AdminSignIn()
        {
            if (TempData.ContainsKey("AdminAuthorized") && TempData["AdminAuthorized"].ToString() == "authorized")
                return View();
            else
                return RedirectToAction("AdminSecurity", new { pageId = "signin" });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminSignIn(UserAuth item)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("auth/signin", item);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(stringData);
                    if (userProfile.token != "unavailable" && userProfile.role == "admin")
                    {
                        HttpContext.Session.SetString("_token", userProfile.token);

                        HttpContext.Session.SetString("_userType", "Admin");
                        HttpContext.Session.SetInt32("_userId", userProfile.userId);
                        return RedirectToAction("AdminPortal");

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
        
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserAuth item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("auth/signin", item);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(stringData);
                        if (userProfile.token != "unavailable" && userProfile.role == "customer")
                        {
                            HttpContext.Session.SetString("_token", userProfile.token);
                            HttpContext.Session.SetString("_userType", "Customer");
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            return RedirectToAction("UserPortal");

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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        public IActionResult AdminSecurity( string pageID)
        {
            TempData["RedirectPage"] = pageID;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminSecurity(SecurityTable securityIdentity)
        {
            try
            {
                if (securityIdentity.SecurityCode != "")
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("auth/adminsecuritycheck", securityIdentity);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        bool authorized = JsonConvert.DeserializeObject<bool>(stringData);
                        if (authorized)
                        {
                            TempData["AdminAuthorized"] = "authorized";
                            if (TempData.ContainsKey("RedirectPage") && TempData["RedirectPage"].ToString() == "signin")
                            {
                                return RedirectToAction("AdminSignIn");
                            }
                            else if (TempData.ContainsKey("RedirectPage") && TempData["RedirectPage"].ToString() == "signup")
                            {
                                return RedirectToAction("AdminSignUp");
                            }
                            else
                            {
                                return RedirectToAction("ErrorPage");
                            }
                        }
                    }
                }
                ViewBag.ErrorMessage = "Please check your security Passcode";
                return View();

            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
                }
        public IActionResult AdminSignUp()
        {
            if (TempData.ContainsKey("AdminAuthorized") && TempData["AdminAuthorized"].ToString() == "authorized")
                return View();
            else
                return RedirectToAction("AdminSecurity",new {pageId="signup" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminSignUp(UserTable item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item.Role = "admin";
                    HttpResponseMessage response = await client.PostAsJsonAsync("auth/signup", item);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(stringData);
                        if (userProfile.token != "unavailable")
                        {
                            HttpContext.Session.SetString("_token", userProfile.token);

                            HttpContext.Session.SetString("_userType", "Admin");
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            return RedirectToAction("AdminPortal");

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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserTable item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item.Role = "customer";
                    HttpResponseMessage response = await client.PostAsJsonAsync("auth/signup", item);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(stringData);
                        if (userProfile.token != "unavailable")
                        {
                            HttpContext.Session.SetString("_token", userProfile.token);

                            HttpContext.Session.SetString("_userType", "Customer");
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            return RedirectToAction("UserPortal");

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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
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
            try
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
                ViewBag.SuccessMessage = "error";
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        public async Task<IActionResult> UserPortal()
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction("UnauthorizedPage");
                
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
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
            try
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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditCar(CarTable car)
        {
            try
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
                return RedirectToAction("EditCar", new { id = car.CarId });
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        public IActionResult ErrorPage()
        {
            return View();
        }

        public async Task<IActionResult> DeleteCar(int id)
        {

            try
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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }

        public async Task<IActionResult> RentCar()
        {
            try
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
                        if (item.Completed=="pending")
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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RentCar(OrderTable order)
        {
            try
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
                        if (noOfDays == 0)
                            noOfDays = 1;
                        order.Total = noOfDays * carDetails.ChargePerDay;
                        order.Completed = "unpaid";
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                        HttpResponseMessage orderResponse = await client.PostAsJsonAsync("Order/addorder", order);
                        if (orderResponse.IsSuccessStatusCode)
                        {
                            var orderStringData = orderResponse.Content.ReadAsStringAsync().Result;
                            int orderId = JsonConvert.DeserializeObject<int>(orderStringData);
                            if (orderId >= 100)
                            {
                                PaymentReciept reciept = new PaymentReciept
                                {
                                    OrderId =orderId,
                                    Type="order",
                                    Total=0,
                                    ExtraDays=0
                                };
                                TempData["PaymentReciept"]= JsonConvert.SerializeObject(reciept);
                                return RedirectToAction("PaymentPage");
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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }

        public async Task<IActionResult> PaymentPage()
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction("UnauthorizedPage");
                if (!TempData.ContainsKey("PaymentReciept"))
                    return RedirectToAction("ErrorPage");
                int userId = (int)HttpContext.Session.GetInt32("_userId");
                PaymentReciept reciept= JsonConvert.DeserializeObject<PaymentReciept>((string)TempData["PaymentReciept"]);
                TempData.Keep("PaymentReciept");
                if (reciept.Type == "order")
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                    HttpResponseMessage response = await client.GetAsync("Order/" +  reciept.OrderId+"/"+userId);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        OrderTable order = JsonConvert.DeserializeObject<OrderTable>(stringData);
                        ViewBag.OrderId = order.OrderId;
                        ViewBag.Total = order.Total;
                        ViewBag.Type = reciept.Type;
                        ViewBag.ExtraDays = reciept.ExtraDays;
                        return View();
                    }
                }
                else if (reciept.Type == "fine")
                {
                    ViewBag.Type = reciept.Type;
                    ViewBag.ExtraDays = reciept.ExtraDays;
                    ViewBag.OrderId = reciept.OrderId;
                            ViewBag.Total = reciept.Total;
                            return View();
                      
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        public async Task<IActionResult> PaymentPagePost(int orderId,int total,string type,int ExtraDays)
        {
            PaymentTable payment = new PaymentTable
            {
                OrderId = orderId,
                UserId = (int)HttpContext.Session.GetInt32("_userId"),
                Total = total
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage paymentResponse = await client.PostAsJsonAsync("Payment/addPayment", payment);
            if (paymentResponse.IsSuccessStatusCode)
            {
                var paymentStringData = paymentResponse.Content.ReadAsStringAsync().Result;
                bool inserted = JsonConvert.DeserializeObject<bool>(paymentStringData);
                if (inserted)
                {
                    if (type == "order")
                    {
                        HttpResponseMessage response = await client.GetAsync("order/makepayment/" + orderId);
                        if (response.IsSuccessStatusCode)
                        {
                            var stringData = response.Content.ReadAsStringAsync().Result;
                            bool updated = JsonConvert.DeserializeObject<bool>(stringData);
                            if (updated)
                            {
                                return RedirectToAction("OrderSuccessfull");

                            }
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
                                return RedirectToAction("OrderSuccessfull");
                            }
                        }
                    }
                }
            }
            
            return RedirectToAction("ErrorPage");
        }
        public async Task<IActionResult> OrderSuccessfull()
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction("UnauthorizedPage");
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        public async Task<IActionResult> OrderHistory()
        {
            try
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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        public async Task<IActionResult> PaymentHistory()
        {
            try
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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        public async Task<IActionResult> CompleteTrip(int orderId)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction("UnauthorizedPage");
                int userId = (int)HttpContext.Session.GetInt32("_userId");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Order/" + orderId+"/"+userId);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    OrderTable order = JsonConvert.DeserializeObject<OrderTable>(stringData);
                    if (order == null)
                        return RedirectToAction("ErrorPage");
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
                            PaymentReciept reciept = new PaymentReciept
                            {
                                OrderId = order.OrderId,
                                Type = "fine",
                                Total = (int)(noOfDays * (car.ChargePerDay * 1.5)),
                                ExtraDays = noOfDays
                            };
                            TempData["PaymentReciept"] = JsonConvert.SerializeObject(reciept);
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
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
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
