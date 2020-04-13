using KobApp.HelperView;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using KobApp.DataModel;
using Plugin.Settings;
using KobApp.DB.Business;
using KobApp.DB.Interfaces;

namespace KobApp
{
    public class StartReportLight : ContentPage
    {
		ObservableCollection<InspectionInspectorsModel> InspectionData = new ObservableCollection<InspectionInspectorsModel>();

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
			Text = "SUCCESSIVO",
			TextColor = Color.White,
			BackgroundColor = Color.Black, 
		};


		StackLayout contentStack = new StackLayout
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.StartAndExpand,
			Orientation = StackOrientation.Vertical,
			Spacing = 15,
		};


		Entry txtStopStart = new Entry
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Placeholder = "Salita",
			PlaceholderColor = Color.FromHex(App.Gray),
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
			FontSize = 20,
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


        public StartReportLight()
        {
            Title = "NUOVO";
            BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);

			btnInsert.Clicked += BtnInsert_Clicked;

			contentStack.Children.Add(txtStopStart);

			MainLayout.Children.Add(btnInsert);
			MainLayout.Children.Add(contentStack);

            MainLayout.Children.Add(lblErrorMsg);
            MainLayout.Children.Add(activityIndicator);

            Content = MainLayout;

        }

		private async void BtnInsert_Clicked(object sender, EventArgs e)
		{
			//await Navigation.PushAsync(new StartReportLight());
		}
	}

}
