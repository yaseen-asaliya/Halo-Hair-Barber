﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Barbar_Salon.ViewModels;

namespace Barbar_Salon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimePage : ContentPage
    {
        public AddTimePage()
        {
            InitializeComponent();
            BindingContext= new AddTimeViewModel();
        }
    }
}