using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CoolBreeze.Common;
using CoolBreeze.Models;
using CoolBreeze.Helpers;

namespace CoolBreeze.ViewModels
{
    public class MainViewModel:ObservableBase
    {
        public MainViewModel()
        {
            this.IsBusy = true;
            this.NeedsRefresh = true;
            this.LocationType = LocationType.City;
            this.CityName = "Amsterdam";
            this.CountryCode = "HL";
            this.CurrentCondition = new WeatherInformation();
        }//End of constructor
        private LocationType _locationType;

        public LocationType LocationType
        {
            get { return this._locationType; }
            set { this.SetProperty(ref this._locationType, value); }
        }
        private string _postalCode;

        public string PostalCode
        {
            get { return this._postalCode; }
            set { this.SetProperty(ref this._postalCode, value); }
        }
        private string _cityName;

        public string CityName
        {
            get { return this._cityName; }
            set { this.SetProperty(ref this._cityName, value); }
        }
        private string _countryCode;

        public string CountryCode
        {
            get { return this._countryCode; }
            set { this.SetProperty(ref this._countryCode, value); }
        }
        private WeatherInformation _currentCondition;

        public WeatherInformation CurrentCondition
        {
            get { return this._currentCondition; }
            set {this.SetProperty(ref this._currentCondition, value); }
        }
        private bool _needsRefresh;

        public bool NeedsRefresh
        {
            get { return this._needsRefresh; }
            set { this.SetProperty(ref this._needsRefresh, value); }
        }
        private bool _isBusy;

        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.SetProperty(ref this._isBusy, value); }
        }
        private Plugin.Geolocator.Abstractions.Position _location;

        public Plugin.Geolocator.Abstractions.Position Location
        {
            get { return this._location; }
            set { this.SetProperty(ref this._location, value); }
        }
        private ObservableCollection<WeatherInformation> _forecast;

        public ObservableCollection<WeatherInformation> Forecast
        {
            get
            {
                if (this._forecast == null)
                    this._forecast = new ObservableCollection<WeatherInformation>();
                return this._forecast;
            }
            set { this.SetProperty(ref this._forecast, value); }
        }

        //Refresh condition depending on the location.
        public async void RefreshCurrentConditionAsync()
        {
            this.IsBusy = true;
            this.NeedsRefresh = false;
            WeatherInformation result = null;
            switch (this.LocationType)
            {
                case LocationType.Location:
                    if(this.Location==null) 
                        this.Location = await Helpers.LocationHelper.GetCurrentLocationAsync();
                    result = await Helpers.WeatherHelper.GetCurrentConditionsAsync(this.Location.Latitude, this.Location.Longitude);
                    break;
                case LocationType.City:
                    result = await Helpers.WeatherHelper.GetCurrentConditionsAsync(this.CityName, this.CountryCode);
                    break;
            }
            
            this.CurrentCondition.Conditions = result.Conditions;
            this.CurrentCondition.Description = result.Description;
            this.CurrentCondition.DisplayName = result.DisplayName;
            this.CurrentCondition.Icon = result.Icon;
            this.CurrentCondition.Id = result.Id;
            this.CurrentCondition.MaxTemperature = result.MaxTemperature;
            this.CurrentCondition.MinTemperature = result.MinTemperature;
            this.CurrentCondition.Temperature = result.Temperature;
            this.CurrentCondition.Humidity = result.Humidity;
            this.CurrentCondition.TimeStamp = result.TimeStamp.ToLocalTime();
            this.IsBusy = false;
        }
        //Used to refresh Forecast depending on the location.
        public async void RefreshForecastAsync()
        {
            this.IsBusy = true;
            this.NeedsRefresh = false;
            List<WeatherInformation> result = null;
            this.Forecast.Clear();
            switch (this.LocationType)
            {
                case LocationType.Location:
                    if (this.Location == null)
                        this.Location = await Helpers.LocationHelper.GetCurrentLocationAsync();
                    result = await Helpers.WeatherHelper.GetForecastAsync(this.Location.Latitude, this.Location.Longitude);
                    break;
                case LocationType.City:
                    result = await Helpers.WeatherHelper.GetForecastAsync(this.CityName, this.CountryCode);
                    break;
            }
            foreach(var results in result)
            {
                this.Forecast.Add(results);
            }
            this.IsBusy = false;
        }
    }
}
