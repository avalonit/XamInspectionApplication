using System;
using System.Collections.Generic;
using Plugin.Settings;
using Xamarin.Forms;
using KobApp.HelperView;
using KobApp.DataModel;
using ZXing.Net.Mobile.Forms;
using ZXing.Mobile;
using System.Collections.ObjectModel;
using Plugin.Connectivity;
using KobApplication.Controls;

namespace KobApp
{
	public class SearchPage : ContentPage
	{
		ZXingScannerPage scanPage;
		ObservableCollection<UsersModel> usersCollection = new ObservableCollection<UsersModel>();

		Grid contentGrid = new Grid
		{
			Padding = new Thickness(10, 8),
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			RowDefinitions = new RowDefinitionCollection
			{
				new RowDefinition () { Height = GridLength.Auto },
				new RowDefinition () { Height = new GridLength (1, GridUnitType.Star) },
			},
			BackgroundColor = Color.FromHex("#30000000")
		};

		StackLayout mainLayout = new StackLayout
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.StartAndExpand,
			Orientation = StackOrientation.Vertical,
			//Padding = new Thickness(10, 5)
		};

		MyButton btnBack = new MyButton
		{
			BackgroundColor = Color.Transparent,
			HorizontalOptions = LayoutOptions.Start,
			Image = "back.png",
			WidthRequest = 24,
			HeightRequest = 15,
			//CustomPadding = new Thickness(1)
		};

		Label lbTitle = new Label
		{
			Text = "CERCA",
			FontSize = 25,
			TextColor = Color.FromHex("F1C40F"),
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			HorizontalOptions = LayoutOptions.Center,

		};

