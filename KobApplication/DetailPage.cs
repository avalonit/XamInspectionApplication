using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using KobApp.HelperView;
using KobApp.DataModel;
using KobApp.APIServices;
using System.Collections.ObjectModel;
using Plugin.Settings;
using KobApplication.Controls;

namespace KobApp
{
	public class DetailPage : ContentPage
	{
		ObservableCollection<TicketModel> ticketsCollection = new ObservableCollection<TicketModel>();

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
			//Padding = new Thickness(8),
			Orientation = StackOrientation.Vertical,
        };
        Image imagebackground = new Image
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Source = "login_screen_background.png",
            Aspect = Aspect.AspectFill,
            Margin = new Thickness(0, 0, 0, 0)
        };

        MyButton btnBack = new MyButton
        {
            BackgroundColor = Color.Transparent,
			HorizontalOptions= LayoutOptions.Start,
            Image = "back.png",
            WidthRequest = 24,
            HeightRequest = 15,
            //Margin = new Thickness(8, 0, 0, 0)
        };

		Label lbTitle = new Label
		{
			Text = "DETTAGLIO",
			FontSize = 25,
			TextColor = Color.FromHex("F1C40F"),
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			HorizontalOptions = LayoutOptions.Center,
            //Margin = new Thickness(100, 0, 0, 0)

        };

		Grid titleStack = new Grid
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            //Orientation = StackOrientation.Horizontal,
            Padding = new Thickness(0,0,0,20),
            BackgroundColor = Color.Transparent,
        };

        Frame profileFrame = new Frame
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.StartAndExpand,
			BackgroundColor = Color.Transparent,
			HasShadow = false,
		};

		StackLayout profileStack = new StackLayout
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.Start,
			Orientation = StackOrientation.Horizontal,
		};

        Grid gridInfor = new Grid
        {
            BackgroundColor = Color.Transparent,
            RowDefinitions = new RowDefinitionCollection() {
                new RowDefinition () { Height = new GridLength (0, GridUnitType.Auto) },
            },
            ColumnDefinitions = new ColumnDefinitionCollection() {
                new ColumnDefinition () { Width = new GridLength (2, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (7, GridUnitType.Star) },
            }
        };

		Image imgPhoto = new Image
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			Aspect = Aspect.AspectFit,
			HeightRequest = 25,
			WidthRequest = 25,
            Margin = new Thickness(5)
		};

        StackLayout stlPersonInfor = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            HorizontalOptions = LayoutOptions.StartAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Padding = new Thickness(0),
            Margin = new Thickness(3,0,5,0),
            Spacing = 0
        };

        Label lbName = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.FromHex("F1C40F"),
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
        };

        StackLayout stlAddress = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Margin = new Thickness(0, 0, 0, 15),
        };

        Label lbTitleAddress = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromHex("F1C40F"),
            Text = "Address",
            
        };

        Label lbAddress = new Label
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
            FontSize = 14,
            LineBreakMode = LineBreakMode.TailTruncation,
        };

        StackLayout stlBirthDay = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
        };

        Label lbTitleDOB = new Label
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.FillAndExpand,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromHex("F1C40F"),
            Text = "D.O.B "
        };

        Label lbBirthDay = new Label
        {
            HorizontalOptions = LayoutOptions.StartAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
            FontSize = 14,
        };

        //      Label lblPersonInfo = new Label
        //{
        //	HorizontalOptions = LayoutOptions.FillAndExpand,
        //	VerticalOptions = LayoutOptions.FillAndExpand,
        //	LineBreakMode = LineBreakMode.WordWrap,
        //	TextColor = Color.Black,
        //	HorizontalTextAlignment = TextAlignment.Start,
        //	VerticalTextAlignment = TextAlignment.Center,
        //	FontSize = 16,
        //};

        View lineBottom = new BoxView
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = 1,
            BackgroundColor = Color.FromHex("#66FFFFFF")
        };

        View lineBlack1 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.Black,
        };

        View lineBlack2 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.Black,
        };

        View lineBlack3 = new BoxView
        {
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Color.Black,
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
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Color = Color.Black,
			IsRunning = false,
			IsVisible = false,
		};

		ListView lstTickets = new ListView
		{
			HasUnevenRows = true,
			BackgroundColor = Color.Transparent,
			SeparatorVisibility = SeparatorVisibility.None,
			SeparatorColor = Color.Transparent,
			ItemTemplate = new DataTemplate(typeof(TicketsViewCell)),
		};


		//List HeaderTemplate Code Start
		Grid listHeaderGrid = new Grid
		{
			//ColumnSpacing = 0.5,
			//RowSpacing = 0.5,
			Padding = new Thickness(1),
			BackgroundColor = Color.Black,
            Margin = new Thickness(0, 5, 0, 0),
            ColumnSpacing = 0,
			RowDefinitions = new RowDefinitionCollection() {
				new RowDefinition () { Height = new GridLength (1, GridUnitType.Auto),},
                
			},
			ColumnDefinitions = new ColumnDefinitionCollection() {
				new ColumnDefinition () { Width = new GridLength (0.40, GridUnitType.Star) },
                new ColumnDefinition () { Width = 1},
                new ColumnDefinition () { Width = new GridLength (0.23, GridUnitType.Star) },
                new ColumnDefinition () { Width = 1},
                new ColumnDefinition () { Width = new GridLength (0.23, GridUnitType.Star) },
                new ColumnDefinition () { Width = 1},
                new ColumnDefinition () { Width = new GridLength (0.16, GridUnitType.Star) },
			}
		};

		Label lblTicketType = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 14,
			FontAttributes = FontAttributes.Bold,
			Text = "Tipo",
            BackgroundColor = Color.FromHex("F1C40F")
        };

		Label lblFrom = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontAttributes = FontAttributes.Bold,
            BackgroundColor = Color.FromHex("F1C40F")
        };

		Label lblTo = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 14,
			FontAttributes = FontAttributes.Bold,
            BackgroundColor = Color.FromHex("F1C40F")
        };

		Label lblValid = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 14,
			FontAttributes = FontAttributes.Bold,
			Text = "Valido",
            BackgroundColor = Color.FromHex("F1C40F")
        };
		//List Header Template code Over.

		public DetailPage(UsersModel usersModel)
		{
			Title = "DETTAGLIO";
			NavigationPage.SetHasNavigationBar(this, false);
			NavigationPage.SetHasBackButton(this, false);
            titleStack.Children.Add(btnBack);
            titleStack.Children.Add(lbTitle);

            imgPhoto.Source = ImageSource.FromUri(new Uri(usersModel.IMAGE));
            stlAddress.Children.Add(lbTitleAddress);
            lbAddress.Text = string.Format("{0}, {1}, {2}", usersModel.RESID_INDIRIZZO, usersModel.RESID_LOCALITA, usersModel.RESID_COMUNE);
            stlAddress.Children.Add(lbAddress);
            stlBirthDay.Children.Add(lbTitleDOB);
            lbBirthDay.Text = String.Format("{0:dd/MM/yyyy}", usersModel.DATA_NASCITA);
            stlBirthDay.Children.Add(lbBirthDay);

            lbName.Text = string.Format("{0} {1}", usersModel.NOME, usersModel.COGNOME);
            stlPersonInfor.Children.Add(lbName);
            stlPersonInfor.Children.Add(stlAddress);
            stlPersonInfor.Children.Add(stlBirthDay);

            //lblPersonInfo.Text = string.Format("{0} {1}\n\n{2}, {3}, {4}\n\nDOB :- {5:dd/MM/yyyy}", usersModel.NOME, usersModel.COGNOME, usersModel.RESID_INDIRIZZO, usersModel.RESID_LOCALITA, usersModel.RESID_COMUNE, usersModel.DATA_NASCITA);

            gridInfor.Children.Add(imgPhoto, 0, 0);
            gridInfor.Children.Add(stlPersonInfor, 1, 0);
			profileStack.Children.Add(gridInfor);
            //profileStack.Children.Add(lblPersonInfo);

            //profileFrame.Content = profileStack;

            mainLayout.Children.Add(titleStack);
			mainLayout.Children.Add(profileStack);
            mainLayout.Children.Add(lineBottom);
			mainLayout.Children.Add(lblErrorMsg);
			mainLayout.Children.Add(activityIndicator);
			//mainLayout.Children.Add(lstTickets);

			listHeaderGrid.Children.Add(lblTicketType, 0, 0);
            listHeaderGrid.Children.Add(lineBlack1, 1, 0);
			listHeaderGrid.Children.Add(lblFrom, 2, 0);
            listHeaderGrid.Children.Add(lineBlack2, 3, 0);
            listHeaderGrid.Children.Add(lblTo, 4, 0);
            listHeaderGrid.Children.Add(lineBlack3, 5, 0);
            listHeaderGrid.Children.Add(lblValid, 6, 0);

			lstTickets.Header = listHeaderGrid;
			lstTickets.ItemsSource = ticketsCollection;
            btnBack.Clicked += BtnBack_Clicked; ;

            contentGrid.Children.Add(mainLayout);
            contentGrid.Children.Add(lstTickets,0 ,1);

            Grid gridDetail = new Grid();
            gridDetail.Children.Add(imagebackground);
            gridDetail.Children.Add(contentGrid);

            Content = gridDetail;

			GetTicketsByUserModel(usersModel);
		}

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
			await Navigation.PopAsync();
            //AVawait Navigation.PushAsync(new SearchPage());
        }

        public DetailPage(string BarcodeID)
		{
			Title = "DETTAGLIO";
			NavigationPage.SetHasNavigationBar(this, true);
			NavigationPage.SetHasBackButton(this, true);

			//mainLayout.Children.Add(profileFrame);
			mainLayout.Children.Add(lblErrorMsg);
			mainLayout.Children.Add(activityIndicator);
			mainLayout.Children.Add(lstTickets);

			listHeaderGrid.Children.Add(lblTicketType, 0, 0);
			listHeaderGrid.Children.Add(lblFrom, 1, 0);
			listHeaderGrid.Children.Add(lblTo, 2, 0);
			listHeaderGrid.Children.Add(lblValid, 3, 0);

			lstTickets.Header = listHeaderGrid;
			lstTickets.ItemsSource = ticketsCollection;

			Content = mainLayout;

			GetTicketsByBarcodeID(BarcodeID);
		}

		private async void GetTicketsByUserModel(UsersModel userModel)
		{
			try
			{
				activityIndicator.IsRunning = true;
				activityIndicator.IsVisible = true;
				ApiServices apiServices = new ApiServices();
				List<TicketModel> ticketsList = await apiServices.GetTicketsAsync(userModel, CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				if (ticketsList != null)
				{
					AddTicketsInCollection(ticketsList);
				}
				else {
					ticketsCollection.Clear();
					lblErrorMsg.IsVisible = true;
				}
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Search Page : Exception : " + pException.Message);
			}
			finally
			{
				activityIndicator.IsRunning = false;
				activityIndicator.IsVisible = false;
			}
		}

		private async void GetTicketsByBarcodeID(string BarcodeID)
		{
			try
			{
				activityIndicator.IsRunning = true;
				activityIndicator.IsVisible = true;
				ApiServices apiServices = new ApiServices();
				List<TicketModel> ticketsList = await apiServices.GetTicketsByBarcodeAsync(BarcodeID, CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				if (ticketsList != null)
				{
					AddTicketsInCollection(ticketsList);
				}
				else
				{
					ticketsCollection.Clear();
					lblErrorMsg.IsVisible = true;
				}
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Search Page : Exception : " + pException.Message);
			}
			finally
			{
				activityIndicator.IsRunning = false;
				activityIndicator.IsVisible = false;
			}
		}

		private void AddTicketsInCollection(List<TicketModel> ticketsList)
		{
			try
			{
				if (ticketsList.Count > 0)
				{
					ticketsCollection.Clear();
					foreach (var item in ticketsList)
					{
						ticketsCollection.Add(item);
					}
					lblErrorMsg.IsVisible = false;
				}
				else
				{
					lblErrorMsg.IsVisible = true;
				}
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Exception : " + pException.Message + " StackTrace : " + pException.StackTrace);
			}
		}
	}
}
