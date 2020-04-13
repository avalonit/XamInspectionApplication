using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using KobApp.DB.SQLDataLayer;

namespace KobApp.DB.Business
{
	public class TipoDocumentoBusiness : KobeAppSQLDB
    {

		public List<TipoDocumentoModel> GetAll()
        {
            try
            {
				TipoDocumentoDataLayerRealm dl = new TipoDocumentoDataLayerRealm();
				List<TipoDocumentoModel> list = new List<TipoDocumentoModel>();
				List<TipoDocumentoRealmModel> models = dl.GetAll();
				foreach (TipoDocumentoRealmModel model in models)
				{
					TipoDocumentoModel realmModel = new TipoDocumentoModel();
					realmModel.Documento = model.Documento;
					realmModel.Documento_breve = model.Documento_breve;
					realmModel.NumeroVerbali = model.NumeroVerbali;
					realmModel.Abilitato = model.Abilitato;
					realmModel.TipoDocumento = model.TipoDocumento;
					list.Add(realmModel);
				}
				//list = list.OrderBy(x => x.a_descrizione).ToList();
				return list;
            }
            catch (Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error TipoDocumentoBusiness->GetAll " + pException.Message);
            }
            return null;
        }

		public void Insert(List<TipoDocumentoModel> model)
		{
			try
			{
				TipoDocumentoDataLayerRealm dl = new TipoDocumentoDataLayerRealm();
				dl.Insert(model);
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoDocumentoBusiness->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{
				TipoDocumentoDataLayerRealm dl = new TipoDocumentoDataLayerRealm();
				dl.Delete();
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error TipoDocumentoBusiness->Delete " + pException.Message);
			}
		}
      
    }
}
