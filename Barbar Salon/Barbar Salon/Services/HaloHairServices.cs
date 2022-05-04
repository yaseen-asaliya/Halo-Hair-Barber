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

    

       
        public async Task AcceptReservations(ReservationsRequestModel control)
        {
            var toUpdatePerson = (await firebaseClient
              .Child("ReservationsRequest")
              .OnceAsync<ReservationsRequestModel>()).Where(a => a.Object.ID_Reservations == control.ID_Reservations).FirstOrDefault();
            try
            {

                ReservationsModel reservationsModel = new ReservationsModel();
                {
                    reservationsModel.AccessToken_Barbar = control.AccessToken_Barbar;
                    reservationsModel.PersonName = control.PersonName;
                    reservationsModel.ListOfService = control.ListOfService;
                    reservationsModel.Time = control.Time;
                    reservationsModel.ID_Reservations = control.ID_Reservations;
                    reservationsModel.DateSelected = control.DateSelected;
                    reservationsModel.id = control.id;

                }

                await firebaseClient.Child("Reservations").PostAsync(reservationsModel);
               await Update(control.id, control.Time, true);

                var todelete = (await firebaseClient.Child("ReservationsRequest").OnceAsync<ReservationsRequestModel>())
                 .FirstOrDefault(item => item.Object.ID_Reservations == control.ID_Reservations);
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
                 .FirstOrDefault(item => item.Object.ID_Reservations == control.ID_Reservations);
                  Update(control.id, control.Time, false);

            await firebaseClient.Child("ReservationsRequest").Child(todelete.Key).DeleteAsync();
        }
        public async Task DeleteReservations(ReservationsModel control)
        {
            var todelete = (await firebaseClient.Child("Reservations").OnceAsync<ReservationsModel>())
                .FirstOrDefault(item => item.Object.ID_Reservations == control.ID_Reservations);
               await Update(control.id, control.Time, false);

            await firebaseClient.Child("Reservations").Child(todelete.Key).DeleteAsync();
        }

        public async Task Update(int Id, string selectedTime, bool Accept)
        {

            TimeModel timeModel = new TimeModel();
            {
                timeModel.Item1 = selectedTime;
                timeModel.Item2 = Accept;
            }


            var todelete = (await firebaseClient.Child("TIME").OnceAsync<TimeModel>())
                   .FirstOrDefault(item => item.Object.Time[Id].Item1 == selectedTime && item.Object.AccessToken_Barbar == accessToken);
            try
            {


                await firebaseClient
                     .Child($"TIME")
                     .Child(todelete.Key)
                     .Child($"Time/{Id}")
                     .PutAsync(timeModel);

            }

            catch (Exception ex)
            {
                await Xamarin.Forms.Shell.Current.DisplayAlert("Failed", ex.Message, "ok");
            }
        }

        public async Task AddService(ServiceModel addServices)
        {
            addServices.AccessToken_Barbar = accessToken;
            await firebaseClient.Child("Services").PostAsync(addServices);
        }




        public async Task UpdateService(MyServicesModel myServices)
        {
            var toUpdatePerson = (await firebaseClient
              .Child("Services")
              .OnceAsync<MyServicesModel>()).Where(a => a.Object.ID_Services == myServices.ID_Services).FirstOrDefault();
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
      
        public async Task DeleteService(MyServicesModel myServices)
        {
           
            try
            {
                var todelete = (await firebaseClient.Child("Services").OnceAsync<MyServicesModel>())
                .FirstOrDefault(item => item.Object.ID_Services == myServices.ID_Services);
                await firebaseClient.Child("Services").Child(todelete.Key).DeleteAsync();

                await Xamarin.Forms.Shell.Current.DisplayAlert("Successful", "Delete Services ", "Ok");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task AddNewUser(AuthenticationModel addUser)
        {
            await firebaseClient.Child("Users").PostAsync(addUser);

        }

        public async Task AddTime(ScheduleTimeModel scheduleTimeModel)
        {
            scheduleTimeModel.AccessToken_Barbar = accessToken;
            scheduleTimeModel.NameSalon = nameSoaln;
            scheduleTimeModel.location = location;
            await firebaseClient.Child("ScheduleTime").PostAsync(scheduleTimeModel);
        }
        public async Task AddTimes(List<(string, bool)> listTimes)
        {
            ScheduleTimeModel timeModel = new ScheduleTimeModel();
            {
                timeModel.Time = listTimes;
                timeModel.AccessToken_Barbar = accessToken;
            }


            await firebaseClient.Child("TIME").PostAsync(timeModel);
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


        public ObservableCollection<MyServicesModel> getServices()
        {

            var data = firebaseClient.Child("Services").AsObservable<MyServicesModel>().AsObservableCollection();

            return data;

        }

        public ObservableCollection<ProfilePageModel> ProfilePage()
        {
            var Users_Customer = firebaseClient.Child("Users").AsObservable<ProfilePageModel>().AsObservableCollection();

            return Users_Customer;
        }

        public async Task<string> StoreImage(Stream stream ,string FileName)
        {
            var image = await firebasStorege.Child("Offer").Child(FileName).PutAsync(stream);
            return image;
        }
        public async Task StoreImageUrl(OfferModel offerModel)

        {
            await firebaseClient.Child("Offer").PostAsync(offerModel);
        }

        public ObservableCollection<ScheduleTimeModel> GeMyTime()
        {

            try
            {
                var MyTime = firebaseClient.Child("ScheduleTime").AsObservable<ScheduleTimeModel>().AsObservableCollection();
                return MyTime;
            }
            catch (Exception ex)
            {
                Xamarin.Forms.Shell.Current.DisplayAlert("Failed", ex.Message, "Ok");
                return null;
            }

        }

        public async Task UpdateUser(ProfilePageModel profilePageModel)
        {

            var todelete = (await firebaseClient.Child("Users").OnceAsync<ProfilePageModel>())
                   .FirstOrDefault(item => item.Object.AccessToken_Barbar == accessToken);
            try
            {

                await firebaseClient
                     .Child($"Users")
                     .Child(todelete.Key)
                     .PutAsync(profilePageModel);
            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Shell.Current.DisplayAlert("Failed", ex.Message, "ok");
            }
        }

        public async Task DeleteMyTime(ScheduleTimeModel control)
        {
            var todelete = (await firebaseClient.Child("ScheduleTime").OnceAsync<ScheduleTimeModel>())
               .FirstOrDefault(item => item.Object.Id == control.Id && item.Object.AccessToken_Barbar == accessToken);
            await firebaseClient.Child("ScheduleTime").Child(todelete.Key).DeleteAsync();

            var delete = (await firebaseClient.Child("TIME").OnceAsync<TimeModel>())
             .FirstOrDefault(item=> item.Object.AccessToken_Barbar == accessToken);
            await firebaseClient.Child("TIME").Child(delete.Key).DeleteAsync();

        }

    }

}
