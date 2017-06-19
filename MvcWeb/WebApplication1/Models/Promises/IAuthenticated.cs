using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcWeb.Models.Promises
{
    public interface IAuthenticated
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        String Id { get; }

        /// <summary>
        /// The access token of the user, used in authorization
        /// </summary>
        String AccessToken { get; }

        /// <summary>
        /// The first name of the user
        /// </summary>
        String FirstName { get; }

        /// <summary>
        /// The last name of the user
        /// </summary>
        String LastName { get; }

        /// <summary>
        /// The associated email address of the user
        /// </summary>
        String Email { get; }

        /// <summary>
        /// The creation date (timestamp) of the user
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Get the full name of the user
        /// </summary>
        /// <returns>String</returns>
        String getFullName();
    }
}
