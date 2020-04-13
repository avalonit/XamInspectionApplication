using KobApp.DataModel;
using System;
using Plugin.Settings;
using Xamarin.Forms;
using Plugin.Connectivity;
using Poz1.NFCForms.Abstract;
using KobApplication.Controls;
using System.Diagnostics;

namespace KobApp
{
    public class LoginPage : ContentPage
    {
        private readonly INfcForms device;

        AbsoluteLayout mainAbsoluteLayout = new AbsoluteLayout
        {
            Padding = new Thickness(15, 0, 15, 0),
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.FromHex("#30000000")
        };

        Image imagebackground = new Image
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Source = "login_screen_background.png",
            Aspect = Aspect.AspectFill,
        };

        AbsoluteLayout activityIndicatorLayout = new AbsoluteLayout
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0.7),
        };

        ActivityIndicator activityIndicator = new ActivityIndicator
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Color = Color.White,
            IsRunning = false,
        };

        ScrollView mainScrollView = new ScrollView
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Orientation = ScrollOrientation.Vertical,
        };

        StackLayout mainContainer = new StackLayout
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Orientation = StackOrientation.Vertical,
            Padding = new Thickness(15, 10),
            Spacing = 20,
        };

        Frame usernameFrame = new Frame
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            //OutlineColor = Color.FromHex(App.Gray),
            HasShadow = false,
            BackgroundColor = Color.Transparent,
            Padding = new Thickness(0),
        };

        MyEntry txtUsername = new MyEntry
        {
            CustomPadding = new Thickness(12, 5, 5, 7),
            CustomBackgroundColor = Color.Transparent,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.Center,
            Placeholder = "Username",
            PlaceholderColor = Color.FromHex(App.Gray),
            TextColor = Color.White,
        };

        Frame passwordFrame = new Frame
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            //OutlineColor = Color.FromHex(App.Gray),
            HasShadow = false,
            BackgroundColor = Color.Transparent,
            Padding = new Thickness(0),
        };

        MyEntry txtPassword = new MyEntry
        {
            CustomPadding = new Thickness(12, 5, 5, 7),
            CustomBackgroundColor = Color.Transparent,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Placeholder = "Password",
            PlaceholderColor = Color.FromHex(App.Gray),
            TextColor = Color.White,
            IsPassword = true,
        };

        Button btnLogin = new Button
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Text = "Login",
            TextColor = Color.Yellow,
            BackgroundColor = Color.Black,
            BorderRadius = 0,
        };

        Image imgLogin = new Image
        {
            Source = "ic_login.png",
            RotationY = 180,
            WidthRequest = 17,
            HeightRequest = 17,
            IsEnabled = false,
            Margin = new Thickness(0, 0, 20, 0),
            HorizontalOptions = LayoutOptions.End,
        };

        Grid grdLogin = new Grid
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.Yellow,
            Padding = new Thickness(1)
        };

        Label txtforgetpassword = new Label
        {
            Text = "Forgot Password?",
            TextColor = Color.White,
            HorizontalOptions = LayoutOptions.EndAndExpand
        };

        Label lblCard = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Text = "",
            TextColor = Color.Black,
            HorizontalTextAlignment = TextAlignment.Center
        };

        public LoginPage()
        {
            device = DependencyService.Get<INfcForms>();
            if (device != null)
            {
                device.NewTag += HandleNewTag;
                device.TagConnected += device_TagConnected;
                device.TagDisconnected += device_TagDisconnected;
            }

            Title = "ACCEDI";
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

            AbsoluteLayout.SetLayoutBounds(activityIndicator, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(activityIndicator, AbsoluteLayoutFlags.PositionProportional);
            activityIndicatorLayout.Children.Add(activityIndicator);

            AbsoluteLayout.SetLayoutBounds(mainScrollView, new Rectangle(0.5, 0.5, 1, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(mainScrollView, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
            mainAbsoluteLayout.Children.Add(mainScrollView);

            txtUsername.Text = "00302";
            txtPassword.Text = "p00302";

            usernameFrame.Content = txtUsername;
            passwordFrame.Content = txtPassword;

            mainContainer.Children.Add(usernameFrame);
            mainContainer.Children.Add(passwordFrame);
            mainContainer.Children.Add(txtforgetpassword);
            grdLogin.Children.Add(btnLogin);
            grdLogin.Children.Add(imgLogin);
            mainContainer.Children.Add(grdLogin);

            mainScrollView.Content = mainContainer;

            btnLogin.Clicked += BtnLogin_Clicked;

            Grid mainGrid = new Grid();
            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(5, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                }
            };

            grid.Children.Add(new Label
            {
                Text = "KOBE",
                FontSize = 30,
                TextColor = Color.Yellow,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 0);
            grid.Children.Add(mainAbsoluteLayout, 0, 1);
            grid.Children.Add(new BoxView
            {

            }, 0, 2);

            mainGrid.Children.Add(imagebackground);
            mainGrid.Children.Add(grid);
            Content = mainGrid;
        }

		

		private async void BtnLogin_Clicked(object sender, EventArgs e)
		{
			if ((txtUsername.Text != null 
			        && !txtUsername.Text.Equals("")) 
			        && (txtPassword.Text != null 
			        && !txtPassword.Text.Equals("")))
			{

                CrossSettings.Current.AddOrUpdateValue<bool>("IsLoggedIn", true);
                CrossSettings.Current.AddOrUpdateValue<string>("Token", "123");

                var navigation = new NavigationPage(new Dashboard());
                navigation.BarBackgroundColor = Color.FromHex(App.Black);
                navigation.BarTextColor = Color.White;
                App.Current.MainPage = navigation;

                /*
                if (CrossConnectivity.Current.IsConnected)
				{
					try
					{
						lblCard.Text = "";
						AddProgressBar();
						APIServices.ApiServices apiServices = new APIServices.ApiServices();
						LoginResult result = await apiServices.LoginAsync(txtUsername.Text, txtPassword.Text);
						if (result != null)
						{
							if (result.AuthStatus)
							{
								CrossSettings.Current.AddOrUpdateValue<bool>("IsLoggedIn", true);
								CrossSettings.Current.AddOrUpdateValue<string>("Token", result.Token);

								var navigation = new NavigationPage(new Dashboard());
								navigation.BarBackgroundColor = Color.FromHex(App.Black);
								navigation.BarTextColor = Color.White;
								App.Current.MainPage = navigation;
							}
							else
							{
								await DisplayAlert("Login Failed!", result.Message, "Ok");
							}
						}
					}
					catch (Exception pException)
					{
						await DisplayAlert("Login Failed!", pException.Message, "Ok");
						System.Diagnostics.Debug.WriteLine("Exception : " + pException.Message);
					}
					finally
					{
						RemoveProgressBar();
					}
				}
				else
				{
					await DisplayAlert("Login", "Internet Connection Not Available.", "Ok");
				}
				*/
			}
			else
			{
				await DisplayAlert("Login Failed!", "Username and Password can not be empty", "Ok");
			}
		}

		private void AddProgressBar()
		{
			AbsoluteLayout.SetLayoutBounds(activityIndicatorLayout, new Rectangle(0.5, 0.5, 1, 1));
			AbsoluteLayout.SetLayoutFlags(activityIndicatorLayout, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.SizeProportional);
			mainAbsoluteLayout.Children.Add(activityIndicatorLayout);

			activityIndicator.IsRunning = true;
		}

		private void RemoveProgressBar()
		{
			activityIndicator.IsRunning = false;
			mainAbsoluteLayout.Children.Remove(activityIndicatorLayout);
		}


        void device_TagDisconnected(object sender, NfcFormsTag e)
		{
			Debug.WriteLine(e.Id);
		}

		void device_TagConnected(object sender, NfcFormsTag e)
		{
			Debug.WriteLine(e.Id);
		}

		void HandleNewTag(object sender, NfcFormsTag e)
		{
			LoginWidthCard(e);

		}

		private async void LoginWidthCard(NfcFormsTag message)
		{
			String CardCert = ByteArrayToString(message.Id);

			if (!String.IsNullOrEmpty(CardCert))
			{

                if (CrossConnectivity.Current.IsConnected)
				{
					try
					{
						lblCard.Text = "";
						AddProgressBar();
						APIServices.ApiServices apiServices = new APIServices.ApiServices();
						LoginResult result = await apiServices.LoginCardAsync(CardCert);
						if (result != null)
						{
							if (result.AuthStatus)
							{
								CrossSettings.Current.AddOrUpdateValue<bool>("IsLoggedIn", true);
								CrossSettings.Current.AddOrUpdateValue<string>("Token", result.Token);

								var navigation = new NavigationPage(new Dashboard());
								navigation.BarBackgroundColor = Color.FromHex(App.Black);
								navigation.BarTextColor = Color.White;
								App.Current.MainPage = navigation;
								lblCard.Text = "";
							}
							else
							{
								await DisplayAlert("Login Failed!", result.Message, "Ok");
								lblCard.Text = CardCert;
							}
						}
					}
					catch (Exception pException)
					{
						await DisplayAlert("Login Failed!", pException.Message, "Ok");
						System.Diagnostics.Debug.WriteLine("Exception : " + pException.Message);
					}
					finally
					{
						RemoveProgressBar();
					}
				}
				else
				{
					await DisplayAlert("Login", "Internet Connection Not Available.", "Ok");
				}
			}
			else
			{
				await DisplayAlert("Login Failed!", "Username and Password can not be empty", "Ok");
			}
		}

		public string ByteArrayToString(byte[] ba)
		{
			String hex = "";
			foreach (byte b in ba)
			{
				hex += String.Format("{0:x2}", b);
			}
			return hex.ToString();
		}
	}
}
