using SQLite;

using Xamarin.Forms;

namespace KobApp.DB
{
    public class KobeAppSQLDB
    {
        public SQLiteConnection _Connection;

        public KobeAppSQLDB()
        {
            _Connection = DependencyService.Get<ISQLite>().GetConnection();
        }
        
    }
}
