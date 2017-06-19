using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MvcWeb
{
    public class RemoteConfig
    {

        #region SubRoutes
        public static readonly SubRoute AUTHENTICATE = new SubRoute("authenticate", Method.POST);

        public static readonly SubRoute USER = new SubRoute("users/me", Method.GET);

        public static readonly SubRoute ROOMS = new SubRoute("rooms", Method.GET);

        public static readonly SubRoute ROOM = new SubRoute("rooms/{room_id}", Method.GET);

        public static readonly SubRoute ROOM_CREATE = new SubRoute("rooms", Method.POST);

        public static readonly SubRoute ROOM_EDIT = new SubRoute("rooms/{room_id}", Method.PUT);

        public static readonly SubRoute ROOM_DELETE = new SubRoute("rooms/{room_id}", Method.DELETE);
        #endregion

        private const String _protocol = "http";

        private const String _ipAddress = "192.168.27.143";

        private const int _port = 3000;

        public static String Route
        {
            get
            {
                return $"{_protocol}://{_ipAddress}:{_port}/api/";
            }
        }

        public static RestClient Client
        {
            get
            {
                return new RestClient(Route);
            }
        }
    }

    public struct SubRoute
    {
        public String Route { private set; get; }

        public Method Method { private set; get; }

        public RestRequest Request
        {
            get
            {
                return new RestRequest(Route, Method);
            }
        }

        public SubRoute(String route, Method method)
        {
            Route = route;
            Method = method;
        }
    }
}