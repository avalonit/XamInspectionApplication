using System;
using Realms;

namespace KobApp.DataModel
{
	public class InspectorsRealmModel : RealmObject
	{

		public String a_CodVer { get; set; }
		public String a_descrizione { get; set; }

		public long? CodiceConsorzioID { get; set; }
		public long? CodiceConsorzioVerbaleID { get; set; }

		public String a_Cid { get; set; }

	}
}