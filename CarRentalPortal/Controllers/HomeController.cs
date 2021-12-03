using CarRentalPortal.Models;
using CarRentalPortal.Models.ViewModels;
using CarRentalPortal.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SalesManagementSystem.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CarTransmission = CarRentalPortal.Models.CarTransmission;
using CarVarient = CarRentalPortal.Models.CarVarient;

namespace CarRentalPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyOptions _options;
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client;
        public async Task<List<CarModel>> RetrieveCarModelList()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
            HttpResponseMessage response = await client.GetAsync("Car/getModellist");
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<CarModel>>(stringData); ;
            }
            return null;

        }
        public async Task<List<string>> RetrieveCarVarients()
        {
            HttpResponseMessage response = await client.GetAsync("Car/carvarient");
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<string>>(stringData); ;
            }
            return new List<string>();
        }
        public HomeController(ILogger<HomeController> logger, HttpClient client, IOptions<MyOptions> options)
        {
            this.client = client;
            _logger = logger;
            _options = options.Value;
        }
        public IActionResult CatchAction(string e)
        {
            using (StreamWriter writetext = new StreamWriter("Error.txt", append: true))
            {
                var currentTime = DateTime.Now;
                writetext.WriteLine(currentTime + " : " + e);
            }
            return RedirectToAction("ErrorPage");

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
            try
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
                            HttpContext.Session.SetString("_avatarUrl", userProfile.ImgUrl);
                            return RedirectToAction("AdminPortal");

                        }
                        else if (userProfile.token != "unavailable" && userProfile.role == "super")
                        {
                            HttpContext.Session.SetString("_token", userProfile.token);
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            HttpContext.Session.SetString("_userName", userProfile.Name);
                            HttpContext.Session.SetString("_userType", "Super");
                            HttpContext.Session.SetInt32("_userId", userProfile.userId);
                            HttpContext.Session.SetString("_avatarUrl", userProfile.ImgUrl);
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }

        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            TempData["ForgotPassword"] = 0;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await client.GetAsync("auth/" + forgotPasswordVM.EmailId);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        bool exists = JsonConvert.DeserializeObject<bool>(stringData);
                        if (exists)
                        {
                            Random generator = new Random();
                            String otp = generator.Next(0, 1000000).ToString("D6");
                            TempData["ForgotPassword"] = 1;
                            HttpContext.Session.SetString("_otp", otp);
                            EmailSender sender = new EmailSender(_options);

                            HttpResponseMessage userIdResponse = await client.GetAsync("auth/getuserid/" + forgotPasswordVM.EmailId);
                            if (userIdResponse.IsSuccessStatusCode)
                            {
                                var userIdStringData = userIdResponse.Content.ReadAsStringAsync().Result;
                                int userId = JsonConvert.DeserializeObject<int>(userIdStringData);
                                HttpContext.Session.SetInt32("_userId", userId);
                            }
                            string message = "Dear user,<br/>Your request for password change has been recieved!.<br>OTP for changing password is :- " + otp;
                            await sender.SendEmailAsync(forgotPasswordVM.EmailId, "Password Reset !", message);
                            return RedirectToAction("PasswordChange");
                        }
                        else
                        {
                            TempData["ForgotPassword"] = -1;
                            return View();
                        }

                    }
                    return RedirectToAction(nameof(ErrorPage));
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult PasswordChange()
        {
            if (HttpContext.Session.GetString("_otp") == null || HttpContext.Session.GetString("_otp") == "")
            {
                return RedirectToAction("ErrorPage");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeVM newPassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (newPassword.OTP == HttpContext.Session.GetString("_otp"))
                    {
                        int userId = (int)HttpContext.Session.GetInt32("_userId");
                        HttpResponseMessage response = await client.GetAsync("auth/getuser/" + userId);
                        if (response.IsSuccessStatusCode)
                        {
                            var stringData = response.Content.ReadAsStringAsync().Result;
                            User user = JsonConvert.DeserializeObject<User>(stringData);
                            user.Password = newPassword.Password;
                            HttpResponseMessage updateResponse = await client.PutAsJsonAsync("auth", user);
                            if (updateResponse.IsSuccessStatusCode)
                            {
                                ViewBag.Message = "success";
                                HttpContext.Session.SetString("_otp", "");
                                HttpContext.Session.SetInt32("_userId", 0);
                                TempData["ForgotPassword"] = 0;
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Message = "invalid";
                        }

                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "invalid";
                        return View();
                    }
                }
                ViewBag.Message = "validationerror";
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
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
                            HttpContext.Session.SetString("_emailId", item.EmailId);
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult AdminSecurity(string pageID)
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult AdminSignUp()
        {
            if (TempData.ContainsKey("AdminAuthorized") && TempData["AdminAuthorized"].ToString() == "authorized")
                return View();
            else
                return RedirectToAction("AdminSecurity", new { pageId = "signup" });
        }
        [HttpPost]
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
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
                            HttpContext.Session.SetString("_emailId", item.EmailId);
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> AdminPortalAsync()
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                    HttpResponseMessage response = await client.GetAsync("Order/getadminrequests");
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        List<AdminRequestVM> requests = JsonConvert.DeserializeObject<List<AdminRequestVM>>(stringData);
                        requests = requests.Where(x => x.Status == "requested").ToList();
                        requests.Reverse();
                        return View(requests);
                    }

                    return View();
                }
                else
                    return RedirectToAction("UnauthorizedPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> RequestDetailAsync(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                    HttpResponseMessage response = await client.GetAsync("Order/getadminrequests");
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        List<AdminRequestVM> requests = JsonConvert.DeserializeObject<List<AdminRequestVM>>(stringData);
                        AdminRequestVM request = requests.Where(x => x.OrderId == id).SingleOrDefault();
                        ViewBag.ImgUrl = request.ImgUrl;
                        ViewBag.TodaysDate = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                        ViewBag.ToDate = request.ToDate;
                        ViewBag.ChargePerDay = request.ChargePerDay;
                        return View(request);
                    }

                    return View();
                }
                else
                    return RedirectToAction("UnauthorizedPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> CouponsList(string sortby, string sortorder)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                {
                    if (sortby == null)
                        ViewBag.SortBy = "created";
                    else
                        ViewBag.SortBy = sortby;
                    if (sortorder == null)
                        ViewBag.SortOrder = "ascending";
                    else
                        ViewBag.SortOrder = sortorder;
                    HttpResponseMessage response = await client.GetAsync("Coupon");
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        List<Coupon> Coupons = JsonConvert.DeserializeObject<List<Coupon>>(stringData);
                        return View(Coupons);
                    }
                    return RedirectToAction(nameof(ErrorPage));
                }
                return RedirectToAction(nameof(UnauthorizedPage));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult CreateCoupon()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCouponAsync(Coupon coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    coupon.CreatedBy = (int)HttpContext.Session.GetInt32("_userId");
                    coupon.CreatedOn = DateTime.Now;
                    coupon.Active = true;
                    HttpResponseMessage response = await client.PostAsJsonAsync("Coupon", coupon);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        bool couponAdded = JsonConvert.DeserializeObject<Boolean>(stringData);
                        if (couponAdded)
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> ChangeCouponActiveAsync(int id,bool active)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Customer")
                    return RedirectToAction("UnauthorizedPage");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Coupon/ChangeCouponStatus/" + id + "/" + active);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    bool deleted = JsonConvert.DeserializeObject<bool>(stringData);
                    if (deleted)
                        return RedirectToAction("CouponsList");
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ActivateCouponSelected([FromBody] int[] selectedId)
        {
            try
            {

                foreach (var item in selectedId)
                {
                    HttpResponseMessage response = await client.GetAsync("Coupon/ChangeCouponStatus/" + item + "/true");

                }
                return Json("All the coupons deleted successfully!");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeActivateCouponSelected([FromBody] int[] selectedId)
        {
            try
            {
                foreach (var item in selectedId)
                {
                    HttpResponseMessage response = await client.GetAsync("Coupon/ChangeCouponStatus/" + item + "/false");

                }
                return Json("All the coupons deleted successfully!");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> EditCoupon(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Customer")
                    return RedirectToAction("UnauthorizedPage");

                HttpResponseMessage response = await client.GetAsync("Coupon/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    Coupon coupon = JsonConvert.DeserializeObject<Coupon>(stringData);
                    if (coupon.CreatedBy== HttpContext.Session.GetInt32("_userId") || HttpContext.Session.GetString("_userType") == "Super")
                        return View(coupon);
                    return RedirectToAction(nameof(UnauthorizedPage));
                }

                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditCoupon(Coupon coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                    HttpResponseMessage response = await client.PutAsJsonAsync("Coupon", coupon);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        bool updated = JsonConvert.DeserializeObject<bool>(stringData);
                        if (updated)
                        {
                            return RedirectToAction("CouponsList");
                        }
                    }
                }
                return RedirectToAction("EditCoupon", new { id = coupon.CouponId });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> RequestApprovedAsync(string returndate, int fineamount, int orderid, int userid, int carmodelid)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                {
                    string userEmailId = null;
                    string userName = null;
                    HttpResponseMessage response = await client.GetAsync("order/" + orderid + "/" + userid);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        Order order = JsonConvert.DeserializeObject<Order>(stringData);
                        DateTime returnedDate = DateTime.Parse(returndate);
                        int noOfDays = (int)(returnedDate - order.ToDate).TotalDays;
                        if (noOfDays <= 0)
                        {
                            order.ExtraDays = 0;
                            order.FineAmount = fineamount;
                            order.Status = "completed";
                        }
                        else
                        {

                            HttpResponseMessage carResponse = await client.GetAsync("car/getcarmodel/" + carmodelid);
                            if (carResponse.IsSuccessStatusCode)
                            {
                                var carStringData = carResponse.Content.ReadAsStringAsync().Result;
                                CarModel car = JsonConvert.DeserializeObject<CarModel>(carStringData);
                                order.FineAmount = (int)(fineamount + (noOfDays * (car.ChargePerDay * 1.5)));
                            }
                            order.ExtraDays = noOfDays;
                            order.Status = "completed";
                        }


                        HttpResponseMessage updateResponse = await client.PostAsJsonAsync("order/updateorder", order);
                        if (updateResponse.IsSuccessStatusCode)
                        {
                            var updateStringData = updateResponse.Content.ReadAsStringAsync().Result;
                            bool updated = JsonConvert.DeserializeObject<bool>(updateStringData);
                            if (updated)
                            {
                                HttpResponseMessage userResponse = await client.GetAsync("auth/getuser/" + userid);
                                if (userResponse.IsSuccessStatusCode)
                                {
                                    var userStringData = userResponse.Content.ReadAsStringAsync().Result;
                                    User user = JsonConvert.DeserializeObject<User>(userStringData);
                                    userEmailId = user.EmailId;
                                    userName = user.Name;
                                    EmailSender sender = new EmailSender(_options);
                                    string message = null;
                                    if (order.FineAmount == 0)
                                        message = "Dear " + userName + "<br/>Your request for returning car is accepted!. Please login to our site to complete the trip.";
                                    else
                                    {
                                        message = "Dear " + userName + "<br/>Your request for returning car is accepted!. You have been charged with a fine for amount $ " + order.FineAmount + " for renting out the car for extra " + order.ExtraDays + ". Please pay the amount to complete the trip.";
                                        await sender.SendEmailAsync(userEmailId, "Return Approved !", message);
                                        return RedirectToAction("CompleteTrip", new { orderId = order.OrderId, userId = user.UserId });
                                    }
                                }

                                return RedirectToAction("AdminPortal");
                            }
                        }
                    }
                    return RedirectToAction(nameof(ErrorPage));
                }
                else
                    return RedirectToAction(nameof(UnauthorizedPage));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> RequestHistory(string? status, string? sortorder, string sortby, string search)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                {
                    if (status == null)
                        status = "all";
                    if (sortorder == null)
                        sortorder = "descending";
                    if (sortby == null)
                        sortby = "orderid";
                    if (search == null)
                        search = "";
                    ViewBag.Status = status;
                    ViewBag.SortOrder = sortorder;
                    ViewBag.SortBy = sortby;
                    ViewBag.Search = search;
                    if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                        HttpResponseMessage response = await client.GetAsync("Order/getadminrequests");
                        if (response.IsSuccessStatusCode)
                        {
                            var stringData = response.Content.ReadAsStringAsync().Result;
                            List<AdminRequestVM> requests = JsonConvert.DeserializeObject<List<AdminRequestVM>>(stringData);
                            requests = requests.Where(x => x.Status == "completed" || x.Status == "rejected").ToList();
                            if (status != "all")
                            {

                                if (status == "approved")
                                    requests = requests.Where(x => x.Status == "completed").ToList();
                                else
                                    requests = requests.Where(x => x.Status == "rejected").ToList();
                            }
                            switch (sortby)
                            {
                                case "orderid":
                                    requests = requests.OrderBy(x => x.OrderId).ToList();
                                    break;
                                case "name":
                                    requests = requests.OrderBy(x => x.Name).ToList();
                                    break;

                                default:
                                    requests = requests.OrderBy(x => x.OrderId).ToList();
                                    break;
                            }
                            if (search != "")
                            {
                                requests = requests.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
                            }
                            switch (sortorder)
                            {
                                case "ascending":
                                    break;
                                case "descending":
                                    requests.Reverse();
                                    break;
                                default:
                                    break;
                            }

                            return View(requests);
                        }

                        return View();
                    }
                    else
                        return RedirectToAction("UnauthorizedPage");
                }
                else
                    return RedirectToAction(nameof(UnauthorizedPage));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> RequestRejectedAsync(int orderid, int userid)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                {
                    string userEmailId = null;
                    string userName = null;

                    HttpResponseMessage response = await client.GetAsync("order/" + orderid + "/" + userid);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        Order order = JsonConvert.DeserializeObject<Order>(stringData);
                        order.Status = "rejected";
                        HttpResponseMessage updateResponse = await client.PostAsJsonAsync("order/updateorder", order);
                        if (updateResponse.IsSuccessStatusCode)
                        {
                            var updateStringData = updateResponse.Content.ReadAsStringAsync().Result;
                            bool updated = JsonConvert.DeserializeObject<bool>(updateStringData);
                            if (updated)
                            {
                                HttpResponseMessage userResponse = await client.GetAsync("auth/getuser/" + userid);
                                if (userResponse.IsSuccessStatusCode)
                                {
                                    var userStringData = userResponse.Content.ReadAsStringAsync().Result;
                                    User user = JsonConvert.DeserializeObject<User>(userStringData);
                                    userEmailId = user.EmailId;
                                    userName = user.Name;
                                    EmailSender sender = new EmailSender(_options);
                                    string message = null;
                                    if (order.FineAmount == 0)
                                        message = "Dear " + userName + "<br/>Your request for returning car is rejected!.Please contact our customer care for further details";
                                    await sender.SendEmailAsync(userEmailId, "Return Rejected!", message);
                                }

                                return RedirectToAction("AdminPortal");
                            }
                        }
                    }
                    return View();
                }
                else return RedirectToAction(nameof(UnauthorizedPage));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult CreateCarModel()
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                return View();
            else
                return RedirectToAction("UnauthorizedPage");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> ViewCarModel(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                {
                    HttpResponseMessage response = await client.GetAsync("Car/getcarmodel/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        CarModelListVM carModel = JsonConvert.DeserializeObject<CarModelListVM>(stringData);
                        return View(carModel);
                    }
                    return RedirectToAction("ErrorPage");
                }
                else return RedirectToAction(nameof(UnauthorizedPage));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> ViewCar(int id)
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync("Car/getcarjoined/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    CarListVM car = JsonConvert.DeserializeObject<CarListVM>(stringData);
                    ViewBag.ImgUrl = car.ImgUrl;
                    ViewBag.Active = car.Active;
                    ViewBag.CarId = id;
                    return View(car);
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult UserPortal()
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "" || HttpContext.Session.GetString("_userType") == "Super")
                    return RedirectToAction("UnauthorizedPage");

                return View();
            }
            catch (Exception e)
            {

                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfileAsync(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user.ImgUrl == null)
                        HttpContext.Session.SetString("_avatarUrl", "0");
                    else
                        HttpContext.Session.SetString("_avatarUrl", user.ImgUrl);
                    HttpResponseMessage response = await client.PutAsJsonAsync("auth", user);
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("_userName", user.Name);
                        if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super")
                            return RedirectToAction("AdminPortal");
                        else
                            return RedirectToAction("UserPortal");
                    }
                    return RedirectToAction("ErrorPage");
                }
                return View(user);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> CarModelList(string sortby, string sortorder)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Customer")
                    return RedirectToAction("UnauthorizedPage");
                if (sortby == null)
                    ViewBag.SortBy = "created";
                else
                    ViewBag.SortBy = sortby;
                if (sortorder == null)
                    ViewBag.SortOrder = "ascending";
                else
                    ViewBag.SortOrder = sortorder;
                if (HttpContext.Session.GetString("_userType") == "Customer" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction("UnauthorizedPage");

                List<CarModel> carModelList = await RetrieveCarModelList();
                switch (sortby)
                {
                    case "created":
                        carModelList = carModelList.OrderBy(x => x.CreatedOn).ToList();
                        break;
                    case "charge":
                        carModelList = carModelList.OrderBy(x => x.ChargePerDay).ToList();
                        break;
                    case "seat":
                        carModelList = carModelList.OrderBy(x => x.SeatCount).ToList();
                        break;
                    default:
                        carModelList = carModelList.OrderBy(x => x.CreatedOn).ToList();
                        break;
                }
                switch (sortorder)
                {
                    case "ascending":
                        break;
                    case "descending":
                        carModelList.Reverse();
                        break;
                    default:
                        break;
                }
                return View(carModelList);

            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }

        public async Task<IActionResult> CarListAsync(string transmission, string status, string varient, string sortby, string sortorder)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Customer")
                    return RedirectToAction("UnauthorizedPage");
                if (transmission == null)
                    ViewBag.Transmission = "all";
                else
                    ViewBag.Transmission = transmission;
                if (status == null)
                    ViewBag.Status = "all";
                else
                    ViewBag.Status = status;
                if (transmission == null)
                    ViewBag.Varient = "all";
                else
                    ViewBag.Varient = varient;
                if (sortby == null)
                    ViewBag.SortBy = "created";
                else
                    ViewBag.SortBy = sortby;
                if (sortorder == null)
                    ViewBag.SortOrder = "ascending";
                else
                    ViewBag.SortOrder = sortorder;
                List<CarModel> carModelList = (List<CarModel>)await RetrieveCarModelList();
                ViewBag.ModelList = carModelList;
                List<string> carVarients = (List<string>)await RetrieveCarVarients();
                ViewBag.VarientList = carVarients;
                status = ViewBag.Status;
                HttpResponseMessage response = await client.GetAsync("car/getcarlist");
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    List<CarListVM> carList = JsonConvert.DeserializeObject<List<CarListVM>>(stringData);
                    switch (transmission)
                    {
                        case "all":
                            break;
                        case "manual":
                            carList = carList.Where(x => x.CarModelDetails.CarTransmission == (CarTransmission)0).ToList();
                            break;
                        case "automatic":
                            carList = carList.Where(x => x.CarModelDetails.CarTransmission == (CarTransmission)1).ToList();
                            break;
                        default:
                            break;
                    }
                    if (status != "all")
                    {

                        if (status == "active")
                            carList = carList.Where(x => x.Active == true).ToList();
                        else
                            carList = carList.Where(x => x.Active == false).ToList();
                    }
                    if (varient != "all" && varient != null)
                    {
                        carList = carList.Where(x => x.CarModelDetails.CarType.ToString() == varient).ToList();
                    }
                    switch (sortby)
                    {
                        case "created":
                            carList = carList.OrderBy(x => x.CreatedOn).ToList();
                            break;
                        case "charge":
                            carList = carList.OrderBy(x => x.CarModelDetails.ChargePerDay).ToList();
                            break;
                        case "seat":
                            carList = carList.OrderBy(x => x.CarModelDetails.SeatCount).ToList();
                            break;
                        default:
                            carList = carList.OrderBy(x => x.CreatedOn).ToList();
                            break;
                    }
                    switch (sortorder)
                    {
                        case "ascending":
                            break;
                        case "descending":
                            carList.Reverse();
                            break;
                        default:
                            break;
                    }
                    return View(carList);
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> CreateCarAsync()
        {
            if (HttpContext.Session.GetString("_userType") == "Customer")
                return RedirectToAction("UnauthorizedPage");
            List<CarModel> carModelList = (List<CarModel>)await RetrieveCarModelList();
            ViewBag.ModelList = carModelList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCar(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    car.UserId = (int)HttpContext.Session.GetInt32("_userId");
                    car.CreatedOn = DateTime.Now;
                    car.RegNo = car.RegNo.ToUpper();
                    car.Colour = car.Colour.ToUpper();
                    HttpResponseMessage response = await client.PostAsJsonAsync("car/addcardetails", car);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringData = response.Content.ReadAsStringAsync().Result;
                        bool carAdded = JsonConvert.DeserializeObject<bool>(stringData);
                        if (carAdded)
                        {
                            ViewBag.SuccessMessage = "Added";
                            ModelState.Clear();
                            ViewBag.ModelList = (List<CarModel>)await RetrieveCarModelList();

                            return View();
                        }
                    }
                }
                ViewBag.SuccessMessage = "error";
                List<CarModel> carModelList = (List<CarModel>)await RetrieveCarModelList();
                ViewBag.ModelList = carModelList;
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> EditCarAsync(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Customer")
                    return RedirectToAction("UnauthorizedPage");

                HttpResponseMessage response = await client.GetAsync("Car/getcar/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    Car car = JsonConvert.DeserializeObject<Car>(stringData);
                    ViewBag.ImgUrl = car.ImgUrl;
                    if (car.UserId == HttpContext.Session.GetInt32("_userId") || HttpContext.Session.GetString("_userType") == "Super")
                        return View(car);
                    return RedirectToAction(nameof(UnauthorizedPage));
                }

                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCarAsync(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    car.RegNo = car.RegNo.ToUpper();
                    car.Colour = car.Colour.ToUpper();
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
                }
                return RedirectToAction("EditCar", new { id = car.CarId });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }

        public async Task<IActionResult> ChangeCarActive(int id, bool active)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Customer")
                    return RedirectToAction("UnauthorizedPage");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Car/changecaractive/" + id + "/" + active);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    bool deleted = JsonConvert.DeserializeObject<bool>(stringData);
                    if (deleted)
                        return RedirectToAction("CarList");
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> EditCarModel(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Customer" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction("UnauthorizedPage");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("car/getcarmodel/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    CarModel carDetails = JsonConvert.DeserializeObject<CarModel>(stringData);
                    if (carDetails.UserId == HttpContext.Session.GetInt32("_userId") || HttpContext.Session.GetString("_userType") == "Super")
                        return View(carDetails);
                    return RedirectToAction("UnauthorizedPage");
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult ErrorPage()
        {
            return View();
        }

        /*public async Task<IActionResult> DeleteCarModel(int id)
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
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCarModelSelected([FromBody] int[] selectedId)
        {
            foreach (var item in selectedId)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("Car/deletecarmodel", item);

            }
            return Json("All the customers deleted successfully!");
        }*/
        [HttpPost]
        public async Task<IActionResult> ActiveCarSelected([FromBody] int[] selectedId)
        {
            try
            {

                foreach (var item in selectedId)
                {
                    HttpResponseMessage response = await client.GetAsync("Car/changecaractive/" + item + "/true");

                }
                return Json("All the customers deleted successfully!");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> InActiveCarSelected([FromBody] int[] selectedId)
        {
            try
            {
                foreach (var item in selectedId)
                {
                    HttpResponseMessage response = await client.GetAsync("Car/changecaractive/" + item + "/false");

                }
                return Json("All the customers deleted successfully!");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult DateSelector()
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction("UnauthorizedPage");
            return View();
        }
        [HttpPost]
        public IActionResult DateSelector(DateSelectorVM dates)
        {
            if (ModelState.IsValid)
            {
                TempData["FromDate"] = dates.FromDate;
                TempData["ToDate"] = dates.ToDate;
                return RedirectToAction("UserCarList");
            }
            return View(dates);
        }
        public async Task<IActionResult> UserCarListAsync(string transmission, string varient, string sortby, string sortorder)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction(nameof(UnauthorizedPage));
                DateTime fromDate = new DateTime(), toDate = new DateTime();
                if (TempData.ContainsKey("FromDate"))
                    fromDate = Convert.ToDateTime(TempData["FromDate"]);
                else
                    return RedirectToAction("DateSelector");
                if (TempData.ContainsKey("ToDate"))
                    toDate = Convert.ToDateTime(TempData["ToDate"]);
                else
                    return RedirectToAction("DateSelector");
                TempData.Keep("FromDate");
                TempData.Keep("ToDate");
                ViewBag.FromDate = fromDate.Day + "/" + fromDate.Month + "/" + fromDate.Year;
                ViewBag.ToDate = toDate.Day + "/" + toDate.Month + "/" + toDate.Year;
                if (transmission == null)
                    ViewBag.Transmission = "all";
                else
                    ViewBag.Transmission = transmission;

                if (transmission == null)
                    ViewBag.Varient = "all";
                else
                    ViewBag.Varient = varient;
                if (sortby == null)
                    ViewBag.SortBy = "created";
                else
                    ViewBag.SortBy = sortby;
                if (sortorder == null)
                    ViewBag.SortOrder = "ascending";
                else
                    ViewBag.SortOrder = sortorder;
                List<CarModel> carModelList = (List<CarModel>)await RetrieveCarModelList();
                ViewBag.ModelList = carModelList;
                List<string> carVarients = (List<string>)await RetrieveCarVarients();
                ViewBag.VarientList = carVarients;
                DatesVM dates = new DatesVM();
                dates.FromDate = fromDate;
                dates.ToDate = toDate;
                HttpResponseMessage response = await client.PostAsJsonAsync("car/getavailablecarlist", dates);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    List<CarListVM> carList = JsonConvert.DeserializeObject<List<CarListVM>>(stringData);
                    carList = carList.Where(x => x.Active == true).ToList();
                    switch (transmission)
                    {
                        case "all":
                            break;
                        case "manual":
                            carList = carList.Where(x => x.CarModelDetails.CarTransmission == (CarTransmission)0).ToList();
                            break;
                        case "automatic":
                            carList = carList.Where(x => x.CarModelDetails.CarTransmission == (CarTransmission)1).ToList();
                            break;
                        default:
                            break;
                    }
                    if (varient != "all" && varient != null)
                    {
                        carList = carList.Where(x => x.CarModelDetails.CarType.ToString() == varient).ToList();
                    }
                    switch (sortby)
                    {
                        case "created":
                            carList = carList.OrderBy(x => x.CreatedOn).ToList();
                            break;
                        case "charge":
                            carList = carList.OrderBy(x => x.CarModelDetails.ChargePerDay).ToList();
                            break;
                        case "seat":
                            carList = carList.OrderBy(x => x.CarModelDetails.SeatCount).ToList();
                            break;
                        default:
                            carList = carList.OrderBy(x => x.CreatedOn).ToList();
                            break;
                    }
                    switch (sortorder)
                    {
                        case "ascending":
                            break;
                        case "descending":
                            carList.Reverse();
                            break;
                        default:
                            break;
                    }
                    return View(carList);
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }

        public async Task<IActionResult> ConfirmPageAsync(int id,int coupon)
        {
            if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super" || HttpContext.Session.GetString("_userType") == "")
                return RedirectToAction(nameof(UnauthorizedPage));
            DateTime FromDate = new DateTime(), ToDate = new DateTime();
            if (TempData.ContainsKey("FromDate"))
                FromDate = Convert.ToDateTime(TempData["FromDate"]);
            if (TempData.ContainsKey("ToDate"))
                ToDate = Convert.ToDateTime(TempData["ToDate"]);
            ViewBag.FromDate = FromDate.Day + "/" + FromDate.Month + "/" + FromDate.Year;
            ViewBag.ToDate = ToDate.Day + "/" + ToDate.Month + "/" + ToDate.Year;
            TempData.Keep("FromDate");
            TempData.Keep("ToDate");
            int noOfDays = (int)(ToDate - FromDate).TotalDays;
            if (noOfDays == 0)
                noOfDays = 1;
            ViewBag.NoOfDays = noOfDays;
            ViewBag.CarId = id;
            try
            {
                int total = 0;
                    int discount=0;
                ConfirmPageVM confirmPageVM = new ConfirmPageVM();
                HttpResponseMessage response = await client.GetAsync("Car/getcarjoined/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    CarListVM car = JsonConvert.DeserializeObject<CarListVM>(stringData);
                    ViewBag.ImgUrl = car.ImgUrl;
                    ViewBag.Total = car.CarModelDetails.ChargePerDay * noOfDays;
                    total = car.CarModelDetails.ChargePerDay * noOfDays;
                    confirmPageVM.carListVM = car;
                }
                HttpResponseMessage couponResponse = await client.GetAsync("Coupon");
                if (couponResponse.IsSuccessStatusCode)
                {
                    var stringData = couponResponse.Content.ReadAsStringAsync().Result;
                    List<Coupon> coupons = JsonConvert.DeserializeObject<List<Coupon>>(stringData);
                    confirmPageVM.Coupons = coupons.Where(x=>x.Active==true).ToList();

                    if (coupon != 0)
                    {
                        Coupon selectedCoupon = coupons.Where(x => x.CouponId == coupon).Single();
                        if (selectedCoupon.MinOrderAmount > total)
                        {
                            ViewBag.Message = "Minimum order value criteria not met";
                            return View(confirmPageVM);
                        }
                        else
                        {
                            discount = (total * selectedCoupon.PercentageDiscount) / 100;
                            if (discount > selectedCoupon.MaxDiscount)
                                discount = selectedCoupon.MaxDiscount;

                            ViewBag.Discount = (discount);
                            ViewBag.Message = "Coupon Applied";
                        }
                    }
                    ViewBag.Coupon = coupon;
                    ViewBag.FinalAmount = total -discount;
                    return View(confirmPageVM);
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }

        public async Task<IActionResult> ConfirmPageYesAsync(int id,int coupon)
        {
            try
            {
                Order order = new Order();
                order.CarId = id;
                order.UserId = (int)HttpContext.Session.GetInt32("_userId");
                DateTime FromDate = new DateTime(), ToDate = new DateTime();
                if (TempData.ContainsKey("FromDate"))
                    FromDate = Convert.ToDateTime(TempData["FromDate"]);
                if (TempData.ContainsKey("ToDate"))
                    ToDate = Convert.ToDateTime(TempData["ToDate"]);
                order.FromDate = FromDate;
                order.ToDate = ToDate;
                order.ExtraDays = 0;
                int noOfDays = (int)(ToDate - FromDate).TotalDays;
                if (noOfDays == 0)
                    noOfDays = 1;
                HttpResponseMessage response = await client.GetAsync("Car/getcarjoined/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    CarListVM car = JsonConvert.DeserializeObject<CarListVM>(stringData);
                    order.Total = car.CarModelDetails.ChargePerDay * noOfDays;
                    order.CouponId = coupon;
                    order.FineAmount = 0;
                    order.Status = "unpaid";
                    order.OrderDate = DateTime.Now;
                }
                HttpResponseMessage orderResponse = await client.PostAsJsonAsync("Order/addorder", order);
                if (orderResponse.IsSuccessStatusCode)
                {
                    var orderStringData = orderResponse.Content.ReadAsStringAsync().Result;
                    int orderId = JsonConvert.DeserializeObject<int>(orderStringData);
                    if (orderId >= 100)
                    {

                        PaymentReciept reciept = new PaymentReciept
                        {
                            OrderId = orderId,
                            Total = 0,
                            ExtraDays = 0
                        };
                        TempData["PaymentReciept"] = JsonConvert.SerializeObject(reciept);
                        return RedirectToAction("PaymentPage");
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }

        public async Task<IActionResult> PaymentPage()
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction(nameof(UnauthorizedPage));
                if (!TempData.ContainsKey("PaymentReciept"))
                    return RedirectToAction("ErrorPage");
                int userId = (int)HttpContext.Session.GetInt32("_userId");
                PaymentReciept reciept = JsonConvert.DeserializeObject<PaymentReciept>((string)TempData["PaymentReciept"]);
                TempData.Keep("PaymentReciept");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Order/" + reciept.OrderId + "/" + userId);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    Order order = JsonConvert.DeserializeObject<Order>(stringData);
                    ViewBag.OrderId = order.OrderId;
                    ViewBag.Total = order.Total-order.Discount;
                    return View();
                }

                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> PaymentPagePost(int orderId, int total)
        {
            try
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
                        HttpResponseMessage response = await client.GetAsync("order/makepayment/" + orderId);
                        if (response.IsSuccessStatusCode)
                        {
                            EmailSender sender = new EmailSender(_options);
                            string emailId = HttpContext.Session.GetString("_emailId");
                            string userName = HttpContext.Session.GetString("_userName");
                            string message = "Dear " + userName + "<br/>Your booking has beeen successfull";
                            await sender.SendEmailAsync(emailId, "Booking Successfull!", message);
                            var stringData = response.Content.ReadAsStringAsync().Result;
                            bool updated = JsonConvert.DeserializeObject<bool>(stringData);
                            if (updated)
                            {
                                return RedirectToAction("OrderSuccessfull", new { id = orderId });

                            }
                        }


                    }
                }

                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> ReceiptPageAsync(int id, int? userId)
        {
            try
            {

                if (userId == null)
                    userId = (int)HttpContext.Session.GetInt32("_userId");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Order/getreceipt/" + id + "/" + userId);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    ReceiptVM receipt = JsonConvert.DeserializeObject<ReceiptVM>(stringData);
                    ViewBag.ImgUrl = receipt.ImgUrl;
                    ViewBag.FinalAmount = receipt.Total - receipt.Discount;
                    ViewBag.ExtraDays = receipt.ExtraDays;
                    ViewBag.ChargePerDay = receipt.ChargePerDay * 1.5;
                    ViewBag.OtherCharges = receipt.FineAmount - (receipt.ExtraDays * (receipt.ChargePerDay * 1.5));
                    return View(receipt);
                }
                return RedirectToAction(nameof(ErrorPage));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public IActionResult OrderSuccessfull(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction(nameof(UnauthorizedPage));
                ViewBag.OrderId = id;
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> OrderHistory(string sortby, string sortorder)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction(nameof(UnauthorizedPage));
                if (sortby == null)
                    ViewBag.SortBy = "orderdate";
                else
                    ViewBag.SortBy = sortby;
                if (sortorder == null)
                {
                    ViewBag.SortOrder = "descending";
                    sortorder = "descending";
                }

                else
                    ViewBag.SortOrder = sortorder;
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction("UnauthorizedPage");
                int userId = (int)HttpContext.Session.GetInt32("_userId");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Order/userId?userId=" + userId);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    List<OrderHistoryVM> orderList = JsonConvert.DeserializeObject<List<OrderHistoryVM>>(stringData);
                    orderList = orderList.Where(x => x.Status != "unpaid").ToList();
                    switch (sortby)
                    {
                        case "orderdate":
                            orderList = orderList.OrderBy(x => x.OrderDate).ToList();
                            break;
                        case "total":
                            orderList = orderList.OrderBy(x => x.Total).ToList();
                            break;
                        default:
                            orderList = orderList.OrderBy(x => x.OrderDate).ToList();
                            break;
                    }
                    if (sortorder == "descending")
                    {
                        orderList.Reverse();
                    }
                    if (orderList.Count == 0)
                        return RedirectToAction("ZeroHistoryPage");
                    ViewBag.OrderList = orderList;
                    return View(orderList);
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> RequestReturn(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Admin" || HttpContext.Session.GetString("_userType") == "Super" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction(nameof(UnauthorizedPage));
                HttpResponseMessage response = await client.GetAsync("Order/requestreturn/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    bool updated = JsonConvert.DeserializeObject<bool>(stringData);
                    if (updated)
                        return RedirectToAction(nameof(OrderHistory));
                    else
                        return RedirectToAction(nameof(ErrorPage));
                }
                return RedirectToAction(nameof(ErrorPage));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }

        public async Task<IActionResult> CompleteTrip(int orderId, int userId)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("_token"));
                HttpResponseMessage response = await client.GetAsync("Order/" + orderId + "/" + userId);
                if (response.IsSuccessStatusCode)
                {
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    Order order = JsonConvert.DeserializeObject<Order>(stringData);
                    if (order == null)
                        return RedirectToAction("ErrorPage");
                    if (order.ExtraDays > 0 || order.FineAmount > 0)
                    {
                        ViewBag.ExtraDays = order.ExtraDays;
                        ViewBag.OrderId = order.OrderId;
                        ViewBag.FineAmount = order.FineAmount;

                        return View();
                    }


                    else
                    {
                        HttpResponseMessage updateResponse = await client.GetAsync("Order/ExtraDays?orderId=" + order.OrderId);
                        if (updateResponse.IsSuccessStatusCode)
                        {
                            var updateStringData = updateResponse.Content.ReadAsStringAsync().Result;
                            bool updated = JsonConvert.DeserializeObject<bool>(updateStringData);
                            if (updated)
                            {
                                EmailSender sender = new EmailSender(_options);
                                string emailId = HttpContext.Session.GetString("_emailId");
                                string userName = HttpContext.Session.GetString("_userName");
                                string message = "Dear " + userName + "<br/>Thank you for using Car Rental Portal.Hope to see you soon";
                                await sender.SendEmailAsync(emailId, "Car Rented Successfully !", message);
                                return RedirectToAction("UserPortal");
                            }
                        }
                    }

                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
            }
        }
        public async Task<IActionResult> CarBookingHistory(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("_userType") == "Customer" || HttpContext.Session.GetString("_userType") == "")
                    return RedirectToAction(nameof(UnauthorizedPage));
                HttpResponseMessage Response = await client.GetAsync("Order/getadminrequests");
                if (Response.IsSuccessStatusCode)
                {
                    var StringData = Response.Content.ReadAsStringAsync().Result;
                    List<AdminRequestVM> requests = JsonConvert.DeserializeObject<List<AdminRequestVM>>(StringData);
                    requests = requests.Where(x => x.CarId == id && x.Status != "unpaid").ToList();
                    ViewBag.CarId = id;
                    return View(requests);
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(CatchAction), new { e = e.Message.ToString() });
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
