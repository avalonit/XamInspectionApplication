using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class TripsDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public TripsDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(TripsRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<TripsRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<TripsRealmModel>().OrderBy((arg) => arg.service_id).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TripsDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<TripsModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (TripsModel model in models)
						{
							TripsRealmModel realmModel = new TripsRealmModel();
							realmModel.direction_id = model.direction_id;
							realmModel.route_id = model.route_id;
							realmModel.service_id = model.service_id;
							realmModel.shape_id = model.shape_id;
							realmModel.trip_headsign = model.trip_headsign;
							realmModel.trip_id = model.trip_id;
							realmModel.trip_short_name = model.trip_short_name;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TripsDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<TripsRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach (TripsRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TripsDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
