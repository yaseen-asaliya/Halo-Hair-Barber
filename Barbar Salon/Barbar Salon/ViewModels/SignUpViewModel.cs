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
    public class SignUpViewModel:BaseViewModel
    {

      
        public string email { get; set; }   
        public string password { get; set; }  
        
        public string name { get; set; }   
        
        public string namesalon { get; set; }   
        public long phone { get; set; } 

        public string location { get; set; }


        public ICommand SigUpCommad { get; }

        FireBaseHaloHair firebase;
     

    
        IAuth auth;
        public SignUpViewModel()
        {
          
             
             auth = DependencyService.Get<IAuth>();
            firebase = new FireBaseHaloHair();
            SigUpCommad = new Command(async () => await SignUp(email, password));
     



        }

        public async void AddUser(string name,string namesalon,long phone,string ulr,string location)
        {

           await firebase.AddNewUser(name, namesalon, phone, ulr,location);

        }


        public async Task SignUp(string email,string password)
        {


            try
            {
                string  ulr = await auth.SignUpWithEmailAndPassword(email, password);
                Console.WriteLine(ulr);
 
                if (null != ulr)
                {
                    AddUser(name,namesalon,phone, ulr,location);
                }
                     


            }
            catch (Exception ex)
            {
                Console.WriteLine("The Exceptions : "+ex);
            }
        }
        







    }
}
