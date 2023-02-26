using DotNetCoreMVCApp.DBAPICall;
using DotNetCoreMVCApp.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DotNetCoreMVCApp.Controllers
{
    public class UserCRUD : Controller
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(UserCRUD));

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                CallNodeJsAPI api = new CallNodeJsAPI();
                List<UserModel> model = api.GetAllUsers();

                return View(model);
            }
            catch (Exception ex) 
            {
                logger.Error("Error occured while displaying all user records",ex);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            try
            {
                CallNodeJsAPI api = new CallNodeJsAPI();
                UserModel model = api.GetUser(id);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error("Error occured while displaying user records with Id:"+id, ex);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserModel model)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    CallNodeJsAPI api = new CallNodeJsAPI();
                    bool success = api.CreateOrEditUser(model, "Create");
                    if (!success)
                        TempData["Message"] = "New user record was not created. Please try again later.";
                }
                else
                    TempData["Message"] = "Kindly provide valid input";
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                logger.Error("Error occured while creating user record user record with id:"+model.Id.ToString()+" ", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CallNodeJsAPI api = new CallNodeJsAPI();
                    bool success = api.CreateOrEditUser(model, "Edit");
                    if (!success)
                        TempData["Message"] = "New user record was not updated. Please try again later.";
                }
                else
                    TempData["Message"] = "Kindly provide valid input";

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                logger.Error("Error occured while updateing user record with id:"+model.Id.ToString()+" ", ex);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                CallNodeJsAPI api = new CallNodeJsAPI();
                bool success = api.DeleteUser(id);
                if(!success)
                    TempData["Message"] = "User record was not deleted. Please try again later.";
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                logger.Error("Error occured while deleting user records with id:"+id.ToString()+" ", ex);
                return View("Error");
            }
        }

        public bool CheckMailExistance(string email)
        {
            bool exists = false;
            try
            {
                CallNodeJsAPI api = new CallNodeJsAPI();
                exists = api.CheckMailExistance(email);
                return exists;            
            }
            catch (Exception ex)
            {
                logger.Error("Error occured while chacking existance of mail", ex);
                return exists;
            }
        }
    }
}
