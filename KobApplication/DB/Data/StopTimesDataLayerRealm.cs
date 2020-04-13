using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class StopTimesDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public StopTimesDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(StopTimesRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<StopTimesRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<StopTimesRealmModel>().OrderBy((arg) => arg.trip_id).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopTimesDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<StopTimesModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (StopTimesModel model in models)
						{
							StopTimesRealmModel realmModel = new StopTimesRealmModel();
							realmModel.arrival_time = model.arrival_time;
							realmModel.departure_time = model.departure_time;
							realmModel.stop_id = model.stop_id;
							realmModel.stop_sequence = model.stop_sequence;
							realmModel.trip_id = model.trip_id;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopTimesDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<StopTimesRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach (StopTimesRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error StopTimesDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
