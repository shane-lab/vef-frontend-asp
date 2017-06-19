using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWeb.ViewModels
{
    public class RoomViewModel
    {
        /// <summary>
        /// The room model instance
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// The owner of the room
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// The created markers
        /// </summary>
        public IEnumerable<Marker> Markers { get; set; }

        /// <summary>
        /// The invited peer assessors
        /// </summary>
        public IEnumerable<User> Peers { get; set; }

        /// <summary>
        /// Constructs a viewmodel from a JSON object
        /// </summary>
        /// <param name="token">the json object to parse</param>
        /// <returns>RoomViewModel</returns>
        public static RoomViewModel FromJson(JToken token)
        {
            return null;
        }
    }

    public class StreamViewModel
    {
        public String PeerId { get; set; }

        public User Owner { get; set; }
    }
}