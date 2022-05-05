using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Barbar_Salon.Models;
using Barbar_Salon.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Barbar_Salon.Views;
using System.Diagnostics;

namespace Barbar_Salon.ViewModels
{
    public class MyTimeViewModel : BaseViewModel
    {

        HaloHairServices firebase;


        public MyTimeViewModel()
        {
            AccessToken();
            firebase = new HaloHairServices();
            FilltedMyTimes = new ObservableCollection<ScheduleTimeModel>();
            MyTimes = new ObservableCollection<ScheduleTimeModel>();
            MyTimes = firebase.GeMyTime();

            MyTimes.CollectionChanged += serviceschanged;
            DeleteCommand = new Command(onDeleteTapped);

            BackPage = new Command(Back_Page);
            PageAddTime = new Command(addTimePage);

        }
        private ObservableCollection<ScheduleTimeModel> myTimes;

        public ObservableCollection<ScheduleTimeModel> MyTimes
        {
            get { return myTimes; }
            set
            {
                myTimes = value;
                OnPropertyChanged();

            }
        }



        private ObservableCollection<ScheduleTimeModel> filltedMyTimes;
        public ObservableCollection<ScheduleTimeModel> FilltedMyTimes
        {
            get
            {
                return filltedMyTimes;
            }
            set
            {
                filltedMyTimes = value;
                OnPropertyChanged();
            }
        }



        private static string accessToken { get; set; }

        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ICommand DeleteCommand { get; }

        public ICommand BackPage { get; }
        public ICommand PageAddTime { get; }

        private int count = 0;
        private void serviceschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ScheduleTimeModel mytime = e.NewItems[0] as ScheduleTimeModel;

                if (mytime.AccessToken_Barbar == accessToken)
                {

                    FilltedMyTimes.Add(mytime);
                    count = 1;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                ScheduleTimeModel Time = e.OldItems[0] as ScheduleTimeModel;


                FilltedMyTimes.Remove(Time);
                count = 0;

            }
        }

        private async void onDeleteTapped(object obj)
        {
            var control = obj as ScheduleTimeModel;
            var res = await App.Current.MainPage.DisplayAlert("Delete Time ", $"Your data are delete \n Start Time {control.StartTime}\nEnd Time {control.EndTime}", "Yes", "Cancel");
            if (res)
            {
                await firebase.DeleteMyTime(control);

            }
        }


        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void addTimePage(object obj)
        {
            if (count == 0)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new AddTimePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failed to Add New Time", "Please delete the old time to add a new time", "Ok");
            }

        }



    }

}



