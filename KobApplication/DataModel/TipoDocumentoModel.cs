using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobApp.DataModel
{
    public class TipoDocumentoModel
    {
		public int TipoDocumento{ get; set; }
		public String Documento{ get; set; }
		public String Documento_breve{ get; set; }
		public int Abilitato{ get; set; }
		public int FlagDocumentoAltroIT{ get; set; }
		public int FlagDocumentoAVoce{ get; set; }
		public int FlagDocumentoDoc{ get; set; }
		public int FlagDocumentoAltroEE{ get; set; }
		public int FlagDocumentoBonusAutisti{ get; set; }
		public int NumeroVerbali{ get; set; }

    }

}
