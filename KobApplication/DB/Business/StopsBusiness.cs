using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class StopsBusinness : KobeAppSQLDB
    {

		public List<StopsModel> GetAll()
        {
            try
            {
				StopsDataLayerRealm dl = new StopsDataLayerRealm();
				List<StopsModel> list = new List<StopsModel>();
				List<StopsRealmModel> models = dl.GetAll();
				foreach (StopsRealmModel model in models)
				{
					StopsModel realmModel = new StopsModel();
					realmModel.stop_code = model.stop_code;
					realmModel.stop_id = model.stop_id;
					realmModel.stop_lat = model.stop_lat;
					realmModel.stop_lon = model.stop_lon;
					realmModel.stop_name = model.stop_name;
					realmModel.routes_short_names = model.routes_short_names;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error StopsBusinness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<StopsModel> model)
		{
			try
			{
				StopsDataLayerRealm dl = new StopsDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopsBusinness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				StopsDataLayerRealm dl = new StopsDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopsBusinness->Delete " + pException.Message);
			}
		}
      
    }
}
