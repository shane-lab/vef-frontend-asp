using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// The associated email address of the user
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// The first name of the user
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// The creation date (timestamp) of the user
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    }
}
