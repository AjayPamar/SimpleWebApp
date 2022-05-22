using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using WebApplication.Models;

namespace WebApplication.DBLayer
{
    public class NodeAPI
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(NodeAPI));
        private static string GetAllUserURl = "http://localhost:3000/api/GetAll";
        private static string CreateUserURL = "http://localhost:3000/api/Create";
        private static string UpdateUserURL = "http://localhost:3000/api/Edit";
        public List<User> GetAllUsers()
        {
            List<User> users = null;
            try{
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetAllUserURl);
                    var responseTask = client.GetAsync(GetAllUserURl);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var asd = result.Content.ReadAsStringAsync();
                        users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(asd.Result);
                        logger.Info("Successfully retrived data from URL : "+ GetAllUserURl);
                    }
                    else {
                        users = (List<User>)Enumerable.Empty<User>();
                        logger.Debug("Issue occured while consuming API");
                    }
                }
                return users;
            }
            catch (Exception ex){
                logger.Error("Exception occuerd while retriving all user records. ",ex);
                return users;
            }
        }

        public User GetUser(int Id)
        {
            User user = null;
            try {
                string GetUserURL = string.Format("http://localhost:3000/api/Read/{0}", Id);
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(GetUserURL);
                    var responseTask = client.GetAsync(GetUserURL);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var asd = result.Content.ReadAsStringAsync();
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        
                        //var xcv = asdd[1];
                        //user = (User)serializer.DeserializeObject(asdd);
                        List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(asd.Result);
                        user = users.First();
                        logger.Info("Successfully retrived data from URL : " + GetUserURL);
                    }
                    else
                    {
                        user = (User)Enumerable.Empty<User>();
                        logger.Debug("Issue occured while consuming API");
                    }
                }
                return user;
            }
            catch (Exception ex) {
                logger.Error("Exception occuerd while retriving user records with Id="+Id.ToString(), ex);
                return user;
            }
        }

        public bool CreateOrEditUser(User user,string Action)
        {
            bool success = false;
            try {
                using (var client = new HttpClient()) {
                    Task<HttpResponseMessage> postTask = null;
                    if (Action == "Create") {
                        client.BaseAddress = new Uri(CreateUserURL);
                        postTask = client.PostAsJsonAsync<User>(CreateUserURL, user);
                    }
                    if (Action == "Edit") {
                        client.BaseAddress = new Uri(UpdateUserURL);
                        postTask = client.PostAsJsonAsync<User>(UpdateUserURL, user);
                    }
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                        success = true;
                }
                return success;
            }
            catch (Exception ex) {
                logger.Error("Exception occuerd while creating or updating user record", ex);
                return success;
            }
        }
         
        public bool DeleteUser(int Id)
        {
            bool success = false;
            try {
                string DeleteUserURL = string.Format("http://localhost:3000/api/Delete/{0}", Id);
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(DeleteUserURL);
                    var postTask = client.PostAsync(DeleteUserURL,null);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                        success = true;
                }
                return success;
            }
            catch (Exception ex) {
                logger.Error("Exception occuerd while deleting user record", ex);
                return success;
            }
        }
    }
}