using BoilerplateData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCore2Boilerplate.Models
{
    public class ApplicationUser : ClaimsPrincipal
    {

        public ApplicationUser(ClaimsPrincipal principal)
        : base(principal) { }

        public int ID
        {
            get
            {
                if (this.FindFirstValue(ClaimTypes.NameIdentifier) != null)
                {
                    return int.Parse(this.FindFirstValue(ClaimTypes.NameIdentifier));
                }
                else
                {
                    return 0;
                }
            }
        }

        public string Username
        {
            get
            {
                if (this.FindFirstValue(ClaimTypes.Name) != null)
                {
                    return this.FindFirstValue(ClaimTypes.Name);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string EmailAddress
        {
            get
            {
                if (this.FindFirstValue(ClaimTypes.Email) != null)
                {
                    return this.FindFirstValue(ClaimTypes.Email);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Role
        {
            get
            {
                if (this.FindFirstValue(ClaimTypes.Role) != null)
                {
                    return this.FindFirstValue(ClaimTypes.Role);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static ClaimsPrincipal SetClaims(UserDTO user)
        {
            var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.EmailAddress),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                        new Claim("LastUpdated", string.Empty, ClaimValueTypes.Time)
                    };

            var userIdentity = new ClaimsIdentity(claims, "User");
            return new ClaimsPrincipal(userIdentity);
        }
    }
}
