using DotNetCoreMVCApp.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetCoreMVCApp.DBAPICall
{
    public class CallNodeJsAPI
    {
        private static readonly string GetAllUserURl = "http://localhost:1337/api/GetAll";
        private static readonly string CreateUserURL = "http://localhost:1337/api/Create";
        private static readonly string UpdateUserURL = "http://localhost:1337/api/Edit";
        private static readonly string ReadUserDetailURL = "http://localhost:1337/api/Read/{0}";
        private static readonly string DeleteuserURL = "http://localhost:1337/api/Delete/{0}";
        private static readonly string CheckMailExistURL = "http://localhost:1337/api/CheckMailExists";

        private readonly ILog logger = LogManager.GetLogger(typeof(CallNodeJsAPI));

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = null;
            try
            {
                APIHandling api = new APIHandling();
                string result = api.Get(GetAllUserURl);
                if(!string.IsNullOrEmpty(result))
                    users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserModel>>(result);
                return users;
            }
            catch (Exception ex)
            {
                logger.Error("Exception occuerd while retriving all user records. ", ex);
                return users;
            }
        }

        public UserModel GetUser(int Id)
        {
            UserModel user = null;
            try
            {
                string GetUserURL = string.Format(ReadUserDetailURL, Id);
                APIHandling api = new APIHandling();
                string result = api.Get(GetUserURL);
                if (!string.IsNullOrEmpty(result))
                    user = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserModel>>(result).First();
                return user;
            }
            catch (Exception ex)
            {
                logger.Error("Exception occuerd while retriving user records with Id=" + Id.ToString(), ex);
                return user;
            }
        }

        public bool CreateOrEditUser(UserModel user, string Action)
        {
            bool success = false;
            string url = string.Empty;
            try
            {
                APIHandling api = new APIHandling();
                if (Action == "Create")
                    url = CreateUserURL;
                if (Action == "Edit")
                    url = UpdateUserURL;

                success = api.Post(url,user,"User");
                return success;
            }
            catch (Exception ex)
            {
                logger.Error("Exception occuerd while creating or updating user record", ex);
                return success;
            }
        }

        public bool DeleteUser(int Id)
        {
            bool success = false;
            try
            {
                string DeleteUserURL = string.Format(DeleteuserURL, Id);
                APIHandling api = new APIHandling();
                success = api.DeletePOST(DeleteUserURL);
                return success;
            }
            catch (Exception ex)
            {
                logger.Error("Exception occuerd while deleting user record", ex);
                return success;
            }
        }

        public bool CheckMailExistance(string email)
        {
            bool res = false;
            try
            {
                APIHandling api = new APIHandling();
                res = api.Post(CheckMailExistURL,email,"CheckUserMail");
                return res;
            }
            catch (Exception ex)
            {
                logger.Error("Exception occured while checking mail existance", ex);
                return res;
            }
        }
    }
}
