using System;
using Realms;

namespace KobApp.DataModel
{
	public class GeoComuniITRealmModel : RealmObject
	{
		public String CodiceProvincia { get; set; }
		public String CodiceComune { get; set; }
		public String DenominazioneInItaliano { get; set; }
	}
}