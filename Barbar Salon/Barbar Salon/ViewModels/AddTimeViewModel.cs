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

        public string StartTime { set; get; }
        public string EndTime { set; get; }

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
        public ICommand StartAMTimeCommand { get; }
        public ICommand StartPMTimeCommand { get; }
        public ICommand EndAMTimeCommand { get; }
        public ICommand EndPMTimeCommand { get; }
        public ICommand AddTimeCommand { get; }

        private HaloHairServices fireBase;


        public AddTimeViewModel()
        {
            fireBase = new HaloHairServices();

           
            dateTime = DateTime.Now;
            Date = dateTime.ToString("dddd, dd MMMM yyyy");
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;

            StartTime = "";
            EndTime = "";

            startAMButtonColor = "Gray";
            startPMButtonColor = "White";

            EndAMButtonColor = "Gray";
            EndPMButtonColor = "White";



            StartAMTimeCommand = new Command(OnStartAMTapped);
            StartPMTimeCommand = new Command(OnStartPMTapped);

            EndAMTimeCommand = new Command(OnEndAMTapped);
            EndPMTimeCommand = new Command(OnEndtPMTapped);


            IncreaseDateCommand = new Command(OnIncreaseTapped);
            DecreaseDateCommand = new Command(OnDecreaseTapped);

            AddTimeCommand = new Command(OnAddTimeTapped);
        }

        

        private void OnStartAMTapped(object obj)
        {
            StartAMButtonColor = "Gray";
            StartPMButtonColor = "White";
        }

        private void OnStartPMTapped(object obj)
        {
            StartAMButtonColor = "White";
            StartPMButtonColor = "Gray";
        }
    
        private void OnEndAMTapped(object obj)
        {
            EndAMButtonColor = "Gray";
            EndPMButtonColor = "White";
        }

        private void OnEndtPMTapped(object obj)
        {
            EndAMButtonColor = "White";
            EndPMButtonColor = "Gray";
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
            if (StartTime.Equals(""))
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "Start time can not be empty" ,"Ok");
                return;
            }
            if (EndTime.Equals(""))
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "End time can not be empty", "Ok");
                return;
            }

            if (StartAMButtonColor == "Gray")
                StartTime += " AM";

            else
                StartTime += " PM";

            if (EndAMButtonColor == "Gray")
                EndTime += " AM";

            else
                EndTime += " PM";


            ScheduleTimeModel scheduleTimeModel = new ScheduleTimeModel
            {
                DateSelected = Date,
                StartTime = StartTime,
                EndTime = EndTime
            };


            await fireBase.AddTime(scheduleTimeModel);
        }


    }
}
