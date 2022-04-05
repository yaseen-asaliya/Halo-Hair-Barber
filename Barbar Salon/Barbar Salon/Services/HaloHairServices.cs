using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Barbar_Salon.Models;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Linq;

namespace Barbar_Salon.Services
{
    public class FireBaseHaloHair
    {
        FirebaseClient firebaseClient;
        public FireBaseHaloHair()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");

        }
        public ObservableCollection<ReservationsModel> getReservations()
        {

            var Reservationsdata = firebaseClient.Child("Reservations").AsObservable<ReservationsModel>().AsObservableCollection();

            return Reservationsdata;
        }
    }
   
}
