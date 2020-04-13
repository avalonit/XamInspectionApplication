using System;
using System.Collections.Generic;
using KobApp.DataModel;
using KobApp.DB.Interfaces;

namespace KobApp.DB.SQLDataLayer
{
	public class InspectionsDataLayerSQL : KobeAppSQLDB, IInspections
	{
		public InspectionsDataLayerSQL()
		{
		}

		public List<InspectionsModel> GetAll()
		{
			try
			{
				string fields = "Inspection_date";
				string sqlQuery = "SELECT " + fields + " FROM  Inspections GROUP BY " + fields;

				return _Connection.Query<InspectionsModel>(sqlQuery);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error In Selecting InspectionsDataLayerSQL " + pException.Message);
			}
			return null;
		}
	}
}
