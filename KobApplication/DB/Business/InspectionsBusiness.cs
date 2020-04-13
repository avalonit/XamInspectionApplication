using System;
using System.Collections.Generic;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
    public class InspectionsBusiness : KobeAppSQLDB, IInspections
    {

        public List<InspectionsModel> GetAll()
        {
            try
            {
				InspectionsDataLayerSQL dl = new InspectionsDataLayerSQL();
				return dl.GetAll();
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error In Selecting InspectionsBusiness " + pException.Message);
            }
            return null;
        }

      
    }
}
