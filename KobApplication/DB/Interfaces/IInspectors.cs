using KobApp.DataModel;
using System.Collections.Generic;

namespace KobApp.DB.Interfaces
{
    public interface IInspectors
    {
		List<InspectorsModel> GetAll();
		void Insert(List<InspectorsModel> inspectorsModel);
    }
}
