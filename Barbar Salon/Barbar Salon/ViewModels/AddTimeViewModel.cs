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
        private DateTime dateTime { set; get; }
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



        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public string CalendarSelectedDate { get; set; }

        public ICommand DateSelectedCommand { get; }
        public Command AddTimeCommand { get; }





        HaloHairServices FireBase;


        public AddTimeViewModel()
        {
            FireBase = new HaloHairServices();

            AddTimeCommand = new Command(async () => await AddTime(StartTime, EndTime, CalendarSelectedDate));
            dateTime = DateTime.Now;
            Date = dateTime.ToString("dddd, dd MMMM yyyy");
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;
            IncreaseDateCommand = new Command(OnIncreaseTapped);
            DecreaseDateCommand = new Command(OnDecreaseTapped);

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

        public async Task AddTime(int startTime, int endTime, string CalendarSelectedDate)
        {
            await FireBase.AddTime(startTime, endTime, CalendarSelectedDate);


        }


    }
}
