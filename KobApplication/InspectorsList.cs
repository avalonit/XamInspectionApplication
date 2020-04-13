using KobApp.HelperView;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using KobApp.DataModel;
using Plugin.Settings;
using KobApp.DB.Business;
using System.Linq;
using KobApp.DB.Interfaces;

namespace KobApp
{
    public class InspectorsList : ContentPage
    {
		ObservableCollection<InspectorsModel> InspectorsData = new ObservableCollection<InspectorsModel>();
		List<InspectorsModel> inspectors;
		IAddReport addReport;

        StackLayout MainLayout = new StackLayout
        {           
            Padding = 4,
            Spacing = 5,
            Orientation = StackOrientation.Vertical,
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
			ItemTemplate = new DataTemplate(typeof(InspectorsListViewCell)),
		};

		Frame searchFrame = new Frame
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.StartAndExpand,
			OutlineColor = Color.FromHex(App.Black),
			Padding = new Thickness(5),
			HasShadow = false,
		};

		StackLayout searchStack = new StackLayout
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.StartAndExpand,
			Orientation = StackOrientation.Horizontal,
			Spacing = 2,
		};

		Image imgSearchIcon = new Image
		{
			HorizontalOptions = LayoutOptions.Start,
			VerticalOptions = LayoutOptions.Center,
			Source = "ic_search.png",
			Aspect = Aspect.Fill,
		};

		Entry txtSearch = new Entry
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Placeholder = "Nome e/o cognome",
			PlaceholderColor = Color.FromHex(App.Gray),
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
		};

		Button btnSearch = new Button
		{
			HorizontalOptions = LayoutOptions.End,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "CERCA",
			TextColor = Color.FromHex(App.Black),
			BackgroundColor = Color.Transparent,
			BorderRadius = 0,
			IsEnabled = false,
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
                new ColumnDefinition () { Width = new GridLength (0.20, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (0.80, GridUnitType.Star) },
            }
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
            Text = "Cod."
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
            Text = "Verificatore"
        };



        public InspectorsList(IAddReport addReport)
        {
			this.addReport = addReport;

            Title = "VERIF.";
            BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);

			lstDatas.ItemsSource = InspectorsData;
			lstDatas.Header = MainGrid;

			MainGrid.Children.Add(lblCode, 0, 0);
			MainGrid.Children.Add(lblInspectors, 1, 0);

			searchStack.Children.Add(imgSearchIcon);
			searchStack.Children.Add(txtSearch);
			searchStack.Children.Add(btnSearch);

			searchFrame.Content = searchStack;

			MainLayout.Children.Add(searchFrame);
			MainLayout.Children.Add(lblErrorMsg);
            MainLayout.Children.Add(activityIndicator);
			MainLayout.Children.Add(lstDatas);

			btnSearch.Clicked += BtnSearch_Clicked;
			txtSearch.TextChanged += TxtSearch_TextChanged;
			lstDatas.ItemSelected += LstDatas_ItemSelected;

            Content = MainLayout;

			LoadInspectorData();
        }

		void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (e.NewTextValue.Length > 0)
			{
				btnSearch.IsEnabled = true;
			}
			else {
				btnSearch.IsEnabled = false;
			}
		}

		private async void BtnSearch_Clicked(object sender, EventArgs e)
		{
			try
			{
				SearchInspectorData(txtSearch.Text);
			}
			catch (Exception pException)
			{
				await DisplayAlert("Searching Failed!", pException.Message, "Ok");
				System.Diagnostics.Debug.WriteLine("Search Page : Exception : " + pException.Message);
			}
			finally
			{
			}

		}


		async void LstDatas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			try
			{
				InspectorsModel inspectorModel = (InspectorsModel)e.SelectedItem;
				addReport.OnAddInspector(inspectorModel);
				await Navigation.PopAsync();
				//await Navigation.PushAsync(new DetailPage(inspectorModel));
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


		private async void LoadInspectorData()
        {
            try
            {
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;                   
                System.Diagnostics.Debug.WriteLine("InspectorsBusiness DB Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
				InspectorsBusiness db = new InspectorsBusiness();
				inspectors= db.GetAll();                
                System.Diagnostics.Debug.WriteLine("InspectorsBusiness DB Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
                AddInspectorData(inspectors);
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

        private void AddInspectorData(List<InspectorsModel> inspections)
        {
    	    InspectorsData.Clear();
            if(inspections != null && inspections.Count > 0)
            {
                lblErrorMsg.IsVisible = false;
                foreach(var item in inspections)
                {
                    InspectorsData.Add(item);
                }
            }
            else
            {
                lblErrorMsg.Text = "Nessun risultato";
                lblErrorMsg.IsVisible = true;
            }
        }

		private void SearchInspectorData(String a_descr)
		{
			var filteredList = from x in inspectors
					where x.a_descrizione.Contains(a_descr)
			               orderby x.a_descrizione
					select x ;
			
			List<InspectorsModel> inspectors_filtered = filteredList.ToList();
			InspectorsData.Clear();
			if (inspectors_filtered != null && inspectors_filtered.Count > 0)
			{
				lblErrorMsg.IsVisible = false;
				foreach (var item in inspectors_filtered)
				{
					InspectorsData.Add(item);
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
