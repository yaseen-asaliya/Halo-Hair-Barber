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



        private HaloHairServices _firebase;

        private Random _random;
        public TimeSpan StartTimeSelected { set; get; }
        public TimeSpan EndTimeSelected { get; set; }
        public ICommand AddTimeCommand { get; }
        public ICommand BackButton { get; }

        public int StartTime { set; get; }
        public int EndTime { set; get; }

        private List<(string, bool)> _listTimes;
        private struct Time
        {
            public int hour;
            public int minute;
        }

        public AddTimeViewModel()
        {
            _firebase = new HaloHairServices();
            AddTimeCommand = new Command(OnAddTimeTapped);
            BackButton = new Command(BackPage);
        }

        private async void OnAddTimeTapped(object obj)
        {
            _random = new Random();
            int id = _random.Next(0, 1236963000);

            ScheduleTimeModel scheduleTimeModel = new ScheduleTimeModel
            {
                Id = id,
                StartTime = StartTimeSelected,
                EndTime = EndTimeSelected,
            };
            await _firebase.AddTime(scheduleTimeModel);
            SearchTime();
            await AddScheduleTimes(id);
            await Application.Current.MainPage.DisplayAlert("Successful", $" Add the working time\nFrom {StartTimeSelected} to {EndTimeSelected}", "Ok");
            await Application.Current.MainPage.Navigation.PopModalAsync();

        }


        private Time GetTimeSplitedAsInt(string time)
        {
            Time temp = new Time();
            string[] tempTime = time.Split(':');
            temp.hour = Int32.Parse(tempTime[0]);
            temp.minute = Int32.Parse(tempTime[1]);
            return temp;
        }
        private string GetTimeAsString(int timeminutes)
        {
            int hours = timeminutes / 60;
            int minutes = timeminutes % 60;
            string time = "";
            if (hours > 12)
            {
                if (minutes < 10)
                {
                    time = (hours - 12) + ":0" + minutes + " PM";

                }
                else
                {
                    time = (hours - 12) + ":" + minutes + " PM";
                }
            }
            else
            {
                if (minutes < 10)
                {
                    time = hours + ":0" + minutes + " AM";
                }
                else
                {
                    time = hours + ":" + minutes + " AM";
                }
            }
            return time;

        }


        private void SearchTime()
        {
            _listTimes = new List<(string, bool)>();

            Time startTime = GetTimeSplitedAsInt(StartTimeSelected.ToString());
            Time endTime = GetTimeSplitedAsInt(EndTimeSelected.ToString());

            int start = (startTime.hour * 60) + startTime.minute;
            int end = (endTime.hour * 60) + endTime.minute;
            bool isBooked = false;

            for (int i = start; i < end; i += 30)
            {
                string tempTime = "";
                tempTime = GetTimeAsString(i);
                isBooked = false;
                _listTimes.Add((tempTime, isBooked));
            }
        }
        private async Task AddScheduleTimes(int id)
        {
            await _firebase.StoreScheduleTimes(_listTimes, id);
        }

        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


    }
}
