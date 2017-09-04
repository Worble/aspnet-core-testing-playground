using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerplateData.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
