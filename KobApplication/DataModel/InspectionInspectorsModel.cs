using System;

namespace KobApp.DataModel
{
    public class InspectionInspectorsModel
    {
        public DateTime Inspection_start_time { get; set; }
		public DateTime Inspection_end_time { get; set; }
		public String InspectorCode { get; set; }
		public String InspectorName { get; set; }
    }
}