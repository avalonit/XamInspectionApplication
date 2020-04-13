using KobApp.DataModel;
using System.Collections.Generic;

namespace KobApp.DB.Interfaces
{
    public interface IInspections
    {
        List<InspectionsModel> GetAll();
    }
}
