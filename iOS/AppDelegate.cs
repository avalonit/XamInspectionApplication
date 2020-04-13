using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using AVFoundation;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;
using TodoAzure;

namespace KobApp.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			global::ZXing.Net.Mobile.Forms.iOS.Platform.Init ();

			LoadApplication (new App ());

			App.ScreenHeight = (double)UIScreen.MainScreen.Bounds.Height;
			App.ScreenWidth = (double)UIScreen.MainScreen.Bounds.Width;
			System.Diagnostics.Debug.WriteLine("Screen Height : " + App.ScreenHeight + " Width : " + App.ScreenWidth);

			InitPushNotification(options);

			//AuthorizeCameraUse();
			return base.FinishedLaunching (app, options);
		}

		private void InitPushNotification(NSDictionary options)
		{
			try
			{

				int notificationHub = 1;

				if (notificationHub == 0)
				{
					// Check if App was opened by Push Notification.
					var keyName = new NSString("UIApplicationLaunchOptionsRemoteNotificationKey");
					if (options != null && options.Keys != null && options.Keys.Length != 0 && options.ContainsKey(keyName))
					{
						NSDictionary pushOptions = options.ObjectForKey(keyName) as NSDictionary;
						//ProcessPushNotification(pushOptions, false);
					}
				}
				else
				{
					var settings = UIUserNotificationSettings.GetSettingsForTypes(
						 UIUserNotificationType.Alert
						 | UIUserNotificationType.Badge
						 | UIUserNotificationType.Sound,
						 new NSSet());

					UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
					UIApplication.SharedApplication.RegisterForRemoteNotifications();
				}
			}
			catch (Exception e)
			{
			}
		}

		/*
		public override void ReceivedRemoteNotification(UIApplication app, NSDictionary userInfo)
		{
			
			Debug.WriteLine("ReceivedRemoteNotification entered.");

			if (app.ApplicationState == UIApplicationState.Active)
			{
			}
			else if (app.ApplicationState == UIApplicationState.Background)
			{
			}
			else if (app.ApplicationState == UIApplicationState.Inactive)
			{
			}

			ProcessPushNotification(userInfo, true);
		}

		protected void ProcessPushNotification(NSDictionary userInfo, bool isAppAlreadyRunning)
		{
			if (userInfo == null) return;
			if (isAppAlreadyRunning)
			{
				// do something with this knowledge...
			}
		}
		*/

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			const string templateBodyAPNS = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";

			JObject templates = new JObject();
			templates["genericMessage"] = new JObject
		 	{
		   		{"body", templateBodyAPNS}
		 	};

			// Register for push with your mobile app
			Push push = TodoItemManager.DefaultManager.CurrentClient.GetPush();
			push.RegisterAsync(deviceToken, templates);
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

			string alert = string.Empty;
			if (aps.ContainsKey(new NSString("alert")))
				alert = (aps[new NSString("alert")] as NSString).ToString();

			//show alert
			if (!string.IsNullOrEmpty(alert))
			{
				UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
				avAlert.Show();
			}
		}

		async Task AuthorizeCameraUse ()
		{
			var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus (AVMediaType.Video);
			if (authorizationStatus != AVAuthorizationStatus.Authorized) {
				await AVCaptureDevice.RequestAccessForMediaTypeAsync (AVMediaType.Video);
			}
		}
	}
}

