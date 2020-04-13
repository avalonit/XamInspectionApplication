using KobApplication.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace KobApp
{
    public class Dashboard : ContentPage
    {
        StackLayout mainStack = new StackLayout
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.StartAndExpand,
            Orientation = StackOrientation.Vertical,
            Padding = new Thickness(10, 8, 10, 0),
            Spacing = 3,
            BackgroundColor = Color.FromHex("#30000000"),
        };

        Image imagebackground = new Image
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Source = "login_screen_background.png",
            Aspect = Aspect.AspectFill,
            Margin = new Thickness(0, 0, 0, 0)
        };

        MyButton btnSearch = new MyButton
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Text = "Ric.Anagrafica",
            TextColor = Color.White,
            BackgroundColor = Color.Transparent,
            FontSize = 12,
            //Image = "ic_search.png"

        };

        Image imgSearch = new Image
        {
            Source = "next.png",
            WidthRequest = 10,
            HeightRequest = 10,
            IsEnabled = false,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0,0,10,0)
        };

        Grid grdSearch = new Grid
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
        };

        MyButton btnActivity = new MyButton
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
            Text = "Verbali emessi",
            TextColor = Color.White,
            FontSize = 12,
            //Image = "ic_activity.png"
        };

        Image imgActivity = new Image
        {
            Source = "next.png",
            WidthRequest = 10,
            HeightRequest = 10,
            IsEnabled = false,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 10, 0)
        };

        Grid grdActivity = new Grid
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
        };

		MyButton btnLoadFromBarCode = new MyButton
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
            Text = "Carica blocchetto",
			TextColor = Color.White,
            FontSize = 12,
            //Image = "ic_inbox.png"
        };

        Image imgLoadFromBarCode = new Image
        {
            Source = "next.png",
            WidthRequest = 10,
            HeightRequest = 10,
            IsEnabled = false,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 10, 0)
        };

        Grid grdLoadFromBarCode = new Grid
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
        };

		MyButton btnManageReport = new MyButton
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
            Text = "Inserisci Rapporto",
			TextColor = Color.White,
            FontSize = 12,
            //Image = "ic_paper.png"
        };

        Image imgManageReport = new Image
        {
            Source = "next.png",
            WidthRequest = 10,
            HeightRequest = 10,
            IsEnabled = false,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 10, 0)
        };

        Grid grdManageReport = new Grid
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
        };

		MyButton btnManageFogliZona = new MyButton
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
            Text = "Inserisci Foglio Zona",
			TextColor = Color.White,
            FontSize = 12,
            //Image = "ic_paper.png"
        };

        Image imgManageFogliZona = new Image
        {
            Source = "next.png",
            WidthRequest = 10,
            HeightRequest = 10,
            IsEnabled = false,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 10, 0)
        };

        Grid grdManageFogliZona = new Grid
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
        };

		MyButton btnAddInspection = new MyButton
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
            Text = "Inserisci Verbale",
			TextColor = Color.White,
            FontSize = 12,
            //Image = "ic_inspection.png"
        };

        Image imgAddInspection = new Image
        {
            Source = "next.png",
            WidthRequest = 10,
            HeightRequest = 10,
            IsEnabled = false,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 10, 0)
        };

        Grid grdAddInspection = new Grid
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
        };


        Label lbHome = new Label
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            Text = "HOME",
            TextColor = Color.FromHex("F1C40F"),
            FontSize = 25,
            //Margin = new Thickness(0, 4, 0, 15)
        };

        MyButton btnSincronizza = new MyButton
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Text = "Sincronizza",
            FontSize = 12,
            TextColor = Color.Yellow,
            BackgroundColor = Color.Black,
            BorderRadius = 25,
            BorderColor = Color.Yellow,
            BorderWidth = 1,
            CustomPadding = new Thickness(15,0,0,0)
        };

        Image imgSincronizza = new Image
        {
            Source = "refresh.png",
            RotationY = 180,
            WidthRequest = 17,
            HeightRequest = 17,
            IsEnabled = false,
            Margin = new Thickness(10, 0, 20, 0),
            HorizontalOptions = LayoutOptions.End,
        };

        Grid grdSincronizza = new Grid
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            BackgroundColor = Color.Transparent,
            Padding = new Thickness(5,0,20,0),
            Margin = new Thickness(55,35,55,30),
            
        };

        View lineBottom = new BoxView
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = 1,
            BackgroundColor = Color.FromHex("#66FFFFFF")
        };

        View lineBottom2 = new BoxView
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = 1,
            BackgroundColor = Color.FromHex("#66FFFFFF")
        };

        View lineBottom3 = new BoxView
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = 1,
            BackgroundColor = Color.FromHex("#66FFFFFF")
        };

        View lineBottom4 = new BoxView
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = 1,
            BackgroundColor = Color.FromHex("#66FFFFFF")
        };

        View lineBottom5 = new BoxView
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = 1,
            BackgroundColor = Color.FromHex("#66FFFFFF")
        };

        View lineBottom6 = new BoxView
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = 1,
            BackgroundColor = Color.FromHex("#66FFFFFF")
        };

        public Dashboard()
        {
            Title = "Homepage";
            BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

            grdSearch.Children.Add(btnSearch);
            grdSearch.Children.Add(imgSearch);
            grdActivity.Children.Add(btnActivity);
            grdActivity.Children.Add(imgActivity);
            grdAddInspection.Children.Add(btnAddInspection);
            grdAddInspection.Children.Add(imgAddInspection);
            grdLoadFromBarCode.Children.Add(btnLoadFromBarCode);
            grdLoadFromBarCode.Children.Add(imgLoadFromBarCode);
            grdManageFogliZona.Children.Add(btnManageFogliZona);
            grdManageFogliZona.Children.Add(imgManageFogliZona);
            grdManageReport.Children.Add(btnManageReport);
            grdManageReport.Children.Add(imgManageReport);
            grdSincronizza.Children.Add(btnSincronizza);
            grdSincronizza.Children.Add(imgSincronizza);
            mainStack.Children.Add(lbHome);
            mainStack.Children.Add(grdSearch);
            mainStack.Children.Add(lineBottom);
            mainStack.Children.Add(grdActivity);
            mainStack.Children.Add(lineBottom2);
            mainStack.Children.Add(grdLoadFromBarCode);
            mainStack.Children.Add(lineBottom3);
            mainStack.Children.Add(grdManageReport);
            mainStack.Children.Add(lineBottom4);
            mainStack.Children.Add(grdManageFogliZona);
            mainStack.Children.Add(lineBottom5);
            mainStack.Children.Add(grdAddInspection);
            mainStack.Children.Add(lineBottom6);
            mainStack.Children.Add(grdSincronizza);

            btnSearch.Clicked += BtnSearch_Clicked;
            btnActivity.Clicked += BtnActivity_Clicked;
			btnLoadFromBarCode.Clicked += btnLoadFromBarCode_Clicked;
			btnManageReport.Clicked += btnManageReport_Clicked;
			btnManageFogliZona.Clicked += btnManageFogliZona_Clicked;
			btnAddInspection.Clicked += btnAddInspection_Clicked;
            btnSincronizza.Clicked += btnSync_Clicked;

            Grid gridMain = new Grid();
            gridMain.Children.Add(imagebackground);
            gridMain.Children.Add(mainStack);
            Content = gridMain;
        }

        private async void BtnActivity_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyActivity());
        }

        private async void BtnSearch_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

		private async void btnLoadFromBarCode_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new TicketBlocksPage());
		}

		private async void btnManageReport_Clicked(object sender, EventArgs e)
		{
			await DisplayAlert("Kobe", "Violazione dell'area di lavoro", "Ok");
			//await Navigation.PushAsync(new MyReports());
		}

		private async void btnManageFogliZona_Clicked(object sender, EventArgs e)
		{
			await DisplayAlert("Kobe", "Violazione dell'area di lavoro", "Ok");
			//await Navigation.PushAsync(new MyReportsLight());
		}

		private async void btnAddInspection_Clicked(object sender, EventArgs e)
		{
			await DisplayAlert("Kobe", "Violazione dell'area di lavoro", "Ok");
			//await Navigation.PushAsync(new AddInspection());
		}

		private async void btnSync_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Sync());
		}

    }
}
