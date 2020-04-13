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
    public class AddReport : ContentPage, IAddReport
    {
		ObservableCollection<InspectionInspectorsModel> InspectionData = new ObservableCollection<InspectionInspectorsModel>();

        StackLayout MainLayout = new StackLayout
        {           
            Padding = 4,
            Spacing = 5,
            Orientation = StackOrientation.Vertical,
        };

		StackLayout contentStack = new StackLayout
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.StartAndExpand,
			Orientation = StackOrientation.Vertical,
			Spacing = 15,
		};


		Entry txtFromTime = new Entry
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Placeholder = "Dalle ore",
			PlaceholderColor = Color.FromHex(App.Gray),
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
			FontSize = 20,
		};

		Entry txtToTime = new Entry
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Placeholder = "Alle ore",
			PlaceholderColor = Color.FromHex(App.Gray),
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
			FontSize = 20,
		};

		Entry txtDutyCode = new Entry
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Placeholder = "Codice turno",
			PlaceholderColor = Color.FromHex(App.Gray),
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
			FontSize = 20,
		};

		Button btnAddInspector = new Button
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "Aggiungi verificatore",
			TextColor = Color.White,
			BackgroundColor = Color.FromHex(App.Orange),
			FontSize = 20,
		};


		Button btnStart = new Button
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Text = "INIZIA",
            TextColor = Color.White,
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
			ItemTemplate = new DataTemplate(typeof(InspectionInspectorsViewCell)),
		};
       
		Label lblInspector = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "Verificatore"
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
				new ColumnDefinition () { Width = new GridLength (0.70, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.15, GridUnitType.Star) },
				new ColumnDefinition () { Width = new GridLength (0.15, GridUnitType.Star) },
			}
		};
        public AddReport()
        {
            Title = "NUOVO";
            BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);


			lstDatas.ItemsSource = InspectionData;
			lstDatas.Header = MainGrid;

			MainGrid.Children.Add(lblInspector, 0, 0);
			MainGrid.Children.Add(lblStart, 1, 0);
			MainGrid.Children.Add(lblEnd, 2, 0);

            btnStart.Clicked += BtnStart_Clicked;

			btnAddInspector.Clicked += BtnInspector1_Clicked;

			contentStack.Children.Add(txtFromTime);
			contentStack.Children.Add(txtToTime);
			contentStack.Children.Add(txtDutyCode);
			contentStack.Children.Add(btnAddInspector);

			MainLayout.Children.Add(contentStack);

            MainLayout.Children.Add(lblErrorMsg);
            MainLayout.Children.Add(activityIndicator);
			MainLayout.Children.Add(lstDatas);

            Content = MainLayout;

			LoadActivityData();
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
        }

		private void BtnInspector1_Clicked(object sender, EventArgs e)
		{
			BtnInspector_Clicked();
		}

		private async void BtnInspector_Clicked()
		{
			await Navigation.PushAsync(new InspectorsList(this));

		}

		private void UpdateInpectors(List<InspectorsModel> inspectorsModel)
		{
			InspectorsBusiness b = new InspectorsBusiness();
			b.Delete();
			b.Insert(inspectorsModel);
		}

        private async void LoadActivityData()
        {
            try
            {
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;                   
                System.Diagnostics.Debug.WriteLine("MyReports DB Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
				APIServices.ApiServices apiServices = new APIServices.ApiServices();
				List<InspectorsModel> inspectorsModel = await apiServices.GetInspectors(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				System.Diagnostics.Debug.WriteLine("MyReports DB Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
				UpdateInpectors(inspectorsModel);
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

		public void OnAddInspector(InspectorsModel inspector)
		{
			InspectionInspectorsModel model = new InspectionInspectorsModel();
			model.InspectorName = inspector.a_CodVer;
			model.InspectorName = inspector.a_descrizione;
			AddActivityData(model);
		}

		private void AddActivityData(InspectionInspectorsModel inspections)
		{
			InspectionData.Add(inspections);
		}
	}
}
