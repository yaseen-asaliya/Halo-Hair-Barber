using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using Barbar_Salon.Models;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace Barbar_Salon.Services
{
    public class FireBaseHaloHair
    {
        FirebaseClient firebaseClient;
        public FireBaseHaloHair()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");

        }


        public async Task AddTime(string StartTime, string EndTime, string CalendarSelectedDate)
        {

            ScheduleTimeModel scheduleTimeModel = new ScheduleTimeModel();
            {
                scheduleTimeModel.StartTime = StartTime;
                scheduleTimeModel.EndTime = EndTime;
                scheduleTimeModel.DateSelected = CalendarSelectedDate;
            }

            await firebaseClient.Child("ScheduleTime").PostAsync(scheduleTimeModel);

        }







    }
}
