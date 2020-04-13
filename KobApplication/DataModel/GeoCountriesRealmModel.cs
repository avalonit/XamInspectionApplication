using System;
using Realms;

namespace KobApp.DataModel
{
	public class GeoCountriesRealmModel : RealmObject
	{
		public String id_stato { get; set; }
		public String nome_stato { get; set; }
		public String sigla_numerica_stato { get; set; }
		public String sigla_iso_3166_1_alpha_3_stato { get; set; }
		public String sigla_iso_3166_1_alpha_2_stato { get; set; }
	}
}