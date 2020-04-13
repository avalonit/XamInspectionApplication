using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class TipoDocumentoDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public TipoDocumentoDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(TipoDocumentoModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<TipoDocumentoRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<TipoDocumentoRealmModel>().OrderBy((arg) => arg.NumeroVerbali ).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoDocumentoDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<TipoDocumentoModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (TipoDocumentoModel model in models)
						{
							TipoDocumentoRealmModel realmModel = new TipoDocumentoRealmModel();
							realmModel.Documento = model.Documento;
							realmModel.Documento_breve = model.Documento_breve;
							realmModel.NumeroVerbali = model.NumeroVerbali;
							realmModel.Abilitato = model.Abilitato;
							realmModel.TipoDocumento = model.TipoDocumento;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoDocumentoDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<TipoDocumentoRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( TipoDocumentoRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoDocumentoDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
