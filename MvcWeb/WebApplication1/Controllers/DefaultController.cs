using MvcWeb.App_Start;
using MvcWeb.Models.Promises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWeb.Controllers
{
    public class DefaultController : BaseController 
    {
        public DefaultController(IAuthenticated authenticated) : base(authenticated)
        {
        }

        public ActionResult Index()
        {
            ViewBag.Title = Authenticated.getFullName();

            return SPFView();
        }
    }
}