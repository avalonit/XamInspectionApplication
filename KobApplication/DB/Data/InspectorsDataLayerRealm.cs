using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class InspectorsDataLayerRealm : KobeAppSQLDB, IInspectorsRealm
	{
		Realm _realm;
		RealmConfiguration config;

		public InspectorsDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(InspectorsRealmModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<InspectorsRealmModel> GetAll()
		{
			try
			{
				var InspectorsRealmModel = _realm.All<InspectorsRealmModel>().OrderBy((arg) => arg.a_descrizione ).ToList();

				return InspectorsRealmModel;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error In Selecting InspectionsDataLayerSQL " + pException.Message);
			}
			return null;
		}

		public void Insert(List<InspectorsModel> inspectorsModel)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						for (int i = 0; i < inspectorsModel.Count; i++)
						{
							var inspector = _realm.CreateObject<InspectorsRealmModel>();
							inspector.a_Cid = inspectorsModel[i].a_Cid;
							inspector.a_CodVer = inspectorsModel[i].a_CodVer;
							inspector.a_descrizione = inspectorsModel[i].a_descrizione;
							inspector.CodiceConsorzioID = inspectorsModel[i].CodiceConsorzioID;
							inspector.CodiceConsorzioVerbaleID = inspectorsModel[i].CodiceConsorzioVerbaleID;
						}
					});
					//trans.Commit();
				};

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

				var inspectors = _realm.All<InspectorsRealmModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach( InspectorsRealmModel inspector in inspectors)
						_realm.Remove(inspector);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error In Selecting InspectionsBusiness " + pException.Message);
			}
		}
	}

}
