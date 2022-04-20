using Barbar_Salon.Models;
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
       public ImageSource ImgSource { get; set; }
        public ICommand BtnPick { get; }
        public ICommand BtnStore { get; }
        public ICommand BtnBack { get; }
        MediaFile file;
        HaloHairServices haloHairServices;
        OfferModel offerModel;
        public AddOfferViewModel()
        {
            haloHairServices = new HaloHairServices();
            offerModel = new OfferModel();
            BtnPick = new Command(OnPickTappedAsync);
            BtnStore = new Command(OnStoreTappedAsync);
        }
        private async  void OnPickTappedAsync(object obj)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                await Application.Current.MainPage.DisplayAlert("successful", "Picture selected", "Ok");
                ImgSource = ImageSource.FromStream(() =>
                 {
                     var imageStram = file.GetStream();
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
            if (file == null)
            {
                await Application.Current.MainPage.DisplayAlert("Faild", "You didn't choose an Picture", "Ok");
                return;
            }
            try
            {
                string image = await haloHairServices.StoreImage(file.GetStream(), Path.GetFileName(file.Path));
                Console.WriteLine("nulllsadasd image :: "+ image);
                offerModel.ImageUrl = image;
                await haloHairServices.StoreImageUrl(offerModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
