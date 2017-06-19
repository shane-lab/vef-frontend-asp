using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MvcWeb.Models.Promises;
using Newtonsoft.Json;
using Models;
using MvcWeb.ViewModels;

namespace MvcWeb.Controllers
{
    public class RoomController : BaseController
    {
        public RoomController(IAuthenticated authenticated) : base(authenticated)
        {
        }
        
        public ActionResult Index()
        {
            return SPFView(new Marker
            {
                Id = "4910-asd0f9-30jad",
                Message = "Resolving view data test"
            });
        }

        public ActionResult Join(String roomCode)
        {
            throw new NotImplementedException();
        }

        public ActionResult PeerTest(String peer)
        {
            return SPFView(new StreamViewModel
            {
                PeerId = peer
            });
        }
    }
}