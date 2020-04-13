using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class RoutesDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public RoutesDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(RoutesRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<RoutesRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<RoutesRealmModel>().OrderBy((arg) => arg.route_id).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error RoutesDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<RoutesModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (RoutesModel model in models)
						{
							RoutesRealmModel realmModel = new RoutesRealmModel();
							realmModel.agency_id = model.agency_id;
							realmModel.route_id = model.route_id;
							realmModel.route_long_name = model.route_long_name;
							realmModel.route_short_name = model.route_short_name;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error RoutesDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<RoutesRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach (RoutesRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error RoutesDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
