using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Models
{
    public class ReservationsRequestModel
    {
        public string PersonName { get; set; }

        public string ListOfService { get; set; }

        public string Time { get; set; }

        public bool Accept { get; set; }

        public string AccessToken { get; set; }


        public string DataSelected { get; set; }


    }
}
