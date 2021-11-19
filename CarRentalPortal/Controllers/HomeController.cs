using CarRentalPortal.Models;
using CarRentalPortal.Models.ViewModels;
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
        public async Task<List<CarModel>> RetrieveCarModelList()
        {


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.GetAsync("Car/getModellist");
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                List<CarModel> carModelList = JsonConvert.DeserializeObject<List<CarModel>>(stringData);
                return carModelList;
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
                        HttpContext.Session.SetInt32("_userId", userProfile.userId);
                        HttpContext.Session.SetString("_userName", userProfile.Name);
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
                            HttpContext.Session.SetString("_userName", userProfile.Name);
                            HttpContext.Session.SetString("_avatarUrl", userProfile.ImgUrl);
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
        public async Task<IActionResult> AdminSignUp(User item)
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
                            HttpContext.Session.SetString("_userName", userProfile.Name);
                            HttpContext.Session.SetString("_avatarUrl", userProfile.ImgUrl);
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
        public async Task<IActionResult> SignUp(User item)
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
                            HttpContext.Session.SetString("_userName", userProfile.Name);
                            HttpContext.Session.SetString("_avatarUrl", userProfile.ImgUrl);
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
        public IActionResult CreateCarModel()
        {
            if (HttpContext.Session.GetString("_userType") == "Admin")
                return View();
            else
                return RedirectToAction("UnauthorizedPage");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCarModel(CarModel cardetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cardetails.UserId = (int)HttpContext.Session.GetInt32("_userId");
                    cardetails.CreatedOn = DateTime.Now;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                    HttpResponseMessage response = await client.PostAsJsonAsync("Car/addmodeldetails", cardetails);
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
        public  IActionResult UserPortal()
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
        public async Task<IActionResult> UserProfile()
        {
            try
            {
                int userId = (int)HttpContext.Session.GetInt32("_userId");
                HttpResponseMessage response = await client.GetAsync("auth/getuser/" + userId);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    User user = JsonConvert.DeserializeObject<User>(stringData);
                    return View(user);
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception)
            {
                return RedirectToAction("ServerError");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> UserProfileAsync(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(user.ImgUrl == null)
            HttpContext.Session.SetString("_avatarUrl", "0");
            else
            HttpContext.Session.SetString("_avatarUrl", user.ImgUrl);
                    HttpResponseMessage response = await client.PutAsJsonAsync("auth", user);
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("_userName", user.Name);
                        return RedirectToAction("UserPortal");
                    }
                        return RedirectToAction("ErrorPage");
                }
                return View(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> CarModelList()
        {
            if (HttpContext.Session.GetString("_userType") == "Customer" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");

            List<CarModel> carModelList = await RetrieveCarModelList();
            return View(carModelList);

        }
        public async Task<IActionResult> CarListAsync()
        {
            HttpResponseMessage response = await client.GetAsync("car/getcarlist");
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                List<CarListVM> carList = JsonConvert.DeserializeObject<List<CarListVM>>(stringData);
                return View(carList);
            }
            return View();
        }
        public async Task<IActionResult> CreateCarAsync()
        {
            List<CarModel> carModelList = (List<CarModel>)await RetrieveCarModelList();
            ViewBag.ModelList = carModelList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    car.UserId = (int)HttpContext.Session.GetInt32("_userId");
                    car.CreatedOn = DateTime.Now;
                    HttpResponseMessage response = await client.PostAsJsonAsync("car/addcardetails", car);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        bool carAdded= JsonConvert.DeserializeObject<bool>(stringData);
                        if (carAdded)
                        {
                            ViewBag.SuccessMessage = "Added";
                            ModelState.Clear();
                            ViewBag.ModelList = (List<CarModel>)await RetrieveCarModelList();
                            
                            return View();
                        }
                    }
                }ViewBag.SuccessMessage = "error";
                List<CarModel> carModelList = (List<CarModel>)await RetrieveCarModelList();
                ViewBag.ModelList = carModelList;
                return RedirectToAction("CreateCar");
            }
            catch (Exception)
            {
                return RedirectToAction("ServerError");
            }
        }
        public async Task<IActionResult> EditCarModel(int id)
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
                    CarModel carDetails = JsonConvert.DeserializeObject<CarModel>(stringData);
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
        public async Task<IActionResult> EditCarModel(CarModel car)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.PostAsJsonAsync("Car/updatecarmodel", car);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    bool updated = JsonConvert.DeserializeObject<bool>(stringData);
                    if (updated)
                    {
                        return RedirectToAction("CarModelList");
                    }
                }
                return RedirectToAction("EditCarModel", new { id = car.CarModelId });
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
                HttpResponseMessage response = await client.PostAsJsonAsync("Car/deletecarmodel", id);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    bool deleted = JsonConvert.DeserializeObject<bool>(stringData);
                    if (deleted)
                        return RedirectToAction("CarModelList");
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
                    List<Order> orderList = JsonConvert.DeserializeObject<List<Order>>(stringData);
                    orderList.ForEach((item) =>
                    {
                        if (item.Completed=="pending")
                        {
                            ++pendingOrders;
                        }
                    });
                }
                ViewBag.PendingOrders = pendingOrders;
                ViewBag.CarListed = (List<CarModel>)await RetrieveCarModelList();
                ViewBag.CarVarients = Enum.GetNames(typeof(CarVarient));
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("ErrorPage");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RentCar(Order order)
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
                        CarModel carDetails = JsonConvert.DeserializeObject<CarModel>(stringData);
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
                        Order order = JsonConvert.DeserializeObject<Order>(stringData);
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
            Payment payment = new Payment
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
                    List<Order> orderList = JsonConvert.DeserializeObject<List<Order>>(stringData);
                    orderList.Reverse();
                    if (orderList.Count == 0)
                        return RedirectToAction("ZeroHistoryPage");
                    ViewBag.OrderList = orderList;
                    return View(orderList);
                }
                return RedirectToAction("ErrorPage");
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
                    List<Payment> paymentList = JsonConvert.DeserializeObject<List<Payment>>(stringData);
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
                    Order order = JsonConvert.DeserializeObject<Order>(stringData);
                    if (order == null)
                        return RedirectToAction("ErrorPage");
                    DateTime today = DateTime.Today;
                    HttpResponseMessage carResponse = await client.GetAsync("Car/" + order.CarId);
                    if (carResponse.IsSuccessStatusCode)
                    {
                        var carStringData = carResponse.Content.ReadAsStringAsync().Result;
                        CarModel car = JsonConvert.DeserializeObject<CarModel>(carStringData);
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
