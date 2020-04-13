using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class StopTimesBusinness : KobeAppSQLDB
    {

		public List<StopTimesModel> GetAll()
        {
            try
            {
				StopTimesDataLayerRealm dl = new StopTimesDataLayerRealm();
				List<StopTimesModel> list = new List<StopTimesModel>();
				List<StopTimesRealmModel> models = dl.GetAll();
				foreach (StopTimesRealmModel model in models)
				{
					StopTimesModel realmModel = new StopTimesModel();
					realmModel.arrival_time = model.arrival_time;
					realmModel.departure_time = model.departure_time;
					realmModel.stop_id = model.stop_id;
					realmModel.stop_sequence = model.stop_sequence;
					realmModel.trip_id = model.trip_id;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error StopTimesBusinness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<StopTimesModel> model)
		{
			try
			{
				StopTimesDataLayerRealm dl = new StopTimesDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopTimesBusinness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				StopTimesDataLayerRealm dl = new StopTimesDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopTimesBusinness->Delete " + pException.Message);
			}
		}
      
    }
}
