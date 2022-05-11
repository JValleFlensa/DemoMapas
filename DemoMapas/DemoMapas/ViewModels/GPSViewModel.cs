using System.Threading;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;

using DemoMapas.Services;

namespace DemoMapas.ViewModels
{
    public class GPSViewModel : BaseViewModel
    {
        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set { SetProperty(ref latitude, value); }
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set { SetProperty(ref longitude, value); }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        CancellationTokenSource cts;

        public ICommand GetCurrentLocationCommand { get; private set; }
        public ICommand ShowExternalMapCommand { get; private set; }
        public ICommand GetLocationFromAddressCommand { get; private set; }
        public ICommand GetAddressFromLocationCommand { get; private set; }
        public ICommand DisposeCancellationTokenCommand { get; private set; }

        private async Task GetCurrentLocation()
        {
            var location = await GPSService.GetCurrentLocation(cts);

            if (location != null)
            {
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
        }

        private async Task ShowExternalMap()
        {
            var options = new MapLaunchOptions()
            {
                Name = "Current location",
                NavigationMode = NavigationMode.None
            };

            await Map.OpenAsync(Latitude, Longitude, options);
        }

        private async Task GetLocationFromAddress()
        {
            var location = await GPSService.GetLocationFromAddress(Address);

            if (location != null)
            {
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
        }

        private async Task GetAddressFromLocation()
        {
            var placemark = await GPSService.GetAddressFromLocation(Latitude, Longitude);

            if (placemark != null)
            {
                Address =
                    $"AdminArea:       {placemark.AdminArea}\n" +
                    $"CountryCode:     {placemark.CountryCode}\n" +
                    $"CountryName:     {placemark.CountryName}\n" +
                    $"FeatureName:     {placemark.FeatureName}\n" +
                    $"Locality:        {placemark.Locality}\n" +
                    $"PostalCode:      {placemark.PostalCode}\n" +
                    $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                    $"SubLocality:     {placemark.SubLocality}\n" +
                    $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                    $"Thoroughfare:    {placemark.Thoroughfare}\n";
            }
        }

        private void DisposeCancellationToken()
        {
            GPSService.DisposeToken(cts);
        }

        public GPSViewModel()
        {
            GetCurrentLocationCommand = new Command(async () => await GetCurrentLocation());
            ShowExternalMapCommand = new Command(async () => await ShowExternalMap());
            GetAddressFromLocationCommand = new Command(async () => await GetAddressFromLocation());
            GetLocationFromAddressCommand = new Command(async () => await GetLocationFromAddress());
            DisposeCancellationTokenCommand = new Command(DisposeCancellationToken);
        }
    }
}
