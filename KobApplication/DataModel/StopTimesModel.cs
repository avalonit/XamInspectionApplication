﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobApp.DataModel
{
	public class StopTimesModel
	{

		public String trip_id{ get; set; }
		public String arrival_time{ get; set; }
		public String departure_time{ get; set; }
		public String stop_id{ get; set; }
		public int stop_sequence{ get; set; }

		public StopTimesModel()
		{
		}
	
	}
}
