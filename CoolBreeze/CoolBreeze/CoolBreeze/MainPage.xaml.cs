using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CoolBreeze.ViewModels;

namespace CoolBreeze
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            ///* if (App.ViewModel == null)
            //     App.ViewModel = new MainViewModel();
            // this.BindingContext = App.ViewModel;
            // if (App.ViewModel.NeedsRefresh)
            //     App.ViewModel.RefreshCurrentConditionAsync();

            // if (cityPicker.SelectedIndex < 0)
            //     cityPicker.SelectedIndex = 0;

            // this.BindingContext = App.ViewModel;
            // if (App.ViewModel.NeedsRefresh)
            //     App.ViewModel.RefreshCurrentConditionAsync();

            useLocationToggle.IsToggled = false;
            base.OnAppearing();
        }
        private void UseLocationToggled(object sender, ToggledEventArgs e)
        {
            App.ViewModel.NeedsRefresh = true;
            if (e.Value)
            {
                BindingContext = App.ViewModel;
                if (App.ViewModel.NeedsRefresh)
                {
                    App.ViewModel.LocationType = Common.LocationType.Location;
                    App.ViewModel.RefreshCurrentConditionAsync();
                }
            }
            else
            {
                DisplayAlert("Alert", "Please check your location settings", "Open Settings", "Cancel");
            }
        }
    }
}
