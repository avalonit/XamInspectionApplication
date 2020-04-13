using System;
using System.Collections.Generic;
using System.Linq;
using KobApp.DataModel;
using KobApp.DB.Interfaces;
using Realms;

namespace KobApp.DB.SQLDataLayer
{
	public class CalendarDatesDataLayerRealm : KobeAppSQLDB
	{
		Realm _realm;
		RealmConfiguration config;

		public CalendarDatesDataLayerRealm()
		{
			config = new RealmConfiguration("kobeapp.realm");
			config.ObjectClasses = new[] { typeof(CalendarDatesRealModel) };

			_realm = Realm.GetInstance(config);
		}

		public List<CalendarDatesRealModel> GetAll()
		{
			try
			{
				var model = _realm.All<CalendarDatesRealModel>().OrderBy((arg) => arg.service_id).ToList();

				return model;
			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error CalendarDatesDataLayerRealm->GetAll " + pException.Message);
			}
			return null;
		}

		public void Insert(List<CalendarDatesModel> models)
		{
			try
			{
				//using (var trans = _realm.BeginWrite())
				{
					_realm.Write(() =>
					{
						foreach (CalendarDatesModel model in models)
						{
							CalendarDatesRealModel realmModel = new CalendarDatesRealModel();
							realmModel.date = model.date;
							realmModel.exception_type = model.exception_type;
							realmModel.service_id = model.service_id;
							_realm.Add(realmModel);
						}
					});
					//trans.Commit();
				};

			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error CalendarDatesDataLayerRealm->Insert " + pException.Message);
			}
		}

		public void Delete()
		{
			try
			{

				var models = _realm.All<CalendarDatesRealModel>();

				// Delete an object with a transaction
				using (var trans = _realm.BeginWrite())
				{
					foreach (CalendarDatesRealModel model in models)
						_realm.Remove(model);
					trans.Commit();
				};


			}
			catch (Exception pException)
			{
				System.Diagnostics.Debug.WriteLine("Error CalendarDatesDataLayerRealm->Delete " + pException.Message);
			}
		}
	}

}
