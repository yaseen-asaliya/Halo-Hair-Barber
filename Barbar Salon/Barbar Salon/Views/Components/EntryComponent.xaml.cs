﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryComponent : ContentView
    {
        public EntryComponent()
        {
            InitializeComponent();
          
            entry.TextChanged += OnTextChanged;

        }
      

        public static readonly BindableProperty Placeholders = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryComponent), default(string), Xamarin.Forms.BindingMode.OneWay);
        public string Placeholder
        {
            get
            {
                return (string)GetValue(Placeholders);
            }

            set
            {
                SetValue(Placeholders, value);
            }
        }



        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryComponent), default(string), BindingMode.TwoWay);
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                entry.Text = Text;
            }
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }
    }
}