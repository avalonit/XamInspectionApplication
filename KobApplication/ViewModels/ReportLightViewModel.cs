#region Copyright Syncfusion Inc. 2001 - 2017
// Copyright Syncfusion Inc. 2001 - 2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KobApp.DataModel;
using KobApp.DB.Business;
using Xamarin.Forms;

namespace KobApp
{
	public class ReportLightViewModel : INotifyPropertyChanged
	{
		#region Fields

		private ImageSource sortIcon;
		private ImageSource centerIcon;

		#endregion

		#region Constructor
		public ReportLightViewModel()
		{
			SortIcon = ImageSource.FromResource("KobApplication.Icons.ListView.SortIcon.png");
			CenterIcon = ImageSource.FromResource("KobApplication.Icons.ListView.LocationIcon.png");
			GenerateSource();
		}

		#endregion

		#region Properties

		public StopsModel SelectedStop;

		public ObservableCollection<StopsModel> stops
		{
			get;
			set;
		}

		public ImageSource SortIcon
		{
			get { return this.sortIcon; }
			set
			{
				this.sortIcon = value;
				OnPropertyChanged("SortIcon");
			}
		}

		public ImageSource CenterIcon
		{
			get { return this.centerIcon; }
			set
			{
				this.centerIcon = value;
				OnPropertyChanged("CenterIcon");
			}
		}

		#endregion

		#region Generate Source

		private void GenerateSource()
		{
			StopsBusinness sb = new StopsBusinness();
			stops = new ObservableCollection<StopsModel>(sb.GetAll());
		}

		#endregion

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}

}
