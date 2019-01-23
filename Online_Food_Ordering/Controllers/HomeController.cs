using Online_Food_Ordering.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering.Controllers
{
    public class HomeController : Controller
    {
        OnlineFoodModel db = new OnlineFoodModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(User user, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                user.Image = new byte[image1.ContentLength];
                image1.InputStream.Read(user.Image, 0, image1.ContentLength);

            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            //ModelState.Clear();
            return RedirectToAction("Login", "Home");

        }







        public ActionResult Login()
        {


            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var details = (from userlist in db.Users
                               where userlist.Name == user.Name && userlist.Password == user.Password
                               select new
                               {

                                   userlist.Email,
                                   userlist.ID_User,
                                   userlist.Name,
                                   userlist.Image,
                                   userlist.Password
                               }).ToList();


               

                
                if (details.FirstOrDefault().Name=="Admin" && details.FirstOrDefault().Password=="Admin")
                {
                   

                    return RedirectToAction("WelcomeAdmin", "Home");
                }
                else if (details.FirstOrDefault() != null)
                {
                    Session["ID_User"] = details.FirstOrDefault().ID_User;
                    Session["Name"] = details.FirstOrDefault().Name;
                    Session["image"] = details.FirstOrDefault().Image;
                    ViewBag.image = Session["Name"];
                    ViewBag.ID_User = details.FirstOrDefault().ID_User;
                    return RedirectToAction("WelcomeUser", "User");

                }

            }




            else
            {
                ModelState.AddModelError("", "Invalid Data");
            }

            return View(user);

        }



        public ActionResult WelcomeAdmin()
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
    }
}