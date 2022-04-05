using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Barbar_Salon.Models;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace Barbar_Salon.Services
{
    public class FireBaseHaloHair
    {
        FirebaseClient firebaseClient;

        public FireBaseHaloHair()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");

        }
        public ObservableCollection<MyServicesModel> getServices()
        {
            var servicesData = firebaseClient.Child("Services").AsObservable<MyServicesModel>().AsObservableCollection();

            return servicesData;
        }
    }
}