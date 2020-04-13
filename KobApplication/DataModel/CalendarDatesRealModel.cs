using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace KobApp.DataModel
{
	public class CalendarDatesRealModel : RealmObject
	{
		public String service_id { get; set; }
		public int date { get; set; }
		public int exception_type { get; set; }

		public CalendarDatesRealModel()
		{
		}
	
	}
}
