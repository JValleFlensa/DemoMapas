using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoMapas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.GPSView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
