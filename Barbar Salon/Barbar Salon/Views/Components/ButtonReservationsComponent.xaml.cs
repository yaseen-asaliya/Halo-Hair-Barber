using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonReservationsComponent : ContentView
    {
        public ButtonReservationsComponent()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty Text_NameButton = BindableProperty.Create(nameof(NameButton), typeof(string), typeof(ButtonReservationsComponent), string.Empty);
        public string NameButton
        {
            get => (string)GetValue(Text_NameButton);

            set => SetValue(Text_NameButton, value);
        }
    

        public static readonly BindableProperty TextColor = BindableProperty.Create(nameof(Text_Color), typeof(string), typeof(ButtonReservationsComponent), string.Empty);
        public string Text_Color
        {
            get => (string)GetValue(TextColor);

            set => SetValue(TextColor, value);
        }
        public static readonly BindableProperty Color_NameButton = BindableProperty.Create(nameof(ColorButton), typeof(string), typeof(ButtonReservationsComponent), string.Empty);
        public string ColorButton
        {
            get => (string)GetValue(Color_NameButton);

            set => SetValue(Color_NameButton, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ButtonReservationsComponent), default);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonReservationsComponent), default(ICommand));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

    }
}