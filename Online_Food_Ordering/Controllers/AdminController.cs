using Online_Food_Ordering.Models;
using Online_Food_Ordering.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering.Controllers
{
    public class AdminController : Controller
    {
        OnlineFoodModel db = new OnlineFoodModel();


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AllBill()
        {
            var bill = db.Bills.ToList();
            var user = db.Users.ToList();

            SelectUser su = new SelectUser()
            {
                Users = user,
                Bill = bill,


            };
           


            return View(su);
        }

        public ActionResult ShowAllBill()
        {
            return View(db.Bills.ToList());
        }




        public ActionResult ShowFood()
        {
            return View(db.Foods.ToList());
        }



        public ActionResult ShowClient()
        {

            return View(db.Users.ToList().Where(x=>x.ID_User !=1));
        }


        public ActionResult AddFood()
        {
            return View();

        }

        [HttpPost]
        public ActionResult AddFood(Food fo, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                fo.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(fo.Image, 0, image1.ContentLength);

            }

            if (ModelState.IsValid)
            {
                db.Foods.Add(fo);
                db.SaveChanges();
            }
            //ModelState.Clear();
            return RedirectToAction("WelcomeAdmin", "Home");


        }





        // GET: Admin/Edit/5
        public ActionResult EditFood(int id)
        {

            Food fo = db.Foods.Find(id);
            if (fo == null)
            {
                return HttpNotFound();
            }
            return View(fo);

            //return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditFood(Food fo, HttpPostedFileBase image1)
        {

            if (image1 != null)
            {
                fo.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(fo.Image, 0, image1.ContentLength);

            }

            if (ModelState.IsValid)
            {
                db.Entry(fo).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ShowFood", "Admin");

        }






        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            Food fo = db.Foods.Single(x => x.ID_Food == id);
            ViewBag.id = fo.ID_Food;
            Session["id"] = ViewBag.id;
            return View(fo);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Food fo)
        {
            try
            {
                // TODO: Add delete logic here
                fo = db.Foods.Find(Session["id"]);
                db.Foods.Remove(fo);
                db.SaveChanges();
                return RedirectToAction("ShowFood", "Admin");
            }
            catch
            {
                return RedirectToAction("ShowFood", "Admin");
            }
        }



        public ActionResult DeleteClient(int id)
        {

            var data = from p in db.Bills.Where(x => x.User_ID == id) select p;
               db.Bills.RemoveRange(data);
            var dat = from p in db.Orders select p;
            db.Orders.RemoveRange(dat);
            // TODO: Add delete logic here
            User us = new User();
                us = db.Users.Find(id);
                db.Users.Remove(us);
                db.SaveChanges();
                return RedirectToAction("ShowClient", "Admin");
            
        }



        //public ActionResult DeleteClient(int id)
        //{
        //    User us = db.Users.Single(x => x.ID_User == id);
        //   // Bill bi = db.Bills.Single(x => x.User_ID == id);
        //    ViewBag.id = us.ID_User;
        //    Session["id"] = ViewBag.id;
        //    return View(us);
        //}

        //// POST: Admin/Delete/5
        //[HttpPost, ActionName("DeleteClient")]
        //public ActionResult DeleteClient(User us)
        //{


        //    //Session["idBill"] = details.FirstOrDefault().ID_Bill;
        //    //bi = db.Bills.Find(Session["idBill"]);
        //    //db.Bills.Remove(bi);
        //    //db.SaveChanges();

        //    var data = from p in db.Bills.Where(x=>x.User_ID== us.ID_User) select p;
        //    db.Bills.RemoveRange(data);

        //        // TODO: Add delete logic here
        //        us = db.Users.Find(ViewBag.id);
        //        db.Users.Remove(us);
        //        db.SaveChanges();


        //        return RedirectToAction("ShowClient", "Admin");

        //}




    }
}
