using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationComponent : ContentView
    {
        public NavigationComponent()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty ChangeTitle = BindableProperty.Create(nameof(Titles), typeof(string), typeof(NavigationComponent), string.Empty);

        public string Titles
        {
            get => (string)GetValue(ChangeTitle);


            set => SetValue(ChangeTitle, value);
        }

    }
}