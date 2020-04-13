using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class MotiviSanzioniDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public MotiviSanzioniDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(MotiviSanzioniRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<MotiviSanzioniRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<MotiviSanzioniRealmModel>().OrderBy((arg) => arg.NumeroVerbali ).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error MotiviSanzioniDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<MotiviSanzioniModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (MotiviSanzioniModel model in models)
						{
							MotiviSanzioniRealmModel realmModel = new MotiviSanzioniRealmModel();
							realmModel.CodiceSanzione = model.CodiceSanzione;
							realmModel.Comma = model.Comma;
							realmModel.MotivoSanzione = model.MotivoSanzione;
							realmModel.MotivoSanzione_breve = model.MotivoSanzione_breve;
							realmModel.NumeroVerbali = model.NumeroVerbali;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error MotiviSanzioniDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<MotiviSanzioniRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( MotiviSanzioniRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error MotiviSanzioniDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
