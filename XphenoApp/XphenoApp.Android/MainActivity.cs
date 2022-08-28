using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Rg.Plugins.Popup;
using Xamarin.Forms;
using Android.Nfc;
using Acr.UserDialogs;

namespace XphenoApp.Droid
{
    [Activity(Label = "XphenoApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Popup.Init(this);
            Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            App app = new App();
            RegisterServices(app);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LoadApplication(app);
        }

        private void RegisterServices(App app)
        {
            app.BuildDIContainer();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
