using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class TripsBusinness : KobeAppSQLDB
    {

		public List<TripsModel> GetAll()
        {
            try
            {
				TripsDataLayerRealm dl = new TripsDataLayerRealm();
				List<TripsModel> list = new List<TripsModel>();
				List<TripsRealmModel> models = dl.GetAll();
				foreach (TripsRealmModel model in models)
				{
					TripsModel realmModel = new TripsModel();
					realmModel.direction_id = model.direction_id;
					realmModel.route_id = model.route_id;
					realmModel.service_id = model.service_id;
					realmModel.shape_id = model.shape_id;
					realmModel.trip_headsign = model.trip_headsign;
					realmModel.trip_id = model.trip_id;
					realmModel.trip_short_name = model.trip_short_name;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error TripsBusinness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<TripsModel> model)
		{
			try
			{
				TripsDataLayerRealm dl = new TripsDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TripsBusinness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				TripsDataLayerRealm dl = new TripsDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TripsBusinness->Delete " + pException.Message);
			}
		}
      
    }
}
