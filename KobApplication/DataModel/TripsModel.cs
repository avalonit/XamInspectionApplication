﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobApp.DataModel
{
	public class TripsModel
	{

		public String route_id{ get; set; }
		public String service_id{ get; set; }
		public String trip_id{ get; set; }
		public String trip_headsign{ get; set; }
		public String trip_short_name{ get; set; }
		public String shape_id{ get; set; }
		public int direction_id{ get; set; }

		public TripsModel()
		{
		}
	
	}
}
