using FLightBookingSystem.DatabaseLayer;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Net.Mail;
using System.Collections.Generic;

namespace FLightBookingSystem.Controllers
{
    public class LoginController : Controller
    {
        private FlightBookingEntities db = new FlightBookingEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("UserInfo"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["UserInfo"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(DatabaseLayer.Customer signup)
        {

            var isEmailAlreadyExists = db.Customers.FirstOrDefault(x => x.EmailAddress == signup.EmailAddress);
            if (isEmailAlreadyExists == null)
            {
                ViewBag.Message = "User Not Registerd";
                return View();
            }
            else
            {
                SendMail(isEmailAlreadyExists.EmailAddress, isEmailAlreadyExists.Password);
                ViewBag.Message = "Email Sent to Register Email Address";
                return RedirectToAction("Login", "Login");
            }

        }
        void SendMail(string toemail, string password)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("reddysowmya935@gmail.com");
                message.To.Add(new MailAddress(toemail));
                message.Subject = "FLight Booking System Login Password";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "your Password Is : " + password;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("reddysowmya935@gmail.com", "igfykfjfepsnrbgy");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {

            }

        }
        public ActionResult Login([Bind(Include = "EmailAddress , Password")] DatabaseLayer.Customer model)
        {
            var data = db.Customers.Where(s => (s.EmailAddress.Equals(model.EmailAddress) || s.AdharCard.Equals(model.EmailAddress)) && s.Password.Equals(model.Password)).ToList();
            if (data.Count() > 0)
            {
                Session["uid"] = data.FirstOrDefault().ID;
                HttpCookie cooskie = new HttpCookie("UserInfo");
                cooskie.Values["idUser"] = Convert.ToString(data.FirstOrDefault().ID);
                cooskie.Values["FullName"] = Convert.ToString(data.FirstOrDefault().Name);
                cooskie.Values["Email"] = Convert.ToString(data.FirstOrDefault().EmailAddress);
                cooskie.Values["IsAdmin"] = Convert.ToString(data.FirstOrDefault().IsAdmin);
                cooskie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cooskie);
                if (data.FirstOrDefault().IsAdmin == "True")
                {
                    return RedirectToAction("Index", "Bookings");
                }
                else
                {
                    return RedirectToAction("Index", "CustomersMaster");
                }
            }
            else
            {
                ViewBag.Message = "Login failed";
                return View( );
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            var userInCookie = Request.Cookies["UserInfo"];
            if (userInCookie != null)
            {
                return RedirectToAction("Index", "CustomersMaster");
            }
            else
            {
                var cust = new DatabaseLayer.Customer();
                return View(cust);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "ID,Name,EmailAddress,MobileNo,Password,IsAdmin,DOB,CIty,AdharCard,Gender")] FLightBookingSystem.DatabaseLayer.Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (db.Customers.Any(x => x.EmailAddress == customer.EmailAddress && x.AdharCard == x.AdharCard))
                {
                    ViewBag.Message = "User Already Exist";
                    var cust1 = new DatabaseLayer.Customer();
                    return View(cust1);
                }
                else
                {
                    customer.IsAdmin = "false";
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            var cust = new DatabaseLayer.Customer();
            return View(cust);
        }

    }
}