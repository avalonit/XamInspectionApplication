using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class GeoCountriesBusiness : KobeAppSQLDB
    {

		public List<GeoCountriesModel> GetAll()
        {
            try
            {
				GeoCountriesDataLayerRealm dl = new GeoCountriesDataLayerRealm();
				List<GeoCountriesModel> list = new List<GeoCountriesModel>();
				List<GeoCountriesRealmModel> models = dl.GetAll();
				foreach (GeoCountriesRealmModel model in models)
				{
					GeoCountriesModel realmModel = new GeoCountriesModel();
					realmModel.id_stato = model.id_stato;
					realmModel.nome_stato = model.nome_stato;
					realmModel.sigla_iso_3166_1_alpha_2_stato = model.sigla_iso_3166_1_alpha_2_stato;
					realmModel.sigla_iso_3166_1_alpha_3_stato = model.sigla_iso_3166_1_alpha_3_stato;
					realmModel.sigla_numerica_stato = model.sigla_numerica_stato;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error GeoCountriesBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<GeoCountriesModel> model)
		{
			try
			{
				GeoCountriesDataLayerRealm dl = new GeoCountriesDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoCountriesBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				GeoCountriesDataLayerRealm dl = new GeoCountriesDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoCountriesBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
