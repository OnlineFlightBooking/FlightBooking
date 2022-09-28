using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FLightBookingSystem.DatabaseLayer;
using FLightBookingSystem.Models;

namespace FLightBookingSystem.Controllers.Customer
{
    public class CustomersMasterController : Controller
    {
        private FlightBookingEntities db = new FlightBookingEntities();

        // GET: CustomersMaster
        public ActionResult Index()
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login","Login");
            }
            var lst = db.Customers.Where(x => x.ID == iduser).ToList();
            return View(lst);
        }

        // GET: CustomersMaster/Details/5
        public ActionResult Details(int? id)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FLightBookingSystem.DatabaseLayer.Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: CustomersMaster/Create
        public ActionResult Create()
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            var cust1 = new DatabaseLayer.Customer();
            return View(cust1);
        }

        // POST: CustomersMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,EmailAddress,MobileNo,Password,IsAdmin,DOB,CIty,AdharCard,Gender")] FLightBookingSystem.DatabaseLayer.Customer customer)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: CustomersMaster/Edit/5
        public ActionResult Edit(int? id)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            if (userInCookie == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FLightBookingSystem.DatabaseLayer.Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: CustomersMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,EmailAddress,MobileNo,Password,IsAdmin,DOB,CIty,AdharCard,Gender")] DatabaseLayer.Customer customer)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: CustomersMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FLightBookingSystem.DatabaseLayer.Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: CustomersMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            FLightBookingSystem.DatabaseLayer.Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Book()
        {
            return View();
        }

        public ActionResult ViewDetails()
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
          
            var list = db.Bookings.Where(x => x.CustomerID == iduser);
            return View("ViewDetails", list);
        }

        public ActionResult CancelBooking()
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            var list = db.Bookings.Where(x => x.CustomerID == iduser);
            ViewBag.BookingID = new SelectList(list, "BookingID", "BookingID");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelBooking(BookingModel booking)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
           
            var first = db.Bookings.Where(x => x.CustomerID == iduser);
            if (first != null)
            {
                foreach (var item in first)
                {
                    item.Status = "Canceld";
                }
                // first.BookingDate = booking.SelectedDate;

                db.SaveChanges();
                ViewBag.Message = "your Flight Canceled Payment Refunded : " + first.FirstOrDefault().Price;

                var list = db.Bookings.Where(x => x.CustomerID == iduser);
                ViewBag.BookingID = new SelectList(list, "BookingID", "BookingID");

                return View();
            }
            else
            {
                ViewBag.Message = "Please select Proper Booking Id and Date";
                var list = db.Bookings.Where(x => x.CustomerID == iduser);
                ViewBag.BookingID = new SelectList(list, "BookingID", "BookingID");
                return View();
            }
        }

        public ActionResult ResheduleBooking()
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
          
            var list = db.Bookings.Where(x => x.CustomerID == iduser);
            ViewBag.BookingID = new SelectList(list, "BookingID", "BookingID");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResheduleBooking(BookingModel booking)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            var first = db.Bookings.Where(x => x.CustomerID == iduser).ToList();
            if (first != null)
            {
                foreach (var item in first)
                {
                    item.BookingDate = DateTime.Now;
                    item.Status = "Resheduled";
                }

                db.SaveChanges();
                ViewBag.Message = "your Flight Resheduled";
                var list = db.Bookings.Where(x => x.CustomerID == iduser && x.BookingDate == booking.SelectedDate);
                ViewBag.BookingID = new SelectList(list, "BookingID", "BookingID");
                return View();
            }
            else
            {
                ViewBag.Message = "Please select Proper Booking Id and Date";
                var list = db.Bookings.Where(x => x.CustomerID == iduser);
                ViewBag.BookingID = new SelectList(list, "BookingID", "BookingID");
                return View();
            }
        }

        public ActionResult Confrim(int id)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }

            var flight = db.Flights.FirstOrDefault(x => x.FlightID == id);
            db.Bookings.Add(new Booking
            {
                CustomerID = iduser,
                FlightID = flight.FlightID,
                BookingDate = flight.DeptTime,
                FromCIty = flight.FromCity,
                ToCity = flight.ToCity,
                PaymentStatus = "Done",
                Price = flight.Price,
                Status = "Confirm",
                TotalSeat = 1,
            });
            db.SaveChanges();
            return View("Confrim");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(FlightSearch flightSearch)
        {
            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            var flights = db.Flights.Where(x => x.FromCity == flightSearch.FromCity && x.ToCity == flightSearch.ToCity && x.DeptTime >= flightSearch.StartTime);
            return View("SearchResult", flights);
        }
        public ActionResult SearchResult(List<Flight> flight)
        {

            var userInCookie = Request.Cookies["UserInfo"];
            int iduser = Convert.ToInt32(userInCookie["idUser"]);
            if (iduser == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
