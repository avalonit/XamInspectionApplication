using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class MotiviSanzioniBusiness : KobeAppSQLDB
    {

		public List<MotiviSanzioniModel> GetAll()
        {
            try
            {
				MotiviSanzioniDataLayerRealm dl = new MotiviSanzioniDataLayerRealm();
				List<MotiviSanzioniModel> list = new List<MotiviSanzioniModel>();
				List<MotiviSanzioniRealmModel> models = dl.GetAll();
				foreach (MotiviSanzioniRealmModel model in models)
				{
					MotiviSanzioniModel realmModel = new MotiviSanzioniModel();
					realmModel.CodiceSanzione = model.CodiceSanzione;
					realmModel.Comma = model.Comma;
					realmModel.MotivoSanzione = model.MotivoSanzione;
					realmModel.MotivoSanzione_breve = model.MotivoSanzione_breve;
					realmModel.NumeroVerbali = model.NumeroVerbali;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error MotiviSanzioniBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<MotiviSanzioniModel> model)
		{
			try
			{
				MotiviSanzioniDataLayerRealm dl = new MotiviSanzioniDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error MotiviSanzioniBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				MotiviSanzioniDataLayerRealm dl = new MotiviSanzioniDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error MotiviSanzioniBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
