using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Room
    {
        /// <summary>
        /// The id of the room
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// The id of the owner of the room
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// The title of the room
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// The code (uuid) of the room, used to share the room 
        /// </summary>
        public String Code { get; set; }
        
        /// <summary>
        /// The start date (timestamp) of the room
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The end date (timestamp) of the room
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
