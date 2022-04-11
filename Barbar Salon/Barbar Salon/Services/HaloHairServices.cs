using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Barbar_Salon.Models;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.Collections.Specialized;

namespace Barbar_Salon.Services
{
    public class HaloHairServices
    {
        FirebaseClient firebaseClient;

        public HaloHairServices()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");
            AccessToken();
        }
        private static string accessToken { get; set; }

        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    

        public async Task AddService(string serviceName, int timeNeede, int prices, string deseription)
        {
            ServiceModel addServices = new ServiceModel();
            {
                addServices.Service_Name = serviceName;
                addServices.Time_Needed = timeNeede;
                addServices.Prices = prices;
                addServices.Deseription = deseription;
                addServices.AccessToken = accessToken;
            }

            await firebaseClient.Child("Services").PostAsync(addServices);
        }
        public async Task AcceptReservations(ReservationsRequestModel control)
        {
            var toUpdatePerson = (await firebaseClient
              .Child("ReservationsRequest")
              .OnceAsync<ReservationsRequestModel>()).Where(a => a.Object.PersonName == control.PersonName).FirstOrDefault();
            try
            {
                await firebaseClient
                  .Child("ReservationsRequest")
                  .Child(toUpdatePerson.Key)
                  .PutAsync(new ReservationsRequestModel() { PersonName = control.PersonName, Time = control.Time, Accept = true, ListOfService = control.ListOfService, AccessToken = control.AccessToken,DataSelected=control.DataSelected });
                   ReservationsModel reservationsModel = new ReservationsModel();
                   {
                    reservationsModel.AccessToken = control.AccessToken;
                    reservationsModel.PersonName = control.PersonName;
                    reservationsModel.ListOfService= control.ListOfService;
                    reservationsModel.Time= control.Time;
                    reservationsModel.Accept = control.Accept;

                }
                
                await firebaseClient.Child("Reservations").PostAsync(reservationsModel);
                var todelete = (await firebaseClient.Child("ReservationsRequest").OnceAsync<ReservationsRequestModel>())
                 .FirstOrDefault(item => item.Object.PersonName == control.PersonName);
                await firebaseClient.Child("ReservationsRequest").Child(todelete.Key).DeleteAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task RefusedReservations(ReservationsRequestModel control)
        {
            var todelete = (await firebaseClient.Child("ReservationsRequest").OnceAsync<ReservationsRequestModel>())
                 .FirstOrDefault(item => item.Object.PersonName == control.PersonName);
            await firebaseClient.Child("ReservationsRequest").Child(todelete.Key).DeleteAsync();
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


        public async Task AddNewUser(string name, string namesalon, long phone, string ulr, string location)
        {
            Console.WriteLine(ulr.ToString());
            AuthenticationModel addUser = new AuthenticationModel();
            {
                addUser.Name = name;
                addUser.NameSalon = namesalon;
                addUser.Phone = phone;
                addUser.ulr = ulr;
                addUser.location = location;


            }
            await firebaseClient.Child("Users").PostAsync(addUser);

        }
        public async Task AddTime(int StartTime, int EndTime, string CalendarSelectedDate)
        {

            ScheduleTimeModel scheduleTimeModel = new ScheduleTimeModel();
            {
                scheduleTimeModel.StartTime = StartTime;
                scheduleTimeModel.EndTime = EndTime;
                scheduleTimeModel.DateSelected = CalendarSelectedDate;
                scheduleTimeModel.AccessToken = accessToken;
            }

            await firebaseClient.Child("ScheduleTime").PostAsync(scheduleTimeModel);

        }

        public ObservableCollection<MyServicesModel> getServices()
        {
        
            var data =  firebaseClient.Child("Services").AsObservable<MyServicesModel>().AsObservableCollection();

            return data;
     
           
        }
        

    


        public ObservableCollection<ReservationsRequestModel> getReservationsRequest()
        {
            var ReservationsRequestdata = firebaseClient.Child("ReservationsRequest").AsObservable<ReservationsRequestModel>().AsObservableCollection();
            
            return ReservationsRequestdata;
        }


        public ObservableCollection<ReservationsModel> getReservation()
        {
            var Reservationsdata = firebaseClient.Child("Reservations").AsObservable<ReservationsModel>().AsObservableCollection();


            return Reservationsdata;
        }

        public ObservableCollection<ReservationsModel> getReservations()
        {

            var reservationsdata = firebaseClient.Child("Reservations").AsObservable<ReservationsModel>().AsObservableCollection();
          
            
            return reservationsdata;

        }

      

        public async Task DeleteReservations(ReservationsModel control)
        {
            var todelete = (await firebaseClient.Child("Reservations").OnceAsync<ReservationsModel>())
                .FirstOrDefault(item => item.Object.PersonName == control.PersonName);
            await firebaseClient.Child("Reservations").Child(todelete.Key).DeleteAsync();
        }





    }

}
