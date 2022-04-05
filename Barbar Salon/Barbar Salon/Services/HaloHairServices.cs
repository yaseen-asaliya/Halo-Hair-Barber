using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Services
{
    public class HaloHairServices
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");
    }
     public ObservableCollection<MyServicesModel> getServices()
        {
            var servicesData = firebaseClient.Child("Services").AsObservable<MyServicesModel>().AsObservableCollection();

            return servicesData;
        }
}
