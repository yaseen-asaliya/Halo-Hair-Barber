using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Models
{
    public class ReservationsModel
    {
        public string PersonName { get; set; }
        public int ID_Reservations { get; set; }

        public string ListOfService { get; set; }

        public string Time { get; set; }
        public int id { get; set; } 

        public bool Accept { get; set; }

        public string AccessToken_Barbar { get; set; }


        public string DateSelected { get; set; }

     

    }
}
