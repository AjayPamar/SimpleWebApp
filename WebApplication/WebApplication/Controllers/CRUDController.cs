using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication.DBLayer;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CRUDController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CRUDController));
        // GET: CRUD
        public ActionResult Index()
        {
            try {
                List<User> users = null;
                NodeAPI api = new NodeAPI();
                users = api.GetAllUsers();
                return View(users);
            }
            catch (Exception ex) {
                logger.Error("Exception occured while displaying all user records. ",ex);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try {
                NodeAPI api = new NodeAPI();
                bool RecordCreated = api.CreateOrEditUser(user, "Create");
                if (RecordCreated)
                    TempData["Message"] = "New user record was not created. Please try again later.";
                return RedirectToAction("index");
            }
            catch (Exception ex) {
                logger.Error("Exception occured while creating new user record. ", ex);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            NodeAPI api = new NodeAPI();
            User usr = api.GetUser(Id);
            return PartialView(usr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                NodeAPI api = new NodeAPI();
                bool RecordUpdated = api.CreateOrEditUser(user,"Edit");
                if (!RecordUpdated)
                    TempData["Message"] = "New user record was not updated. Please try again later.";

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                logger.Error("Exception occured while updatng user record. ", ex);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Delete(int Id) {
            NodeAPI api = new NodeAPI();
            User usr = api.GetUser(Id);
            return PartialView(usr);
         }

        [HttpPost]
        public ActionResult Delete(User user)
        {
            NodeAPI api = new NodeAPI();
            bool Deleted = api.DeleteUser(user.Id);
            if (!Deleted)
                TempData["Message"] = "User record was not deleted. Please try again later.";
            
            return RedirectToAction("index");
        }
    }
}