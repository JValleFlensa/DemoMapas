﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoMapas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GPSView : ContentPage
    {
        public GPSView()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            var vm = (ViewModels.GPSViewModel)this.BindingContext;
            vm.DisposeCancellationTokenCommand.Execute(null);
            base.OnDisappearing();
        }
    }
}