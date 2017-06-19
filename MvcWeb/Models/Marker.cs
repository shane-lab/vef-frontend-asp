using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Marker
    {
        /// <summary>
        /// The id of the marker
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// The id of the owner of the marker
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// The id of the room this marker belongs to
        /// </summary>
        public String RoomId { get; set; }

        /// <summary>
        /// (optional) The message of the marker
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// The creation date (timestamp) of the marker
        /// </summary>
        public DateTime? TimeStamp { get; set; }
    }
}
