using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceStation.Models;

namespace ServiceStation.Controllers
{
    public class ServiceClientsController : Controller
    {
        private ServiceStationContext db = new ServiceStationContext();

        public ActionResult Index()
        {
            return View(db.ServiceClients.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceClient serviceClient = db.ServiceClients.Find(id);
            if (serviceClient == null)
            {
                return HttpNotFound();
            }
            return View(serviceClient);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceClientID,FirstName,LastName,DateofBirth,Phone,Email")] ServiceClient serviceClient)
        {
            if (ModelState.IsValid)
            {
                db.ServiceClients.Add(serviceClient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceClient);
        }

        public ActionResult CarCreate(int id)
        {            
            var car = new Car();
            car.ServiceClientID = id;
            ViewBag.ServiceClientID = new SelectList(db.ServiceClients, "ServiceClientID", "FirstName", id);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarCreate([Bind(Include = "CarID,Make,Model,Year,VIN,ServiceClientID")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = car.ServiceClientID });
            }

            ViewBag.ServiceClientID = new SelectList(db.ServiceClients, "ServiceClientID", "FirstName", car.ServiceClientID);
            return View(car);
        }

        public ActionResult CarEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceClientID = new SelectList(db.ServiceClients, "ServiceClientID", "FirstName", car.ServiceClientID);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarEdit([Bind(Include = "CarID,Make,Model,Year,VIN,ServiceClientID")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = car.ServiceClientID });
            }
            ViewBag.ServiceClientID = new SelectList(db.ServiceClients, "ServiceClientID", "FirstName", car.ServiceClientID);
            return View(car);
        }

        public ActionResult CarDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost, ActionName("CarDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CarDeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            if (car.Orders.Count == 0)
            {
                db.Cars.Remove(car);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = car.ServiceClientID });
            }
            ViewBag.ErrMessage = "Can`t delete. This car have orders";
            return View(car);            
        }

        public ActionResult OrderCreate(int id)
        {
            var order = new Order();
            order.CarID = id;
            order.Car = db.Cars.Find(id);
            if (order.Car == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Make", id);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCreate([Bind(Include = "OrderID,Date,OrderAmount,OrderStatus,CarID")] Order order)
        {
            order.Car = db.Cars.Find(order.CarID);
            if (order.Car == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = order.Car.ServiceClientID });
            }
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Make", order.CarID);
            return View(order);
        }

        public ActionResult OrderEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Car = db.Cars.Find(order.CarID);
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Make", order.CarID);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderEdit([Bind(Include = "OrderID,Date,OrderAmount,OrderStatus,CarID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = order.Car.ServiceClientID });
            }
            order.Car = db.Cars.Find(order.CarID);
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Make", order.CarID);
            return View(order);
        }

        public ActionResult OrderDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Car = db.Cars.Find(order.CarID);
            return View(order);
        }

        [HttpPost, ActionName("OrderDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult OrderDeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            int prevID = order.Car.ServiceClientID;
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = prevID });
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
