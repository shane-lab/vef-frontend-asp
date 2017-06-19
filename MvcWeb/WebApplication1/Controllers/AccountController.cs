using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWeb.App_Start;
using MvcWeb.ViewModels;
using MvcWeb.Models.Promises;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using MvcWeb.Models;
using Models;

namespace MvcWeb.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IAuthenticated authenticated) : base(authenticated)
        {
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAccountViewModel account)
        {
            IAuthenticated authenticated = null;
            if (ModelState.IsValid)
            {
                RestClient client = RemoteConfig.Client;

                RestRequest request = RemoteConfig.AUTHENTICATE.Request;
                request.AddParameter("email", account.Email);
                request.AddParameter("password", account.Password);

                IRestResponse response = client.Execute(request);
                
                try
                {
                    JObject jObject = JObject.Parse(response.Content);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        String token = (String)jObject["token"];

                        request = RemoteConfig.USER.Request;
                        request.AddHeader("Authorization", $"JWT {token}");
                        response = client.Execute(request);

                        try
                        {
                            jObject = JObject.Parse(response.Content);

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                JToken jToken = jObject["user"];
                                authenticated = new AuthUser(new User
                                {
                                    Id = (String)jToken["_id"],
                                    FirstName = (String)jToken["firstName"],
                                    LastName = (String)jToken["lastName"],
                                    Email = (String)jToken["email"],
                                    // TODO createdAt, convert string to date
                                }, token);
                            }
                        }
                        catch
                        {
                            Debug.WriteLine("exception");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("password", (String)jObject["message"]);
                    }
                }
                catch
                {
                    ModelState.AddModelError("password", "Unable issue the server for authentication");
                }
            }

            if (authenticated != null)
            {
                Session[Reference.KEY_USER_SESSION] = authenticated as IAuthenticated;

                return RedirectToAction("Index", "Default");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            return RedirectToAction("Login");
        }
    }
}