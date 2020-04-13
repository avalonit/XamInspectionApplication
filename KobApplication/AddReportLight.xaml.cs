#region Copyright Syncfusion Inc. 2001 - 2017
// Copyright Syncfusion Inc. 2001 - 2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using KobApp.DataModel;
using Plugin.Geolocator;
using System.Diagnostics;
using KobApplication;
using System.Collections.ObjectModel;

namespace KobApp
{
	public partial class AddReportLight : ContentPage

	{
		private ReportLightViewModel viewModel;
		static bool isAscending = true;
		private TapGestureRecognizer tappedSort;
		private TapGestureRecognizer tappedCenter;



		public AddReportLight()
		{
			InitializeComponent();

			listView.IsVisible = true;
			listView.BackgroundColor = Color.White;
			listView.SelectionChanging += ListView_SelectionChanging;
			listView.SelectionChanged += ListView_OnSelectionChanged;
			listView.SelectionMode = SelectionMode.Single;
			listView.SelectionGesture = TouchGesture.Tap;

			viewModel = new ReportLightViewModel();
			listView.ItemsSource = viewModel.stops;
			this.headerGrid.BindingContext = viewModel;

			tappedSort = new TapGestureRecognizer() { Command = new Command(MakeSort) };
			this.sortImage.GestureRecognizers.Add(tappedSort);

			tappedCenter = new TapGestureRecognizer() { Command = new Command(MakeCenter) };
			this.centerImage.GestureRecognizers.Add(tappedCenter);

			GetPostion();
		}


		private async void GetPostion()
		{
			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 50;

				var position = await locator.GetPositionAsync(10000);

				Debug.WriteLine("Position Status: {0}", position.Timestamp);
				Debug.WriteLine("Position Latitude: {0}", position.Latitude);
				Debug.WriteLine("Position Longitude: {0}", position.Longitude);

				for (int i = 0; viewModel.stops != null && i < viewModel.stops.Count; i++)
				{
					viewModel.stops[i].stop_distance = DistanceHelper.DistanceTo(viewModel.stops[i].stop_lat
																			   , viewModel.stops[i].stop_lon
																			   , position.Latitude
																				, position.Longitude);
				}
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("GetPostion : " + pException.Message);
			}
			finally
			{
				GC.Collect();
			}
		}

		private void MakeCenter()
		{
			if (listView.DataSource != null)
			{
				this.listView.DataSource.Filter = FilterByPosition;
				MakeSort();
			}

			listView.RefreshView();
		}

		private void MakeSort()
		{
			if (listView.DataSource != null)
			{
				viewModel.stops = new ObservableCollection<StopsModel>(viewModel.stops.OrderBy((arg) => arg.stop_distance));
				listView.ItemsSource = viewModel.stops;
			}

		}

		SearchBar searchBar = null;
		private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
		{
			searchBar = (sender as SearchBar);
			if (listView.DataSource != null)
			{
				this.listView.DataSource.Filter = FilterByData;
				this.listView.DataSource.RefreshFilter();
			}
			listView.RefreshView();
		}

		private bool FilterByData(object obj)
		{
			if (searchBar == null || searchBar.Text == null)
				return true;

			var stop = obj as StopsModel;

			int n;

			bool isNumeric = int.TryParse(searchBar.Text, out n);
			if (isNumeric)
			{
				Boolean isRoutes_short_names = stop.routes_short_names != null && (stop.routes_short_names.ToLower().Contains(searchBar.Text.ToLower()));
				return isRoutes_short_names;
			}
			else
			{
				Boolean isStop_name = stop.stop_name != null && (stop.stop_name.ToLower().Contains(searchBar.Text.ToLower()));
				Boolean isStop_code = stop.stop_code != null && (stop.stop_code.ToLower().Contains(searchBar.Text.ToLower()));

				return isStop_name || isStop_code;
			}
		}

		private bool FilterByPosition(object obj)
		{
			var stop = obj as StopsModel;

			return stop.stop_distance <= 0.5;
		}

		protected override void OnDisappearing()
		{
			if (listView != null)
				listView.Dispose();
			listView = null;
			base.OnDisappearing();
		}


		private void ListView_OnSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
		{
			StopsModel selectedStop = (StopsModel)listView.SelectedItem;
			DisplayAlert("Kobe", selectedStop.stop_id, "OK");
		}

		void ListView_SelectionChanging(object sender, ItemSelectionChangingEventArgs e)
		{
			DisplayAlert("Kobe", "PIPPO", "OK");
		}

		public void StopClicked(object sender, EventArgs e)
		{
			StopsModel selectedStop = (StopsModel)listView.SelectedItem;
			if (selectedStop != null)
				DisplayAlert("Kobe", selectedStop.stop_code, "OK");
			else
				DisplayAlert("Kobe", "CIAO", "OK");
		}
	}
}