		Grid titleStack = new Grid
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			//Orientation = StackOrientation.Horizontal,
			Padding = new Thickness(2),
		};

		Image imagebackground = new Image
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			Source = "login_screen_background.png",
			Aspect = Aspect.AspectFill,
			Margin = new Thickness(0, 0, 0, 0)
		};

		Frame searchFrame = new Frame
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.Fill,
			//OutlineColor = Color.FromHex(App.Gray),
			HasShadow = false,
			BackgroundColor = Color.Transparent,
			Padding = new Thickness(0),
		};

		MyEntry txtSearch = new MyEntry
		{
			CustomPadding = new Thickness(12, 8, 12, 8),
			CustomBackgroundColor = Color.Transparent,
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.Center,
			PlaceholderColor = Color.White,
			FontSize = 15,
			TextColor = Color.White,
		};

		//StackLayout searchStack = new StackLayout
		//{
		//	HorizontalOptions = LayoutOptions.FillAndExpand,
		//	VerticalOptions = LayoutOptions.StartAndExpand,
		//	Orientation = StackOrientation.Horizontal,
		//	Spacing = 2,
		//};

		Button btnSearch = new Button
		{
			Image = "ic_search.png",
			//RotationY = 180,
			WidthRequest = 30,
			HeightRequest = 20,
			IsEnabled = false,
			Margin = new Thickness(0, 0, 5, 0),
			HorizontalOptions = LayoutOptions.End,
			BackgroundColor = Color.Transparent,
		};

		Grid grdSearch = new Grid
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			BackgroundColor = Color.Transparent,
			//Padding = new Thickness(5, 0, 20, 0),

			Margin = new Thickness(15, 15, 15, 15),

		};

		//Entry txtSearch = new Entry
		//{
		//	HorizontalOptions = LayoutOptions.FillAndExpand,
		//	VerticalOptions = LayoutOptions.CenterAndExpand,
		//	Placeholder = "Nome e/o cognome",
		//	PlaceholderColor = Color.FromHex(App.Gray),
		//	TextColor = Color.Black,
		//	BackgroundColor = Color.Transparent,
		//};

		//Button btnSearch = new Button
		//{
		//	HorizontalOptions = LayoutOptions.End,
		//	VerticalOptions = LayoutOptions.CenterAndExpand,
		//	Text = "CERCA",
		//	TextColor = Color.FromHex(App.Black),
		//	BackgroundColor = Color.Transparent,
		//	BorderRadius = 0,
		//	IsEnabled = false,
		//};

		//Button btnBarcode = new Button
		//{
		//	HorizontalOptions = LayoutOptions.FillAndExpand,
		//	VerticalOptions = LayoutOptions.CenterAndExpand,
		//	BackgroundColor = Color.FromHex(App.Orange),
		//	Text = "Scansione barcode",
		//	TextColor = Color.White,
		//	Image = "ic_barcode.png"
		//};

		Button btnBarcode = new Button
		{
			HorizontalOptions = LayoutOptions.Fill,
			VerticalOptions = LayoutOptions.Fill,
			Text = "Scansione barcode",
			TextColor = Color.Yellow,
			BackgroundColor = Color.Black,
			BorderRadius = 0,
		};

		Image imgBarcode = new Image
		{
			Source = "barcode.png",
			WidthRequest = 20,
			HeightRequest = 20,
			IsEnabled = false,
			Margin = new Thickness(0, 0, 20, 0),
			HorizontalOptions = LayoutOptions.End,
		};

		Grid grdBarcode = new Grid
		{
			HorizontalOptions = LayoutOptions.Fill,
			VerticalOptions = LayoutOptions.Fill,
			BackgroundColor = Color.Yellow,
			Padding = new Thickness(1),
			Margin = new Thickness(15, 0, 15, 20)
		};

		Label lblErrorMsg = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "Non ci sono risultati",
			TextColor = Color.Red,
			HorizontalTextAlignment = TextAlignment.Center,
			IsVisible = false,
		};

		ActivityIndicator activityIndicator = new ActivityIndicator
		{
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			VerticalOptions = LayoutOptions.StartAndExpand,
			Color = Color.Black,
			IsRunning = false,
			IsVisible = false,
		};

		ListView lstUsers = new ListView
		{
			HorizontalOptions = LayoutOptions.Fill,
			VerticalOptions = LayoutOptions.Fill,
			BackgroundColor = Color.Transparent,
			HasUnevenRows = true,
			SeparatorVisibility = SeparatorVisibility.None,
			ItemTemplate = new DataTemplate(typeof(NewSearchUserViewCell)),
		};

		//List HeaderTemplate Code Start
		Grid MainGrid = new Grid
		{
			ColumnSpacing = 2,
			RowSpacing = 0,
			//Padding = new Thickness(1),
			BackgroundColor = Color.FromHex(App.Black),
			RowDefinitions = new RowDefinitionCollection() {
				new RowDefinition () { Height = new GridLength (1, GridUnitType.Auto) },
			},
			ColumnDefinitions = new ColumnDefinitionCollection() {
				new ColumnDefinition () { Width = new GridLength (0.20, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.35, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.20, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.25, GridUnitType.Star) },
			}
		};

		Label lblName = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 14,
			FontAttributes = FontAttributes.Bold,
			Text = "Name"
		};

		Label lblAddress = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 14,
			FontAttributes = FontAttributes.Bold,
			Text = "Indirizzo"
		};

		Label lblDateOfBirth = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 14,
			FontAttributes = FontAttributes.Bold,
			Text = "Nascita"
		};

		Label lblImage = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 14,
			FontAttributes = FontAttributes.Bold,
			Text = "Foto"
		};
		//List Header Template code Over.

		public SearchPage()
		{
			Title = "CERCA";
			NavigationPage.SetHasNavigationBar(this, false);
			NavigationPage.SetHasBackButton(this, false);
			titleStack.Children.Add(btnBack);
			titleStack.Children.Add(lbTitle);

			var options = new MobileBarcodeScanningOptions
			{
				AutoRotate = false,
				UseFrontCameraIfAvailable = false,
				TryHarder = true,
				PossibleFormats = new List<ZXing.BarcodeFormat> {
					ZXing.BarcodeFormat.CODE_39
					//ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.QR_CODE, ZXing.BarcodeFormat.All_1D,
				}
			};

			//add options and customize page
			scanPage = new ZXingScannerPage(options)
			{
				DefaultOverlayTopText = "Align the barcode within the frame",
				DefaultOverlayBottomText = string.Empty,
				DefaultOverlayShowFlashButton = true,
			};


			scanPage.OnScanResult += ScanPage_OnScanResult;
			CrossConnectivity.Current.ConnectivityChanged += CrossConnectivity_Current_ConnectivityChanged;

			//MainGrid.Children.Add(lblName, 0, 0);
			//MainGrid.Children.Add(lblAddress, 1, 0);
			//MainGrid.Children.Add(lblDateOfBirth, 2, 0);
			//MainGrid.Children.Add(lblImage, 3, 0);

			lstUsers.ItemsSource = usersCollection;
			lstUsers.Header = MainGrid;

			//searchStack.Children.Add(imgSearchIcon);
			//searchStack.Children.Add(txtSearch);
			//searchStack.Children.Add(btnSearch);
			grdSearch.Children.Add(txtSearch);
			grdSearch.Children.Add(btnSearch);

			btnSearch.Clicked += BtnSearch_Clicked;
			btnBarcode.Clicked += BtnBarcode_Clicked;
			txtSearch.TextChanged += TxtSearch_TextChanged;
			lstUsers.ItemTapped += LstUsers_ItemTapped;
			btnBack.Clicked += BtnBack_Clicked;

			searchFrame.Content = grdSearch;
			grdBarcode.Children.Add(btnBarcode);
			grdBarcode.Children.Add(imgBarcode);

			mainLayout.Children.Add(titleStack);
			mainLayout.Children.Add(searchFrame);
			mainLayout.Children.Add(grdBarcode);
			mainLayout.Children.Add(lblErrorMsg);
			mainLayout.Children.Add(activityIndicator);
			//mainLayout.Children.Add(lstUsers);

			contentGrid.Children.Add(mainLayout);
			contentGrid.Children.Add(lstUsers);
			Grid.SetRow(lstUsers, 1);

			Grid gridMainSearch = new Grid();
			gridMainSearch.Children.Add(imagebackground);
			gridMainSearch.Children.Add(contentGrid);
			Content = gridMainSearch;
		}

		async void LstUsers_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			try
			{
				UsersModel userModel = (UsersModel)e.Item;
				await Navigation.PushAsync(new DetailPage(userModel));
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Item Selected Exception : " + pException.Message);
			}
			finally
			{
				GC.Collect();
			}
		}

		private async void BtnBack_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Dashboard());
		}

		async void LstUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			try
			{
				UsersModel userModel = (UsersModel)e.SelectedItem;
				await Navigation.PushAsync(new DetailPage(userModel));
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Item Selected Exception : " + pException.Message);
			}
			finally
			{
				GC.Collect();
			}
		}

		void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (e.NewTextValue.Length > 0)
			{
				btnSearch.IsEnabled = true;
			}
			else
			{
				btnSearch.IsEnabled = false;
			}
		}

		void CrossConnectivity_Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				lblErrorMsg.Text = "Nessun dato"; //set Error Message Default Value.
				lblErrorMsg.IsVisible = false;
				txtSearch.IsEnabled = true;
				btnBarcode.IsEnabled = true;
				if (txtSearch.Text != null && txtSearch.Text.Length > 0)
					btnSearch.IsEnabled = true;
			}
			else
			{
				lblErrorMsg.Text = "Internet Connection Not Available.";
				lblErrorMsg.IsVisible = true;
				btnSearch.IsEnabled = false;
				txtSearch.IsEnabled = false;
				btnBarcode.IsEnabled = false;
			}
		}

		async void BtnBarcode_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(scanPage);
		}

		private async void BtnSearch_Clicked(object sender, EventArgs e)
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					activityIndicator.IsVisible = true;
					activityIndicator.IsRunning = true;
					btnSearch.IsEnabled = false;
					btnBarcode.IsEnabled = false;
					System.Diagnostics.Debug.WriteLine("Search Page API Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
					APIServices.ApiServices apiServices = new APIServices.ApiServices();
					List<UsersModel> usersModel = await apiServices.SearchAsync(txtSearch.Text, CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
					System.Diagnostics.Debug.WriteLine("Search Page API Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
					activityIndicator.IsVisible = false;
					AddDataInList(usersModel);

				}
				catch (Exception pException)
				{
					await DisplayAlert("Searching Failed!", pException.Message, "Ok");
					System.Diagnostics.Debug.WriteLine("Search Page : Exception : " + pException.Message);
				}
				finally
				{
					//activityIndicator.IsVisible = false;
					activityIndicator.IsRunning = false;
					btnBarcode.IsEnabled = true;
					btnSearch.IsEnabled = true;
					GC.Collect();
				}
			}
			else
			{
				lblErrorMsg.Text = "Internet connection not available.";
				lblErrorMsg.IsVisible = true;
			}
		}

		private void AddDataInList(List<UsersModel> usersModel)
		{
			try
			{
				if (usersModel != null)
				{
					usersCollection.Clear();
					if (usersModel.Count > 0)
					{
						System.Diagnostics.Debug.WriteLine("Search Page Record Add Loop Start : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
						foreach (var item in usersModel)
						{
							usersCollection.Add(item);
						}
						lblErrorMsg.IsVisible = false;
						System.Diagnostics.Debug.WriteLine("Search Page Record Add Loop Over : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
					}
					else
					{
						lblErrorMsg.IsVisible = true;
					}
				}
				else
				{
					usersCollection.Clear();
					lblErrorMsg.IsVisible = true;
				}
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Exception In Add Data In List : " + pException.Message + " StackTrace : " + pException.StackTrace);
			}
		}

		void ScanPage_OnScanResult(ZXing.Result result)
		{
			scanPage.IsScanning = false;

			// Pop the page and show the result
			Device.BeginInvokeOnMainThread(async () =>
			{
				await Navigation.PopAsync();

				System.Diagnostics.Debug.WriteLine("Barcode Value : " + result.Text);
				String code = result.Text; //"21908"
										   //await Navigation.PushAsync(new DetailPage(result.Text));

				if (CrossConnectivity.Current.IsConnected)
				{
					try
					{
						activityIndicator.IsVisible = true;
						activityIndicator.IsRunning = true;
						btnSearch.IsEnabled = false;
						btnBarcode.IsEnabled = false;
						APIServices.ApiServices apiServices = new APIServices.ApiServices();
						List<UsersModel> usersModel = await apiServices.GetTicketsByBarcodeAsync2(code, CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
						AddDataInList(usersModel);

					}
					catch (Exception pException)
					{
						await DisplayAlert("Searching Failed by Barcode!", pException.Message, "Ok");
						System.Diagnostics.Debug.WriteLine("Search Page : Exception : " + pException.Message);
					}
					finally
					{
						//activityIndicator.IsVisible = false;
						activityIndicator.IsRunning = false;
						btnBarcode.IsEnabled = true;
						btnSearch.IsEnabled = true;
						GC.Collect();
					}
				}
				else
				{
					lblErrorMsg.Text = "Internet connection not available.";
					lblErrorMsg.IsVisible = true;
				}
			});
		}
	}
}
