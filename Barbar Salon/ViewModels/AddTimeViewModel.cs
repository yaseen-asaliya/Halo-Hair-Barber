using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Barbar_Salon.Services;

namespace Barbar_Salon.ViewModels
{
    public class AddTimeViewModel
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public string CalendarSelectedDate { get; set; }

        public ICommand DateSelectedCommand { get; }

        public Command AddTimeCommand { get; }


        

        FireBaseHaloHair FireBase;


        public AddTimeViewModel()
        {
            FireBase = new FireBaseHaloHair();
            
            AddTimeCommand = new Command( async  ()=> await AddTime(StartTime, EndTime, CalendarSelectedDate));
           


        }


        public async Task AddTime(int startTime,int endTime,string CalendarSelectedDate)
        {
            await FireBase.AddTime(startTime, endTime, CalendarSelectedDate);


        }




    }
}
