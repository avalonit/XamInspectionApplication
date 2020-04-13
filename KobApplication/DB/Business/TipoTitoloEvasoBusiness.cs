using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class TipoTitoloEvasoBusiness : KobeAppSQLDB
    {

		public List<TipoTitoloEvasoModel> GetAll()
        {
            try
            {
				TipoTitoloEvasoDataLayerRealm dl = new TipoTitoloEvasoDataLayerRealm();
				List<TipoTitoloEvasoModel> list = new List<TipoTitoloEvasoModel>();
				List<TipoTitoloEvasoRealmModel> models = dl.GetAll();
				foreach (TipoTitoloEvasoRealmModel model in models)
				{
					TipoTitoloEvasoModel realmModel = new TipoTitoloEvasoModel();
					realmModel.IDTipoTitoloEvaso = model.IDTipoTitoloEvaso;
					realmModel.ImportoTitoloEvaso = model.ImportoTitoloEvaso;
					realmModel.TipoTitoloEvaso = model.TipoTitoloEvaso;

					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error TipoTitoloEvasoBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<TipoTitoloEvasoModel> model)
		{
			try
			{
				TipoTitoloEvasoDataLayerRealm dl = new TipoTitoloEvasoDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoTitoloEvasoBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				TipoTitoloEvasoDataLayerRealm dl = new TipoTitoloEvasoDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoTitoloEvasoBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
