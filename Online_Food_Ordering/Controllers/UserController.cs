using Online_Food_Ordering.Models;
using Online_Food_Ordering.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Food_Ordering.Controllers;
using System.Data.SqlClient;
using System.Net;

namespace Online_Food_Ordering.Controllers
{
    public class UserController : Controller
    {

        OnlineFoodModel db = new OnlineFoodModel();
            
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AfterBill(int id)
        {

            return View();
        }

       

       
        public ActionResult DeleteBill(int id)
        {
            var data = from p in db.Orders select p;
            db.Orders.RemoveRange(data);
            try
            {
                // TODO: Add delete logic here
                Bill bil = new Bill();
                bil = db.Bills.Find(id);
                db.Bills.Remove(bil);
                db.SaveChanges();
                return RedirectToAction("WelcomeUser", "User");
            }
            catch
            {
                return RedirectToAction("WelcomeUser", "User");
            }
        }




        public void price(Food food)
        {
            if (ModelState.IsValid)
            {
                var details = (from userlist in db.Foods
                               where userlist.ID_Food == food.ID_Food 
                               select new
                               {
                                   userlist.Name,
                                   userlist.Price
                               }).ToList();
            }

            }

        public ActionResult EditUser(int id)
        {

            User us = db.Users.Find(id);
            if (us == null)
            {
                return HttpNotFound();
            }
            return View(us);

            //return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditUser(User us, HttpPostedFileBase image1)
        {

            if (image1 != null)
            {
                us.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(us.Image, 0, image1.ContentLength);

            }

            if (ModelState.IsValid)
            {
                db.Entry(us).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("WelcomeUser", "Home");

        }



        public ActionResult CreateBill(Bill bill,Order order)
        {
            ViewBag.i = Session["ID_User"];
            int to = ViewBag.i;
            var totalPrice = db.Orders.Where(a => a.User_ID == to)
                    .Sum(a => a.Price);



            

            bill.User_ID = to;
            bill.Date = DateTime.Now;
            bill.Total_Price = totalPrice;
            db.Bills.Add(bill);
            db.SaveChanges();


            
            int b = to;
            var bi = db.Bills.SingleOrDefault(c => c.User_ID == b && c.Total_Price== totalPrice);
            //3lshan el moshkla de tt7al a3ml el fun de 5asa bs b create el bill w a3ml zorar tani 
            // le show my bill
            var data = from p in db.Orders select p;
            db.Orders.RemoveRange(data);
            return View(bi);
           
        }

        public ActionResult previousBill() {
            ViewBag.i = Session["ID_User"];
            int to = ViewBag.i;
            return View(db.Bills.ToList().Where(a=> a.User_ID==to));
        }

        public ActionResult showAllFood()
        {
            return View(db.Foods.ToList());
        }

        public ActionResult WelcomeUser()
        {
            var data = from p in db.Orders select p;
            db.Orders.RemoveRange(data);
            return View();
        }

        // GET: User/Create
        public ActionResult MakeOrder()
        {
            Order order = new Order();
            var fo = db.Foods.ToList();
           
            SelectFood sf = new SelectFood()
            {
                Order = order,
                Foods = fo,
               

            };
            var get = db.Foods.ToList();
            SelectList list = new SelectList(get, "ID_Food", "Name","Price");
            ViewBag.pg = list;
           

            return View(sf);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult MakeOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                ViewBag.id = Session["ID_User"];

                order.User_ID = ViewBag.id;


                var details = (from userlist in db.Foods
                               where userlist.ID_Food == order.Food_ID
                               select new
                               {
                                   userlist.Name,
                                   userlist.Price
                               }).ToList();

                Session["Price"] = details.FirstOrDefault().Price;
                ViewBag.Price = Session["Price"];
                order.Price = ViewBag.Price;
                db.Orders.Add(order);
                db.SaveChanges();
            }

            
            return RedirectToAction("MakeOrder", "User");
        }



    }
}
