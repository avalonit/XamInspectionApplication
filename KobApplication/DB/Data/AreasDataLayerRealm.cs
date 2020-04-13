using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class AreasDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public AreasDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(AreasRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<AreasRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<AreasRealmModel>().OrderBy((arg) => arg.Area ).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error AreasDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<AreasModel> model)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						for (int i = 0; i < model.Count; i++)
						{
							AreasRealmModel realmModel = new AreasRealmModel();
							realmModel.IDArea = model[i].IDArea;
							realmModel.Area = model[i].Area;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error AreasDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<AreasRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( AreasRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error AreasDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
