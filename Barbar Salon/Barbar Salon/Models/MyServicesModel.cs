using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Models
{
    public class MyServicesModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Price { get; set; }
        public int TimeNeed { get; set; }
        public string Deseription { get; set; }
        public string BarberAccessToken { get; set; }


    }
}
