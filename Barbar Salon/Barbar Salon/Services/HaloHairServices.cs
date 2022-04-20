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
using static System.Net.Mime.MediaTypeNames;
using Firebase.Storage;
using System.IO;

namespace Barbar_Salon.Services
{
    public class HaloHairServices
    {
        FirebaseClient firebaseClient;
        FirebaseStorage firebasStorege;
        public HaloHairServices()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");
            firebasStorege = new FirebaseStorage("halo-hair-676ed.appspot.com");
            AccessToken();
        }
        private static string accessToken { get; set; }
        private static string nameSoaln { get; set; }
        private static string location { get; set; }

        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                var onameSoaln = await SecureStorage.GetAsync("NameSoaln");
                var olocation = await SecureStorage.GetAsync("location");

                accessToken = oauthToken;
                nameSoaln = onameSoaln;
                location = olocation;

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
                addServices.AccessToken_Barbar = accessToken;
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
                  .PutAsync(new ReservationsRequestModel() { PersonName = control.PersonName, Time = control.Time,
                      Accept = true, ListOfService = control.ListOfService,
                      AccessToken_Barbar = control.AccessToken_Barbar,
                      DataSelected=control.DataSelected });
                   ReservationsModel reservationsModel = new ReservationsModel();
                   {
                    reservationsModel.AccessToken_Barbar = control.AccessToken_Barbar;
                    reservationsModel.PersonName = control.PersonName;
                     reservationsModel.ListOfService = control.ListOfService;
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

        public async Task UpdateService(MyServicesModel myServices)
        {
            var toUpdatePerson = (await firebaseClient
              .Child("Services")
              .OnceAsync<MyServicesModel>()).Where(a => a.Object.Service_Name == myServices.Service_Name).FirstOrDefault();
            myServices.AccessToken_Barbar = accessToken;
            try
            {
                await firebaseClient
                  .Child("Services")
                  .Child(toUpdatePerson.Key)
                  .PutAsync(myServices);
                await Xamarin.Forms.Shell.Current.DisplayAlert("Successful", "Update Services ", "Ok");



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public async Task AddNewUser(string name, string namesalon, long phone, string ulr, string location)
        {
            AuthenticationModel addUser = new AuthenticationModel();
            {
                addUser.Name = name;
                addUser.NameSalon = namesalon;
                addUser.Phone = phone;
                addUser.AccessToken_Barbar = ulr;
                addUser.location = location;


            }
            await firebaseClient.Child("Users").PostAsync(addUser);

        }

        public async Task AddTime(ScheduleTimeModel scheduleTimeModel)
        {

            //ScheduleTimeModel scheduleTimeModel = new ScheduleTimeModel();
            //{
            //    scheduleTimeModel.StartTime = StartTime;
            //    scheduleTimeModel.EndTime = EndTime;
            //    scheduleTimeModel.DateSelected = CalendarSelectedDate;
            //    scheduleTimeModel.AccessToken = accessToken;
            //}

            scheduleTimeModel.AccessToken_Barbar = accessToken;
            scheduleTimeModel.NameSalon = nameSoaln;
            scheduleTimeModel.location = location;




            await firebaseClient.Child("ScheduleTime").PostAsync(scheduleTimeModel);
            await Xamarin.Forms.Shell.Current.DisplayAlert("Successful", "Schedule Time", "Ok");



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

        public ObservableCollection<ProfilePageModel> ProfilePage()
        {
            var Users_Customer = firebaseClient.Child("Users").AsObservable<ProfilePageModel>().AsObservableCollection();


            return Users_Customer;
        }

        public async Task<string> StoreImage(Stream stream ,string FileName)
        {
            Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            var image = await firebasStorege.Child("Offer").Child(FileName).PutAsync(stream);
            return image;
        }
        public async Task StoreImageUrl(OfferModel offerModel)

        {
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            await firebaseClient.Child("Offer").PostAsync(offerModel);
        }

    }

}
