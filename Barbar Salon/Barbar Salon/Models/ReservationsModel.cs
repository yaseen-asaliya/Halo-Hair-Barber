using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Models
{
    public class ReservationsModel
    {
        public string CustomerName { get; set; }
        public int ReservationsId { get; set; }
        public string ListOfService { get; set; }
        public string TimeSelected { get; set; }
        public int Id { get; set; } 
        public string BarberAccessToken { get; set; }
        public string DateSelected { get; set; }

     
    }
}
