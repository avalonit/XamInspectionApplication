using KobApp.HelperView;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using KobApp.DataModel;
using KobApp.DB.Business;

namespace KobApp
{
    public class AddInspection : ContentPage
    {
		ObservableCollection<AreasModel> AreaData = new ObservableCollection<AreasModel>();
		ObservableCollection<LinesModel> LineData = new ObservableCollection<LinesModel>();

		List<AreasModel> areas = null;
		List<LinesModel> lines = null;

		AreasModel selectedArea;
		LinesModel selectedLine;

        StackLayout MainLayout = new StackLayout
        {           
            Padding = 4,
            Spacing = 5,
            Orientation = StackOrientation.Vertical,
        };

		// AREAS

		Label lblArea = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "AREA",
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
			FontSize = 20,
		};

		Entry lblSelectedArea = new Entry
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "",
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
			FontSize = 15,
		};

		ListView lstAreas = new ListView
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			HasUnevenRows = true,
			ItemTemplate = new DataTemplate(typeof(AreasViewCell)),
		};


		Label lblListingAreaCaption = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "Area"
		};


		// LINES

		Label lblLine = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "LINEA",
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
			FontSize = 20,
		};

		Entry lblSelectedLine = new Entry
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "",
			TextColor = Color.Black,
			BackgroundColor = Color.Transparent,
			FontSize = 15,
		};

		ListView lstLines = new ListView
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			HasUnevenRows = true,
			ItemTemplate = new DataTemplate(typeof(LinesViewCell)),
		};

		Label lblListingLineCaption = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
			TextColor = Color.White,
			HorizontalTextAlignment = TextAlignment.Center,
			VerticalTextAlignment = TextAlignment.Center,
			FontSize = 11,
			FontAttributes = FontAttributes.Bold,
			Text = "Linea"
		};

		// OTHERS

		Button btnNext = new Button
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Text = "NEXT",
			TextColor = Color.White,
			BackgroundColor = Color.FromHex(App.Orange),
        };



		//List Header Template Code.
		Grid lstAreasHeader = new Grid
		{
			ColumnSpacing = 1,
			RowSpacing = 0,
			Padding = new Thickness(1),
			BackgroundColor = Color.FromHex(App.Black),
			RowDefinitions = new RowDefinitionCollection() {
				new RowDefinition () { Height = new GridLength (1, GridUnitType.Auto) },
			},
			ColumnDefinitions = new ColumnDefinitionCollection() {
				new ColumnDefinition () { Width = new GridLength (1, GridUnitType.Star) },
			}
		};
		Grid lstLinesHeader = new Grid
		{
			ColumnSpacing = 1,
			RowSpacing = 0,
			Padding = new Thickness(1),
			BackgroundColor = Color.FromHex(App.Black),
			RowDefinitions = new RowDefinitionCollection() {
				new RowDefinition () { Height = new GridLength (1, GridUnitType.Auto) },
			},
			ColumnDefinitions = new ColumnDefinitionCollection() {
				new ColumnDefinition () { Width = new GridLength (1, GridUnitType.Star) },
			}
		};

        public AddInspection()
        {
            Title = "VERBALE";
            BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);

			DataBind();

			lstAreas.ItemsSource = AreaData;
			lstAreas.Header = lstAreasHeader;
			lstAreas.ItemSelected += LstAreas_ItemSelected;
			lstAreasHeader.Children.Add(lblListingAreaCaption, 0, 0);

			lstLines.ItemsSource = LineData;
			lstLines.Header = lstLinesHeader;
			lstLines.ItemSelected += LstLines_ItemSelected;
			lstLinesHeader.Children.Add(lblListingLineCaption, 0, 0);

			btnNext.Clicked += BtnNext_Clicked;

			Grid MainGrid = new Grid
			{
				ColumnSpacing = 1,
				RowSpacing = 0,
				Padding = new Thickness(1),
				RowDefinitions = new RowDefinitionCollection() {
					new RowDefinition () { Height = new GridLength (60, GridUnitType.Auto) },
				},
				ColumnDefinitions = new ColumnDefinitionCollection() {
				   	new ColumnDefinition () { Width = new GridLength (0.3, GridUnitType.Star) },
					new ColumnDefinition () { Width = new GridLength (0.7, GridUnitType.Star) },
				}
			};

			MainGrid.Children.Add(lblArea, 0, 0);
			MainGrid.Children.Add(lblSelectedArea, 1, 0);
			MainGrid.Children.Add(lstAreas, 0, 1);
			Grid.SetColumnSpan(lstAreas, 2);

			MainGrid.Children.Add(lblLine, 0, 2);
			MainGrid.Children.Add(lblSelectedLine, 1, 2);
			MainGrid.Children.Add(lstLines, 0, 3);
			Grid.SetColumnSpan(lstLines, 2);

			MainLayout.Children.Add(btnNext);
			MainLayout.Children.Add(MainGrid);

			lblSelectedLine.TextChanged+= LblSelectedLine_TextChanged;

            Content = MainLayout;

        }

		void LblSelectedLine_TextChanged(object sender, TextChangedEventArgs e)
		{
			LineData.Clear();
			if (lines != null && lines.Count > 0)
			{
				foreach (var line in lines)
				{
					if (line.Linea.Contains(e.NewTextValue) )
						LineData.Add(line);
				}
			}

		}

		private void BtnNext_Clicked(object sender, EventArgs e)
        {
        }

        private async void DataBind()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("DataBind DB Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
				DataBindList();
				System.Diagnostics.Debug.WriteLine("DataBind DB Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
            }
            catch (Exception pException)
            {
                await DisplayAlert("DataBind Failed!", pException.Message, "Ok");
                System.Diagnostics.Debug.WriteLine("DataBind : Exception : " + pException.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

		private void DataBindList()
		{
			AreasBusiness a = new AreasBusiness();
			areas = a.GetAll();
			AreaData.Clear();
			if (areas != null && areas.Count > 0)
			{
				foreach (var area in areas)
				{
					AreaData.Add(area);
				}
			}

			LinesBusiness l = new LinesBusiness();
			lines = l.GetAll();
			LineData.Clear();
			if (lines != null && lines.Count > 0)
			{
				foreach (var line in lines)
				{
					LineData.Add(line);
				}
			}
		}		


		void LstAreas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			try
			{
				AreasModel model = (AreasModel)e.SelectedItem;
				if (model != null)
				{
					selectedArea = model;
					lblSelectedArea.Text = model.Area;
				}
				//await Navigation.PushAsync(new DetailPage(inspectorModel));
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("LstAreas_ItemSelected : " + pException.Message);
			}
			finally
			{
				GC.Collect();
			}
		}

		void LstLines_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			try
			{
				LinesModel model = (LinesModel)e.SelectedItem;
				if (model != null)
				{
					selectedLine = model;
					lblSelectedLine.Text = model.Linea;
				}
				//await Navigation.PushAsync(new DetailPage(inspectorModel));
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("LstLines_ItemSelected : " + pException.Message);
			}
			finally
			{
				GC.Collect();
			}
		}

	}
}
