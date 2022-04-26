using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Barbar_Salon.Models;
using Barbar_Salon.Services;
using Xamarin.Forms;

namespace Barbar_Salon.ViewModels
{
    public class AddTimeViewModel
    {

        HaloHairServices Firebase;


       public ICommand AddTimeCommand { get; }


        public AddTimeViewModel()
        {
            SearchTime();
            Firebase = new HaloHairServices();
            AddTimeCommand = new Command(async () => await AddTIME());

        }



        struct time
        {
            public int hour;
            public int minute;
        }
  
        time getTimeSplitedAsInt(string time)
        {
            time temp = new time();
            string[] tempTime = time.Split(':');
            temp.hour = Int32.Parse(tempTime[0]);
            temp.minute = Int32.Parse(tempTime[1]);
            return temp;
        }
        string getTimeAsString(int time)
        {
            int hours = time / 60;
            int minutes = time % 60;
            string Time = "";
            if (hours > 12)
            {
                if (minutes < 10)
                {
                    Time = (hours - 12) + ":0" + minutes + " PM";

                }
                else
                {
                    Time = (hours - 12) + ":" + minutes + " PM";
                }
            }
            else
            {
                if (minutes < 10)
                {
                    Time = hours + ":0" + minutes + " AM";
                }
                else
                {
                    Time = hours + ":" + minutes + " AM";
                }
            }
            return Time;

        }
        private List<(string, bool)> ListTimes;

        public async void SearchTime()
        {
   
            ListTimes = new List<(string, bool)>();
            //  time startTime = getTimeSplitedAsInt(starttime);
            //  time endTime = getTimeSplitedAsInt(endtime);
            time startTime = getTimeSplitedAsInt("10:35");
            time endTime = getTimeSplitedAsInt("15:34");
            int start = (startTime.hour * 60) + startTime.minute;
            int end = (endTime.hour * 60) + endTime.minute;
            bool isBooked = false;

            for (int i = start; i < end; i += 30)
            {
                string tempTime = "";
                tempTime = getTimeAsString(i);
                isBooked = false;
                ListTimes.Add((tempTime, isBooked));



            }
            

        }


        public async Task AddTIME()
        {
            await Firebase.AddTimes(ListTimes);
        }







        




    }
}
