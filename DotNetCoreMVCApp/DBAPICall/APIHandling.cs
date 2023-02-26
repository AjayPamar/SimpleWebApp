using DotNetCoreMVCApp.Models;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNetCoreMVCApp.DBAPICall
{
    public class APIHandling
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(CallNodeJsAPI));
        public string? Get(string URL)
        {
            try
            {
                using (var client = new HttpClient()) 
                {
                    client.BaseAddress = new Uri(URL);
                    var responseTask = client.GetAsync(URL);
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                        return result.Content.ReadAsStringAsync().Result;
                    else
                        logger.Debug("Api call to get info not succeded");
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Exception occured while calling Get api",ex);
                return null;
            }
        }

        public bool Post(string URL, object model, string Action)
        {
            bool res = false;
            HttpResponseMessage result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    Task<HttpResponseMessage> postTask = null;
                    client.BaseAddress = new Uri(URL);

                    switch (Action)
                    {
                        case "User":
                            postTask = client.PostAsJsonAsync<UserModel>(URL, (UserModel)model);
                            postTask.Wait();
                            result = postTask.Result;
                            if (result.IsSuccessStatusCode)
                                res = true;

                            break;

                        case "CheckUserMail":
                            UserModel asd = new UserModel() { Email = (string)model };
                            postTask = client.PostAsJsonAsync<UserModel>(URL, (UserModel)asd);
                            postTask.Wait();
                            result = postTask.Result;

                            if (result.IsSuccessStatusCode)
                            {
                                //res = true;
                                var asds = result.Content.ReadAsStringAsync().Result;
                                if (asds=="[]")
                                    res = true;
                                else
                                    res = false;
                            }

                            break;

                        default:
                            break;
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                logger.Error("Exception occured while calling Post Api",ex);
                return res;
            }
        }

        public bool DeletePOST(string URL)
        {
            bool res = false;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    var postTask = client.PostAsync(URL, null);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                        res = true;
                }
                return res;
            }
            catch (Exception ex)
            {
                logger.Error("Exception occured while calling Post Api to Delete Record", ex);
                return res;
            }
        }
    }
}
