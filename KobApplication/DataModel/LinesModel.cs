using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobApp.DataModel
{
	public class LinesModel
	{
		public string Linea { get; set; }
		public long IDLinea { get; set; }


		public LinesModel()
		{
		}
		public LinesModel(String Linea, long IDLinea)
		{
			this.Linea = Linea;
			this.IDLinea = IDLinea;
		}
    }
}
