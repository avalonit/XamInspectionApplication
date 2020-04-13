using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobApp.DataModel
{
	public class UsersModel
	{
		public string NOME { get; set; }
		public string COGNOME { get; set; }
		public string SESSO { get; set; }
		public string LUOGO_NASCITA { get; set; }
		public string PROV_NASCITA { get; set; }

		public DateTime DATA_NASCITA { get; set; }

		public string RESID_COMUNE { get; set; }

		public string RESID_INDIRIZZO { get; set; }

		public string RESID_LOCALITA { get; set; }

		public string RESID_PROV { get; set; }

		public string TELEFONO { get; set; }

		public string CELLULARE { get; set; }

		public string SORGENTE { get; set; }

		public string SOURCE { get; set; }

		public string IMAGE { get; set; }

	}
}
