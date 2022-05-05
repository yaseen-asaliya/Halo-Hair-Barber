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
    public class AddTimeViewModel : BindableObject
    {

        public int StartTime { set; get; }
        public int EndTime { set; get; }
        public TimeSpan StartTimeSelected { set; get; }
        public TimeSpan EndTimeSelected { get; set; }
        public ICommand AddTimeCommand { get; }

        public ICommand BackPage { get; }
        private string date;
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddTime1Command { get; }

        private HaloHairServices fireBase;

        public AddTimeViewModel()
        {
            fireBase = new HaloHairServices();
            AddTimeCommand = new Command(OnAddTimeTapped);
            BackPage = new Command(Back_Page);
        }

        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }



        Random rnd;


        private async void OnAddTimeTapped(object obj)
        {
            rnd = new Random();
            int id = rnd.Next(0, 1236963000);

            ScheduleTimeModel scheduleTimeModel = new ScheduleTimeModel
            {

                Id = id,
                StartTime = StartTimeSelected,
                EndTime = EndTimeSelected,
            };
            await fireBase.AddTime(scheduleTimeModel);
            SearchTime();
            await AddTIME();
            await Application.Current.MainPage.DisplayAlert("Successful", $" Add the working time of the day {Date} \n From hour {StartTimeSelected} to hour {EndTimeSelected}", "Ok");
            await Application.Current.MainPage.Navigation.PopModalAsync();

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

            time startTime = getTimeSplitedAsInt(StartTimeSelected.ToString());
            time endTime = getTimeSplitedAsInt(EndTimeSelected.ToString());

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
            await fireBase.AddTimes(ListTimes);
        }
    }
}
