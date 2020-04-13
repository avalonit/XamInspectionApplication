using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobApp.DataModel
{
	public class RoutesModel
	{

		public String route_id{ get; set; }
		public String agency_id{ get; set; }
		public String route_short_name{ get; set; }
		public String route_long_name{ get; set; }

		public RoutesModel()
		{
		}
	
	}
}
