using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class InspectorsBusiness : KobeAppSQLDB, IInspectors
    {

		public List<InspectorsModel> GetAll()
        {
            try
            {
				InspectorsDataLayerRealm dl = new InspectorsDataLayerRealm();
				List<InspectorsModel> list = new List<InspectorsModel>();
				List<InspectorsRealmModel> listr = dl.GetAll();
				foreach (InspectorsRealmModel r in listr)
				{
					InspectorsModel m = new InspectorsModel();
					m.a_Cid = r.a_Cid;
					m.a_CodVer = r.a_CodVer;
					m.a_descrizione = r.a_descrizione;
					m.CodiceConsorzioID = r.CodiceConsorzioID;
					m.CodiceConsorzioVerbaleID = r.CodiceConsorzioVerbaleID;
					list.Add(m);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error In Selecting InspectionsBusiness " + pException.Message);
            }
            return null;
        }

		public void Insert(List<InspectorsModel> inspectorsModel)
		{
			try
			{
				InspectorsDataLayerRealm dl = new InspectorsDataLayerRealm();
				dl.Insert(inspectorsModel);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error In Selecting InspectionsBusiness " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				InspectorsDataLayerRealm dl = new InspectorsDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error In Selecting InspectionsBusiness " + pException.Message);
			}
		}
      
    }
}
