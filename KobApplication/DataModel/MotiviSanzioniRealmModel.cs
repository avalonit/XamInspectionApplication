using System;
using Realms;

namespace KobApp.DataModel
{
	public class MotiviSanzioniRealmModel : RealmObject
	{

		public int CodiceSanzione { get; set; }
		public String MotivoSanzione { get; set; }
		public String MotivoSanzione_breve { get; set; }
		public int Comma { get; set; }
		public int NumeroVerbali { get; set; }
	}
}