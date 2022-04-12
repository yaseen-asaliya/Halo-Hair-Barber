using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimePage : ContentPage
    {
        // DateTime dateTime;
        //DateTime date;
        //int Year, Month, Day;
        public AddTimePage()
        {
            InitializeComponent();
            am.BackgroundColor = Color.Gray;
              //  dateTime = DateTime.Now;

              ////  label.Text = dateTime.ToString("dddd, dd MMMM yyyy");
              //  Year = DateTime.Now.Year;
              //  Month = DateTime.Now.Month;
              //  Day = DateTime.Now.Day;

        }

        private void am_Clicked(object sender, EventArgs e)
        {
            pm.BackgroundColor = Color.White;
            am.BackgroundColor = Color.Gray;
            
        }

        private void pm_Clicked(object sender, EventArgs e)
        {
            am.BackgroundColor = Color.White;
            pm.BackgroundColor= Color.Gray;
            
        }
        
    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
    if (e.Value == true)
    {
      OffTime.IsVisible = true;
    }
    else
    {
      OffTime.IsVisible = false;
    }
    }

        private void amE_Clicked(object sender, EventArgs e)
        {
            pmE.BackgroundColor = Color.White;
            amE.BackgroundColor = Color.Gray;
        }

        private void pmE_Clicked(object sender, EventArgs e)
        {
            amE.BackgroundColor = Color.White;
            pmE.BackgroundColor = Color.Gray;
        }

        private void amOs_Clicked(object sender, EventArgs e)
        {
            pmOs.BackgroundColor = Color.White;
            amOs.BackgroundColor = Color.Gray;
        }

        private void pmOs_Clicked(object sender, EventArgs e)
        {
            amOs.BackgroundColor = Color.White;
            pmOs.BackgroundColor = Color.Gray;
        }

        private void amOe_Clicked(object sender, EventArgs e)
        {
            pmOe.BackgroundColor = Color.White;
            amOe.BackgroundColor = Color.Gray;
        }

        private void pmOe_Clicked(object sender, EventArgs e)
        {
            amOe.BackgroundColor = Color.White;
            pmOe.BackgroundColor = Color.Gray;
        }

  //      private async void ImageButton_Clicked(object sender, EventArgs e)
  //  {
  //  DateTime nowDate = new DateTime(Year, Month, Day);

  //  var previewDate = nowDate.AddDays(-1);
  ////  label.Text = previewDate.ToString("dddd, dd MMMM yyyy");
  //  Year = previewDate.Year;
  //  Month = previewDate.Month;
  //  Day = previewDate.Day;
  //  }

   // private void ImageButton_Clicked_1(object sender, EventArgs e)
   // {
   // DateTime nowDate = new DateTime(Year, Month, Day);
   // var previewDate = nowDate.AddDays(1);
   //// label.Text = previewDate.ToString("dddd, dd MMMM yyyy");
   // Year = previewDate.Year;
   // Month = previewDate.Month;
   // Day = previewDate.Day;
           
   // }
    }
}