using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class RoutesBusinness : KobeAppSQLDB
    {

		public List<RoutesModel> GetAll()
        {
            try
            {
				RoutesDataLayerRealm dl = new RoutesDataLayerRealm();
				List<RoutesModel> list = new List<RoutesModel>();
				List<RoutesRealmModel> models = dl.GetAll();
				foreach (RoutesRealmModel model in models)
				{
					RoutesModel realmModel = new RoutesModel();
					realmModel.agency_id = model.agency_id;
					realmModel.route_id = model.route_id;
					realmModel.route_long_name = model.route_long_name;
					realmModel.route_short_name = model.route_short_name;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error RoutesBusinness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<RoutesModel> model)
		{
			try
			{
				RoutesDataLayerRealm dl = new RoutesDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error RoutesBusinness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				RoutesDataLayerRealm dl = new RoutesDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error RoutesBusinness->Delete " + pException.Message);
			}
		}
      
    }
}
