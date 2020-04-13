using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobApp.DataModel
{
	public class StopsModel : INotifyPropertyChanged
	{

		private String _stop_id;
		private String _stop_name;
		private double _stop_lat;
		private double _stop_lon;
		private String _stop_code;
		private String _routes_short_names;
		private double _stop_distance;

		public string stop_id
		{
			get
			{
				return _stop_id;
			}
			set
			{
				_stop_id = value;
				this.RaisePropertyChanged("stop_id");
			}
		}
		public string stop_name
		{
			get
			{
				return _stop_name;
			}
			set
			{
				_stop_name = value;
				this.RaisePropertyChanged("stop_name");
			}
		}
		public double stop_lat
		{
			get
			{
				return _stop_lat;
			}
			set
			{
				_stop_lat = value;
				this.RaisePropertyChanged("stop_lat");
			}
		}
		public double stop_lon
		{
			get
			{
				return _stop_lon;
			}
			set
			{
				_stop_lon = value;
				this.RaisePropertyChanged("stop_lon");
			}
		}

		public double stop_distance
		{
			get
			{
				return _stop_distance;
			}
			set
			{
				_stop_distance = value;
				this.RaisePropertyChanged("stop_distancei");
			}
		}
		public string stop_code
		{
			get
			{
				return _stop_code;
			}
			set
			{
				_stop_code = value;
				this.RaisePropertyChanged("stop_code");
			}
		}

		public string routes_short_names
		{
			get
			{
				return _routes_short_names;
			}
			set
			{
				_routes_short_names = value;
				this.RaisePropertyChanged("routes_short_names");
			}
		}

		public String stop_distance_txt
		{
			get
			{
				return _stop_distance.ToString("N2");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(String name)
		{
			if (PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

	}
}
