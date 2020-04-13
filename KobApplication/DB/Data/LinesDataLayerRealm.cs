using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class LinesDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public LinesDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(LinesRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<LinesRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<LinesRealmModel>().OrderBy((arg) => arg.Linea ).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error LinesDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<LinesModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (LinesModel model in models)
						{
							LinesRealmModel realmModel = new LinesRealmModel();
							realmModel.Linea = model.Linea;
							realmModel.IDLinea = model.IDLinea;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error LinesDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<LinesRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( LinesRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error LinesDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
