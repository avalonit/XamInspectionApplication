using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class GeoComuniITBusiness : KobeAppSQLDB
    {

		public List<GeoComuniITModel> GetAll()
        {
            try
            {
				GeoComuniITDataLayerRealm dl = new GeoComuniITDataLayerRealm();
				List<GeoComuniITModel> list = new List<GeoComuniITModel>();
				List<GeoComuniITRealmModel> models = dl.GetAll();
				foreach (GeoComuniITRealmModel model in models)
				{
					GeoComuniITModel realmModel = new GeoComuniITModel();
					realmModel.CodiceComune = model.CodiceComune;
					realmModel.CodiceProvincia = model.CodiceProvincia;
					realmModel.DenominazioneInItaliano = model.DenominazioneInItaliano;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error GeoComuniITBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<GeoComuniITModel> model)
		{
			try
			{
				GeoComuniITDataLayerRealm dl = new GeoComuniITDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoComuniITBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				GeoComuniITDataLayerRealm dl = new GeoComuniITDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error GeoComuniITBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
