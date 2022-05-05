﻿using Barbar_Salon.Models;
using Barbar_Salon.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Barbar_Salon.ViewModels
{
    public class AddOfferViewModel: BaseViewModel
    {
        private ImageSource _imgSource;
        public ImageSource ImgSource 
        {
            get
            {
                return _imgSource;
            }
          set
            {
                _imgSource = value;
                OnPropertyChanged();
            }
                
         }
        public ICommand PickButton { get; }
        public ICommand StoredButton { get; }
        public ICommand BackButton { get; }

        MediaFile _file;
        HaloHairServices _haloHairSercvice;
        OfferModel _offerModel;

     
        public AddOfferViewModel()
        {
            _haloHairSercvice = new HaloHairServices();
            _offerModel = new OfferModel();
            PickButton = new Command(OnPickTappedAsync);
            StoredButton = new Command(OnStoreTappedAsync);
            BackButton = new Command(BackPage);
        }
        private async  void OnPickTappedAsync(object obj)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                _file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (_file == null)
                    return;
                ImgSource = ImageSource.FromStream(() =>
                 {
                     var imageStram = _file.GetStream();
                     return imageStram;
                 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async void OnStoreTappedAsync(object obj)
        {
            if (_file == null)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You didn't choose an Picture", "Ok");
                return;
            }
            try
            {
                string image = await _haloHairSercvice.StoreImage(_file.GetStream(), Path.GetFileName(_file.Path));
                _offerModel.ImageUrl = image;
                await _haloHairSercvice.StoreImageUrl(_offerModel);
                await Application.Current.MainPage.DisplayAlert("Successful","Uploaded Image", "Ok");
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


    }
}
