using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace KobApp.DataModel
{
	public class StopsRealmModel : RealmObject
	{
		public String stop_id { get; set; }
		public String stop_name { get; set; }
		public double stop_lat { get; set; }
		public double stop_lon { get; set; }
		public String stop_code { get; set; }
		public String routes_short_names { get; set; }

		public StopsRealmModel()
		{
		}
	
	}
}
