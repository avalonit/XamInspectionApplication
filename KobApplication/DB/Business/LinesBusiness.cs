using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class LinesBusiness : KobeAppSQLDB
    {

		public List<LinesModel> GetAll()
        {
            try
            {
				LinesDataLayerRealm dl = new LinesDataLayerRealm();
				List<LinesModel> list = new List<LinesModel>();
				List<LinesRealmModel> models = dl.GetAll();
				foreach (LinesRealmModel model in models)
				{
					LinesModel realmModel = new LinesModel();
					realmModel.Linea = model.Linea;
					realmModel.IDLinea = model.IDLinea;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error LinesBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<LinesModel> model)
		{
			try
			{
				LinesDataLayerRealm dl = new LinesDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error LinesBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				LinesDataLayerRealm dl = new LinesDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error LinesBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
