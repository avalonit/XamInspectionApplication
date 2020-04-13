using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class GeoProvinceITDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public GeoProvinceITDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(GeoProvinceITRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<GeoProvinceITRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<GeoProvinceITRealmModel>().OrderBy((arg) => arg.NomeProvincia ).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoProvinceITDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<GeoProvinceITModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (GeoProvinceITModel model in models)
						{
							GeoProvinceITRealmModel realmModel = new GeoProvinceITRealmModel();
							realmModel.CodiceProvincia = model.CodiceProvincia;
							realmModel.DenominazioneRegione = model.DenominazioneRegione;
							realmModel.NomeProvincia = model.NomeProvincia;
							realmModel.SiglaAutomobilistica = model.SiglaAutomobilistica;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoProvinceITDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<GeoProvinceITRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( GeoProvinceITRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoProvinceITDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
