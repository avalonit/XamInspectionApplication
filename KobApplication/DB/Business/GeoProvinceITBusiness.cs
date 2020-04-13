using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class GeoProvinceITBusiness : KobeAppSQLDB
    {

		public List<GeoProvinceITModel> GetAll()
        {
            try
            {
				GeoProvinceITDataLayerRealm dl = new GeoProvinceITDataLayerRealm();
				List<GeoProvinceITModel> list = new List<GeoProvinceITModel>();
				List<GeoProvinceITRealmModel> models = dl.GetAll();
				foreach (GeoProvinceITRealmModel model in models)
				{
					GeoProvinceITModel realmModel = new GeoProvinceITModel();
					realmModel.CodiceProvincia= model.CodiceProvincia;
					realmModel.DenominazioneRegione = model.DenominazioneRegione;
					realmModel.NomeProvincia = model.NomeProvincia;
					realmModel.SiglaAutomobilistica = model.SiglaAutomobilistica;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error GeoProvinceITBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<GeoProvinceITModel> model)
		{
			try
			{
				GeoProvinceITDataLayerRealm dl = new GeoProvinceITDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoProvinceITBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				GeoProvinceITDataLayerRealm dl = new GeoProvinceITDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoProvinceITBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
