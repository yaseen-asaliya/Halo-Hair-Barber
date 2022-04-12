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

        private string startAMButtonColor;
        public string StartAMButtonColor
        {
            get
            {
                return startAMButtonColor;
            }
            set
            {
                startAMButtonColor = value;
                OnPropertyChanged();
            }
        }

        private string startPMButtonColor;
        public string StartPMButtonColor
        {
            get
            {
                return startPMButtonColor;
            }
            set
            {
                startPMButtonColor = value;
                OnPropertyChanged();
            }
        }



        private string endAMButtonColor;
        public string EndAMButtonColor
        {
            get
            {
                return endAMButtonColor;
            }
            set
            {
                endAMButtonColor = value;
                OnPropertyChanged();
            }
        }

        private string endPMButtonColor;
        public string EndPMButtonColor
        {
            get
            {
                return endPMButtonColor;
            }
            set
            {
                endPMButtonColor = value;
                OnPropertyChanged();
            }
        }

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
        public ICommand StartTimeCommand { get; }
        public ICommand EndTimeCommand { get; }






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

            startAMButtonColor = "Gray";
            startPMButtonColor = "White";

            EndAMButtonColor = "Gray";
            EndPMButtonColor = "White";



            StartTimeCommand = new Command(OnStartTapped);
            EndTimeCommand = new Command(OnEndTapped);
            IncreaseDateCommand = new Command(OnIncreaseTapped);
            DecreaseDateCommand = new Command(OnDecreaseTapped);

        }

        private void OnStartTapped(object obj)
        {
            string x = (string)obj;
            if (x == startAMButtonColor)
            {
                startAMButtonColor = "Gray";
                startPMButtonColor = "White";
            }
            

            else if (x == startPMButtonColor)
            {
                startAMButtonColor = "White";
                startPMButtonColor = "Gray";
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("x",x,"ok");
                Application.Current.MainPage.DisplayAlert("x",obj.ToString(),"ok");
            }
        }
        

        private void OnEndTapped(object obj)
        {
            
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
