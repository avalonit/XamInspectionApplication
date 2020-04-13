using KobApp.DB;
using Foundation;
using System;
using System.IO;
using Xamarin.Forms;
using KobApp.iOS;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace KobApp.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS()
        {
        }

        public SQLite.SQLiteConnection GetConnection()
        {
            try
            {
                string dbName = Config.DBName;
                string doumentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string libraryPath = Path.Combine(doumentPath, "..", "Library");
                string path = Path.Combine(libraryPath, dbName);
                if (!File.Exists(path))
                {
                    CopyDatabase(path);
                }
                Config.DBPath = path;
                var Connection = new SQLite.SQLiteConnection(path);
                return Connection;
            }
            catch(Exception pException)
            {
                throw pException;
            }
            finally
            {
                GC.Collect();
            }
            return null;            
        }

        private bool CopyDatabase(string DatabasePath)
        {
            try
            {
                string DBNameWithoutExt = Config.DBName.Replace(".db","");
                var existingDb = NSBundle.MainBundle.PathForResource("DB/" + DBNameWithoutExt, "db");
                File.Copy(existingDb,DatabasePath);
                
                System.Diagnostics.Debug.WriteLine("File Copied Successfully");
            }
            catch(Exception pException)
            {
                System.Diagnostics.Debug.WriteLine("Error while copied file : " + pException.Message);
                return false;
            }
            finally
            {
                GC.Collect();
            }           
            return true;
        } 
    }
}
