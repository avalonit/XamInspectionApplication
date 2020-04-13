using System;

using Xamarin.Forms;
using Plugin.Settings;

namespace KobApp
{
	public class App : Application
	{
		public static string Gray = "#A9A9A9", XBlue = "#0000FF", Orange = "#FFB500", XGreen = "#5CB85C", Black = "#000000";
		public static double ScreenHeight = 0, ScreenWidth = 0;

		public App()
		{
			//The root page of your application                        
			var navigation = new NavigationPage(new LoginPage());
			navigation.BarBackgroundColor = Color.FromHex(Black);
			navigation.BarTextColor = Color.White;
			MainPage = navigation;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

