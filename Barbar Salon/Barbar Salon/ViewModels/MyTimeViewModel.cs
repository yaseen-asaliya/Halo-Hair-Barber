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

        private HaloHairServices _firebase;

        private ObservableCollection<ScheduleTimeModel> _myTimes;

        private ObservableCollection<ScheduleTimeModel> _filltedMyTimes;
        private static string _accessToken { get; set; }
        public ICommand DeleteButton { get; }

        public ICommand BackButton { get; }
        public ICommand PageAddTime { get; }

        private int _count = 0;
        public MyTimeViewModel()
        {
            AccessToken();
            _firebase = new HaloHairServices();
            FilltedMyTimes = new ObservableCollection<ScheduleTimeModel>();
            MyTimes = new ObservableCollection<ScheduleTimeModel>();
            MyTimes = _firebase.GeMyTime();

            MyTimes.CollectionChanged += ServicesChanged;
            DeleteButton = new Command(OnDeleteTapped);

            BackButton = new Command(BackPage);
            PageAddTime = new Command(AddTimePage);

        }


        public ObservableCollection<ScheduleTimeModel> MyTimes
        {
            get { return _myTimes; }
            set
            {
                _myTimes = value;
                OnPropertyChanged();

            }
        }




        public ObservableCollection<ScheduleTimeModel> FilltedMyTimes
        {
            get
            {
                return _filltedMyTimes;
            }
            set
            {
                _filltedMyTimes = value;
                OnPropertyChanged();
            }
        }



   

        private async void AccessToken()
        {
            try
            {
                _accessToken = await SecureStorage.GetAsync("oauth_token");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

      
        private void ServicesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ScheduleTimeModel mytime = e.NewItems[0] as ScheduleTimeModel;

                if (mytime.BarberAccessToken == _accessToken)
                {
                    FilltedMyTimes.Add(mytime);
                    _count = 1;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                ScheduleTimeModel Time = e.OldItems[0] as ScheduleTimeModel;
                FilltedMyTimes.Remove(Time);
                _count = 0;

            }
        }

        private async void OnDeleteTapped(object obj)
        {
            var control = obj as ScheduleTimeModel;
            var result = await App.Current.MainPage.DisplayAlert("Delete Time ", $"Your data are delete \nStart Time {control.StartTime}\nEnd Time {control.EndTime}", "Yes", "Cancel");
            if (result)
            {
                await _firebase.DeleteMyTime(control);

            }
        }


        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void AddTimePage(object obj)
        {
            if (_count == 0)
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



