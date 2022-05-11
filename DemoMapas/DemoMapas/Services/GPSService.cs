using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace DemoMapas.Services
{
    public class GPSService
    {
        public static async Task<Location> GetCurrentLocation(CancellationTokenSource cts)
        {
            try
            {
                cts = new CancellationTokenSource();
                var request = new GeolocationRequest(GeolocationAccuracy.Medium,TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                return location;
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return default;
        }

        public static async Task<Location> GetLocationFromAddress(string address)
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(address);
                return locations?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }

            return default;
        }

        public static async Task<Placemark> GetAddressFromLocation(double latitude, double longitude)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
                return placemarks?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }

            return default;
        }

        public static void DisposeToken(CancellationTokenSource cts)
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
        }
    }
}
