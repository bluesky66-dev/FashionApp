using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using Lottie.Forms.Droid;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Plugin.Media.Abstractions;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;


using Android;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.Design.Widget;
using Android.Views;

namespace FashionApp.Droid
{
    [Activity(Label = "FashionApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        internal static MainActivity Instance { get; private set; }
        private static int PERMISSION_REQUEST_CODE = 200;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CarouselViewRenderer.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            AnimationViewRenderer.Init();

            Instance = this;
            requestPermission();
        }

        private void requestPermission()
        {

            if (!(ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.Camera) == (int)Android.Content.PM.Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.Camera, }, PERMISSION_REQUEST_CODE);
            }

            if (!(ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReadExternalStorage) == (int)Android.Content.PM.Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.ReadExternalStorage, }, PERMISSION_REQUEST_CODE);
            }

            if (!(ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WriteExternalStorage) == (int)Android.Content.PM.Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.WriteExternalStorage, }, PERMISSION_REQUEST_CODE);
            }


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}