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
  
    public class HaloHairServices
    {
        FirebaseClient firebaseClient;

        public HaloHairServices()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");

        }



        public async Task AddService(string serviceName, int timeNeede, int prices, string deseription)
        {
            ServiceModel addServices = new ServiceModel();
            {
                addServices.Service_Name = serviceName;
                addServices.Time_Needed = timeNeede;
                addServices.Prices = prices;
                addServices.Deseription = deseription;

            }


            await firebaseClient.Child("Services").PostAsync(addServices);
        }




        public async Task UpdateService(string servicesName, int time_Needed, int Prices, string Deseription)
        {
            var toUpdatePerson = (await firebaseClient
              .Child("Services")
              .OnceAsync<MyServicesModel>()).Where(a => a.Object.Service_Name == servicesName).FirstOrDefault();
            try
            {
                await firebaseClient
                  .Child("Services")
                  .Child(toUpdatePerson.Key)
                  .PutAsync(new MyServicesModel() { Service_Name = servicesName, Time_Needed = time_Needed, Prices = Prices, Deseription = Deseription });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


    }


}
