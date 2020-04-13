using System;
using Realms;

namespace KobApp.DataModel
{
	public class TipoTitoloEvasoRealmModel : RealmObject
	{

		public int IDTipoTitoloEvaso { get; set; }
		public String TipoTitoloEvaso { get; set; }
		public double ImportoTitoloEvaso { get; set; }
	}
}