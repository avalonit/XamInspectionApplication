using KobApp.HelperView;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using KobApp.DataModel;
using Plugin.Settings;
using Plugin.Connectivity;

namespace KobApp
{
	public class MyActivity : ContentPage
	{
		ObservableCollection<ActivityModel> ActivityDatas = new ObservableCollection<ActivityModel>();

		StackLayout MainLayout = new StackLayout
		{
			Padding = 4,
			Spacing = 5,
			Orientation = StackOrientation.Vertical,
		};

		StackLayout DateStack = new StackLayout
		{
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			Orientation = StackOrientation.Horizontal,
			Spacing = 10,
		};

		Image imgFrom = new Image
		{
			Aspect = Aspect.Fill,
			Source = "ic_dates.png",
		};

		Button btnlFrom = new Button
		{
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = string.Format("DAL : {0:dd/MM/yyyy}", DateTime.Now),
			TextColor = Color.Black,
			FontSize = 12,
		};

		Frame frmFrom = new Frame
		{
			HasShadow = true,
			OutlineColor = Color.FromHex(App.Orange),
			Padding = 3,
		};

		DatePicker dpFrom = new DatePicker
		{
			Format = "dd/MM/yyyy",
			Date = DateTime.Now,
			IsVisible = false,
		};

		Image imgTo = new Image
		{
			Aspect = Aspect.Fill,
			Source = "ic_dates.png",
		};

		Button btnTo = new Button
		{
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = string.Format("AL : {0:dd/MM/yyyy}", DateTime.Now),
			TextColor = Color.Black,
			FontSize = 12,
		};

		Frame frmTo = new Frame
		{
			HasShadow = true,
			OutlineColor = Color.FromHex(App.Orange),
			Padding = 3,
		};

		DatePicker dpTo = new DatePicker
		{
			Format = "dd/MM/yyyy",
			Date = DateTime.Now,
			IsVisible = false,
		};

		Button btnSearch = new Button
		{
			HorizontalOptions = LayoutOptions.End,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "CERCA",
			TextColor = Color.White,
			BackgroundColor = Color.FromHex(App.Orange),
		};

		Label lblErrorMsg = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "Nessun risultato",
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

		ListView lstDatas = new ListView
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			HasUnevenRows = true,
			ItemTemplate = new DataTemplate(typeof(ActivityViewCell)),
		};

