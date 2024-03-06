
using Android.App;
using Android.Content.PM;
using Android.Runtime; 
using Android.OS;
using System;
using Plugin.CurrentActivity;

namespace PolyStock.Droid
{
    [Activity(Label = "PolyStock", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.FullUser, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            //SfChartRenderer.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this,savedInstanceState); 
             // prior to using it.\nSee more info: https://github.com/rotorgames/Rg.Plugins.Popup/wiki/Getting-started#initialization"    string
        global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            DroidBootstrapper.Run();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

#if DEBUG
    [Application(Debuggable = true)]
#else
	[Application(Debuggable = false)]
#endif
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
        }
    }
}