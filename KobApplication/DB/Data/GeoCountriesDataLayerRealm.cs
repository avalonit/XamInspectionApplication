using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class GeoCountriesDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public GeoCountriesDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(GeoCountriesRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<GeoCountriesRealmModel> GetAll()
		{
			try
			{
				var model = _realm.All<GeoCountriesRealmModel>().OrderBy((arg) => arg.nome_stato ).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoCountriesDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<GeoCountriesModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (GeoCountriesModel model in models)
						{
							GeoCountriesRealmModel realmModel = new GeoCountriesRealmModel();
							realmModel.id_stato = model.id_stato;
							realmModel.nome_stato = model.nome_stato;
							realmModel.sigla_iso_3166_1_alpha_2_stato = model.sigla_iso_3166_1_alpha_2_stato;
							realmModel.sigla_iso_3166_1_alpha_3_stato = model.sigla_iso_3166_1_alpha_3_stato;
							realmModel.sigla_numerica_stato = model.sigla_numerica_stato;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoCountriesDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<GeoCountriesRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( GeoCountriesRealmModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoCountriesDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
