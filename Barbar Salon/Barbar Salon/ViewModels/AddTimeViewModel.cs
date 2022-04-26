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
        private DateTime dateTime { set; get; }
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

        private int year, month, day;
        public ICommand IncreaseDateCommand { get; }
        public ICommand DecreaseDateCommand { get; }
        public ICommand AddTime1Command { get; }

        private HaloHairServices fireBase;

        public AddTimeViewModel()
        {
            fireBase = new HaloHairServices();
            
            dateTime = DateTime.Now;
            Date = dateTime.ToString("dddd, dd MMMM yyyy");
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;

            IncreaseDateCommand = new Command(OnIncreaseTapped);
            DecreaseDateCommand = new Command(OnDecreaseTapped);
            AddTimeCommand = new Command(OnAddTimeTapped);
            BackPage = new Command(Back_Page);
        }

        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void OnDecreaseTapped(object obj)
        {
            DateTime nowDate = new DateTime(year, month, day);
            var previewDate = nowDate.AddDays(-1);
            Date = previewDate.ToString("dddd, dd MMMM yyyy");
            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;
        }

        private void OnIncreaseTapped(object obj)
        {
            DateTime nowDate = new DateTime(year, month, day);
            var previewDate = nowDate.AddDays(1);
            Date = previewDate.ToString("dddd, dd MMMM yyyy");
            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;
        }

        private async void OnAddTimeTapped(object obj)
        {
            if (StartTimeSelected.Equals("00:00:00"))
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "Start time can not be empty" ,"Ok");
                return;
            }
            if (EndTimeSelected.Equals("00:00:00"))
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "End time can not be empty", "Ok");
                return;
            }

            ScheduleTimeModel scheduleTimeModel = new ScheduleTimeModel
            {
                DateSelected = Date,
                StartTime = StartTimeSelected,
                EndTime = EndTimeSelected,
            };
                await fireBase.AddTime(scheduleTimeModel);
                SearchTime();
                await AddTIME();
                await Application.Current.MainPage.DisplayAlert("Successful", $" Add the working time of the day {Date} \n From hour {StartTimeSelected} to hour {EndTimeSelected}", "Ok");
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
            temp.hour =   Int32.Parse(tempTime[0]);
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
            Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            string sta = StartTimeSelected.ToString();
            Console.WriteLine(sta);
            time startTime = getTimeSplitedAsInt(StartTimeSelected.ToString());
            time endTime = getTimeSplitedAsInt(EndTimeSelected.ToString());
            Console.WriteLine("sssssssssssssssssssssssssssssssssssssssssssssss");
            Console.WriteLine(StartTimeSelected.ToString());
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
            await fireBase.AddTimes1(ListTimes);
        }
    }
}
