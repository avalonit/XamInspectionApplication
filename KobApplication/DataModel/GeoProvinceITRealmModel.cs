using System;
using Realms;

namespace KobApp.DataModel
{
	public class GeoProvinceITRealmModel : RealmObject
	{

		public String CodiceProvincia { get; set; }
		public String NomeProvincia { get; set; }
		public String DenominazioneRegione { get; set; }
		public String SiglaAutomobilistica { get; set; }
	}
}