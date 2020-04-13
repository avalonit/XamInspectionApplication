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
	public class Sync : ContentPage
	{
		ObservableCollection<InspectionsModel> InspectionData = new ObservableCollection<InspectionsModel>();

		StackLayout MainLayout = new StackLayout
		{
			Padding = 4,
			Spacing = 5,
			Orientation = StackOrientation.Vertical,
		};

		Button btnSync = new Button
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "SINCRONIZZA",
			TextColor = Color.White,
			BackgroundColor = Color.FromHex(App.Orange),
		};

		Label lblMsg = new Label
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			Text = "",
			TextColor = Color.Black,
			HorizontalTextAlignment = TextAlignment.Center,
		};

		ActivityIndicator activityIndicator = new ActivityIndicator
		{
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			VerticalOptions = LayoutOptions.StartAndExpand,
			Color = Color.Black,
			IsRunning = false,
			IsVisible = false,
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



		public Sync()
		{
			Title = "SYNC";
			BackgroundColor = Color.White;
			NavigationPage.SetHasNavigationBar(this, true);
			NavigationPage.SetHasBackButton(this, true);

			btnSync.Clicked += BtnSync_Clicked;

			MainLayout.Children.Add(btnSync);
			MainLayout.Children.Add(lblMsg);
			MainLayout.Children.Add(activityIndicator);

			Content = MainLayout;

		}

		private async void BtnSync_Clicked(object sender, EventArgs e)
		{
			SyncData();
		}

		private async void SyncData()
		{
			try
			{
				activityIndicator.IsVisible = true;
				activityIndicator.IsRunning = true;
				System.Diagnostics.Debug.WriteLine("Sync DB Call Start Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
				APIServices.ApiServices apiServices = new APIServices.ApiServices();

				AppendLog(string.Empty, false);

				APIServices.ApiServices.ApiReadWay apiReadWay = APIServices.ApiServices.ApiReadWay.NormalJSon;
				if( Device.RuntimePlatform == Device.iOS )
					apiReadWay = APIServices.ApiServices.ApiReadWay.HugeJson;	
				
				List<StopTimesModel> stopTimes = await apiServices.GetStopTimes(CrossSettings.Current.GetValueOrDefault<string>("Token", ""), apiReadWay);
				AppendLog("Orari: " + stopTimes.Count.ToString());
				UpdateStopTimes(stopTimes);
				stopTimes = null;
				GC.Collect();

				List<TripsModel> trips = await apiServices.GetTrips(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Corse: " + trips.Count.ToString());
				UpdateTrips(trips);
				trips = null;
				GC.Collect();

				List<StopsModel> stops = await apiServices.GetStops(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Fermate: " + stops.Count.ToString());
				UpdateStops(stops);
				stops = null;
				GC.Collect();

				List<CalendarDatesModel> calendarDates = await apiServices.GetCalendarDates(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Calendario: " + calendarDates.Count.ToString());
				UpdateCalendarDates(calendarDates);
				calendarDates = null;
				GC.Collect();

				List<AreasModel> areas = await apiServices.GetAreas(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Aree: " + areas.Count.ToString());
				UpdateAreas(areas);

				List<LinesModel> lines = await apiServices.GetLines(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Linee: " + lines.Count.ToString());
				UpdateLines(lines);

				List<GeoCountriesModel> countries = await apiServices.GetGeoCountries(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Nazioni: " + countries.Count.ToString());
				UpdateCountries(countries);

				List<GeoProvinceITModel> provinces = await apiServices.GetGeoProvinceIT(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Province: " + provinces.Count.ToString());
				UpdateProvinces(provinces);

				List<GeoComuniITModel> cities = await apiServices.GetGeoComuniIT(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Città: " + cities.Count.ToString());
				UpdateCities(cities);

				List<TipoDocumentoModel> documents = await apiServices.GetTipoDocumento(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Tipi Documenti: " + documents.Count.ToString());
				UpdateDocumentTypes(documents);

				List<TipoTitoloEvasoModel> ticketViolations = await apiServices.GetTipoTitoloEvaso(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Tipi Violazioni: " + ticketViolations.Count.ToString());
				UpdateTicketViolations(ticketViolations);

				List<MotiviSanzioniModel> violations = await apiServices.GetMotiviSanzioni(CrossSettings.Current.GetValueOrDefault<string>("Token", ""));
				AppendLog("Violazioni: " + violations.Count.ToString());
				UpdateViolations(violations);


				System.Diagnostics.Debug.WriteLine("Sync DB Call Over Time : " + DateTime.Now + " Milisecond : " + DateTime.Now.Millisecond);
			}
			catch (Exception pException)
			{
				await DisplayAlert("Sincronizzazione fallita!", pException.Message, "Ok");
				AppendLog(pException.Message, false);
				System.Diagnostics.Debug.WriteLine("SyncData : Exception : " + pException.Message);
			}
			finally
			{
				activityIndicator.IsVisible = false;
				activityIndicator.IsRunning = false;
				GC.Collect();
			}
		}

		private void UpdateAreas(List<AreasModel> models)
		{
			AreasBusiness b = new AreasBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateLines(List<LinesModel> models)
		{
			LinesBusiness b = new LinesBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateCountries(List<GeoCountriesModel> models)
		{
			GeoCountriesBusiness b = new GeoCountriesBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateProvinces(List<GeoProvinceITModel> models)
		{
			GeoProvinceITBusiness b = new GeoProvinceITBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateCities(List<GeoComuniITModel> models)
		{
			GeoComuniITBusiness b = new GeoComuniITBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateDocumentTypes(List<TipoDocumentoModel> models)
		{
			TipoDocumentoBusiness b = new TipoDocumentoBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateTicketViolations(List<TipoTitoloEvasoModel> models)
		{
			TipoTitoloEvasoBusiness b = new TipoTitoloEvasoBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateViolations(List<MotiviSanzioniModel> models)
		{
			MotiviSanzioniBusiness b = new MotiviSanzioniBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateStops(List<StopsModel> models)
		{
			StopsBusinness b = new StopsBusinness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateCalendarDates(List<CalendarDatesModel> models)
		{
			CalendarDatesBusiness b = new CalendarDatesBusiness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateTrips(List<TripsModel> models)
		{
			TripsBusinness b = new TripsBusinness();
			b.Delete();
			b.Insert(models);
		}

		private void UpdateStopTimes(List<StopTimesModel> models)
		{
			StopTimesBusinness b = new StopTimesBusinness();
			b.Delete();
			b.Insert(models);
		}

		private void AppendLog(String logLine, Boolean append = true)
		{
			if (append)
				lblMsg.Text = lblMsg.Text + "\n" + logLine;
			else
				lblMsg.Text = logLine;
		}

	}
}
