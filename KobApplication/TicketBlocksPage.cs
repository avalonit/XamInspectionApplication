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
	public class TicketBlocksPage : ContentPage
	{
		ZXingScannerPage scanPage;
		ObservableCollection<TicketBlockModel> ticketsCollection = new ObservableCollection<TicketBlockModel>();

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
			VerticalOptions = LayoutOptions.FillAndExpand,
			Orientation = StackOrientation.Vertical,
            BackgroundColor = Color.FromHex("#30000000")
        };

        MyButton btnBack = new MyButton
        {
            BackgroundColor = Color.Transparent,
			HorizontalOptions= LayoutOptions.Start,
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
            VerticalOptions = LayoutOptions.StartAndExpand,
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
            CustomPadding = new Thickness(12,8,12,8),
            CustomBackgroundColor = Color.Transparent,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.Center,
            Placeholder = "Attesa scansione barcode",
            PlaceholderColor = Color.White,
            FontSize = 15,
            TextColor = Color.White,
        };

        Image imgSearchIcon = new Image
        {
            Source = "ic_search.png",
            //RotationY = 180,
            WidthRequest = 17,
            HeightRequest = 17,
            IsEnabled = false,
            Margin = new Thickness(10, 15, 10, 15),
            HorizontalOptions = LayoutOptions.End,
        };

        Grid grdSearch = new Grid
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.Transparent,
            //Padding = new Thickness(5, 0, 20, 0),

            Margin = new Thickness(15, 15, 15, 15),

        };

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
            Source = "ic_barcode.png",
            WidthRequest = 17,
            HeightRequest = 17,
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

		ListView lstTickets = new ListView
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			BackgroundColor = Color.Transparent,
			HasUnevenRows = true,
			ItemTemplate = new DataTemplate(typeof(TicketBlockModelViewCell)),
		};

		//List HeaderTemplate Code Start
		Grid grdRow = new Grid
		{
			//ColumnSpacing = 2,
			//RowSpacing = 0,
			//Padding = new Thickness(1),
			BackgroundColor = Color.Transparent,
			RowDefinitions = new RowDefinitionCollection() {
				new RowDefinition () { Height = new GridLength (1, GridUnitType.Auto) },
			},
			ColumnDefinitions = new ColumnDefinitionCollection() {
				new ColumnDefinition () { Width = new GridLength (1, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (5, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (2, GridUnitType.Star) },
			}
		};

        Grid grdColumInfor = new Grid
        {
            BackgroundColor = Color.Transparent,
            RowDefinitions = new RowDefinitionCollection {
                new RowDefinition () { Height = new GridLength (1, GridUnitType.Star)},
                new RowDefinition () { Height = new GridLength (1, GridUnitType.Star)},
            },
            ColumnDefinitions = new ColumnDefinitionCollection() {
                new ColumnDefinition () { Width = new GridLength(1, GridUnitType.Auto) },
            }
        };

        Image imgRow = new Image
        {
            BackgroundColor = Color.Transparent,
            Source = "icon.png",
            WidthRequest = 25,
            HeightRequest = 25,
            IsEnabled = false,
        };

        Label lbName = new Label
        {
            BackgroundColor = Color.Transparent,
            Text = "Alberto Valenti",
            TextColor = Color.Yellow,
        };

        Label lbInfor = new Label
        {
            BackgroundColor = Color.Transparent,
            Text = "Alberto Valenti",
            TextColor = Color.Yellow,
        };

        Label lblBlocco_dateassign = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            Text = "Data"
        };

        Label lblBlocco_start = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            Text = "Inizio"
        };

        Label lblBlocco_end = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            Text = "Fine"
        };


        Grid MainGrid = new Grid
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Start,
            RowDefinitions = new RowDefinitionCollection() {
                new RowDefinition () { Height = new GridLength (1, GridUnitType.Auto) },
            },
            ColumnDefinitions = new ColumnDefinitionCollection() {
                new ColumnDefinition () { Width = new GridLength (0.33, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.33, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.33, GridUnitType.Star) },
            }
        };

        //List Header Template code Over.

        public TicketBlocksPage()
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

            MainGrid.Children.Add(lblBlocco_dateassign, 0, 0);
            MainGrid.Children.Add(lblBlocco_start, 1, 0);
            MainGrid.Children.Add(lblBlocco_end, 2, 0);

            //grdRow.Children
            lstTickets.ItemsSource = ticketsCollection;
            lstTickets.Header = MainGrid;

            //searchStack.Children.Add(imgSearchIcon);
            grdSearch.Children.Add(txtSearch);
            grdSearch.Children.Add(imgSearchIcon);

            btnBarcode.Clicked += BtnBarcode_Clicked;
            btnBack.Clicked += BtnBack_Clicked;

            searchFrame.Content = grdSearch;

            grdBarcode.Children.Add(btnBarcode);
            grdBarcode.Children.Add(imgBarcode);

            mainLayout.Children.Add(titleStack);
            mainLayout.Children.Add(searchFrame);
			mainLayout.Children.Add(grdBarcode);
			mainLayout.Children.Add(lblErrorMsg);
			mainLayout.Children.Add(activityIndicator);
            //mainLayout.Children.Add(imagebackground);
			//mainLayout.Children.Add(lstTickets);

			contentGrid.Children.Add(mainLayout);
            contentGrid.Children.Add(lstTickets);
			Grid.SetRow(lstTickets, 1);

            Grid gridTicketBlocks = new Grid();
            gridTicketBlocks.Children.Add(imagebackground);
            gridTicketBlocks.Children.Add(contentGrid);
			Content = gridTicketBlocks;
			UpdateList();
		}

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Dashboard());
        }

        void CrossConnectivity_Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				lblErrorMsg.Text = "Nessun dato"; //set Error Message Default Value.
				lblErrorMsg.IsVisible = false;
				lblErrorMsg.TextColor = Color.Red;
				txtSearch.IsEnabled = true;
				btnBarcode.IsEnabled = true;
			}
			else {
				lblErrorMsg.Text = "Internet Connection Not Available.";
				lblErrorMsg.IsVisible = true;
				lblErrorMsg.TextColor = Color.Red;
				txtSearch.IsEnabled = false;
				btnBarcode.IsEnabled = false;
			}
		}

		async void BtnBarcode_Clicked(object sender, EventArgs e)
		{
			lblErrorMsg.Text = "Attesa dati...";
			lblErrorMsg.IsVisible = true;
			lblErrorMsg.TextColor = Color.Red;
			await Navigation.PushAsync(scanPage);
		}

		private async void UpdateList()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					activityIndicator.IsVisible = true;
					activityIndicator.IsRunning = true;
					btnBarcode.IsEnabled = false;
					System.Diagnostics.Debug.WriteLine("Search Page API Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
					APIServices.ApiServices apiServices = new APIServices.ApiServices();
					List<TicketBlockModel> ticketsModel = await apiServices.GetTicketBlocksAsync(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
					System.Diagnostics.Debug.WriteLine("Search Page API Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
					AddDataInList(ticketsModel);

				}
				catch (Exception pException)
				{
					await DisplayAlert("Searching Failed!", pException.Message, "Ok");
					System.Diagnostics.Debug.WriteLine("Search Page : Exception : " + pException.Message);
				}
				finally
				{
					activityIndicator.IsVisible = false;
					activityIndicator.IsRunning = false;
					btnBarcode.IsEnabled = true;
					GC.Collect();
				}
			}
			else {
				lblErrorMsg.Text = "Internet connection not available.";
				lblErrorMsg.IsVisible = true;
				lblErrorMsg.TextColor = Color.Red;
			}
		}

		private void AddDataInList(List<TicketBlockModel> ticketsModel)
		{
			try
			{
				if (ticketsModel != null)
				{
					ticketsCollection.Clear();
					if (ticketsModel.Count > 0)
					{
						System.Diagnostics.Debug.WriteLine("Search Page Record Add Loop Start : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
						foreach (var item in ticketsModel)
						{
							ticketsCollection.Add(item);
						}
						System.Diagnostics.Debug.WriteLine("Search Page Record Add Loop Over : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
					}
					else
					{
					}
				}
				else
				{
					ticketsCollection.Clear();
					lblErrorMsg.IsVisible = true;
					lblErrorMsg.TextColor = Color.Red;
				}
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Exception In Add Data In List : " + pException.Message + " StackTrace : " + pException.StackTrace);
			}
		}

		void CheckResult(String block_number,List<TicketBlockModel> ticketsModel)
		{
			int Blocco_start = 0;
			Boolean found = false;

			Int32.TryParse(block_number, out Blocco_start);
			foreach(TicketBlockModel t in ticketsModel)
			{
				if (t.Blocco_start == Blocco_start)
				{
					lblErrorMsg.IsVisible = true;
					lblErrorMsg.Text = "Blocchetto " + Blocco_start.ToString() + " assegnato";
					lblErrorMsg.TextColor = Color.Orange;
					found = true;
					break;
				}
			}
			if( !found )
			{
				lblErrorMsg.IsVisible = true;
				lblErrorMsg.Text = "ERRORE: blocchetto " + Blocco_start.ToString() + " NON inserito";
				lblErrorMsg.TextColor = Color.Red;
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
				//await Navigation.PushAsync(new DetailPage(result.Text));

				if (CrossConnectivity.Current.IsConnected)
				{
					try
					{
						String block_number = result.Text;
						txtSearch.Text = block_number;

						activityIndicator.IsVisible = true;
						activityIndicator.IsRunning = true;
						btnBarcode.IsEnabled = false;

						System.Diagnostics.Debug.WriteLine("Search Page API Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
						APIServices.ApiServices apiServices = new APIServices.ApiServices();
						List<TicketBlockModel> ticketsModel = await apiServices.AddTicketBlocksAsync(block_number, CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
						System.Diagnostics.Debug.WriteLine("Search Page API Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);

						CheckResult(block_number,ticketsModel);
						AddDataInList(ticketsModel);
					}
					catch (Exception pException)
					{
						await DisplayAlert("Searching Failed by Barcode!", pException.Message, "Ok");
						System.Diagnostics.Debug.WriteLine("Search Page : Exception : " + pException.Message);
					}
					finally
					{
						activityIndicator.IsVisible = false;
						activityIndicator.IsRunning = false;
						btnBarcode.IsEnabled = true;
						GC.Collect();
					}
				}
				else
				{
					lblErrorMsg.Text = "Internet connection not available.";
					lblErrorMsg.IsVisible = true;
					lblErrorMsg.TextColor = Color.Red;
				}
			});
		}
	}
}
