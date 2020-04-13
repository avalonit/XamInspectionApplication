using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class TipoTitoloEvasoDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public TipoTitoloEvasoDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(TipoTitoloEvasoRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<TipoTitoloEvasoRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<TipoTitoloEvasoRealmModel>().OrderBy((arg) => arg.IDTipoTitoloEvaso ).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoTitoloEvasoDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<TipoTitoloEvasoModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (TipoTitoloEvasoModel model in models)
						{
							TipoTitoloEvasoRealmModel realmModel = new TipoTitoloEvasoRealmModel();
							realmModel.IDTipoTitoloEvaso = model.IDTipoTitoloEvaso;
							realmModel.ImportoTitoloEvaso = model.ImportoTitoloEvaso;
							realmModel.TipoTitoloEvaso = model.TipoTitoloEvaso;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoTitoloEvasoDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<TipoTitoloEvasoRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( TipoTitoloEvasoRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoTitoloEvasoDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
