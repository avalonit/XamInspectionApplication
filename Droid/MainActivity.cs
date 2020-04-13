using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using Android.Nfc;


using Poz1.NFCForms.Abstract;
using Poz1.NFCForms.Droid;
using Gcm.Client;

namespace KobApp.Droid
{
	[Activity (Label = "KobApp", Icon = "@drawable/icon",WindowSoftInputMode=SoftInput.AdjustResize, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{

		// Create a new instance field for this activity.
		static MainActivity instance = null;

		// Return the current activity instance.
		public static MainActivity CurrentActivity
		{
			get
			{
				return instance;
			}
		}

		public NfcAdapter NFCdevice;
        public NfcForms x;

		protected override void OnCreate (Bundle bundle)
		{
			instance = this;

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			NfcManager NfcManager = (NfcManager)Android.App.Application.Context.GetSystemService(Context.NfcService);
			NFCdevice = NfcManager.DefaultAdapter;

			Xamarin.Forms.DependencyService.Register<INfcForms, NfcForms>();
			x = Xamarin.Forms.DependencyService.Get<INfcForms>() as NfcForms;

			global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

			LoadApplication (new App ());

			App.ScreenHeight = (double)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
			App.ScreenWidth = (double)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

			this.ActionBar.SetIcon(Android.Resource.Color.Transparent);

			InitPushNotification();
		}

		private void InitPushNotification()
		{
			try
			{
				// Check to ensure everything's set up right
				GcmClient.CheckDevice(this);
				GcmClient.CheckManifest(this);

				// Register for push notifications
				System.Diagnostics.Debug.WriteLine("Registering...");
				GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
			}
			catch (Java.Net.MalformedURLException)
			{
				CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
			}
			catch (Exception e)
			{
				CreateAndShowDialog(e.Message, "Error");
			}
		}

		private void CreateAndShowDialog(String message, String title)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);

			builder.SetMessage(message);
			builder.SetTitle(title);
			builder.Create().Show();
		}

		public override void OnLowMemory ()
		{
			base.OnLowMemory ();
			GC.Collect();
		}

		public override void OnRequestPermissionsResult (int requestCode, string[] permissions, Permission[] grantResults)
		{			
			global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult (requestCode, permissions, grantResults);           
		    Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}


        protected override void OnResume()
		{
			base.OnResume();
			if (NFCdevice != null)
			{
				var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
				NFCdevice.EnableForegroundDispatch
				(
					this,
					PendingIntent.GetActivity(this, 0, intent, 0),
					new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
					new String[][] {new string[] {
								NFCTechs.Ndef,
							},
							new string[] {
								NFCTechs.MifareClassic,
							},
							new string[] {
								NFCTechs.MifareUltralight,
							},
					}
				);
			}
		}

		protected override void OnPause()
		{
			base.OnPause();
			if( NFCdevice!=null )
				NFCdevice.DisableForegroundDispatch(this);
		}

		protected override void OnNewIntent(Intent intent)
		{
			base.OnNewIntent(intent);
			try
			{
				if (intent != null)
					x.OnNewIntent(this, intent);
			}
			catch (Exception e)
			{
			}
		}
	}
}

