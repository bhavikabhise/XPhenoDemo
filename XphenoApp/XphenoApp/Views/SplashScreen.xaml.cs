using System;
using System.Collections.Generic;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XphenoApp.Constants;
using XphenoApp.Service;

namespace XphenoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public Timer timer;

        public SplashScreen()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            timer = new Timer
            {
                Interval = 3000
            };
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await Navigator.InitializeMasterDetail();
        }
    }
}

