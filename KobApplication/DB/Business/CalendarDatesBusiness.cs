using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class CalendarDatesBusiness : KobeAppSQLDB
    {

		public List<CalendarDatesModel> GetAll()
        {
            try
            {
				CalendarDatesDataLayerRealm dl = new CalendarDatesDataLayerRealm();
				List<CalendarDatesModel> list = new List<CalendarDatesModel>();
				List<CalendarDatesRealModel> models = dl.GetAll();
				foreach (CalendarDatesRealModel model in models)
				{
					CalendarDatesModel realmModel = new CalendarDatesModel();
					realmModel.date = model.date;
					realmModel.exception_type = model.exception_type;
					realmModel.service_id = model.service_id;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error CalendarDatesBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<CalendarDatesModel> model)
		{
			try
			{
				CalendarDatesDataLayerRealm dl = new CalendarDatesDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error CalendarDatesBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				CalendarDatesDataLayerRealm dl = new CalendarDatesDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error CalendarDatesBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
