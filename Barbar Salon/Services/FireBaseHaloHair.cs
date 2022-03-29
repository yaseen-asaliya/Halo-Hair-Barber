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
       

        public async Task AddTime(int StartTime,int EndTime,string CalendarSelectedDate)
        {

            ScheduleTimeModel scheduleTimeModel = new ScheduleTimeModel();
            {
                scheduleTimeModel.StartTime = StartTime;
                scheduleTimeModel.EndTime = EndTime;
                scheduleTimeModel.DateSelected = CalendarSelectedDate;
            }

            await firebaseClient.Child("ScheduleTime").PostAsync(scheduleTimeModel);

        }


        public ObservableCollection<MyServicesModel> getServices()
        {
            var servicesData = firebaseClient.Child("Services").AsObservable<MyServicesModel>().AsObservableCollection();

            return servicesData;
        }










    }
}
