using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobApp.DataModel
{
    public class MotiviSanzioniModel
    {

		public int CodiceSanzione{ get; set; }
		public String MotivoSanzione{ get; set; }
		public String MotivoSanzione_breve{ get; set; }
		public int Comma{ get; set; }
		public int NumeroVerbali{ get; set; }
    }

}