		//List Header Template Code.
		Grid MainGrid = new Grid
		{
			ColumnSpacing = 1,
			RowSpacing = 0,
			Padding = new Thickness(1),
			BackgroundColor = Color.FromHex(App.Black),
			RowDefinitions = new RowDefinitionCollection() {
				new RowDefinition () { Height = new GridLength (1, GridUnitType.Auto) },
			},
			ColumnDefinitions = new ColumnDefinitionCollection() {
				new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.19, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.05, GridUnitType.Star) },
			}
		};

		Label lblName = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "Nome"
		};

		Label lblDate = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "Data"
		};

		Label lblCode = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "Codice"
		};

		Label lblAmount = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "Importo"
		};

		Label lblFee = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "Aggio"
		};

		Label lblOC = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "S"
		};

		public MyActivity()
		{
			Title = "ELENCO VERBALI";
			BackgroundColor = Color.White;
			NavigationPage.SetHasNavigationBar(this, true);
			NavigationPage.SetHasBackButton(this, true);

			CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

			TapGestureRecognizer DateTapGestureRecognizer = new TapGestureRecognizer();
			//DateTapGestureRecognizer.Tapped += DateTapGestureRecognizer_Tapped;
			//lblFrom.GestureRecognizers.Add(DateTapGestureRecognizer);
			btnlFrom.Clicked += DateTapGestureRecognizer_Tapped;
			//lblTo.GestureRecognizers.Add(DateTapGestureRecognizer);
			btnTo.Clicked += DateTapGestureRecognizer_Tapped;
			btnSearch.Clicked += BtnSearch_Clicked;

			dpFrom.Date = DateTime.Now;
			dpTo.Date = DateTime.Now;
			dpFrom.DateSelected += DpFrom_DateSelected;
			dpTo.DateSelected += DpTo_DateSelected;

			lstDatas.ItemsSource = ActivityDatas;
			lstDatas.Header = MainGrid;

			MainGrid.Children.Add(lblName, 0, 0);
			MainGrid.Children.Add(lblDate, 1, 0);
			MainGrid.Children.Add(lblCode, 2, 0);
			MainGrid.Children.Add(lblAmount, 3, 0);
			MainGrid.Children.Add(lblFee, 4, 0);
			MainGrid.Children.Add(lblOC, 5, 0);

			//DateStack.Children.Add(imgFrom);
			frmFrom.Content = btnlFrom;
			frmTo.Content = btnTo;
			DateStack.Children.Add(frmFrom);
			DateStack.Children.Add(dpFrom);
			//DateStack.Children.Add(imgTo);
			DateStack.Children.Add(frmTo);
			DateStack.Children.Add(dpTo);
			DateStack.Children.Add(btnSearch);

			MainLayout.Children.Add(DateStack);
			MainLayout.Children.Add(lblErrorMsg);
			MainLayout.Children.Add(activityIndicator);
			MainLayout.Children.Add(lstDatas);

			Content = MainLayout;
		}

		private void BtnSearch_Clicked(object sender, EventArgs e)
		{
			LoadActivityData(dpFrom.Date, dpTo.Date);
		}

		private void DpTo_DateSelected(object sender, DateChangedEventArgs e)
		{
			btnTo.Text = string.Format("To : {0:dd/MM/yyyy}", e.NewDate);
			//LoadActivityData(dpFrom.Date,e.NewDate);
		}

		private void DpFrom_DateSelected(object sender, DateChangedEventArgs e)
		{
			btnlFrom.Text = string.Format("From : {0:dd/MM/yyyy}", e.NewDate);
			//LoadActivityData(e.NewDate, dpTo.Date);
		}

		private void DateTapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			if (sender.Equals(btnlFrom))
				dpFrom.Focus();
			else
				dpTo.Focus();
		}

		private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				lblErrorMsg.Text = "Nessun risultato"; //set Error Message Default Value.
				lblErrorMsg.IsVisible = false;
			}
			else
			{
				lblErrorMsg.Text = "Internet Connection Not Available.";
				lblErrorMsg.IsVisible = true;
			}
		}

		private async void LoadActivityData(DateTime From, DateTime To)
		{
			System.Diagnostics.Debug.WriteLine("From : " + string.Format("{0:yy-MM-dd}", From) + " To : " + string.Format("{0:yy-MM-dd}", To));
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					activityIndicator.IsVisible = true;
					activityIndicator.IsRunning = true;
					System.Diagnostics.Debug.WriteLine("MyActivity API Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
					APIServices.ApiServices apiServices = new APIServices.ApiServices();
					List<ActivityModel> lstActivityModel = await apiServices.GetActivityModelData(dpTo.Date, dpFrom.Date, CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
					System.Diagnostics.Debug.WriteLine("MyActivity API Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
					AddActivityData(lstActivityModel);
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
					GC.Collect();
				}
			}
			else
			{
				lblErrorMsg.Text = "Internet connection not available.";
				lblErrorMsg.IsVisible = true;
			}
		}

		private void AddActivityData(List<ActivityModel> activityDatas)
		{
			ActivityDatas.Clear();
			if (activityDatas != null && activityDatas.Count > 0)
			{
				lblErrorMsg.IsVisible = false;
				foreach (var item in activityDatas)
				{
					ActivityDatas.Add(item);
				}
			}
			else
			{
				lblErrorMsg.Text = "Nessun risultato";
				lblErrorMsg.IsVisible = true;
			}
		}
	}
}
