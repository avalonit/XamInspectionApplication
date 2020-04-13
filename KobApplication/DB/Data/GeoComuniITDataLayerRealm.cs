using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class GeoComuniITDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public GeoComuniITDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(GeoComuniITRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<GeoComuniITRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<GeoComuniITRealmModel>().OrderBy((arg) => arg.DenominazioneInItaliano ).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoComuniITDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<GeoComuniITModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (GeoComuniITModel model in models)
						{
							GeoComuniITRealmModel realmModel = new GeoComuniITRealmModel();
							realmModel.CodiceComune = model.CodiceComune;
							realmModel.CodiceProvincia = model.CodiceProvincia;
							realmModel.DenominazioneInItaliano = model.DenominazioneInItaliano;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoComuniITDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<GeoComuniITRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( GeoComuniITRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoComuniITDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
