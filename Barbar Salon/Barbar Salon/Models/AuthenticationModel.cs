using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Models
{
    public class AuthenticationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SalonName { get; set; }
        public string BarberName { get; set; }
        public long Phone { get; set; }
        public string Location { get; set; }
        public string BarberAccessToken { get; set; }

    }
}
