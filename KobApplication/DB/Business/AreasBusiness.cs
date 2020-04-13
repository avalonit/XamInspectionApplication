using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class AreasBusiness : KobeAppSQLDB
    {

		public List<AreasModel> GetAll()
        {
            try
            {
				AreasDataLayerRealm dl = new AreasDataLayerRealm();
				List<AreasModel> list = new List<AreasModel>();
				List<AreasRealmModel> listr = dl.GetAll();
				foreach (AreasRealmModel r in listr)
				{
					AreasModel m = new AreasModel();
					m.Area = r.Area;
					m.IDArea = r.IDArea;
					list.Add(m);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error AreasBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<AreasModel> model)
		{
			try
			{
				AreasDataLayerRealm dl = new AreasDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error AreasBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				AreasDataLayerRealm dl = new AreasDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error AreasBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
