using KobApp.HelperView;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using KobApp.DataModel;
using Plugin.Settings;
using KobApp.DB.Business;

namespace KobApp
{
    public class MyReports : ContentPage
    {
		ObservableCollection<InspectionsModel> InspectionData = new ObservableCollection<InspectionsModel>();

        StackLayout MainLayout = new StackLayout
        {           
            Padding = 4,
            Spacing = 5,
            Orientation = StackOrientation.Vertical,
        };

		Button btnInsert = new Button
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Text = "NUOVO RAPPORTO",
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
                new ColumnDefinition () { Width = new GridLength (0.15, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.55, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.15, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.15, GridUnitType.Star) },
            }
        };

		Label lblDuty = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 11,
            FontAttributes = FontAttributes.Bold,
            Text = "Turno"
        };

		Label lblInspectors = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 11,
            FontAttributes = FontAttributes.Bold,
            Text = "Verificatori"
        };

		Label lblStart = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 11,
            FontAttributes = FontAttributes.Bold,
            Text = "Inizio"
        };

		Label lblEnd = new Label
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            TextColor = Color.White,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 11,
            FontAttributes = FontAttributes.Bold,
            Text = "Fine"
        };

        public MyReports()
        {
            Title = "LISTA";
            BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);

            btnInsert.Clicked += BtnInsert_Clicked;

			lstDatas.ItemsSource = InspectionData;
			lstDatas.Header = MainGrid;

            MainGrid.Children.Add(lblDuty, 0, 0);
            MainGrid.Children.Add(lblInspectors, 1, 0);
            MainGrid.Children.Add(lblStart, 2, 0);
            MainGrid.Children.Add(lblEnd, 3, 0);

			MainLayout.Children.Add(btnInsert);
			MainLayout.Children.Add(lblErrorMsg);
            MainLayout.Children.Add(activityIndicator);
			MainLayout.Children.Add(lstDatas);

            Content = MainLayout;

			LoadActivityData();
        }

		private async void BtnInsert_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddReport());
		}

		private async void LoadActivityData()
        {
            try
            {
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;                   
                System.Diagnostics.Debug.WriteLine("MyReports DB Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
			    InspectionsBusiness db = new InspectionsBusiness();
				List<InspectionsModel> inspections= db.GetAll();                
                System.Diagnostics.Debug.WriteLine("MyReports DB Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
                AddActivityData(inspections);
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

        private void AddActivityData(List<InspectionsModel> inspections)
        {
            InspectionData.Clear();
            if(inspections != null && inspections.Count > 0)
            {
                lblErrorMsg.IsVisible = false;
                foreach(var item in inspections)
                {
                    InspectionData.Add(item);
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
