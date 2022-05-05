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
        private FirebaseClient _firebaseClient;
        private FirebaseStorage _firebasStorege;
        private static string _accessToken { get; set; }
        private static string _salonName{ get; set; }
        private static string _location { get; set; }

        public HaloHairServices()
        {
            _firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");
            _firebasStorege = new FirebaseStorage("halo-hair-676ed.appspot.com");
            AccessToken();

        }
    

        private async void AccessToken()
        {
            try
            {
                _accessToken = await SecureStorage.GetAsync("oauth_token");
                _salonName = await SecureStorage.GetAsync("NameSoaln");
                _location = await SecureStorage.GetAsync("location");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    

       
        public async Task AcceptReservations(ReservationsRequestModel control)
        {
           
            try
            {

                ReservationsModel reservationsModel = new ReservationsModel();
                {
                    reservationsModel.BarberAccessToken = control.BarberAccessToken;
                    reservationsModel.CustomerName = control.CustomerName;
                    reservationsModel.ListOfService = control.ListOfService;
                    reservationsModel.TimeSelected = control.TimeSelected;
                    reservationsModel.ReservationsId = control.ReservationsId;
                    reservationsModel.DateSelected = control.DateSelected;
                    reservationsModel.Id = control.Id;

                }

                await _firebaseClient.Child("Reservations").PostAsync(reservationsModel);
                await UpdateReservationsTime(control.Id, control.TimeSelected, true);

                var toReservationsRequest = (await _firebaseClient.Child("ReservationsRequest").OnceAsync<ReservationsRequestModel>())
                 .FirstOrDefault(item => item.Object.ReservationsId == control.ReservationsId);
                await _firebaseClient.Child("ReservationsRequest").Child(toReservationsRequest.Key).DeleteAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public async Task RefusedReservations(ReservationsRequestModel control)
        {
            var toDeleteRequest = (await _firebaseClient.Child("ReservationsRequest").OnceAsync<ReservationsRequestModel>())
                 .FirstOrDefault(item => item.Object.ReservationsId == control.ReservationsId);
                  await UpdateReservationsTime(control.Id, control.TimeSelected, false);

            await _firebaseClient.Child("ReservationsRequest").Child(toDeleteRequest.Key).DeleteAsync();
        }
        public async Task DeleteReservations(ReservationsModel control)
        {
            var toDeleteReservations = (await _firebaseClient.Child("Reservations").OnceAsync<ReservationsModel>())
                .FirstOrDefault(item => item.Object.ReservationsId == control.ReservationsId);
               await UpdateReservationsTime(control.Id, control.TimeSelected, false);

            await _firebaseClient.Child("Reservations").Child(toDeleteReservations.Key).DeleteAsync();
        }

        public async Task UpdateReservationsTime(int id, string selectedTime, bool accept)
        {

            TimeModel timeModel = new TimeModel();
            {
                timeModel.Item1 = selectedTime;
                timeModel.Item2 = accept;
            }

            var toUpdateChild = (await _firebaseClient.Child("Worktime").OnceAsync<TimeModel>())
                   .FirstOrDefault(item => item.Object.Time[id].Item1 == selectedTime && item.Object.BarberAccessToken == _accessToken);
   
           await _firebaseClient
                 .Child($"Worktime")
                 .Child(toUpdateChild.Key)
                 .Child($"Time/{id}")
                 .PutAsync(timeModel);

           
        }



        public async Task AddService(MyServicesModel addServices)
        {
            addServices.BarberAccessToken = _accessToken;
            await _firebaseClient.Child("Services").PostAsync(addServices);
        }

        public async Task UpdateService(MyServicesModel myServices)
        {
            var toUpdateService = (await _firebaseClient
              .Child("Services")
              .OnceAsync<MyServicesModel>()).Where(a => a.Object.ServiceId == myServices.ServiceId).FirstOrDefault();
            myServices.BarberAccessToken = _accessToken;
            try
            {

                await _firebaseClient
                  .Child("Services")
                  .Child(toUpdateService.Key)
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
                var toDeleteServices = (await _firebaseClient.Child("Services").OnceAsync<MyServicesModel>())
                .FirstOrDefault(item => item.Object.ServiceId == myServices.ServiceId);
                await _firebaseClient.Child("Services").Child(toDeleteServices.Key).DeleteAsync();

                await Xamarin.Forms.Shell.Current.DisplayAlert("Successful", "Delete Services ", "Ok");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task AddNewUser(AuthenticationModel addUser)
        {
            await _firebaseClient.Child("BarBers").PostAsync(addUser);

        }

        public async Task AddTime(ScheduleTimeModel scheduleTimeModel)
        {
            scheduleTimeModel.BarberAccessToken = _accessToken;
            scheduleTimeModel.SalonName = _salonName;
            scheduleTimeModel.Location = _location;
            await _firebaseClient.Child("ScheduleTime").PostAsync(scheduleTimeModel);
        }
        public async Task StoreScheduleTimes(List<(string, bool)> listTimes,int id)
        {
            ScheduleTimeModel timeModel = new ScheduleTimeModel();
            {
                timeModel.Time = listTimes;
                timeModel.BarberAccessToken = _accessToken;
                timeModel.Id = id;
            }
            await _firebaseClient.Child("Worktime").PostAsync(timeModel);
        }
       

        public ObservableCollection<ReservationsRequestModel> GetReservationsRequest()
        {
            var ReservationsRequestdata = _firebaseClient.Child("ReservationsRequest").AsObservable<ReservationsRequestModel>().AsObservableCollection();
            
            return ReservationsRequestdata;
        }

      
    public ObservableCollection<ReservationsModel> GetReservation()
        {
            var Reservationsdata = _firebaseClient.Child("Reservations").AsObservable<ReservationsModel>().AsObservableCollection();


            return Reservationsdata;
        }


        public ObservableCollection<MyServicesModel> GetServices()
        {

            var data = _firebaseClient.Child("Services").AsObservable<MyServicesModel>().AsObservableCollection();

            return data;

        }

        public ObservableCollection<ProfilePageModel> ProfilePage()
        {
            var Users_Customer = _firebaseClient.Child("BarBers").AsObservable<ProfilePageModel>().AsObservableCollection();

            return Users_Customer;
        }

        public async Task<string> StoreImage(Stream stream ,string FileName)
        {
            var image = await _firebasStorege.Child("Offer").Child(FileName).PutAsync(stream);
            return image;
        }
        public async Task StoreImageUrl(OfferModel offerModel)

        {
            await _firebaseClient.Child("Offer").PostAsync(offerModel);
        }

        public ObservableCollection<ScheduleTimeModel> GeMyTime()
        {

            try
            {
                var MyTime = _firebaseClient.Child("ScheduleTime").AsObservable<ScheduleTimeModel>().AsObservableCollection();
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

            var toUpdateBarbar = (await _firebaseClient.Child("Barbers").OnceAsync<ProfilePageModel>())
                   .FirstOrDefault(item => item.Object.BarberAccessToken == _accessToken);
            try
            {

                await _firebaseClient
                     .Child("Barbers")
                     .Child(toUpdateBarbar.Key)
                     .PutAsync(profilePageModel);
            }
            catch (Exception ex)
            {
                await Xamarin.Forms.Shell.Current.DisplayAlert("Failed", ex.Message, "ok");
            }
        }

        public async Task DeleteMyTime(ScheduleTimeModel control)
        {
            var toDeleteMyScheduleTime = (await _firebaseClient.Child("ScheduleTime").OnceAsync<ScheduleTimeModel>())
               .FirstOrDefault(item => item.Object.Id == control.Id && item.Object.BarberAccessToken == _accessToken);
            await _firebaseClient.Child("ScheduleTime").Child(toDeleteMyScheduleTime.Key).DeleteAsync();

            var toDeleteMyWorktime = (await _firebaseClient.Child("Worktime").OnceAsync<TimeModel>())
             .FirstOrDefault(item=> item.Object.BarberAccessToken == _accessToken);
            await _firebaseClient.Child("Worktime").Child(toDeleteMyWorktime.Key).DeleteAsync();

        }

    }

}
