using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class StopsDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public StopsDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(StopsRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<StopsRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<StopsRealmModel>().OrderBy((arg) => arg.stop_name).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopsDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<StopsModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (StopsModel model in models)
						{
							StopsRealmModel realmModel = new StopsRealmModel();
							realmModel.stop_code = model.stop_code;
							realmModel.stop_id = model.stop_id;
							realmModel.stop_lat = model.stop_lat;
							realmModel.stop_lon = model.stop_lon;
							realmModel.stop_name = model.stop_name;
							realmModel.routes_short_names = model.routes_short_names;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopsDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<StopsRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach (StopsRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopsDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
