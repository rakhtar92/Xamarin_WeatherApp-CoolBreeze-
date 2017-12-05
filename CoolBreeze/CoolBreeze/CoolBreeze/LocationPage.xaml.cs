using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoolBreeze
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : ContentPage
    {
        public LocationPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (cityPicker.SelectedIndex < 0)
                cityPicker.SelectedIndex = 0;
            this.BindingContext = App.ViewModel;
            if (App.ViewModel.NeedsRefresh)
                App.ViewModel.RefreshCurrentConditionAsync();
            base.OnAppearing();
        }
        //private void UseLocationToggled(object sender, ToggledEventArgs e)
        //{
        //    App.ViewModel.NeedsRefresh = true;
        //    if (e.Value)
        //    {
        //        App.ViewModel.LocationType = Common.LocationType.Location;
        //    }
        //    else
        //    {
        //        App.ViewModel.LocationType = Common.LocationType.City;
        //    }
        //    //if (e.Value)
        //    //{
        //    //    App.ViewModel.LocationType = Common.LocationType.Location;
        //    //    if (!App.ViewModel.IsBusy)
        //    //    {
        //    //        useLocationToggle.IsToggled = false;
        //    //        App.ViewModel.NeedsRefresh = true;
        //    //        App.ViewModel.LocationType = Common.LocationType.City;
        //    //    }
        //    //}
        //}
        private void SelectedCityChanged(object sender, EventArgs e)
        {
            if (!App.ViewModel.IsBusy)
            {
                //useLocationToggle.IsToggled = false;
                App.ViewModel.NeedsRefresh = true;
                App.ViewModel.LocationType = Common.LocationType.City;

                string selecteditem = (sender as Picker).Items[(sender as Picker).SelectedIndex];
                var cityname = selecteditem.Split('(').First().Trim();
                var countrycode = selecteditem.Split('(').Last().Replace(")", "");

                App.ViewModel.CityName = cityname;
                App.ViewModel.CountryCode = countrycode;
                App.ViewModel.RefreshCurrentConditionAsync();
            }
            App.ViewModel.IsBusy = false;
        }

    }
}
